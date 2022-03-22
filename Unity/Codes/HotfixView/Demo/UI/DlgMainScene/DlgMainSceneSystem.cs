using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgMainSceneSystem
    {
        public static void RegisterUIEvent(this DlgMainScene self)
        {
            // self.View.E_BagButton.onClick.AddListener(() => { self.BagButtonClick();});
            self.View.E_BagButton.AddListenerAsync(() => { return self.BagButtonClick(); });
            self.View.E_CallHeroButton.AddListenerAsync(() => { return self.CallHeroButtonClick(); });
            self.View.E_PVEButton.AddListenerAsync(() => { return self.PvEButtonClick(); });
            
        }

        public static async ETTask PvEButtonClick(this DlgMainScene self)
        {
            Log.Debug("pve button click");
            self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_EditorTroopLayer);
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_MainScene);
            await ETTask.CompletedTask;
        }

        public static async ETTask CallHeroButtonClick(this DlgMainScene self)
        {
            Log.Debug("call hero button click");
            self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_CallHeroLayer);
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_MainScene);
            await ETTask.CompletedTask;
        }

        public static async ETTask BagButtonClick(this DlgMainScene self)
        {
            Log.Debug("Bag button click");
            self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_BagLayer);
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_MainScene);

            await ETTask.CompletedTask;
        }

        public static void ShowWindow(this DlgMainScene self, Entity contextData = null)
        {
            
        }
    }
}