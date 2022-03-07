namespace ET
{
    public class M2C_SyncCreateRoomMessageHandler: AMHandler<M2C_SyncCreateRoomMessage>
    {
        protected override async ETTask Run(Session session, M2C_SyncCreateRoomMessage message)
        {
            // Log.Error("收到了创建犯贱的消息");
            //发送消息到view层
            Game.EventSystem.Publish(new EventType.SyncCreateRoomMessage() { InRoomIndex = message.InRoomIndex, zoneScene = session.DomainScene()});
            await ETTask.CompletedTask;
        }
    }
}