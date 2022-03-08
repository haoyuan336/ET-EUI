using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Net.NetworkInformation;
namespace ET
{
    public class MatchComponentAwakeSystem: AwakeSystem<MatchComponent>
    {
        public override void Awake(MatchComponent self)
        {
            // self.SyncCurrentMatchingUnitCount();
        }
    }

    public class MatchComponentSystemUpdateSystem: UpdateSystem<MatchComponent>
    {
        public override void Update(MatchComponent self)
        {
        }
    }

    public static class MatchComponentSystem
    {
        public static async ETTask<int> CancelMatch(this MatchComponent self, Unit unit)
        {
            if (self.MatchingUnits.Contains(unit))
            {
                self.MatchingUnits.Remove(unit);
                return ErrorCode.ERR_Success;
            }

            self.SyncCurrentMatchingUnitCount();
            await ETTask.CompletedTask;
            return ErrorCode.ERR_Success;
        }

        public static void AddMatch(this MatchComponent self, Unit unit)
        {
            if (!self.MatchingUnits.Contains(unit))
            {
                self.MatchingUnits.Add(unit);
            }

            if (self.MatchingUnits.Count >= 1)
            {
                // self.MatchingUnits.
                List<Unit> units = new List<Unit>();
                for (var i = 0; i < 1; i++)
                {
                    units.Add(self.MatchingUnits[0]);
                    self.MatchingUnits.RemoveAt(0);
                }
                self.DomainScene().GetComponent<RoomComponent>().CreateRoom(units);
            }
            self.SyncCurrentMatchingUnitCount();
        }

        public static void SyncCurrentMatchingUnitCount(this MatchComponent self)
        {
            UnitComponent unitComponent = self.DomainScene().GetComponent<UnitComponent>();
            foreach (Entity entity in unitComponent.Children.Values)
            {
                try
                {
                    Unit unit = (Unit) entity;
                    if (!unit.IsDisposed)
                    {
                        MessageHelper.SendToClient(unit, new M2C_SyncCurrentMatchingCount() { Content = self.MatchingUnits.Count });
                    }
                }
                catch (Exception e)
                {
                    Log.Debug("cast failed" + e.ToString());
                }
            }
        }
    }
}