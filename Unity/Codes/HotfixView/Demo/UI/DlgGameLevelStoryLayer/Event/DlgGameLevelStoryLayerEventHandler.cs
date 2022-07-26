namespace ET
{
    public class SyncLevelInfoEvent: AEvent<EventType.UpdateRoomInfo>
    {
        protected override async ETTask Run(EventType.UpdateRoomInfo a)
        {
            Log.Debug($"同步房间信息{a}");

            var levelNum = a.CurrentLevelNum;

            LevelConfig levelConfig = LevelConfigCategory.Instance.Get(levelNum);

            //战前剧情

            var story = levelConfig.LevelStoryBegin;

            Log.Debug($"story{story}");

            if (!string.IsNullOrEmpty(story))
            {
                //显示UI层
                UIComponent uiComponent = a.zoneScene.GetComponent<UIComponent>();
                await uiComponent.ShowWindow(WindowID.WindowID_GameLevelStoryLayer);
                UIBaseWindow uiBaseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_GameLevelStoryLayer);
                await uiBaseWindow.GetComponent<DlgGameLevelStoryLayer>().SetContent(story);
                Log.Debug("剧情流程结束");
            }

            await ETTask.CompletedTask;
        }
    }

    [AUIEvent(WindowID.WindowID_GameLevelStoryLayer)]
    public class DlgGameLevelStoryLayerEventHandler: IAUIEventHandler
    {
        public void OnInitWindowCoreData(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.WindowData.windowType = UIWindowType.Normal;
        }

        public void OnInitComponent(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.AddComponent<DlgGameLevelStoryLayerViewComponent>();
            uiBaseWindow.AddComponent<DlgGameLevelStoryLayer>();
        }

        public void OnRegisterUIEvent(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.GetComponent<DlgGameLevelStoryLayer>().RegisterUIEvent();
        }

        public void OnShowWindow(UIBaseWindow uiBaseWindow, Entity contextData = null)
        {
            uiBaseWindow.GetComponent<DlgGameLevelStoryLayer>().ShowWindow(contextData);
        }

        public void OnHideWindow(UIBaseWindow uiBaseWindow)
        {
        }

        public void BeforeUnload(UIBaseWindow uiBaseWindow)
        {
        }
    }
}