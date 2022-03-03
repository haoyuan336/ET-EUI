using System.Collections.Generic;

namespace ET
{
    public class C2M_MatchRoomMessageHandler: AMActorLocationHandler<Unit, C2M_MatchRoomActorLocationMessage>
    {
        protected override async ETTask Run(Unit entity, C2M_MatchRoomActorLocationMessage message)
        {
            UnitComponent unitComponent = entity.DomainScene().GetComponent<UnitComponent>();
            // var unitCount = unitComponent.Children.Count;
            // Log.Debug("unit match count = " + unitCount);

            entity.isMatching = true;
            List<Unit> matchingUnits = new List<Unit>();

            foreach (Entity child in unitComponent.Children.Values)
            {
                Unit unit;
                try
                {
                    unit = (Unit) child;
                    if (unit.isMatching)
                    {
                        matchingUnits.Add(unit);
                    }
                }
                finally
                {
                }
            }

            foreach (var unit in matchingUnits)
            {
                MessageHelper.SendToClient(unit, new M2C_SyncCurrentMatchingCount() { Content = matchingUnits.Count });
            }

            // MessageHelper.SendToClient();
            await ETTask.CompletedTask;
        }
    }
}