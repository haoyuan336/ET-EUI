using System;
using System.Runtime.InteropServices;

namespace ET
{
    //todo 卡牌的数值组件
    public class HeroCardDataComponent: Entity, IAwake
    {
        public int HP; //当前的血量
        public int DiamondAttackAddition;
        public int Damage;
        public bool IsCritical;
        public int Angry; //当前的怒气值
        
        public long CurrentSkillId;
        public int TotalHP;


        public int AddAngry;
        public int AddHP;
        public int SubAngry;    //降低怒气值

        // public int BeAttackPower;   //被攻击时候的攻击力
    }
}