using System.Collections;
using System.Collections.Generic;
using System;
using ET.Account;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgGoldInfoUISystem
    {
        public static void RegisterUIEvent(this DlgGoldInfoUI self)
        {
        }

        public static async ETTask ShowWindow(this DlgGoldInfoUI self, Entity contextData = null)
        {
            long AccountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            M2C_GetGoldInfoResponse response = (M2C_GetGoldInfoResponse) await session.Call(new C2M_GetGoldInfoRequest() { AccountId = AccountId });
            if (response.Error == ErrorCode.ERR_Success)
            {
                
                Log.Debug($"get gold info  {response}");
                self.View.E_GoldText.text = $"{response.GoldCount}";
                self.View.E_PowerText.text = $"{response.PowerCount}";
                self.View.E_DiamondText.text = $"{response.DiamondCount}";
            }
        }
    }
}