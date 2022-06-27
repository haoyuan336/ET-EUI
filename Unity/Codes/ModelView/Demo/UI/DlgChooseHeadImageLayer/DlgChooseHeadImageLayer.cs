using System;
using System.Collections.Generic;

namespace ET
{
	public  class DlgChooseHeadImageLayer :Entity,IAwake
	{

		public DlgChooseHeadImageLayerViewComponent View { get => this.Parent.GetComponent<DlgChooseHeadImageLayerViewComponent>();}


		public Dictionary<int, Scroll_ItemChooseHead> ItemChooseHeads = new Dictionary<int, Scroll_ItemChooseHead>();

		// public Dictionary<int, Item>

		public List<PlayerHeadImageResConfig> ImageResConfigs = new List<PlayerHeadImageResConfig>();

		public AccountInfo CurrentAccountInfo;

		public Action<HeadImageType,int> ChooseHeadCallBackAction;

	}
}
