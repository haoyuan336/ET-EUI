using OfficeOpenXml.Table.PivotTable;

namespace ET
{
    public class C2M_PlayerScrollScreenHandler: AMActorLocationHandler<Unit, C2M_PlayerScrollScreen>
    {
        protected override async ETTask Run(Unit entity, C2M_PlayerScrollScreen message)
        {
            //玩家发来了，滑动屏幕的消息
            //首先判断当前的seatindex是否正确 
            RoomComponent roomComponent = entity.DomainScene().GetComponent<RoomComponent>();
            Room room = roomComponent.GetChild<Room>(message.RoomId);

            if (room != null)
            {
                // Log.Debug("room is have");
                room.PlayerScrollScreen(message);
            }
            else
            {
                Log.Error("room is null");
            }

            await ETTask.CompletedTask;
        }
    }
}