using ET.EventType;

namespace ET
{
    public class UpdateCurrentTurnIndexEventHandler: AEvent<EventType.UpdateCurrentTurnSeatIndex>
    {
        protected override async ETTask Run(UpdateCurrentTurnSeatIndex a)
        {

            Scene scene = a.zoneScene;
            UIComponent uiComponent = scene.GetComponent<UIComponent>();
            UIBaseWindow uiBaseWindow = uiComponent.AllWindowsDic[(int)WindowID.WindowID_RoomInfo];
            uiBaseWindow.GetComponent<DlgRoomInfo>().UpdateCurrentTurnSeatIndex(a.TurnIndex);
            await ETTask.CompletedTask;
        }
    }
}