namespace ET
{
    public class Buff: Entity, IAwake<int>, IDestroy
    {
        public int ConfigId = 0;
        public int RoundCount = 1;
        public int OverlabCount = 0; //叠加次数
        public int Damage = 0; //伤害值  
        public int HealthShield = 0; //生命值护盾
        // public int AlRoundCount = 0; //已经进行了几个回合
        // public int CastAttackPower = 0; //施法者的攻击力值
        // public long AttachHeroId = 0;   //施加buff 的英雄
        public HeroCard AttachHeroCard; //施法者英雄
        public BuffConfig Config => BuffConfigCategory.Instance.Get(this.ConfigId);
    }
}