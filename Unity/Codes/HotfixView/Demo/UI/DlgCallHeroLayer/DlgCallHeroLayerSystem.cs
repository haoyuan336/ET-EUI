using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	public static  class DlgCallHeroLayerSystem
	{

		public static void RegisterUIEvent(this DlgCallHeroLayer self)
		{
		 
			self.View.ELoopScrollListHeroLoopVerticalScrollRect.AddItemRefreshListener((tr , index) => { self.RefershListener(tr, index);});
		}

		public static void RefershListener(this DlgCallHeroLayer self, Transform transform, int index)
		{
		}
		public static void ShowWindow(this DlgCallHeroLayer self, Entity contextData = null)
		{
			// Log.Debug("show call hero layer window");
			// self.AddUIScrollItems(ref  self.ItemHeroCards, 100);
			// self.View.ELoopScrollListHeroLoopVerticalScrollRect.SetVisible(true, 100);
			
		}

		public static void HideWindow(this DlgCallHeroLayer self)
		{
			
		}

		 

	}
}
