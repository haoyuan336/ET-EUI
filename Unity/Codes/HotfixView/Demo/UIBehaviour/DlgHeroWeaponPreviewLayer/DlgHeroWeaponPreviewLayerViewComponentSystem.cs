
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgHeroWeaponPreviewLayerViewComponentAwakeSystem : AwakeSystem<DlgHeroWeaponPreviewLayerViewComponent> 
	{
		public override void Awake(DlgHeroWeaponPreviewLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgHeroWeaponPreviewLayerViewComponentDestroySystem : DestroySystem<DlgHeroWeaponPreviewLayerViewComponent> 
	{
		public override void Destroy(DlgHeroWeaponPreviewLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
