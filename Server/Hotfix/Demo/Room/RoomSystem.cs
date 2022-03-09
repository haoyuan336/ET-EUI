using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Numerics;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ET
{
    public class RoomAwakeSystem: AwakeSystem<Room>
    {
        public override void Awake(Room self)
        {
            Log.Debug("room awake" + self.Id);
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
            PvPLevelConfig pvPLevelConfig = PvPLevelConfigCategory.Instance.Get(1);
            self.HangCount = pvPLevelConfig.HangCount;
            self.LieCount = pvPLevelConfig.LieCount;
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
            self.ToggleTurnSeatIndex();
            self.syncCurrentTurnIndex();
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
                self.Diamonds[LieIndex, HangIndex] = nextDiamond;
                self.Diamonds[LieIndex + OffsetLie, HangIndex + OffsetHang] = diamond;
                diamond.SetIndex(LieIndex + OffsetLie, HangIndex + OffsetHang);
                nextDiamond.SetIndex(LieIndex, HangIndex);
            }
            
            List<DiamondInfo> diamondInfos = new List<DiamondInfo>();
            diamondInfos.Add(diamond.GetMessageInfo());
            diamondInfos.Add(nextDiamond.GetMessageInfo());
            //同步结果
            foreach (var unit in self.Units)
            {
                MessageHelper.SendToClient(unit, new M2C_SyncDiamondUpdatePos() { DiamondInfos = diamondInfos });
            }
        }

        public static Diamond GetDiamond(this Room self, int LieIndex, int HangIndex)
        {
            // Diamond diamond = null;
            return self.Diamonds[LieIndex, HangIndex];
        }
    }
}