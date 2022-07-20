
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgDayTaskLayerViewComponentAwakeSystem : AwakeSystem<DlgDayTaskLayerViewComponent> 
	{
		public override void Awake(DlgDayTaskLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgDayTaskLayerViewComponentDestroySystem : DestroySystem<DlgDayTaskLayerViewComponent> 
	{
		public override void Destroy(DlgDayTaskLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
