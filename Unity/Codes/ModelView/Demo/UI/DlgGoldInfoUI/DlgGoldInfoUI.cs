﻿using System;

namespace ET
{
	public  class DlgGoldInfoUI :Entity,IAwake
	{

		public DlgGoldInfoUIViewComponent View { get => this.Parent.GetComponent<DlgGoldInfoUIViewComponent>();}

		public Action DataChangeAction;

	}
}
