using System.Collections.Generic;

namespace ET
{
	public  class DlgWeaponClearLayer :Entity,IAwake
	{

		public DlgWeaponClearLayerViewComponent View { get => this.Parent.GetComponent<DlgWeaponClearLayerViewComponent>();}


		public WeaponInfo WeaponInfo;
		public List<ESCommonWordBar> WordBarItems = new List<ESCommonWordBar>();

		public List<WordBarInfo> ChooseWordBarInfos = new List<WordBarInfo>();
		public List<WordBarInfo> CurrentWordBarInfos = new List<WordBarInfo>();
	}
}
