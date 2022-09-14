
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgChooseWeaponLayerViewComponentAwakeSystem : AwakeSystem<DlgChooseWeaponLayerViewComponent> 
	{
		public override void Awake(DlgChooseWeaponLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgChooseWeaponLayerViewComponentDestroySystem : DestroySystem<DlgChooseWeaponLayerViewComponent> 
	{
		public override void Destroy(DlgChooseWeaponLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
