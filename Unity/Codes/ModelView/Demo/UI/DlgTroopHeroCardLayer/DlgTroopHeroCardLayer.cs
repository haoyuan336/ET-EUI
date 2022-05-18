using System;
using System.Collections.Generic;

namespace ET
{
	public  class DlgTroopHeroCardLayer :Entity,IAwake
	{

		public DlgTroopHeroCardLayerViewComponent View { get => this.Parent.GetComponent<DlgTroopHeroCardLayerViewComponent>();} 

		public Dictionary<int, Scroll_ItemHeroCard> ItemTroopHeroCards = new Dictionary<int, Scroll_ItemHeroCard>();
		// public Dictionary<int, Scroll_ItemHeroCard> ItemHeroCards = new Dictionary<int, Scroll_ItemHeroCard>();

		// public List<TroopInfo> TroopInfos = new List<TroopInfo>();

		// public List<HeroCardInfo> HeroCardInfos = new List<HeroCardInfo>();

		public List<HeroCardInfo> TroopHeroCardInfos = new List<HeroCardInfo>();

		// public long CurrentChooseTroopId;   //当前选择的队伍id

		public Action<HeroCardInfo, bool> ItemCardClickAction;

	}
}
