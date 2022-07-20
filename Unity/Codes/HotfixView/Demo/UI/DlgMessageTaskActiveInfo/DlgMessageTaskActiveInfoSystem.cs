using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgMessageTaskActiveInfoSystem
    {
        public static void RegisterUIEvent(this DlgMessageTaskActiveInfo self)
        {
            self.View.E_TaskButton.AddListenerAsync(self.OnTaskButtonClick);
        }

        public static async ETTask OnTaskButtonClick(this DlgMessageTaskActiveInfo self)
        {
            Log.Debug("任务按钮点击");
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_TaskLabelLayer);

            await ETTask.CompletedTask;
        }

        public static void ShowWindow(this DlgMessageTaskActiveInfo self, Entity contextData = null)
        {
        }
    }
}