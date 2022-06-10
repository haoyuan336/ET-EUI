using System.Collections.Generic;

namespace ET
{
	public  class DlgWeaponSpecialClearLayer :Entity,IAwake
	{

		public DlgWeaponSpecialClearLayerViewComponent View { get => this.Parent.GetComponent<DlgWeaponSpecialClearLayerViewComponent>();}

		public WeaponInfo WeaponInfo;
		public List<WordBarInfo> WordBarInfos = new List<WordBarInfo>();
		public List<ESCommonWordBar> ESCommonWordBars = new List<ESCommonWordBar>();
		public List<WordBarInfo> ChooseWordBarInfos = new List<WordBarInfo>();
	}
}
