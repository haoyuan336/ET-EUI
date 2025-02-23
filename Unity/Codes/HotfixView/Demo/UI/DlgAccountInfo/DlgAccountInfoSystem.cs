﻿using System.Collections;
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
            // self
            self.View.E_NameButton.AddListenerAsync(self.OnNameButtonClick);
        }

        public static async ETTask OnNameButtonClick(this DlgAccountInfo self)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_UserInfoLayer);
            UIBaseWindow uiBaseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_UserInfoLayer);
            if (uiBaseWindow != null)
            {
                uiBaseWindow.GetComponent<DlgUserInfoLayer>().SetUserInfo(self.AccountInfo);
            }
        }

        public static async ETTask ShowWindow(this DlgAccountInfo self, Entity contextData = null)
        {
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            // session.Call()
            long AccountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            M2C_GetUserExpInfoResponse m2CGetUserExpInfoResponse =
                    (M2C_GetUserExpInfoResponse) await session.Call(new C2M_GetUserExpInfoRequest() { AccountId = AccountId });
            if (m2CGetUserExpInfoResponse.Error == ErrorCode.ERR_Success)
            {
                // int level = m2CGetUserExpInfoResponse.UserLevel;
                // int exp = m2CGetUserExpInfoResponse.Exp;
                // int nextExp = UserUpdateLevelConfigCategory.Instance.Get(level).NeedExp;
                // self.View.E_IDText.text = $"ID:{m2CGetUserExpInfoResponse.UserName}";
                // self.View.E_IDText.text = m2CGetUserExpInfoResponse.UserName;
                self.View.E_NameContentText.text = m2CGetUserExpInfoResponse.UserName;
            }


            var request = new C2M_GetAccountInfoWithAccountIdRequest() { AccountId = AccountId };
            var response = await session.Call(request) as M2C_GetAccountInfoWidthAccointIdResponse;


            if (response.Error == ErrorCode.ERR_Success)
            {
                self.AccountInfo = response.AccountInfo;

            }

            await ETTask.CompletedTask;
        }
    }
}