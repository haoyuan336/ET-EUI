
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgChooseHeadImageLayerViewComponentAwakeSystem : AwakeSystem<DlgChooseHeadImageLayerViewComponent> 
	{
		public override void Awake(DlgChooseHeadImageLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgChooseHeadImageLayerViewComponentDestroySystem : DestroySystem<DlgChooseHeadImageLayerViewComponent> 
	{
		public override void Destroy(DlgChooseHeadImageLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
