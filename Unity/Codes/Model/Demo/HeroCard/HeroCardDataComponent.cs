using System.Runtime.InteropServices;

namespace ET
{
    //todo 卡牌的数值组件
    public class HeroCardDataComponent: Entity, IAwake
    {
        public int HP;  //当前的血量
        public int DiamondAttack;
        public int HeroAttack;
        public int WeaponAttack;
        public int SkillAttack;
        public int NormalDamage;
        public int CriticalDamage; //暴击伤害

    }
}