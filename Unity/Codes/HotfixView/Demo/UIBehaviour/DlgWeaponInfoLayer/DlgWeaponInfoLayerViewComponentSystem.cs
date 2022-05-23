
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgWeaponInfoLayerViewComponentAwakeSystem : AwakeSystem<DlgWeaponInfoLayerViewComponent> 
	{
		public override void Awake(DlgWeaponInfoLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgWeaponInfoLayerViewComponentDestroySystem : DestroySystem<DlgWeaponInfoLayerViewComponent> 
	{
		public override void Destroy(DlgWeaponInfoLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
