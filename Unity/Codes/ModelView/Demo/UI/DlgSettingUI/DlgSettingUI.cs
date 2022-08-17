using System.Collections.Generic;
using UnityEngine;

namespace ET
{
	public  class DlgSettingUI :Entity,IAwake
	{

		public DlgSettingUIViewComponent View { get => this.Parent.GetComponent<DlgSettingUIViewComponent>();} 

		public Dictionary<long, ChatInfo> ChatInfosMap = new Dictionary<long, ChatInfo>();

		public GameObject HeroMode;

	}
}
