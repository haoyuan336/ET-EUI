
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgShowHeroInfoLayerViewComponentAwakeSystem : AwakeSystem<DlgShowHeroInfoLayerViewComponent> 
	{
		public override void Awake(DlgShowHeroInfoLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgShowHeroInfoLayerViewComponentDestroySystem : DestroySystem<DlgShowHeroInfoLayerViewComponent> 
	{
		public override void Destroy(DlgShowHeroInfoLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
