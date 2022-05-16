using System;
using System.Collections.Generic;

namespace ET
{
	public  class DlgAllHeroBagLayer :Entity,IAwake
	{

		public DlgAllHeroBagLayerViewComponent View { get => this.Parent.GetComponent<DlgAllHeroBagLayerViewComponent>();} 

		 
		public List<HeroCardInfo> HeroCardInfos = new List<HeroCardInfo>();
		public Dictionary<int, Scroll_ItemHeroCard> ItemHeroCards = new Dictionary<int, Scroll_ItemHeroCard>();
		public int CurrentChooseTypeIndex = 5;

		// public Action<HeroCardInfo> OnHeroItemClick;

		public HeroCardInfo UnAbleHeroCardInfo = null;
		public Action<HeroCardInfo, Scroll_ItemHeroCard, bool> OnHeroItemInfoClick;
	}
}
