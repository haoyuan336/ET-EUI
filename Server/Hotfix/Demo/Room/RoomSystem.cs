using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

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
                if (self.CurrentTurnIndex >= self.Units.Count)
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
    }
}