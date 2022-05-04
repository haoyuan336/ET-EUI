using System.Collections;
using System.Collections.Generic;
using System;
using ET.Account;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	public static  class DlgGameLoaseUISystem
	{

		public static void RegisterUIEvent(this DlgGameLoaseUI self)
		{
		 
			self.View.E_MainMenuButton.AddListenerAsync(self.MainMenuButtonClick);
		}

		public static async ETTask MainMenuButtonClick(this DlgGameLoaseUI self)
		{
			Session session = self.DomainScene().GetComponent<SessionComponent>().Session;
			long AccountId = self.DomainScene().GetComponent<AccountInfoComponent>().AccountId;
			long RoomId = self.DomainScene().GetComponent<PlayerComponent>().RoomId;
			M2C_BackGameToMainMenuResponse response =
					(M2C_BackGameToMainMenuResponse) await session.Call(new C2M_BackGameToMainMenuRequest() { Account = AccountId, RoomId = RoomId});

			if (response.Error == ErrorCode.ERR_Success)
			{
				Log.Debug(" back game success");
				self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_GameLoaseUI);
				self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_GameUI);
			}

			await ETTask.CompletedTask;
		}

		public static void ShowWindow(this DlgGameLoaseUI self, Entity contextData = null)
		{
		}

		 

	}
}
