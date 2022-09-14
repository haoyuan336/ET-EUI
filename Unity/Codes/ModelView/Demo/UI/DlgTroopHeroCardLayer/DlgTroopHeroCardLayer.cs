using System;
using System.Collections.Generic;

namespace ET
{
	public  class DlgTroopHeroCardLayer :Entity,IAwake
	{

		public DlgTroopHeroCardLayerViewComponent View { get => this.Parent.GetComponent<DlgTroopHeroCardLayerViewComponent>();} 

		public Dictionary<int, Scroll_ItemHeroCard> ItemTroopHeroCards = new Dictionary<int, Scroll_ItemHeroCard>();

		public List<HeroCardInfo> TroopHeroCardInfos = new List<HeroCardInfo>();

		public Action<HeroCardInfo, bool> ItemCardClickAction;

	}
}
