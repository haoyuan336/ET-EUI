using System.Runtime.InteropServices;

namespace ET
{
    //todo 卡牌的数值组件
    public class HeroCardDataComponent: Entity, IAwake, IAwake<HeroCardDataComponentInfo>
    {
        public int HP;  //当前的血量
        public int DiamondAttackAddition;
        public int HeroAttack;
        public int WeaponAttack;
        public int SkillAttack;
        public int NormalDamage;
        public int CriticalRate;    //暴击率
        public int CriticalDamage; //暴击伤害
        public int Angry;   //当前的怒气值
        public long CurrentSkillId;
    }
}