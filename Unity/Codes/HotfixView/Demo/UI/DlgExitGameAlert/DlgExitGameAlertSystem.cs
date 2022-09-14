using System.Collections;
using System.Collections.Generic;
using System;
using ET.Account;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgExitGameAlertSystem
    {
        public static void RegisterUIEvent(this DlgExitGameAlert self)
        {
            self.View.E_OKButton.AddListenerAsync(self.OkButtonClick);
            self.View.E_CancelButton.AddListenerAsync(self.CancelButtonClick);
        }

        public static async ETTask OkButtonClick(this DlgExitGameAlert self)
        {
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            long AccountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            long RoomId = self.ZoneScene().GetComponent<PlayerComponent>().RoomId;
            M2C_BackGameToMainMenuResponse response =
                    (M2C_BackGameToMainMenuResponse) await session.Call(new C2M_BackGameToMainMenuRequest() { Account = AccountId, RoomId = RoomId });

            if (response.Error == ErrorCode.ERR_Success)
            {
                Log.Debug("player exit game success");
                self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_GameWinUI);
                self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_GameUI);
                self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_GameLevelLayer);
                self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_ExitGameAlert);
                // Game.EventSystem.Publish(new EventType.ExitGameScene());
                // Game.EventSystem.RegisterSystem();
            }

            await ETTask.CompletedTask;
        }

        public static async ETTask CancelButtonClick(this DlgExitGameAlert self)
        {
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_ExitGameAlert);
            await ETTask.CompletedTask;
        }

        public static void ShowWindow(this DlgExitGameAlert self, Entity contextData = null)
        {
        }
    }
}