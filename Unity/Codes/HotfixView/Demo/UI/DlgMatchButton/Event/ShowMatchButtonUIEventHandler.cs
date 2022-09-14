using ET.EventType;

namespace ET
{
    public class ShowMatchButtonUIEventHandler: AEvent<EventType.ShowMatchButtonUIMessage>
    {
        protected override async ETTask Run(ShowMatchButtonUIMessage a)
        {
            Scene zoneScene = a.zoneScene;
            await zoneScene.GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_MatchButton);
            await ETTask.CompletedTask;
        }
    }
}