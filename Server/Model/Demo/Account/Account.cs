namespace ET
{
    public enum AccountType
    {
        General = 0,
        BlackList = 1
    }
    public class Account:Entity,IAwake
    {
        public string AccountName;
        public string Password;
        public long CreateTime;
        public int AccountType;
        public int PVELevelNumber;  //pve模式下，玩家玩到第几关了
    }
}