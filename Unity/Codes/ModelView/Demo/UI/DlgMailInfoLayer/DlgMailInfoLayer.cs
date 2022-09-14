using System.Collections.Generic;

namespace ET
{
	public  class DlgMailInfoLayer :Entity,IAwake
	{

		public DlgMailInfoLayerViewComponent View { get => this.Parent.GetComponent<DlgMailInfoLayerViewComponent>();}


		public List<HeroCardInfo> AwardHeroCardInfos = new List<HeroCardInfo>();
		public List<WeaponInfo> AwardWeaponInfos = new List<WeaponInfo>();
		public List<ItemInfo> AwardItemInfos = new List<ItemInfo>();

		public Dictionary<int, Scroll_ItemAward> ItemAwards = new Dictionary<int, Scroll_ItemAward>();

		public MailInfo MailInfo;

	}
}
