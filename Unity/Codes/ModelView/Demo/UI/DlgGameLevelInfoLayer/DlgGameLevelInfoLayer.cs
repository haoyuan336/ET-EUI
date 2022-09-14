using System.Collections.Generic;

namespace ET
{
	public  class DlgGameLevelInfoLayer :Entity,IAwake
	{

		public DlgGameLevelInfoLayerViewComponent View { get => this.Parent.GetComponent<DlgGameLevelInfoLayerViewComponent>();}


		public long CurrentChooseTroopId;

		public List<Scroll_ItemHeroCard> ItemHeroCards = new List<Scroll_ItemHeroCard>();
		// public List<HeroCardInfo> TroopHeroCardInfos = new List<HeroCardInfo>();

	}
}
