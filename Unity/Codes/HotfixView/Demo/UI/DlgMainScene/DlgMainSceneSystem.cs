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

        public static async void ShowWindow(this DlgMainScene self, Entity contextData = null)
        {
            Log.Debug("dlg main scene show window");
            // Log.Debug($"macin scene show window{self.DomainScene()}");
            // UIComponent uiComponent = self.ZoneScene().GetComponent<UIComponent>();
            await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_MainSceneBg);
            await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_GoldInfoUI);
            await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_AccountInfo);
            await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_MessageTaskActiveInfo);
            await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_SettingUI);
            await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_FormationUI);
            await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_MainSceneMenu);
        }
    }
}