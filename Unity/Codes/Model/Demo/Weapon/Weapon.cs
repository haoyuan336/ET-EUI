namespace ET
{
    public class Weapon: Entity, IAwake
    {
        public int Level = 1;
        // public string Entry = "";   
        public long OwnerId;
        public int ConfigId;
        public int Count;
        public int State;
        public long OnWeaponHeroId; //装备到英雄身上的id
        public int CurrentExp;  //装备当前剩余的经验值
    }
}