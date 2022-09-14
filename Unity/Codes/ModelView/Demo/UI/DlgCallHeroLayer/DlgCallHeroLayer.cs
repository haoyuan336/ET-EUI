using System.Collections.Generic;

namespace ET
{
	public  class DlgCallHeroLayer :Entity,IAwake
	{

		public DlgCallHeroLayerViewComponent View { get => this.Parent.GetComponent<DlgCallHeroLayerViewComponent>();}

		public Dictionary<int, Scroll_ItemHeroCard> ItemHeroCards = new Dictionary<int, Scroll_ItemHeroCard>();

		public List<HeroCardInfo> HeroCardInfos = new List<HeroCardInfo>();

	}
}
