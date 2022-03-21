
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgBagLayerViewComponentAwakeSystem : AwakeSystem<DlgBagLayerViewComponent> 
	{
		public override void Awake(DlgBagLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgBagLayerViewComponentDestroySystem : DestroySystem<DlgBagLayerViewComponent> 
	{
		public override void Destroy(DlgBagLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
