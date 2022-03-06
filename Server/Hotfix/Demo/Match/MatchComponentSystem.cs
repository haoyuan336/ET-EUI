using System;
using System.ComponentModel.Design;

namespace ET
{

    public class MatchComponentAwakeSystem: AwakeSystem<MatchComponent>
    {
        public override void Awake(MatchComponent self)
        {
            self.SyncCurrentMatchingUnitCount();
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
        public static void AddMatch(this MatchComponent self, Unit unit)
        {
            if (!self.MatchingUnits.Contains(unit))
            {
                self.MatchingUnits.Add(unit);
            }
        }
        
        public static async void SyncCurrentMatchingUnitCount(this MatchComponent self)
        {
            while (true)
            {
                await TimerComponent.Instance.WaitAsync(1000);
                // Log.Debug("sync current matching unit count");
                foreach (var unit in self.MatchingUnits)
                {
                    // unit.GetComponent<SessionInfoComponent>().Session.Send(new M2C_SyncCurrentMatchingCount(){Content = self.MatchingUnits.Count});
                    // MessageHelper.CallActor(unit)
                    // MessageHelper.SendActor(unit.Id,new M2C_SyncCurrentMatchingCount(){Content = self.MatchingUnits.Count});
                    MessageHelper.SendToClient(unit, new M2C_SyncCurrentMatchingCount(){Content = self.MatchingUnits.Count});
                }
            }
        }
    }
}