using System.Collections.Generic;

namespace ET
{
    public class MatchComponent: Entity,IAwake,IDestroy,IUpdate
    {
        public List<Unit> MatchingUnits = new List<Unit>();
    }
}