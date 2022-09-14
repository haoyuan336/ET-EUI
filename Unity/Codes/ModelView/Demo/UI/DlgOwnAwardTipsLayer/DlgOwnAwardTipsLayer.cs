using System.Collections.Generic;

namespace ET
{
	public  class DlgOwnAwardTipsLayer :Entity,IAwake
	{

		public DlgOwnAwardTipsLayerViewComponent View { get => this.Parent.GetComponent<DlgOwnAwardTipsLayerViewComponent>();}

		public List<Scroll_ItemOwnAward> ItemOwnAwards = new List<Scroll_ItemOwnAward>();

	}
}
