using System;
using System.Collections.Generic;

namespace ET
{
    public class HeroCard: Entity, IAwake<int>, IAwake<int,HeroCardInfo>,IUpdate, IDestroy, IAwake<HeroCardInfo>
    {
        public String HeroName; //英雄名称 支持可编辑
        public long OwnerId; //拥有者的id  也就是玩家id
        public int ConfigId; //在配置表里面的id
        public long TroopId; //队伍Id
        public int InTroopIndex; //在队伍里面的index
        public int HeroColor; //英雄属性
        public float Attack;
        public float HP;
        public float Defence;
        public float DiamondAttack;
        public float DiamondDefence;
        public int CampIndex; //todo 阵营index
        public float Angry; //怒气值
        public int Level;   //当前英雄的等级
        public long CurrentSkillId; //当前需要释放的技能
    }
}