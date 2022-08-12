using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	public static  class DlgPVPFightPrepareLayerSystem
	{

		public static void RegisterUIEvent(this DlgPVPFightPrepareLayer self)
		{
			// self.View.E_BackButtonButton
			self.View.E_BackButton.AddListener(() =>
			{
				self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_PVPFightPrepareLayer);
				// Log.Debug("back button");
			},ConstValue.BackButtonAudioStr);
			
			self.View.E_MatchPlayerButton.AddListener(self.OnMatchButtonClick);
			// self.View.e_
		}

		public static async void OnMatchButtonClick(this DlgPVPFightPrepareLayer self)
		{
			await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_PVPMatchFightLayer);
			await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_GoldInfoUI);
		}

		public static void ShowWindow(this DlgPVPFightPrepareLayer self, Entity contextData = null)
		{
		}

		public static void HideWindow(this  DlgPVPFightPrepareLayer self)
		{
			UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
			uiComponent.HideWindow(WindowID.WindowID_EditorTroopLayer);
		}

		 

	}
}
