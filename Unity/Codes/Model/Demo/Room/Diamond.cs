namespace ET
{
    public class Diamond: Entity, IAwake, IAwake<DiamondInfo>, IAwake<int, int>, IAwake<DiamondInfo, ETTask>
    {
        public int HangIndex = 0;
        public int LieIndex = 0;
        public int DiamondType = 0;
        public int ConfigId = 0;
        public int InitLieIndex = 0;    //初始的index
        public int InitHangIndex = 0;   //初始的行index
        public int BoomType = 0;    //炸弹类型
    }
}