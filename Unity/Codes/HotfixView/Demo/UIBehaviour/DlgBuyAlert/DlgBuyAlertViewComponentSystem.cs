
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgBuyAlertViewComponentAwakeSystem : AwakeSystem<DlgBuyAlertViewComponent> 
	{
		public override void Awake(DlgBuyAlertViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgBuyAlertViewComponentDestroySystem : DestroySystem<DlgBuyAlertViewComponent> 
	{
		public override void Destroy(DlgBuyAlertViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
