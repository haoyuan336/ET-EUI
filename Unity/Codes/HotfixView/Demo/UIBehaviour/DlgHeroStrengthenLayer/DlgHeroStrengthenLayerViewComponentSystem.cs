
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgHeroStrengthenLayerViewComponentAwakeSystem : AwakeSystem<DlgHeroStrengthenLayerViewComponent> 
	{
		public override void Awake(DlgHeroStrengthenLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgHeroStrengthenLayerViewComponentDestroySystem : DestroySystem<DlgHeroStrengthenLayerViewComponent> 
	{
		public override void Destroy(DlgHeroStrengthenLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
