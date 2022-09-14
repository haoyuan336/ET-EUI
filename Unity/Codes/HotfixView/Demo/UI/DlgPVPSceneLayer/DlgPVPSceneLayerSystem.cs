using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgPVPSceneLayerSystem
    {
        public static void RegisterUIEvent(this DlgPVPSceneLayer self)
        {
            self.View.E_BackButton.AddListener(() =>
            {
                self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_PVPSceneLayer);
                self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_MainScene).Coroutine();
            },ConstValue.BackButtonAudioStr);
            self.View.E_StartFightButton.AddListener(self.OnStartFightButtonClick);
        }

        public static async void OnStartFightButtonClick(this DlgPVPSceneLayer self)
        {
            await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_PVPFightPrepareLayer);
            await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_EditorTroopLayer);
        }

        public static void ShowWindow(this DlgPVPSceneLayer self, Entity contextData = null)
        {
        }
    }
}