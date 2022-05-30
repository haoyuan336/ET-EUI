
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgUpdateHeroStarLayerViewComponentAwakeSystem : AwakeSystem<DlgUpdateHeroStarLayerViewComponent> 
	{
		public override void Awake(DlgUpdateHeroStarLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgUpdateHeroStarLayerViewComponentDestroySystem : DestroySystem<DlgUpdateHeroStarLayerViewComponent> 
	{
		public override void Destroy(DlgUpdateHeroStarLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
