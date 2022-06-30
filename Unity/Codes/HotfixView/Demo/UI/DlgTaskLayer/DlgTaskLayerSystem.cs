using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	public static  class DlgTaskLayerSystem
	{

		public static void RegisterUIEvent(this DlgTaskLayer self)
		{
			self.View.E_BackButton.AddListener(self.OnBackButtonClick);
		 
		}

		public static void OnBackButtonClick(this DlgTaskLayer self)
		{

			UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
			uiComponent.HideWindow(WindowID.WindowID_TaskLayer);
		}

		public static void ShowWindow(this DlgTaskLayer self, Entity contextData = null)
		{
			
			//根据配置表，初始化任务奖励列表
			List<TaskConfig> taskConfigs = TaskConfigCategory.Instance.GetAll().Values.ToList();

			
			
		}

		 

	}
}
