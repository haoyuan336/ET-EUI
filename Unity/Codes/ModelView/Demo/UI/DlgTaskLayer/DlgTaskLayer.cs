using System.Collections.Generic;

namespace ET
{
	public  class DlgTaskLayer :Entity,IAwake
	{

		public DlgTaskLayerViewComponent View { get => this.Parent.GetComponent<DlgTaskLayerViewComponent>();}

		public Dictionary<int, Scroll_ItemTaskAward> ItemsTaskAwards = new Dictionary<int, Scroll_ItemTaskAward>();

		public List<TaskConfig> TaskConfigs = new List<TaskConfig>();

		// public Dictionary<>
	}
}
