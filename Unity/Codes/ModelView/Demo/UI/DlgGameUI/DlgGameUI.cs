using System;
using System.Collections.Generic;

namespace ET
{
	public  class DlgGameUI :Entity,IAwake
	{

		public DlgGameUIViewComponent View { get => this.Parent.GetComponent<DlgGameUIViewComponent>();}


		// public Queue<Scroll_ItemGameCombo> ItemGameCombos = new Queue<Scroll_ItemGameCombo>();
		public Scroll_ItemGameCombo ItemGameCombo;

		public ETCancellationToken CancellationToken = new ETCancellationToken();
		public bool IsShow = false;
		public float CountDownTime = 0;
	}
}
