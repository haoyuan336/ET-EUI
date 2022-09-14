using System.Collections.Generic;

namespace ET
{
	public  class DlgHeroStrengthenLayer :Entity,IAwake
	{

		public DlgHeroStrengthenLayerViewComponent View { get => this.Parent.GetComponent<DlgHeroStrengthenLayerViewComponent>();}

		public Dictionary<int, Scroll_ItemHeroCard> ItemHeroCards = new Dictionary<int, Scroll_ItemHeroCard>();

		public HeroCardInfo HeroCardInfo;

		public List<HeroCardInfo> AlChooseHeroCardInfo = new List<HeroCardInfo>();

	}
}
