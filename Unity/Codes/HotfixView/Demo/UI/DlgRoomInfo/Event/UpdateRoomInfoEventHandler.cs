using ET.EventType;

namespace ET
{
    public class UpdateRoomInfoEventHandler: AEvent<EventType.UpdateRoomInfo>
    {
        protected override async ETTask Run(UpdateRoomInfo a)
        {
            UIComponent uiComponent = a.zoneScene.GetComponent<UIComponent>();
            // UIBaseWindow uiBaseWindow = uiComponent.AllWindowsDic[(int) WindowID.WindowID_RoomInfo];
            // DlgRoomInfo dlgRoomInfo = uiBaseWindow.GetComponent<DlgRoomInfo>();
            // dlgRoomInfo.UpdateRoomInfo(a);
            await ETTask.CompletedTask;
        }
    }
}