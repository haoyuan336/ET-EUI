namespace ET
{
    [AUIEvent(WindowID.WindowID_AllHeroBagLayer)]
    public class DlgAllHeroBagLayerEventHandler: IAUIEventHandler
    {
        public void OnInitWindowCoreData(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.WindowData.windowType = UIWindowType.Normal;
        }

        public void OnInitComponent(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.AddComponent<DlgAllHeroBagLayerViewComponent>();
            uiBaseWindow.AddComponent<DlgAllHeroBagLayer>();
        }

        public void OnRegisterUIEvent(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.GetComponent<DlgAllHeroBagLayer>().RegisterUIEvent();
        }

        public void OnShowWindow(UIBaseWindow uiBaseWindow, Entity contextData = null)
        {
            uiBaseWindow.GetComponent<DlgAllHeroBagLayer>().ShowWindow(contextData);
        }

        public void OnHideWindow(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.GetComponent<DlgAllHeroBagLayer>().HideWindow();
        }

        public void BeforeUnload(UIBaseWindow uiBaseWindow)
        {
        }
    }
}