using ET.EventType;

namespace ET
{
    public class UpdateRoomInfoEventHandler: AEvent<EventType.UpdateRoomInfo>
    {
        protected override async ETTask Run(UpdateRoomInfo a)
        {
            UIComponent uiComponent = a.zoneScene.GetComponent<UIComponent>();
            UIBaseWindow uiBaseWindow = uiComponent.AllWindowsDic[(int) WindowID.WindowID_GameLevelLayer];
            DlgGameLevelLayer dlgRoomInfo = uiBaseWindow.GetComponent<DlgGameLevelLayer>();
            // dlgRoomInfo.UpdateRoomInfo(a);
            dlgRoomInfo.View.E_LevelText.text = $"第{a.CurrentLevelNum}关";
            await ETTask.CompletedTask;
        }
    }
}