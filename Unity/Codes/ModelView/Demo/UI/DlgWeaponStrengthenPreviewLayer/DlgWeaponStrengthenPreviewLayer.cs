using System.Collections.Generic;

namespace ET
{
	public  class DlgWeaponStrengthenPreviewLayer :Entity,IAwake
	{

		public DlgWeaponStrengthenPreviewLayerViewComponent View { get => this.Parent.GetComponent<DlgWeaponStrengthenPreviewLayerViewComponent>();}


		public List<Scroll_ItemWeapon> ItemWeapons = new List<Scroll_ItemWeapon>();

		public ESWeaponBagCommon WeaponBagCommon;

		public WeaponInfo CurrentWeaponInfo = null;

		public List<WeaponInfo> AlChooseWeaponInfos = new List<WeaponInfo>();

		public List<ESCommonWordBar> CommonWordBars = new List<ESCommonWordBar>();

	}
}
