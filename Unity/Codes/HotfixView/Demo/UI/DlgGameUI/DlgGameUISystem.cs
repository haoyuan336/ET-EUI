using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	public static  class DlgGameUISystem
	{

		public static void RegisterUIEvent(this DlgGameUI self)
		{
		 
			self.View.E_BackButton.AddListenerAsync(self.ExitGameButtonClick);
		}

		public static async ETTask ExitGameButtonClick(this DlgGameUI self)
		{


			self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_ExitGameAlert).Coroutine();
			
			
			await ETTask.CompletedTask;
		}

		public static void ShowWindow(this DlgGameUI self, Entity contextData = null)
		{
		}

		public static void HideWindow(this DlgGameUI self)
		{
			// self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.);
		}

		 

	}
}
