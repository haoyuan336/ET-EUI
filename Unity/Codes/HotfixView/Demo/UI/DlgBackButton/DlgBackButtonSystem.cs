using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	public static  class DlgBackButtonSystem
	{

		public static void RegisterUIEvent(this DlgBackButton self)
		{
			self.View.E_BackButton.AddListener(() =>
			{
				if (self.BackButtonClickAction != null)
				{
					self.BackButtonClickAction();
				}
			}, ConstValue.BackButtonAudioStr); 
		}

		public static void ShowWindow(this DlgBackButton self, Entity contextData = null)
		{
			
		}

		public static void HideWindow(this DlgBackButton self)
		{
			// self.BackButtonClickAction = null;
		}
		

		 

	}
}
