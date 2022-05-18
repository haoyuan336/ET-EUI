
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgTroopHeroCardLayerViewComponentAwakeSystem : AwakeSystem<DlgTroopHeroCardLayerViewComponent> 
	{
		public override void Awake(DlgTroopHeroCardLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgTroopHeroCardLayerViewComponentDestroySystem : DestroySystem<DlgTroopHeroCardLayerViewComponent> 
	{
		public override void Destroy(DlgTroopHeroCardLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
