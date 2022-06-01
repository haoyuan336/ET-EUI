using System.Collections.Generic;

namespace ET
{
	public  class DlgHeroStrengthenPreviewLayer :Entity,IAwake
	{

		public DlgHeroStrengthenPreviewLayerViewComponent View { get => this.Parent.GetComponent<DlgHeroStrengthenPreviewLayerViewComponent>();}


		public HeroCardInfo HeroCardInfo;
		public ESCommonHeroCard CurrentCommonHeroCard;
		public List<ESCommonHeroCard> ESCommonHeroCards = new List<ESCommonHeroCard>();
		public List<HeroCardInfo> AlChooseHeroCardInfo = new List<HeroCardInfo>();
	}
}
