namespace ET
{
    public class PlayerComponent: Entity, IAwake
    {
        public long MyId;
        public long RoomId;
        public int MySeatIndex;
        public int CurrentTurnIndex;
        public int SeatCount;   //作为的数量
    }
}