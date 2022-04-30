using System.Collections;
using System.Collections.Generic;
using System;
using ET.Account;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgGameWinUISystem
    {
        public static void RegisterUIEvent(this DlgGameWinUI self)
        {
            self.View.E_BackButton.AddListenerAsync(self.BackButtonClick);
        }

        public static async ETTask BackButtonClick(this DlgGameWinUI self)
        {
            Session session = self.DomainScene().GetComponent<SessionComponent>().Session;
            long AccountId = self.DomainScene().GetComponent<AccountInfoComponent>().AccountId;
            long RoomId = self.DomainScene().GetComponent<PlayerComponent>().RoomId;
            M2C_BackGameToMainMenuResponse response =
                    (M2C_BackGameToMainMenuResponse) await session.Call(new C2M_BackGameToMainMenuRequest() { Account = AccountId, RoomId = RoomId});

            if (response.Error == ErrorCode.ERR_Success)
            {
                Log.Debug(" back game success");
                self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_GameWinUI);
                self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_GameUI);
            }

            await ETTask.CompletedTask;
        }

        public static void ShowWindow(this DlgGameWinUI self, Entity contextData = null)
        {
        }
    }
}