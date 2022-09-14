using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	public static  class DlgPowerNotEnoughAlertSystem
	{

		public static void RegisterUIEvent(this DlgPowerNotEnoughAlert self)
		{
			self.View.E_OKButton.AddListener(() =>
			{
				self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_PowerNotEnoughAlert);
			});
		}

		public static void ShowWindow(this DlgPowerNotEnoughAlert self, Entity contextData = null)
		{
		}

		 

	}
}
