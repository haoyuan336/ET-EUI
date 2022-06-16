
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgMailLayerViewComponentAwakeSystem : AwakeSystem<DlgMailLayerViewComponent> 
	{
		public override void Awake(DlgMailLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgMailLayerViewComponentDestroySystem : DestroySystem<DlgMailLayerViewComponent> 
	{
		public override void Destroy(DlgMailLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
