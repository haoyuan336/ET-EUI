namespace ET
{
    public class GameAction: Entity
    {

        public long OnwerId;
        public int ConfigId;
        public int State = (int)StateType.Active;
        public long CreateTime = TimeHelper.ServerNow();
    }
}