
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgExitGameAlertViewComponentAwakeSystem : AwakeSystem<DlgExitGameAlertViewComponent> 
	{
		public override void Awake(DlgExitGameAlertViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgExitGameAlertViewComponentDestroySystem : DestroySystem<DlgExitGameAlertViewComponent> 
	{
		public override void Destroy(DlgExitGameAlertViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
