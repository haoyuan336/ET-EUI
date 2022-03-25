using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Numerics;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MongoDB.Driver;
using MongoDB.Driver.Core.Operations;
using NLog.Fluent;
using OfficeOpenXml.Export.ToDataTable;

namespace ET
{
    public class RoomAwakeSystem: AwakeSystem<Room>
    {
        public override void Awake(Room self)
        {
            Log.Debug("room awake" + self.Id);
            PvPLevelConfig pvPLevelConfig = PvPLevelConfigCategory.Instance.Get(1);
            self.HangCount = pvPLevelConfig.HangCount;
            self.LieCount = pvPLevelConfig.LieCount;

            // self.InitGameData();
        }
    }

    public static class RoomSystem
    {
        public static void AddUnits(this Room self, List<Unit> units)
        {
            self.Units = units;

            for (var i = 0; i < units.Count; i++)
            {
                units[i].InRoomIndex = i + 1;
            }

            foreach (var unit in self.Units)
            {
                Log.Debug("sync create room message");
                MessageHelper.SendToClient(unit, new M2C_SyncCreateRoomMessage() { InRoomIndex = unit.InRoomIndex });
            }

            // self.InitGameData();
            self.SyncRoomInfo();
            self.ToggleTurnSeatIndex();
            self.syncCurrentTurnIndex();
        }

        public static void ToggleTurnSeatIndex(this Room self)
        {
            //切换轮流操作游戏的座位号
            if (self.CurrentTurnIndex == 0)
            {
                self.CurrentTurnIndex = 1;
            }
            else
            {
                self.CurrentTurnIndex++;
                if (self.CurrentTurnIndex > self.Units.Count)
                {
                    self.CurrentTurnIndex = 1;
                }
            }
        }

        public static void syncCurrentTurnIndex(this Room self)
        {
            foreach (var unit in self.Units)
            {
                M2C_ChangeCurrentTurnSeatIndex m2CChangeCurrentTurnSeatIndex = new M2C_ChangeCurrentTurnSeatIndex();
                m2CChangeCurrentTurnSeatIndex.CurrentTurnIndex = self.CurrentTurnIndex;
                MessageHelper.SendToClient(unit, m2CChangeCurrentTurnSeatIndex);
            }
        }

        //同步房间信息
        public static void SyncRoomInfo(this Room self)
        {
            foreach (var unit in self.Units)
            {
                M2C_SyncRoomInfo m2CSyncRoomInfo = new M2C_SyncRoomInfo();
                m2CSyncRoomInfo.RoomId = self.Id;
                m2CSyncRoomInfo.TurnIndex = self.CurrentTurnIndex;
                m2CSyncRoomInfo.MySeatIndex = unit.InRoomIndex;
                MessageHelper.SendToClient(unit, m2CSyncRoomInfo);
            }
        }

        public static void InitGameData(this Room self)
        {
            self.Diamonds = new Diamond[self.LieCount, self.HangCount];
            List<DiamondInfo> diamondInfos = new List<DiamondInfo>();
            DiamondComponent diamondComponent = self.DomainScene().GetComponent<DiamondComponent>();
            self.DiamondComponent = diamondComponent;
            // diamondComponent.CreateOneDiamond();
            int[,] map =
            {
                { 1, 2, 3, 4, 5, 6, 1, 1 }, { 2, 3, 4, 5, 6, 1, 2, 3 }, { 3, 2, 5, 4, 1, 2, 3, 2 }, { 4, 1, 6, 2, 2, 5, 2, 1 },
                { 5, 6, 1, 2, 1, 1, 5, 6 }, { 6, 5, 2, 1, 4, 2, 6, 5 }, { 1, 4, 3, 6, 5, 1, 1, 4 }, { 2, 3, 4, 5, 6, 1, 2, 3 }
            };
            for (var i = 0; i < self.HangCount; i++)
            {
                for (var j = 0; j < self.LieCount; j++)
                {
                    Diamond diamond = diamondComponent.CreateOneDiamond();
                    diamond.SetIndex(j, i);
                    diamond.DiamondType = map[i, j];
                    self.Diamonds[j, i] = diamond;
                    diamondInfos.Add(diamond.GetMessageInfo());
                }
            }

            foreach (var unit in self.Units)
            {
                MessageHelper.SendToClient(unit, new M2C_InitMapData() { DiamondInfo = diamondInfos });
            }
        }

