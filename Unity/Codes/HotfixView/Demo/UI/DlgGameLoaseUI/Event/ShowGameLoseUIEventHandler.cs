using ET.EventType;
using UnityEngine;

namespace ET
{
    public class ShowGameLoseUIEventHandler: AEvent<EventType.ShowGameLoaseUI>
    {
        protected override async ETTask Run(ShowGameLoaseUI a)
        {
            await a.ZoneScene.GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_GameLoaseUI);
            await ETTask.CompletedTask;
        }
    }
}