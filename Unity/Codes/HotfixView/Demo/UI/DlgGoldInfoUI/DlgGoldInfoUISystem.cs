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
            self.View.E_AddPowerButton.AddListenerAsync(self.RequestAddPowerAsync);
        }

        public static async ETTask RequestAddPowerAsync(this DlgGoldInfoUI self)
        {
            long AccountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            M2C_AddPowerResponse response =
                    (M2C_AddPowerResponse) await session.Call(new C2M_AddPowerRequest() { AccountId = AccountId, Count = 10 });
            if (response.Error == ErrorCode.ERR_Success)
            {
                // Log.Debug("增加体力值成功");
                self.View.E_PowerText.text = $"{response.Count}";
            }
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