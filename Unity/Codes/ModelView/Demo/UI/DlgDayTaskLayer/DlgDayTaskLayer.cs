using System.Collections.Generic;

namespace ET
{
	public  class DlgDayTaskLayer :Entity,IAwake
	{

		public DlgDayTaskLayerViewComponent View { get => this.Parent.GetComponent<DlgDayTaskLayerViewComponent>();}

		public Dictionary<int, Scroll_ItemTaskAward> ItemTaskAwards = new Dictionary<int, Scroll_ItemTaskAward>();
		public List<TaskConfig> TaskConfigs = new List<TaskConfig>();

	}
}
