using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgTaskLabelLayerSystem
    {
        public static void RegisterUIEvent(this DlgTaskLabelLayer self)
        {
            self.View.E_BackButton.AddListener(self.BackButtonClick);
            self.View.EDayTaskButton.AddListener(self.DayTaskButtonClick);
        }

        public static async void DayTaskButtonClick(this DlgTaskLabelLayer self)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_DayTaskLayer);
        }

        public static void BackButtonClick(this DlgTaskLabelLayer self)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            uiComponent.HideWindow(WindowID.WindowID_TaskLabelLayer);
        }

        public static async void  ShowWindow(this DlgTaskLabelLayer self, Entity contextData = null)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_DayTaskLayer);
        }

        public static void HideWindow(this DlgTaskLabelLayer self)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            uiComponent.HideWindow(WindowID.WindowID_DayTaskLayer);
        }
    }
}