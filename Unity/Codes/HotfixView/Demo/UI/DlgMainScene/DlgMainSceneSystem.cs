using System.Collections;
using System.Collections.Generic;
using System;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgMainSceneSystem
    {
        public static void RegisterUIEvent(this DlgMainScene self)
        {
          
        }
        public static void ShowWindow(this DlgMainScene self, Entity contextData = null)
        {
            Log.Debug("macin scene show window");
            self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_MainSceneBg);
            self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_GoldInfoUI);
            self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_AccountInfo);
            self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_MessageTaskActiveInfo);
            self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_SettingUI);
            self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_FormationUI);
            self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_MainSceneMenu);

        }
    }
}