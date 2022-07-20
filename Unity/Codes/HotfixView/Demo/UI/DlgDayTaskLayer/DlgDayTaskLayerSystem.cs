using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	public static  class DlgDayTaskLayerSystem
	{

		public static void RegisterUIEvent(this DlgDayTaskLayer self)
		{
			//初始化每日任务内容
			//首先取出来任务配置
			List<TaskConfig> taskConfigs = TaskConfigCategory.Instance.GetAll().Values.ToList();
			Log.Debug($"task configs {taskConfigs.Count}");
			self.TaskConfigs = taskConfigs.FindAll(a => a.Type.Equals((int)TaskType.DayTask));
			self.View.E_AwardListLoopVerticalScrollRect.AddItemRefreshListener(self.OnLoopEvent);
			
			//取出来任务配置之后，渲染任务
			Log.Debug($"task config count {self.TaskConfigs.Count}");
			
			self.AddUIScrollItems(ref  self.ItemTaskAwards, self.TaskConfigs.Count);
			self.View.E_AwardListLoopVerticalScrollRect.SetVisible(true, self.TaskConfigs.Count);

		}

		public static void OnLoopEvent(this DlgDayTaskLayer self, Transform transform, int index)
		{
			Scroll_ItemTaskAward itemTaskAward = self.ItemTaskAwards[index].BindTrans(transform);
			
			itemTaskAward.SetConfig(self.TaskConfigs[index]);
		}

		public static void ShowWindow(this DlgDayTaskLayer self, Entity contextData = null)
		{
		}

		 

	}
}
