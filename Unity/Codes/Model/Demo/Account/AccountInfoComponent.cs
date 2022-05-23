namespace ET
{
    public class AccountInfoComponent:Entity,IAwake,IDestroy
    {
        public string Token;
        public long AccountId;
        public string RealmToken;
        public string RealmAddress;
        public long GateKey;    
        public string GateAddress;
        public long PlayerId;
    }
}