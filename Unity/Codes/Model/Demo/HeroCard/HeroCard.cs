using System;
using System.Collections.Generic;

namespace ET
{
    public class HeroCard: Entity, IAwake, IUpdate, IDestroy
    {
        public String HeroName; //英雄名称 支持可编辑
        public long OwnerId; //拥有者的id  也就是玩家id
        public int ConfigId; //在配置表里面的id
        public long TroopId; //队伍Id
        public int InTroopIndex; //在队伍里面的index

        public int IsDead; //是否已经死亡

        public int HeroColor; //英雄属性

        public float Attack;
        public float HP;
        public float Defence;

        public int CampIndex; //todo 阵营index

        public float Angry; //怒气值

        // public Skill BigSkill; //必杀技
        // public Skill NormalSkill; //普通技能

        public long CurrentSkillId; //当前需要释放的技能
    }
}