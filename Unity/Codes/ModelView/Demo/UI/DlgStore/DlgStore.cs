using System.Collections.Generic;

namespace ET
{
	public  class DlgStore :Entity,IAwake
	{

		public DlgStoreViewComponent View { get => this.Parent.GetComponent<DlgStoreViewComponent>();}


		public Dictionary<int, Scroll_ItemGoods> ItemGoods = new Dictionary<int, Scroll_ItemGoods>();
		// public List<WeaponsConfig> WeaponConfigs = new List<WeaponsConfig>();
		public List<GoodsConfig> GoodsConfigs = new List<GoodsConfig>();

	}
}
