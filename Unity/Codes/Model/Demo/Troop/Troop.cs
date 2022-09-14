namespace ET
{
    // [BsonElement("Troop")]
    public class Troop: Entity,IAwake
    {
        public long OnwerId;
        public int State = 1;
        public long CreateTime = TimeHelper.ServerNow();
        public int Index = 1;
        public bool IsOn = false; //是否为选择状态
    }
}