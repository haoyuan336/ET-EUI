
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgAlertLayerViewComponentAwakeSystem : AwakeSystem<DlgAlertLayerViewComponent> 
	{
		public override void Awake(DlgAlertLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgAlertLayerViewComponentDestroySystem : DestroySystem<DlgAlertLayerViewComponent> 
	{
		public override void Destroy(DlgAlertLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
