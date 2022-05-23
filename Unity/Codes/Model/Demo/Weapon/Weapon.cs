namespace ET
{
    public class Weapon: Entity, IAwake
    {
        public int Level = 0;
        // public string Entry = "";   
        public long OwnerId;
        public int ConfigId;
        public int Count;
        public int State;
    }
}