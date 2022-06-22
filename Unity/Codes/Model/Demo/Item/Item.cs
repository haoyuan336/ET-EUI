namespace ET
{
    public class Item: Entity
    {
        public int ConfigId; //配置id
        public int Count; //个数
        public long OwnerId; //所属者id
        public int State = (int) StateType.Active; //当前状态 1， 可用状态 -1，不可用状态
        public long MailId;
        public long CreateTime = TimeHelper.ServerNow();
        public long ItemType = 
    }
}