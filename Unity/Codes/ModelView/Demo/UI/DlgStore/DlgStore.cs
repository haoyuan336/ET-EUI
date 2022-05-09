using System.Collections.Generic;

namespace ET
{
	public  class DlgStore :Entity,IAwake
	{

		public DlgStoreViewComponent View { get => this.Parent.GetComponent<DlgStoreViewComponent>();}


		public Dictionary<int, Scroll_ItemWeapon> ItemWeapons = new Dictionary<int, Scroll_ItemWeapon>();
		public List<WeaponConfig> WeaponConfigs = new List<WeaponConfig>();

	}
}
