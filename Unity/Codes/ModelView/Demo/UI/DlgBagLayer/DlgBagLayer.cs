using System.Collections.Generic;

namespace ET
{
	public  class DlgBagLayer :Entity,IAwake
	{

		public DlgBagLayerViewComponent View { get => this.Parent.GetComponent<DlgBagLayerViewComponent>();}

		public Dictionary<int, Scroll_ItemHeroCard> ItemHeroCards = new Dictionary<int, Scroll_ItemHeroCard>();

		public List<HeroCardInfo> HeroCardInfos = new List<HeroCardInfo>();

	}
}
