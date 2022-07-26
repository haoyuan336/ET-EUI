namespace ET
{
    public class M2C_SyncRoomInfoHandler: AMHandler<M2C_SyncRoomInfo>
    {
        protected override async ETTask Run(Session session, M2C_SyncRoomInfo message)
        {
            Game.EventSystem.Publish(new EventType.UpdateRoomInfo()
            {
                zoneScene = session.DomainScene(),
                RoomId = message.RoomId,
                TurnIndex = message.TurnIndex,
                MySeatIndex = message.MySeatIndex,
                CurrentLevelNum = message.LevelNum
                
            });
            session.DomainScene().GetComponent<PlayerComponent>().SeatCount = message.SeatCount;
            session.DomainScene().GetComponent<PlayerComponent>().RoomId = message.RoomId;
            session.DomainScene().GetComponent<PlayerComponent>().MySeatIndex = message.MySeatIndex;
            session.DomainScene().GetComponent<PlayerComponent>().CurrentTurnIndex = message.TurnIndex;
            session.DomainScene().GetComponent<PlayerComponent>().CurrentLevelNum = message.LevelNum;
            await ETTask.CompletedTask;
        }
    }
}