using System;

namespace ET
{
    public class GameAction: Entity
    {

        public long OwnerId;
        public int ConfigId;
        public int State = (int)StateType.Active;
        public long CreateTime = TimeHelper.ServerNow();
        public bool Win;
    }
}