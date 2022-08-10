using OfficeOpenXml.Table.PivotTable;

namespace ET
{
    public class C2M_PlayerScrollScreenHandler: AMActorLocationHandler<Unit, C2M_PlayerScrollScreen>
    {
        protected override async ETTask Run(Unit entity, C2M_PlayerScrollScreen message)
        {
            //玩家发来了，滑动屏幕的消息
            //首先判断当前的seatindex是否正确 
            switch (entity.DomainScene().SceneType)
            {
                case SceneType.PVEGameScene:
                    Log.Debug("receive player scrollscreen message");
                    RoomComponent roomComponent = entity.DomainScene().GetComponent<RoomComponent>();
                    PVERoom room = roomComponent.GetChild<PVERoom>(message.RoomId);
                    if (room != null)
                    {
                        room.PlayerScrollScreen(message);
                    }

                    break;
                case SceneType.PVPGameScene:
                    Log.Debug("reveive player scroll screen message pvp room");
                    RoomComponent pvpRoomComponent = entity.DomainScene().GetComponent<RoomComponent>();
                    PVPRoom pvpRoom = pvpRoomComponent.GetChild<PVPRoom>(message.RoomId);
                    if (pvpRoom != null)
                    {
                        // pvpRoom.
                        pvpRoom.PlayerScrollScreen(message,entity);
                        
                    }
                    break;
            }

            await ETTask.CompletedTask;
        }
    }
}