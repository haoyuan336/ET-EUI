using ET.EventType;

namespace ET
{
    public class DlgSettingSetMailEventHandler: AEvent<EventType.SetNewMails>
    {
        protected override async ETTask Run(SetNewMails a)
        {
            Log.Debug("显示新邮件提示");
            Scene scene = a.Scene;
            UIComponent uiComponent = scene.GetComponent<UIComponent>();
            UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_SettingUI);
            if (baseWindow != null)
            {
                baseWindow.GetComponent<DlgSettingUI>().ShowRedDot(true);
            }
            else
            {
                Log.Debug("无basewindow");
            }

            await ETTask.CompletedTask;
        }
    }
    
    

    [AUIEvent(WindowID.WindowID_SettingUI)]
    public class DlgSettingUIEventHandler: IAUIEventHandler
    {
        public void OnInitWindowCoreData(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.WindowData.windowType = UIWindowType.Normal;
        }

        public void OnInitComponent(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.AddComponent<DlgSettingUIViewComponent>();
            uiBaseWindow.AddComponent<DlgSettingUI>();
        }

        public void OnRegisterUIEvent(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.GetComponent<DlgSettingUI>().RegisterUIEvent();
        }

        public void OnShowWindow(UIBaseWindow uiBaseWindow, Entity contextData = null)
        {
            uiBaseWindow.GetComponent<DlgSettingUI>().ShowWindow(contextData);
        }

        public void OnHideWindow(UIBaseWindow uiBaseWindow)
        {
        }

        public void BeforeUnload(UIBaseWindow uiBaseWindow)
        {
        }
    }
}