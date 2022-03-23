using System.Collections.Generic;

namespace ET
{
    public class PVERoom: Entity, IAwake, IUpdate, IDestroy
    {
        public List<Unit> Units = new List<Unit>();
        public int CurrentTurnIndex = 0;
        public int HangCount = 0;
        public int LieCount = 0;
        public Diamond[,] Diamonds = null;
        public DiamondComponent DiamondComponent = null;
    }
}