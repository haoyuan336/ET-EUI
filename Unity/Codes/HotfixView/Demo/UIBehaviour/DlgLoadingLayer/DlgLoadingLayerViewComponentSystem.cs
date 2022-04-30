
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgLoadingLayerViewComponentAwakeSystem : AwakeSystem<DlgLoadingLayerViewComponent> 
	{
		public override void Awake(DlgLoadingLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgLoadingLayerViewComponentDestroySystem : DestroySystem<DlgLoadingLayerViewComponent> 
	{
		public override void Destroy(DlgLoadingLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
