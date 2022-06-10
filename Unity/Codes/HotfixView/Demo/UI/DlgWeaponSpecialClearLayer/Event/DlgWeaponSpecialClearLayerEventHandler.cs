namespace ET
{
	[AUIEvent(WindowID.WindowID_WeaponSpecialClearLayer)]
	public  class DlgWeaponSpecialClearLayerEventHandler : IAUIEventHandler
	{

		public void OnInitWindowCoreData(UIBaseWindow uiBaseWindow)
		{
		  uiBaseWindow.WindowData.windowType = UIWindowType.Normal; 
		}

		public void OnInitComponent(UIBaseWindow uiBaseWindow)
		{
		  uiBaseWindow.AddComponent<DlgWeaponSpecialClearLayerViewComponent>(); 
		  uiBaseWindow.AddComponent<DlgWeaponSpecialClearLayer>(); 
		}

		public void OnRegisterUIEvent(UIBaseWindow uiBaseWindow)
		{
		  uiBaseWindow.GetComponent<DlgWeaponSpecialClearLayer>().RegisterUIEvent(); 
		}

		public void OnShowWindow(UIBaseWindow uiBaseWindow, Entity contextData = null)
		{
		  uiBaseWindow.GetComponent<DlgWeaponSpecialClearLayer>().ShowWindow(contextData); 
		}

		public void OnHideWindow(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.GetComponent<DlgWeaponSpecialClearLayer>().HideWindow();
			
		}

		public void BeforeUnload(UIBaseWindow uiBaseWindow)
		{
		}

	}
}
