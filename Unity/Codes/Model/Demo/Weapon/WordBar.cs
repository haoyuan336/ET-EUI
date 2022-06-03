namespace ET
{
    public class WordBar: Entity
    {
        public long OwnerId = 0;
        public long ConfigId = 0;
        public int Value = 0;
        public bool IsMain = false; //是否是主词条
        public int State = 1;   //词条状态
    }
}