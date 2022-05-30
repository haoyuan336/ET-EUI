
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgUpdateHeroRankLayerViewComponentAwakeSystem : AwakeSystem<DlgUpdateHeroRankLayerViewComponent> 
	{
		public override void Awake(DlgUpdateHeroRankLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgUpdateHeroRankLayerViewComponentDestroySystem : DestroySystem<DlgUpdateHeroRankLayerViewComponent> 
	{
		public override void Destroy(DlgUpdateHeroRankLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
