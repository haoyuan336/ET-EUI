using System.Collections.Generic;
namespace ET
{
    public class PVPRoom: Entity, IAwake,IDestroy, IUpdate
    {
        public List<Unit> Units = new List<Unit>();
        public int CurrentTurnIndex = 0;
        public int HangCount = 0;
        public int LieCount = 0;
        public DiamondComponent DiamondComponent = null;
    }
}