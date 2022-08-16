using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class HeroCard: Entity, IAwake, IUpdate, IDestroy
    {
        public String HeroName; //英雄名称 支持可编辑
        public long OwnerId; //拥有者的id  也就是玩家id
        public int ConfigId; //在配置表里面的id
        public long TroopId; //队伍Id
        public int InTroopIndex; //在队伍里面的index
        public long MailId; //邮件Id
        // public int HeroColor; //英雄属性
        // public float HP;
        // public float Defence;
        // public float DiamondAttack;
        // public float DiamondDefence;
        public int CampIndex; //todo 阵营index
        // public float Angry; //怒气值
        // public float TotalAngry; //怒气值上线
        public int Level = 1; //当前英雄的等级
        // public long CurrentSkillId; //当前需要释放的技能
        // public HeroCardInfo HeroCardInfo;
        public int Star = 0; //英雄的星级数量
        public int Rank = 0; //英雄的阶数
        public int Count = 1; //材料数量
        public int State = (int)StateType.Active; //英雄的状态 
        public int CurrentExp = 0; //当前的经验值
        public long CallTime = TimeHelper.ServerNow(); //召唤时间戳
        public bool IsLock = false; //是否被锁定 状态
    }
}