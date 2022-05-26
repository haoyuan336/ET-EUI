namespace ET
{
    public class HeroCardDataComponentAwakeSystem: AwakeSystem<HeroCardDataComponent>
    {
        public override void Awake(HeroCardDataComponent self)
        {
        }
    }

    public static class HeroCardDataComponentSystem
    {
        public static HeroCardDataComponentInfo GetInfo(this HeroCardDataComponent self)
        {
            return new HeroCardDataComponentInfo()
            {
                HP = self.HP,
                HeroAttack = self.HeroAttack,
                DiamondAttack = self.DiamondAttack,
                WeaponAttack = self.WeaponAttack,
                SkillAttack = self.SkillAttack,
                NormalDamage = self.NormalDamage,
                CriticalDamage = self.CriticalDamage
            };
        }
        
    }
}