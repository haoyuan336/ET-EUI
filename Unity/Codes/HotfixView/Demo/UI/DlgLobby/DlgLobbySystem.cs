using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgLobbySystem
    {
        public static void RegisterUIEvent(this DlgLobby self)
        {
            self.View.E_EnterMapButton.AddListener(() => { self.OnEnterMapClickHandler().Coroutine(); });
        }

        public static void ShowWindow(this DlgLobby self, Entity contextData = null)
        {
        }

        public static async ETTask OnEnterMapClickHandler(this DlgLobby self)
        {
            await EnterMapHelper.EnterMapAsync(self.ZoneScene());
            // Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            // Log.Debug("enter may async");
            // M2C_TestActorLocationResponse m2CTestActorLocationResponse = (M2C_TestActorLocationResponse) await session.Call(new C2M_TestActorLocationRequest() { Content = "11111111111111111111111" });
            // Log.Debug("c2m_test actor location resquest" + m2CTestActorLocationResponse.Content);
            // session.Send(new C2M_TestActorLocationMessage() { Content = "3333333333333333333333" });
        }
    }
}