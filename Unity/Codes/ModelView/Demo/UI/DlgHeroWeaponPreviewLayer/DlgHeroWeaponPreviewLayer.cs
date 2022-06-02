using System.Collections.Generic;

namespace ET
{
	public  class DlgHeroWeaponPreviewLayer :Entity,IAwake
	{

		public DlgHeroWeaponPreviewLayerViewComponent View { get => this.Parent.GetComponent<DlgHeroWeaponPreviewLayerViewComponent>();}



		public List<ESCommonWeaponItem> WeaponItems = new List<ESCommonWeaponItem>();

		public HeroCardInfo HeroCardInfo;

	}
}
