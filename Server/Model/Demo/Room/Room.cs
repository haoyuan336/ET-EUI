using System.Collections.Generic;

namespace ET.Room
{
    public class Room: Entity, IAwake,IDestroy, IUpdate
    {
        public List<Unit> Units = new List<Unit>();
        public int CurrentTurnIndex = 0;
    }
}