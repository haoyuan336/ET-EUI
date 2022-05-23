using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	public static  class DlgWeaponInfoLayerSystem
	{

		public static void RegisterUIEvent(this DlgWeaponInfoLayer self)
		{
		 
			self.View.E_BackButton.AddListener(() =>
			{
				self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_WeaponInfoLayer);
			});
		}

		public static void ShowWindow(this DlgWeaponInfoLayer self, Entity contextData = null)
		{
		}

		 

	}
}
