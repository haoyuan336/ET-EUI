using System;
using System.Runtime.InteropServices;
using ET.EventType;

namespace ET
{
    public class ShowGameWinUIEventHandler: AEvent<EventType.ShowGameWinUI>
    {
        protected override async ETTask Run(ShowGameWinUI a)
        {
            var currentLevelNum = a.ZondScene.GetComponent<PlayerComponent>().CurrentLevelNum;
            Log.Debug($"current level num {currentLevelNum}");

            //首先展示剧情

            await a.ZondScene.GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_GameLevelStoryLayer);


            UIBaseWindow baseWindow = a.ZondScene.GetComponent<UIComponent>().GetUIBaseWindow(WindowID.WindowID_GameLevelStoryLayer);
            var gameLevelStoryLayer = baseWindow.GetComponent<DlgGameLevelStoryLayer>();


            await gameLevelStoryLayer.ShowContentAsync(currentLevelNum);
            
            await a.ZondScene.GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_GameWinUI);
            await ETTask.CompletedTask;
        }
    }
}