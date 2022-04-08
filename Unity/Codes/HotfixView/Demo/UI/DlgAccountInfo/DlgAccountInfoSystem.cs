using System.Collections;
using System.Collections.Generic;
using System;
using ET.Account;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgAccountInfoSystem
    {
        public static void RegisterUIEvent(this DlgAccountInfo self)
        {
        }

        public static async ETTask ShowWindow(this DlgAccountInfo self, Entity contextData = null)
        {
            Log.Debug("account info");
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            // session.Call()
            long AccountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            M2C_GetUserExpInfoResponse m2CGetUserExpInfoResponse =
                    (M2C_GetUserExpInfoResponse) await session.Call(new C2M_GetUserExpInfoRequest() { AccountId = AccountId });
            Log.Debug($"error {m2CGetUserExpInfoResponse.Error}");
            if (m2CGetUserExpInfoResponse.Error == ErrorCode.ERR_Success)
            {
                int level = m2CGetUserExpInfoResponse.UserLevel;
                Log.Debug($"current level {level}");
                int exp = m2CGetUserExpInfoResponse.Exp;
                Log.Debug($"current exp {exp}");
                int nextExp = UserUpdateLevelConfigCategory.Instance.Get(level).NeedExp;
                Log.Debug($"next exp{nextExp}");
                self.View.E_ExpText.text = $"Exp:{exp}/{nextExp}";
                self.View.E_AccountText.text = $"ID:{AccountId}";
                self.View.E_LevelText.text = $"Lv:{level}";
            }

            await ETTask.CompletedTask;
        }
    }
}