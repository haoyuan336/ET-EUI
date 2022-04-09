using System.Collections.Generic;

namespace ET
{
	public  class DlgHeroInfoLayerUI :Entity,IAwake
	{

		public DlgHeroInfoLayerUIViewComponent View { get => this.Parent.GetComponent<DlgHeroInfoLayerUIViewComponent>();}
		public List<HeroCardInfo> HeroCardInfos = new List<HeroCardInfo>();
		public Dictionary<int, Scroll_ItemHeroCard> ItemHeroCards = new Dictionary<int, Scroll_ItemHeroCard>();

	}
}
