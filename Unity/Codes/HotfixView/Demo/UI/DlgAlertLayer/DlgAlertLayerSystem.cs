using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	public static  class DlgAlertLayerSystem
	{

		public static void RegisterUIEvent(this DlgAlertLayer self)
		{
		 
			self.View.E_OkButtonButton.AddListener(() =>
			{
				UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
				uiComponent.HideWindow(WindowID.WindowID_AlertLayer);
			});
		}

		public static void ShowWindow(this DlgAlertLayer self, Entity contextData = null)
		{
		}

		public static void SetText(this DlgAlertLayer self, string text)
		{
			self.View.E_InfoText.text = text;
		}

		 

	}
}
