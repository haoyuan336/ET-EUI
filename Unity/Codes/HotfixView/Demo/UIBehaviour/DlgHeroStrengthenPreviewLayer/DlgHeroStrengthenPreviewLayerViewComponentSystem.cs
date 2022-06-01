
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgHeroStrengthenPreviewLayerViewComponentAwakeSystem : AwakeSystem<DlgHeroStrengthenPreviewLayerViewComponent> 
	{
		public override void Awake(DlgHeroStrengthenPreviewLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgHeroStrengthenPreviewLayerViewComponentDestroySystem : DestroySystem<DlgHeroStrengthenPreviewLayerViewComponent> 
	{
		public override void Destroy(DlgHeroStrengthenPreviewLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