        public static void PlayerScrollScreen(this Room self, C2M_PlayerScrollScreen message)
        {
            //交换两个位置的钻石
            //首先取出临时钻石
            int LieIndex = message.StartX;
            int HangIndex = message.StartY;
            Diamond diamond = self.GetDiamond(LieIndex, HangIndex);
            Diamond nextDiamond = self.GetDiamondWithDir(diamond, message.DirType);
            if (diamond != null && nextDiamond != null)
            {
                M2C_SyncDiamondAction m2CSyncDiamondAction = new M2C_SyncDiamondAction();
                m2CSyncDiamondAction.DiamondActionItems.Add(self.SwapDiamondPos(diamond, nextDiamond));
                bool isCrash = true;
                bool isCrashSuccess = false;
                bool isMoveDown = false;
                bool isFirstAddSpecial = true;
                // bool isSpecial = false;
                Queue<Diamond> specialDiamonds = new Queue<Diamond>();
                while (isCrash || isMoveDown || specialDiamonds.Count > 0)
                {
                    isCrash = self.CheckCrash(m2CSyncDiamondAction.DiamondActionItems, diamond, nextDiamond, specialDiamonds, isFirstAddSpecial);
                    if (isFirstAddSpecial)
                    {
                        isFirstAddSpecial = false;
                    }
                    isMoveDown = self.MoveDownAllDiamond(m2CSyncDiamondAction.DiamondActionItems);
                    if (isCrashSuccess == false && isCrash)
                    {
                        isCrashSuccess = true;
                    }

                    if (specialDiamonds.Count > 0)
                    {
                        Diamond specialDiamond = specialDiamonds.Dequeue();
                        self.AutoCastSpecialDiamond(m2CSyncDiamondAction.DiamondActionItems, specialDiamond);
                    }
                }

                if (isCrashSuccess)
                {
                    self.ToggleTurnSeatIndex();
                    self.syncCurrentTurnIndex();
                }
                else
                {
                    //todo 交换失败。反向交换
                    m2CSyncDiamondAction.DiamondActionItems.Add(self.SwapDiamondPos(diamond, nextDiamond));
                }

                //todo 下发交换action 消息
                foreach (var unit in self.Units)
                {
                    MessageHelper.SendToClient(unit, m2CSyncDiamondAction);
                }
            }
            else
            {
                //todo 非法操作
            }
        }

        //交换宝石的位置
        public static DiamondActionItem SwapDiamondPos(this Room self, Diamond diamond1, Diamond diamond2)
        {
            DiamondActionItem diamondActionItem = new DiamondActionItem();
            int LieIndex1 = diamond1.LieIndex;
            int HangIndex1 = diamond1.HangIndex;

            int LieIndex2 = diamond2.LieIndex;
            int HangIndex2 = diamond2.HangIndex;
            self.Diamonds[LieIndex1, HangIndex1] = diamond2;
            diamond2.SetIndex(LieIndex1, HangIndex1);
            diamondActionItem.DiamondActions.Add(new DiamondAction()
            {
                DiamondInfo = diamond2.GetMessageInfo(), ActionType = (int) DiamondActionType.Move
            });

            self.Diamonds[LieIndex2, HangIndex2] = diamond1;
            diamond1.SetIndex(LieIndex2, HangIndex2);
            diamondActionItem.DiamondActions.Add(new DiamondAction()
            {
                DiamondInfo = diamond1.GetMessageInfo(), ActionType = (int) DiamondActionType.Move
            });
            return diamondActionItem;
        }

        public static Diamond GetDiamond(this Room self, int LieIndex, int HangIndex)
        {
            // Diamond diamond = null;
            if (LieIndex < 0 || HangIndex < 0 || LieIndex >= self.LieCount || HangIndex >= self.HangCount)
            {
                return null;
            }

            return self.Diamonds[LieIndex, HangIndex];
        }

        //获得列上的可以消除的列表
        public static List<List<Diamond>> GetLieCrashListList(this Room self)
        {
            List<List<Diamond>> crashListList = new List<List<Diamond>>();
            for (int i = 0; i < self.HangCount; i++)
            {
                for (int j = 0; j < self.LieCount; j++)
                {
                    Diamond diamond = self.Diamonds[j, i];
                    if (diamond == null)
                    {
                        continue;
                    }

                    if (self.CheckIsContainInListList(crashListList, diamond))
                    {
                        continue;
                    }

                    List<Diamond> sameLieList = self.CheckLieSameDiamond(self.Diamonds[j, i]);
                    if (sameLieList.Count >= 3)
                    {
                        crashListList.Add(sameLieList);
                    }
                }
            }

            return crashListList;
        }

