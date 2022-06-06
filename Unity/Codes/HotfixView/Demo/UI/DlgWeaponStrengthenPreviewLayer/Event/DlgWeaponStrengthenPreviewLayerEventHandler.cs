namespace ET
{
	[AUIEvent(WindowID.WindowID_WeaponStrengthenPreviewLayer)]
	public  class DlgWeaponStrengthenPreviewLayerEventHandler : IAUIEventHandler
	{

		public void OnInitWindowCoreData(UIBaseWindow uiBaseWindow)
		{
		  uiBaseWindow.WindowData.windowType = UIWindowType.Normal; 
		}

		public void OnInitComponent(UIBaseWindow uiBaseWindow)
		{
		  uiBaseWindow.AddComponent<DlgWeaponStrengthenPreviewLayerViewComponent>(); 
		  uiBaseWindow.AddComponent<DlgWeaponStrengthenPreviewLayer>(); 
		}

		public void OnRegisterUIEvent(UIBaseWindow uiBaseWindow)
		{
		  uiBaseWindow.GetComponent<DlgWeaponStrengthenPreviewLayer>().RegisterUIEvent(); 
		}

		public void OnShowWindow(UIBaseWindow uiBaseWindow, Entity contextData = null)
		{
		  uiBaseWindow.GetComponent<DlgWeaponStrengthenPreviewLayer>().ShowWindow(contextData); 
		}

		public void OnHideWindow(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.GetComponent<DlgWeaponStrengthenPreviewLayer>().HideWindow();
		}

		public void BeforeUnload(UIBaseWindow uiBaseWindow)
		{
		}

	}
}
