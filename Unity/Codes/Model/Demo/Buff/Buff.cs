namespace ET
{
    public class Buff: Entity, IAwake<int>, IDestroy
    {
        public int ConfigId = 0;
        public int RoundCount = 1;
        public int OverlabCount = 1; //叠加次数
        public int Damage = 0;  //伤害值  
    }
}