        public static List<List<Diamond>> GetHangCrashListList(this Room self)
        {
            List<List<Diamond>> crashListList = new List<List<Diamond>>();
            for (int i = 0; i < self.HangCount; i++)
            {
                for (int j = 0; j < self.LieCount; j++)
                {
                    Diamond diamond = self.Diamonds[j, i];
                    if (diamond == null)
                    {
                        continue;
                    }

                    if (self.CheckIsContainInListList(crashListList, diamond))
                    {
                        continue;
                    }

                    List<Diamond> sameHangList = self.CheckHangSameDiamond(self.Diamonds[j, i]);
                    if (sameHangList.Count >= 3)
                    {
                        Log.Debug("same hang list =  " + sameHangList.Count);
                        crashListList.Add(sameHangList);
                    }
                }
            }

            return crashListList;
        }

        public static bool CheckIsContainInListList<T>(this Room self, List<List<T>> listlist, T target)
        {
            foreach (var list in listlist)
            {
                if (list.Contains(target))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool CheckCrash(this Room self, List<DiamondActionItem> diamondActionItems, Diamond touchDiamond, Diamond swapDiamond,
        Queue<Diamond> specialDiamonds, bool isFirstAddSpecial)
        {
            List<List<Diamond>> lieCrashListList = self.GetLieCrashListList();
            List<List<Diamond>> hangCrashListList = self.GetHangCrashListList();
            List<List<Diamond>> endListList = new List<List<Diamond>>();
            // //行列 查重
            foreach (var lieList in lieCrashListList)
            {
                for (var i = 0; i < hangCrashListList.Count; i++)
                {
                    if (self.CheckListIsOver(lieList, hangCrashListList[i]))
                    {
                        var hangList = hangCrashListList[i];
                        hangCrashListList.RemoveAt(i);
                        i--;
                        foreach (var target in hangList)
                        {
                            if (!lieList.Contains(target))
                            {
                                lieList.Add(target);
                            }
                        }
                    }
                }
            }

            //
            foreach (var lieList in lieCrashListList)
            {
                endListList.Add(lieList);
            }

            //
            foreach (var hangList in hangCrashListList)
            {
                endListList.Add(hangList);
            }

            //
            DiamondActionItem diamondActionItem = new DiamondActionItem();
            foreach (var crashList in endListList)
            {
                foreach (var diamond in crashList)
                {
                    DiamondAction diamondAction = new DiamondAction();
                    diamondAction.ActionType = (int) DiamondActionType.Destory;
                    diamondAction.DiamondInfo = diamond.GetMessageInfo();
                    self.Diamonds[diamond.LieIndex, diamond.HangIndex] = null;
                    diamond.Dispose();
                    diamondActionItem.DiamondActions.Add(diamondAction);
                }
            }

            DiamondActionItem specialActionItem = new DiamondActionItem();

            if (isFirstAddSpecial)
            {
                foreach (var list in endListList)
                {
                    //增加特殊钻石
                    DiamondAction diamondAction = self.AddSpecialDiamond(list, touchDiamond, swapDiamond, specialDiamonds);
                    if (diamondAction != null)
                    {
                        // diamondActionItem.DiamondActions.Add(diamondAction);
                        specialActionItem.DiamondActions.Add(diamondAction);
                    }
                }
            }

            if (specialActionItem.DiamondActions.Count > 0)
            {
                diamondActionItems.Add(specialActionItem);
            }

            if (diamondActionItem.DiamondActions.Count > 0)
            {
                diamondActionItems.Add(diamondActionItem);
                return true;
            }

            return false;
        }

        public static bool CheckListIsOver<T>(this Room self, List<T> list1, List<T> list2)
        {
            foreach (var target in list1)
            {
                if (list2.Contains(target))
                {
                    return true;
                }
            }

            //检查列表是否重合
            return false;
        }

        public static List<Diamond> ConnectSameDiamondList(this Room self, List<Diamond> list1, List<Diamond> list2)
        {
            List<Diamond> endList = new List<Diamond>();
            if (list1 != null && list1.Count > 2)
            {
                foreach (var diamond in list1)
                {
                    endList.Add(diamond);
                }
            }

            if (list2 != null && list2.Count > 2)
            {
                foreach (var diamond in list2)
                {
                    endList.Add(diamond);
                }
            }

            foreach (var diamond in endList)
            {
                Log.Debug($"{diamond.LieIndex} , {diamond.HangIndex}");
            }

            Dictionary<Diamond, bool> dictionary = new Dictionary<Diamond, bool>();
            foreach (var diamond in endList)
            {
                if (!dictionary.Keys.Contains(diamond))
                {
                    dictionary.Add(diamond, true);
                }
            }

            return dictionary.Keys.ToList();
        }

        public static List<Diamond> CheckHangSameDiamond(this Room self, Diamond currentDiamond)
        {
            //todo 找到并保存相同类型的宝石
            List<Diamond> sameList = new List<Diamond>();
            //寻找上下方向上是否有相同类型的宝石
            Queue<Diamond> checkQueue = new Queue<Diamond>();
            checkQueue.Enqueue(currentDiamond);
            sameList.Add(currentDiamond);

            List<Diamond> alCheckList = new List<Diamond>();
            while (checkQueue.Count > 0)
            {
                Diamond diamond = checkQueue.Dequeue();
                if (alCheckList.Contains(diamond))
                {
                    continue;
                }

                alCheckList.Add(diamond);

                ScrollDirType[] scrollDirTypes = new ScrollDirType[2] { ScrollDirType.Left, ScrollDirType.Right };
                for (var h = 0; h < scrollDirTypes.Length; h++)
                {
                    var type = scrollDirTypes[h];
                    Diamond findDiamond = self.GetDiamondWithDir(diamond, (int) type);

                    if (findDiamond != null)
                    {
                        if (alCheckList.Contains(findDiamond))
                        {
                            continue;
                        }

                        if (findDiamond.DiamondType == diamond.DiamondType)
                        {
                            checkQueue.Enqueue(findDiamond);
                            sameList.Add(findDiamond);
                        }
                    }
                }
            }

            return sameList;
        }

        public static List<Diamond> CheckLieSameDiamond(this Room self, Diamond currentDiamond)
        {
            //todo 找到并保存相同类型的宝石
            List<Diamond> sameList = new List<Diamond>();
            //寻找上下方向上是否有相同类型的宝石
            Queue<Diamond> checkQueue = new Queue<Diamond>();
            checkQueue.Enqueue(currentDiamond);
            sameList.Add(currentDiamond);
            List<Diamond> alCheckList = new List<Diamond>();
            while (checkQueue.Count > 0)
            {
                Diamond diamond = checkQueue.Dequeue();
                if (alCheckList.Contains(diamond))
                {
                    continue;
                }

                alCheckList.Add(diamond);
                ScrollDirType[] scrollDirTypes = { ScrollDirType.Down, ScrollDirType.Up };
                for (var h = 0; h < scrollDirTypes.Length; h++)
                {
                    var type = scrollDirTypes[h];
                    Diamond findDiamond = self.GetDiamondWithDir(diamond, (int) type);

                    if (findDiamond != null)
                    {
                        if (alCheckList.Contains(findDiamond))
                        {
                            continue;
                        }

                        if (findDiamond.DiamondType == diamond.DiamondType)
                        {
                            checkQueue.Enqueue(findDiamond);
                            sameList.Add(findDiamond);
                        }
                    }
                }
            }

            return sameList;
        }

        public static Diamond GetDiamondWithDir(this Room self, Diamond diamond, int type)
        {
            int lieIndex = diamond.LieIndex;
            int hangIndex = diamond.HangIndex;
            switch (type)
            {
                case (int) ScrollDirType.Down:
                    hangIndex -= 1;
                    break;
                case (int) ScrollDirType.Left:
                    lieIndex -= 1;
                    break;
                case (int) ScrollDirType.Right:
                    lieIndex += 1;
                    break;
                case (int) ScrollDirType.Up:
                    hangIndex += 1;
                    break;
            }

            if (hangIndex < 0 || hangIndex >= self.HangCount || lieIndex < 0 || lieIndex >= self.LieCount)
            {
                return null;
            }

            return self.Diamonds[lieIndex, hangIndex];
        }

        public static bool MoveDownAllDiamond(this Room self, List<DiamondActionItem> diamondActionItems)
        {
            bool isMoveDown = false;
            //todo 将宝石都向下移动
            DiamondActionItem moveActionItem = new DiamondActionItem();
            DiamondActionItem createActionItem = new DiamondActionItem();
            DiamondComponent diamondComponent = self.DiamondComponent;
            //遍历每一列
            for (var i = 0; i < self.LieCount; i++)
            {
                List<Diamond> diamonds = new List<Diamond>();
                //找到这一列有多少是空的
                for (var j = 0; j < self.HangCount; j++)
                {
                    Diamond diamond = self.GetDiamond(i, j);
                    if (diamond != null)
                    {
                        diamonds.Add(diamond);
                    }
                }

                for (var j = 0; j < self.HangCount; j++)
                {
                    if (j < diamonds.Count)
                    {
                        Diamond diamond = diamonds[j];
                        if (!diamond.EqualsIndex(i, j))
                        {
                            self.Diamonds[i, j] = diamond;
                            diamond.SetIndex(i, j);
                            DiamondAction action = new DiamondAction();
                            action.ActionType = (int) DiamondActionType.Move;
                            action.DiamondInfo = diamond.GetMessageInfo();
                            moveActionItem.DiamondActions.Add(action);
                        }
                    }
                    else
                    {
                        Diamond diamond = diamondComponent.CreateOneDiamond();
                        self.Diamonds[i, j] = diamond;
                        diamond.InitLieIndex = i;
                        diamond.InitHangIndex = self.HangCount + j;
                        diamond.SetIndex(i, j);
                        DiamondAction action = new DiamondAction();
                        action.ActionType = (int) DiamondActionType.Create;
                        action.DiamondInfo = diamond.GetMessageInfo();
                        createActionItem.DiamondActions.Add(action);
                    }
                }
            }

            if (moveActionItem.DiamondActions.Count > 0)
            {
                diamondActionItems.Add(moveActionItem);
                // isMoveDown = true;
            }

            if (createActionItem.DiamondActions.Count > 0)
            {
                diamondActionItems.Add(createActionItem);
                isMoveDown = true;
            }

            return isMoveDown;
        }

        /*
         * 增加特殊钻石的逻辑
         **/
        public static DiamondAction AddSpecialDiamond(this Room self, List<Diamond> crashList, Diamond touchDiamond, Diamond swapDiamond,
        Queue<Diamond> specialDiamonds)
        {
            DiamondAction diamondAction = null;

            if (crashList.Count >= 4)
            {
                DiamondComponent diamondComponent = self.DomainScene().GetComponent<DiamondComponent>();
                Diamond diamond = diamondComponent.CreateOneDiamond();
                DirectionType directionType = self.GetListDirection(crashList);
                Log.Debug($"direction type {directionType}");
                Diamond targetDiamond = null;
                BoomType boomType = BoomType.Invalide;
                switch (directionType)
                {
                    case DirectionType.Horizontal:
                        targetDiamond = self.GetLastLeftDiamond(crashList);
                        if (crashList.Count >= 5)
                        {
                            boomType = BoomType.BlackHole;
                        }
                        else
                        {
                            boomType = BoomType.LazerH;
                        }

                        break;
                    case DirectionType.Vertical:
                        targetDiamond = self.GetLastDownDiamond(crashList);
                        if (crashList.Count >= 5)
                        {
                            boomType = BoomType.BlackHole;
                        }
                        else
                        {
                            boomType = BoomType.LazerV;
                        }

                        break;
                    case DirectionType.Cross:
                        if (crashList.Count >= 5)
                        {
                            Log.Debug("Cross");
                            boomType = BoomType.Boom;
                            targetDiamond = self.GetCrossDiamond(crashList);
                        }

                        break;
                }

                if (crashList.Contains(touchDiamond))
                {
                    targetDiamond = touchDiamond;
                }

                if (crashList.Contains(swapDiamond))
                {
                    targetDiamond = swapDiamond;
                }

                if (!specialDiamonds.Contains(diamond))
                {
                    Log.Debug("create boom type" + boomType);
                    diamond.InitLieIndex = targetDiamond.LieIndex;
                    diamond.InitHangIndex = targetDiamond.HangIndex;
                    diamond.SetIndex(targetDiamond.LieIndex, targetDiamond.HangIndex);
                    diamond.DiamondType = targetDiamond.DiamondType;
                    diamond.BoomType = (int) boomType;
                    self.Diamonds[diamond.LieIndex, diamond.HangIndex] = diamond;
                    diamondAction = new DiamondAction();
                    diamondAction.ActionType = (int) DiamondActionType.Create;
                    diamondAction.DiamondInfo = diamond.GetMessageInfo();
                    specialDiamonds.Enqueue(diamond);
                }
            }

            return diamondAction;
        }

        public static Diamond GetLastDownDiamond(this Room self, List<Diamond> list)
        {
            Diamond target = null;
            var minHangIndex = 1000;
            foreach (var diamond in list)
            {
                if (diamond.HangIndex < minHangIndex)
                {
                    minHangIndex = diamond.HangIndex;
                    target = diamond;
                }
            }

            return target;
        }

        public static Diamond GetLastLeftDiamond(this Room self, List<Diamond> list)
        {
            Diamond target = null;
            var maxLeftIndex = 1000;
            foreach (var diamond in list)
            {
                if (diamond.LieIndex < maxLeftIndex)
                {
                    maxLeftIndex = diamond.LieIndex;
                    target = diamond;
                }
            }

            return target;
        }

        public static Diamond GetLastRightDiamond(this Room self, List<Diamond> list)
        {
            Diamond target = null;
            var maxRightIndex = -1000;
            foreach (var diamond in list)
            {
                if (diamond.LieIndex > maxRightIndex)
                {
                    maxRightIndex = diamond.LieIndex;
                    target = diamond;
                }
            }

            return target;
        }

        public static Diamond GetCrossDiamond(this Room self, List<Diamond> list)
        {
            List<Diamond> HList = new List<Diamond>();
            List<Diamond> VList = new List<Diamond>();
            Dictionary<int, int> Lmap = new Dictionary<int, int>();
            Dictionary<int, int> HMap = new Dictionary<int, int>();
            foreach (var diamond in list)
            {
                if (!Lmap.ContainsKey(diamond.LieIndex))
                {
                    Lmap.Add(diamond.LieIndex, 1);
                }
                else
                {
                    Lmap[diamond.LieIndex]++;
                }

                if (!HMap.ContainsKey(diamond.HangIndex))
                {
                    HMap.Add(diamond.HangIndex, 1);
                }
                else
                {
                    HMap[diamond.HangIndex]++;
                }
            }

            var max = 0;
            var LieIndex = 0;
            foreach (var key in Lmap.Keys)
            {
                if (Lmap[key] > max)
                {
                    max = Lmap[key];
                    LieIndex = key;
                }
            }

            max = 0;
            var HangIndex = 0;
            foreach (var key in HMap.Keys)
            {
                if (HMap[key] > max)
                {
                    max = HMap[key];
                    HangIndex = key;
                }
            }

            Log.Debug("Lie index = " + LieIndex);
            Log.Debug("Hang index" + HangIndex);
            foreach (var diamond in list)
            {
                if (diamond.LieIndex == LieIndex)
                {
                    VList.Add(diamond);
                }
            }

            Log.Debug("v list count" + VList.Count);
            foreach (var diamond in list)
            {
                if (diamond.HangIndex == HangIndex)
                {
                    HList.Add(diamond);
                }
            }

            Log.Debug("h list count" + HList.Count);

            foreach (var diamond in HList)
            {
                if (VList.Contains(diamond))
                {
                    return diamond;
                }
            }

            return null;
        }

        public static DirectionType GetListDirection(this Room self, List<Diamond> list)
        {
            //获取当前列表的方向
            Dictionary<int, bool> lieMap = new Dictionary<int, bool>();
            Dictionary<int, bool> hangMap = new Dictionary<int, bool>();
            foreach (var diamond in list)
            {
                if (!lieMap.ContainsKey(diamond.LieIndex))
                {
                    lieMap.Add(diamond.LieIndex, true);
                }

                if (!hangMap.ContainsKey(diamond.HangIndex))
                {
                    hangMap.Add(diamond.HangIndex, true);
                }
            }

            Log.Debug("Lie map = " + lieMap.Count);
            Log.Debug("Hang map = " + hangMap.Count);
            if (lieMap.Count == list.Count)
            {
                return DirectionType.Horizontal;
            }

            if (hangMap.Count == list.Count)
            {
                return DirectionType.Vertical;
            }

            return DirectionType.Cross;
        }

        public static bool AutoCastSpecialDiamond(this Room self, List<DiamondActionItem> diamondActionItems, Diamond diamond)
        {
            bool isSpecial = false;
            DiamondActionItem diamondActionItem = new DiamondActionItem();
            if (diamond != null && diamond.BoomType != (int) BoomType.Invalide)
            {
                Log.Debug("存在特殊diamond");
                List<Diamond> crashList = new List<Diamond>();
                switch (diamond.BoomType)
                {
                    case (int) BoomType.Boom:
                        crashList = self.GetCrossLineDiamond(diamond);
                        break;
                    case (int) BoomType.BlackHole:
                        crashList = self.GetAroundDiamond(diamond, 5);
                        break;
                    case (int) BoomType.LazerH:
                        crashList = self.GetLineDiamond(diamond, DirectionType.Horizontal);
                        break;
                    case (int) BoomType.LazerV:
                        crashList = self.GetLineDiamond(diamond, DirectionType.Vertical);
                        break;
                }

                isSpecial = true;
                // crashListList.Add(crashList);
                foreach (var crashDiamond in crashList)
                {
                    if (crashDiamond != null && !crashDiamond.IsDisposed && self.Diamonds[crashDiamond.LieIndex, crashDiamond.HangIndex] != null)
                    {
                        DiamondAction diamondAction = new DiamondAction();
                        diamondAction.ActionType = (int) DiamondActionType.Destory;
                        diamondAction.DiamondInfo = crashDiamond.GetMessageInfo();
                        self.Diamonds[crashDiamond.LieIndex, crashDiamond.HangIndex] = null;
                        crashDiamond.Dispose();
                        diamondActionItem.DiamondActions.Add(diamondAction);
                    }
                }
            }

            if (diamondActionItem.DiamondActions.Count > 0)
            {
                diamondActionItems.Add(diamondActionItem);
            }

            return isSpecial;
        }

        public static List<Diamond> GetLineDiamond(this Room self, Diamond diamond, DirectionType directionType)
        {
            List<Diamond> list = new List<Diamond>();
            var count = directionType == DirectionType.Horizontal? self.LieCount : self.HangCount;
            for (int i = 0; i < count; i++)
            {
                Diamond target = self.Diamonds[directionType == DirectionType.Horizontal? i : diamond.LieIndex,
                    directionType == DirectionType.Horizontal? diamond.HangIndex : i];

                if (!target.Equals(diamond) && target.BoomType != (int) BoomType.Invalide)
                {
                    //todo 如果被消除的对象是一个特殊猪，并且它不与原来猪是同一颗
                }
                else
                {
                    list.Add(target);
                }
            }

            return list;
        }

        public static List<Diamond> GetCrossLineDiamond(this Room self, Diamond diamond)
        {
            List<Diamond> list = new List<Diamond>();
            //获取交叉点上所有钻石
            for (int i = 0; i < self.HangCount; i++)
            {
                Diamond target = self.Diamonds[diamond.LieIndex, i];
                if (!target.Equals(diamond) && target.BoomType != (int) BoomType.Invalide)
                {
                    //todo 如果被消除的对象是一个特殊猪，并且它不与原来猪是同一颗
                }
                else
                {
                    list.Add(target);
                }
            }

            for (int i = 0; i < self.LieCount; i++)
            {
                Diamond target = self.Diamonds[i, diamond.HangIndex];
                if (!target.Equals(diamond) && target.BoomType != (int) BoomType.Invalide)
                {
                    //todo 如果被消除的对象是一个特殊猪，并且它不与原来猪是同一颗
                }
                else
                {
                    if (!list.Contains(target))
                    {
                        list.Add(target);
                    }
                }
            }

            return list;
        }

        public static List<Diamond> GetAroundDiamond(this Room self, Diamond diamond, int count)
        {
            var startLieIndex = diamond.LieIndex - (count - 1) / 2;
            var endLieIndex = startLieIndex + count;
            var startHangIndex = diamond.HangIndex - (count - 1) / 2;
            var endHangIndex = startHangIndex + count;
            List<Diamond> list = new List<Diamond>();
            for (int i = startHangIndex; i < endHangIndex; i++)
            {
                for (int j = startLieIndex; j < endLieIndex; j++)
                {
                    Diamond targetDiamond = self.GetDiamond(j, i);
                    if (targetDiamond != null)
                    {
                        if (!targetDiamond.Equals(diamond) && targetDiamond.BoomType != (int) BoomType.Invalide)
                        {
                            //todo 如果被消除的对象是一个特殊猪，并且它不与原来猪是同一颗
                        }
                        else
                        {
                            list.Add(targetDiamond);
                        }
                    }
                }
            }

            return list;
        }
    }
}