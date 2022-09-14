using System;

namespace ET
{
	public  class DlgChooseWeaponLayer :Entity,IAwake
	{

		public DlgChooseWeaponLayerViewComponent View { get => this.Parent.GetComponent<DlgChooseWeaponLayerViewComponent>();} 

		 

		
		public Action<WeaponInfo> OnWeaponItemClickAction;

		public WeaponInfo AlChooseWeaponInfo;
	}
}
