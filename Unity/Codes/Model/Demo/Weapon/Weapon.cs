namespace ET
{
    public class Weapon: Entity, IAwake
    {
        public int Level;
        public string Entry = "";
        public long OwnerId;
        public int ConfigId;
        public int Count;
    }
}