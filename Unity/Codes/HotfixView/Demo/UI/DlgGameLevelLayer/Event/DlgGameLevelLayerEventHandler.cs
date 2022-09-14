using ET.EventType;

namespace ET
{

	// public class HideGaneLevelEVent: AEvent<EventType.ExitGameScene>
	// {
	// 	protected override async ETTask Run(ExitGameScene a)
	// 	{
	// 		
	// 		await ETTask.CompletedTask;
	// 	}
	// }
	[AUIEvent(WindowID.WindowID_GameLevelLayer)]
	public  class DlgGameLevelLayerEventHandler : IAUIEventHandler
	{

		public void OnInitWindowCoreData(UIBaseWindow uiBaseWindow)
		{
		  uiBaseWindow.WindowData.windowType = UIWindowType.Normal; 
		}

		public void OnInitComponent(UIBaseWindow uiBaseWindow)
		{
		  uiBaseWindow.AddComponent<DlgGameLevelLayerViewComponent>(); 
		  uiBaseWindow.AddComponent<DlgGameLevelLayer>(); 
		}

		public void OnRegisterUIEvent(UIBaseWindow uiBaseWindow)
		{
		  uiBaseWindow.GetComponent<DlgGameLevelLayer>().RegisterUIEvent(); 
		}

		public void OnShowWindow(UIBaseWindow uiBaseWindow, Entity contextData = null)
		{
		  uiBaseWindow.GetComponent<DlgGameLevelLayer>().ShowWindow(contextData); 
		}

		public void OnHideWindow(UIBaseWindow uiBaseWindow)
		{
			
		}

		public void BeforeUnload(UIBaseWindow uiBaseWindow)
		{
		}

	}
}
