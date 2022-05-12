namespace ET
{
    public enum AccountType
    {
        General = 0,
        BlackList = 1
    }

    public class Account: Entity, IAwake
    {
        public string AccountName;
        public string Password;
        public long CreateTime;
        public int AccountType;
        public int PVELevelNumber; //pve模式下，玩家玩到第几关了
        public long CurrentTroopId; //当前选择的队伍id
        // public int GoldCount; //金币个数
        // public int PowerCount; //体力个数
        // public int DiamondCount; //钻石个数
        public int Level;   //等级
        public int Exp;     //经验值
    }
}