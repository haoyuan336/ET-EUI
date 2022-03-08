using System;
using System.Collections.Generic;

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
        }

        public static void InitGameData(this Room self)
        {
            PvPLevelConfig pvPLevelConfig = PvPLevelConfigCategory.Instance.Get(1);
            self.HangCount = pvPLevelConfig.HangCount;
            self.LieCount = pvPLevelConfig.LieCount;
            self.Diamonds = new Diamond[self.LieCount, self.HangCount];
            List<DiamondInfo> diamondInfos = new List<DiamondInfo>();
            for (var i = 0; i< self.HangCount; i ++)
            {
                for (var j = 0 ; j < self.LieCount; j ++)
                {

                    Diamond diamond = self.GetParent<DiamondComponent>().CreateOneDiamond(j, i);
                    diamondInfos.Add(diamond.GetMessageInfo());
                }
            }

            

            foreach (var unit in self.Units)
            {
                
                MessageHelper.SendToClient(unit,new M2C_InitMapData(){DiamondInfo = diamondInfos});
            }

            

        }
    }
}