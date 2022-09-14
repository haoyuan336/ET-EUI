
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgTaskLayerViewComponentAwakeSystem : AwakeSystem<DlgTaskLayerViewComponent> 
	{
		public override void Awake(DlgTaskLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgTaskLayerViewComponentDestroySystem : DestroySystem<DlgTaskLayerViewComponent> 
	{
		public override void Destroy(DlgTaskLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
