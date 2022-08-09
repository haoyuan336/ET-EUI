namespace ET
{
    public class M2C_MatchPVPRoomSuccessHandler: AMHandler<M2C_MatchPVPSuccess>
    {
        protected override async ETTask Run(Session session, M2C_MatchPVPSuccess message)
        {
            long roomId = message.RoomId;
            Log.Debug("匹配成功");
            Game.EventSystem.Publish(new EventType.ShowMatchPVPRoomSuccessAnim()
            {
                Scene = session.DomainScene(),
                RoomId = roomId
            });
            
            await ETTask.CompletedTask;
        }
    }
}