
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgWeaponStrengthenPreviewLayerViewComponentAwakeSystem : AwakeSystem<DlgWeaponStrengthenPreviewLayerViewComponent> 
	{
		public override void Awake(DlgWeaponStrengthenPreviewLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgWeaponStrengthenPreviewLayerViewComponentDestroySystem : DestroySystem<DlgWeaponStrengthenPreviewLayerViewComponent> 
	{
		public override void Destroy(DlgWeaponStrengthenPreviewLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
