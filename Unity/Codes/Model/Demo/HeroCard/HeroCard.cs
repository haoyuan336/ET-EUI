using System;

namespace ET
{
    public class HeroCard: Entity,IAwake,IUpdate,IDestroy
    {
        public String HeroName;    //英雄名称 支持可编辑
        public long OwnerId;        //拥有者的id  也就是玩家id
        public int ConfigId;        //在配置表里面的id
        public long TroopId; //队伍Id
        public int InTroopIndex;    //在队伍里面的index

        public int HeroColor;   //英雄属性
        
        public int Attack;
        public int HP;
        public int Defence;
    }
}