
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgWeaponClearLayerViewComponentAwakeSystem : AwakeSystem<DlgWeaponClearLayerViewComponent> 
	{
		public override void Awake(DlgWeaponClearLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgWeaponClearLayerViewComponentDestroySystem : DestroySystem<DlgWeaponClearLayerViewComponent> 
	{
		public override void Destroy(DlgWeaponClearLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
