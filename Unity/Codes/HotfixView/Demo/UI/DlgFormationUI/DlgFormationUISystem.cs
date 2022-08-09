using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgFormationUISystem
    {
        public static void RegisterUIEvent(this DlgFormationUI self)
        {
            self.View.E_PVEButton.AddListener(self.PVEButtonClick);
            self.View.E_PVPButton.AddListener(self.PVPButtonClick);
        }

        public static async void PVPButtonClick(this DlgFormationUI self)
        {
            await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_PVPSceneLayer);
            self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_GoldInfoUI).Coroutine();

            // await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_EditorTroopLayer);
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_MainScene);
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_MainSceneBg);
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_AccountInfo);
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_MessageTaskActiveInfo);
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_SettingUI);
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_FormationUI);
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_MainSceneMenu);
        }

        public static async void PVEButtonClick(this DlgFormationUI self)
        {
            await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_PVESceneLayer);
            await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_EditorTroopLayer);

            self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_GoldInfoUI).Coroutine();

            // await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_EditorTroopLayer);
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_MainScene);
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_MainSceneBg);
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_AccountInfo);
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_MessageTaskActiveInfo);
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_SettingUI);
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_FormationUI);
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_MainSceneMenu);
        }

        public static void ShowWindow(this DlgFormationUI self, Entity contextData = null)
        {
        }
    }
}