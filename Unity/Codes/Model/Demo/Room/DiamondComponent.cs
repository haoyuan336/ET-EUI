namespace ET
{
    // public struct DiamondStruct
    // {
    //     public Diamond[] Diamonds;
    // }

    public class DiamondComponent: Entity, IAwake
    {
        // public Diamond[,] Diamonds;
        // public DiamondStruct[] Diamonds;
        public Diamond[] Diamonds;
        public LevelConfig LevelConfig;
    }
}