namespace ET.Account
{
    public class AccountInfoComponent:Entity,IAwake,IDestroy
    {
        public string Token;
        public long AccountId;
        public string RealmToken;
        public string RealmAddress;
        public string GateToken;    
        public string GateAddress;
    }
}