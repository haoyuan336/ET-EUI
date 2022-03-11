using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Numerics;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
            int OffsetLie = 0;
            int OffsetHang = 0;
            switch (message.DirType)
            {
                case (int) ScrollDirType.Up:
                    OffsetHang += 1;
                    break;
                case (int) ScrollDirType.Down:
                    OffsetHang -= 1;
                    break;
                case (int) ScrollDirType.Left:
                    OffsetLie -= 1;
                    break;
                case (int) ScrollDirType.Right:
                    OffsetLie += 1;
                    break;
            }

            Diamond nextDiamond = self.Diamonds[LieIndex + OffsetLie, HangIndex + OffsetHang];

            if (diamond != null && nextDiamond != null)
            {
                M2C_SyncDiamondAction m2CSyncDiamondAction = new M2C_SyncDiamondAction();
                m2CSyncDiamondAction.DiamondActionItems.Add(self.SwapDiamondPos(diamond, nextDiamond));
                bool isCrash = self.CheckCrash();
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
            diamondActionItem.DiamondActions.Add(new DiamondAction() { DiamondInfo = diamond2.GetMessageInfo() });

            self.Diamonds[LieIndex2, HangIndex2] = diamond1;
            diamond1.SetIndex(LieIndex2, HangIndex2);
            diamondActionItem.DiamondActions.Add(new DiamondAction() { DiamondInfo = diamond1.GetMessageInfo() });
            return diamondActionItem;
        }

        public static Diamond GetDiamond(this Room self, int LieIndex, int HangIndex)
        {
            // Diamond diamond = null;
            return self.Diamonds[LieIndex, HangIndex];
        }

        public static bool CheckCrash(this Room self)
        {
            PvPLevelConfig config = PvPLevelConfigCategory.Instance.Get(1);
            //检查存在的消除组合
            int LieCount = config.LieCount;
            int HangCount = config.HangCount;
            // Diamond[]

            List<Diamond> alLieCheckList = new List<Diamond>();
            List<Diamond> alHangCheckList = new List<Diamond>();
            for (int i = 0; i < HangCount; i++)
            {
                for (int j = 0; j < LieCount; j++)
                {
                    List<Diamond> sameLieList = self.CheckLieSameDiamond(self.Diamonds[j, i], alLieCheckList);
                    if (sameLieList.Count > 2)
                    {
                        Log.Error("Find Same List");
                    }

                    List<Diamond> sameHangLie = self.CheckHangSameDiamond(self.Diamonds[j, i], alHangCheckList);
                    if (sameHangLie.Count > 2)
                    {
                        Log.Error("Find same hang");
                    }
                }
            }

            return true;
        }

        public static List<Diamond> CheckHangSameDiamond(this Room self, Diamond currentDiamond, List<Diamond> alCheckList)
        {
            if (alCheckList.Contains(currentDiamond))
            {
                return null;
            }

            //todo 找到并保存相同类型的宝石
            List<Diamond> sameList = new List<Diamond>();
            //寻找上下方向上是否有相同类型的宝石
            Queue<Diamond> checkQueue = new Queue<Diamond>();
            checkQueue.Enqueue(currentDiamond);
            sameList.Add(currentDiamond);
            alCheckList.Add(currentDiamond);
            while (checkQueue.Count > 0)
            {
                Diamond diamond = checkQueue.Dequeue();
                ScrollDirType[] scrollDirTypes = new ScrollDirType[2] { ScrollDirType.Left, ScrollDirType.Right };
                for (var h = 0; h < scrollDirTypes.Length; h++)
                {
                    var type = scrollDirTypes[h];
                    Diamond findDiamond = self.GetDiamondWithDir(diamond, type);
                    if (findDiamond != null)
                    {
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

        public static List<Diamond> CheckLieSameDiamond(this Room self, Diamond currentDiamond, List<Diamond> alCheckList)
        {
            //todo 找到并保存相同类型的宝石
            List<Diamond> sameList = new List<Diamond>();
            //寻找上下方向上是否有相同类型的宝石
            Queue<Diamond> checkQueue = new Queue<Diamond>();
            checkQueue.Enqueue(currentDiamond);
            sameList.Add(currentDiamond);
            alCheckList.Add(currentDiamond);
            while (checkQueue.Count > 0)
            {
                Diamond diamond = checkQueue.Dequeue();
                ScrollDirType[] scrollDirTypes = new ScrollDirType[2] { ScrollDirType.Down, ScrollDirType.Up };
                for (var h = 0; h < scrollDirTypes.Length; h++)
                {
                    var type = scrollDirTypes[h];
                    Diamond findDiamond = self.GetDiamondWithDir(diamond, type);
                    if (findDiamond != null)
                    {
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

        public static Diamond GetDiamondWithDir(this Room self, Diamond diamond, ScrollDirType type)
        {
            int lieIndex = diamond.LieIndex;
            int hangIndex = diamond.HangIndex;
            switch (type)
            {
                case ScrollDirType.Down:
                    hangIndex -= 1;
                    break;
                case ScrollDirType.Left:
                    lieIndex -= 1;
                    break;
                case ScrollDirType.Right:
                    lieIndex += 1;
                    break;
                case ScrollDirType.Up:
                    hangIndex += 1;
                    break;
            }

            if (hangIndex < 0 || hangIndex >= self.HangCount || lieIndex < 0 || hangIndex > self.LieCount)
            {
                return null;
            }

            return self.Diamonds[lieIndex, hangIndex];
        }
    }
}