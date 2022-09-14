
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgPowerNotEnoughAlertViewComponentAwakeSystem : AwakeSystem<DlgPowerNotEnoughAlertViewComponent> 
	{
		public override void Awake(DlgPowerNotEnoughAlertViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgPowerNotEnoughAlertViewComponentDestroySystem : DestroySystem<DlgPowerNotEnoughAlertViewComponent> 
	{
		public override void Destroy(DlgPowerNotEnoughAlertViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
