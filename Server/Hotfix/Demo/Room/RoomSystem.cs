using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Numerics;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MongoDB.Driver.Core.Operations;
using NLog.Fluent;

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

            self.InitGameData();
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
            for (var i = 0; i < self.HangCount; i++)
            {
                for (var j = 0; j < self.LieCount; j++)
                {
                    Diamond diamond = diamondComponent.CreateOneDiamond();
                    diamond.SetIndex(j, i);
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
                bool isCrash = self.CheckCrash(m2CSyncDiamondAction.DiamondActionItems);
                if (isCrash)
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
            return self.Diamonds[LieIndex, HangIndex];
        }

        public static bool CheckCrash(this Room self, List<DiamondActionItem> diamondActionItems)
        {
            PvPLevelConfig config = PvPLevelConfigCategory.Instance.Get(1);
            //检查存在的消除组合
            int LieCount = config.LieCount;
            int HangCount = config.HangCount;
            DiamondActionItem diamondActionItem = new DiamondActionItem();
            // Diamond[]
            for (int i = 0; i < HangCount; i++)
            {
                for (int j = 0; j < LieCount; j++)
                {
                    Diamond diamond = self.Diamonds[j, i];
                    if (diamond == null)
                    {
                        continue;
                    }

                    List<Diamond> sameLieList = self.CheckLieSameDiamond(self.Diamonds[j, i]);
                    if (sameLieList.Count > 2)
                    {
                        Log.Debug($"same lie list count = {sameLieList.Count}+ {sameLieList[0].DiamondType}");
                        foreach (var d in sameLieList)
                        {
                            Log.Debug($"id={d.Id}");
                        }
                    }

                    List<Diamond> sameHangList = self.CheckHangSameDiamond(self.Diamonds[j, i]);
                    if (sameHangList == null)
                    {
                        Log.Debug("same hang list is null");
                    }

                    if (sameHangList != null)
                    {
                        Log.Debug($"same hang list count = {sameHangList.Count}");
                    }

                    List<Diamond> endList = self.ConnectSameDiamondList(sameLieList, sameHangList);
                    Log.Debug($"carsh diamond count ={endList.Count}");
                    if (endList.Count > 2)
                    {
                        // DiamondActionItem diamondActionItem = new DiamondActionItem();
                        foreach (var crashDiamond in endList)
                        {
                            // m2CSyncDiamondAction.DiamondActionItems.
                            DiamondAction diamondAction = new DiamondAction();
                            diamondAction.ActionType = (int) DiamondActionType.Destory;
                            diamondAction.DiamondInfo = crashDiamond.GetMessageInfo();
                            self.Diamonds[crashDiamond.LieIndex, crashDiamond.HangIndex] = null;
                            // self.DiamondComponent.RemoveLocation(crashDiamond);
                            crashDiamond.Dispose();
                            diamondActionItem.DiamondActions.Add(diamondAction);
                        }
                    }
                }
            }

            if (diamondActionItem.DiamondActions.Count > 0)
            {
                diamondActionItems.Add(diamondActionItem);
                return true;
            }

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

                ScrollDirType[] scrollDirTypes = new ScrollDirType[2] { ScrollDirType.Down, ScrollDirType.Up };
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
    }
}