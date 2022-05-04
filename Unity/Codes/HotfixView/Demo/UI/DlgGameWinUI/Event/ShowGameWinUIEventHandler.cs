using System.Runtime.InteropServices;
using ET.EventType;

namespace ET
{
    public class ShowGameWinUIEventHandler: AEvent<EventType.ShowGameWinUI>
    {
        protected override async ETTask Run(ShowGameWinUI a)
        {
            await a.ZondScene.GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_GameWinUI);
            await ETTask.CompletedTask;
        }
    }
    
}