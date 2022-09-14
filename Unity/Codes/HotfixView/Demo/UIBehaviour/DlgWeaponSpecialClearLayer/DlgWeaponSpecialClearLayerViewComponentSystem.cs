
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgWeaponSpecialClearLayerViewComponentAwakeSystem : AwakeSystem<DlgWeaponSpecialClearLayerViewComponent> 
	{
		public override void Awake(DlgWeaponSpecialClearLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgWeaponSpecialClearLayerViewComponentDestroySystem : DestroySystem<DlgWeaponSpecialClearLayerViewComponent> 
	{
		public override void Destroy(DlgWeaponSpecialClearLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
