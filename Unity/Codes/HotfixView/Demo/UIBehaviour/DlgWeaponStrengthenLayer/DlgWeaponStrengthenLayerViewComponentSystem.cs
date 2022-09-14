
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgWeaponStrengthenLayerViewComponentAwakeSystem : AwakeSystem<DlgWeaponStrengthenLayerViewComponent> 
	{
		public override void Awake(DlgWeaponStrengthenLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgWeaponStrengthenLayerViewComponentDestroySystem : DestroySystem<DlgWeaponStrengthenLayerViewComponent> 
	{
		public override void Destroy(DlgWeaponStrengthenLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
