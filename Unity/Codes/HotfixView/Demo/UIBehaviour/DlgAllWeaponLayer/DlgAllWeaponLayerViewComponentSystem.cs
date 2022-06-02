
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgAllWeaponLayerViewComponentAwakeSystem : AwakeSystem<DlgAllWeaponLayerViewComponent> 
	{
		public override void Awake(DlgAllWeaponLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgAllWeaponLayerViewComponentDestroySystem : DestroySystem<DlgAllWeaponLayerViewComponent> 
	{
		public override void Destroy(DlgAllWeaponLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
