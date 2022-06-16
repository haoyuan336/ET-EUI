namespace ET
{
    public static class CoroutineLockType
    {
        public const int None = 0;
        public const int Location = 1; // location进程上使用
        public const int ActorLocationSender = 2; // ActorLocationSender中队列消息 
        public const int Mailbox = 3; // Mailbox中队列
        public const int UnitId = 4; // Map服务器上线下线时使用
        public const int DB = 5;
        public const int Resources = 6;
        public const int ResourcesLoader = 7;
        public const int LoginAccount = 8; //
        public const int LoginCenterLock = 9;
        public const int LoginGateLock = 10;
        public const int AccountGetRealmKey = 11;
        public const int AccountGetRate = 12;
        public const int BuyWeapon = 13; //购买武器的操作
        public const int ClearWordBar = 14; //洗练词条
        public const int ReadMail = 15; //读取邮箱
        public const int ReceiveAward = 16; //收取奖励
        public const int UpdateHeroLevel = 17;  //升级英雄等级

        public const int Max = 100; // 这个必须最大
    }
}