using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	public static  class DlgMatchButtonSystem
	{

		public static void RegisterUIEvent(this DlgMatchButton self)
		{
			self.View.E_MatchButtonButton.AddListener(() =>
			{
				self.OnClickEventHandler().Coroutine();
			});
		}

		public static async ETTask OnClickEventHandler(this DlgMatchButton self)
		{
			await ETTask.CompletedTask;
			// Log.Debug("Match Button click");
			MatchHelper.Match(self.ZoneScene());
		}

		public static void ShowWindow(this DlgMatchButton self, Entity contextData = null)
		{
		}

		 

	}
}
