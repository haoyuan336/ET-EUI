using UnityEngine;

namespace ET
{
	public  class DlgShowHeroInfoLayer :Entity,IAwake
	{

		public DlgShowHeroInfoLayerViewComponent View { get => this.Parent.GetComponent<DlgShowHeroInfoLayerViewComponent>();}


		public GameObject HeroModeShow;


	}
}
