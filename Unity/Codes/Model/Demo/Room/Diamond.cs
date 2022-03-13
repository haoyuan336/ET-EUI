namespace ET
{
    public class Diamond: Entity, IAwake
    {
        public int HangIndex = 0;
        public int LieIndex = 0;
        public int DiamondType = 0;

        public int InitLieIndex = 0;    //初始的index
        public int InitHangIndex = 0;   //初始的行index
    }
}