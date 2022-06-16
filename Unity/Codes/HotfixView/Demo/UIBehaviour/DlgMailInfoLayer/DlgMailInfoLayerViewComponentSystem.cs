
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgMailInfoLayerViewComponentAwakeSystem : AwakeSystem<DlgMailInfoLayerViewComponent> 
	{
		public override void Awake(DlgMailInfoLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgMailInfoLayerViewComponentDestroySystem : DestroySystem<DlgMailInfoLayerViewComponent> 
	{
		public override void Destroy(DlgMailInfoLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
