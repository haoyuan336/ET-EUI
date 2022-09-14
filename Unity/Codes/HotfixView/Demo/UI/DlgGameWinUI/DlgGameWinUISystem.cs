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
            self.View.E_BackButton.AddListenerAsync(self.BackButtonClick,ConstValue.BackButtonAudioStr);
            self.View.E_NextLevelButton.AddListenerAsync(self.NextLevelButtonClick);
        }

        public static async ETTask NextLevelButtonClick(this DlgGameWinUI self)
        {
            //获取当前玩家选择的关卡数
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            uiComponent.HideWindow(WindowID.WindowID_GameWinUI);

            var Account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            long RoomId = self.DomainScene().GetComponent<PlayerComponent>().RoomId;

            C2M_EnterChangeTempSceneRequest request = new C2M_EnterChangeTempSceneRequest() { Account = Account, RoomId = RoomId };
            M2C_EnterChangeTempSceneResponse response = await session.Call(request) as M2C_EnterChangeTempSceneResponse;
            if (response.Error == ErrorCode.ERR_Success)
            {
                Log.Debug("进入下一关成功");

                // self.DomainScene().GetComponent<PlayerComponent>().CurrentTurnIndex;
                var accountResponse =
                        (M2C_GetAccountInfoWidthAccointIdResponse) await session.Call(
                            new C2M_GetAccountInfoWithAccountIdRequest() { AccountId = Account });
                if (accountResponse.Error == ErrorCode.ERR_Success)
                {
                    var CurrentChooseTroopId = accountResponse.AccountInfo.CurrentTroopId;
                    Log.Debug($"current choose troop id {CurrentChooseTroopId}");
                    M2C_StartPVEGameResponse m2CStartPveGameResponse =
                            (M2C_StartPVEGameResponse) await session.Call(new C2M_StartPVEGameRequest()
                            {
                                AccountId = Account, TroopId = CurrentChooseTroopId
                            });
                }
            }

            await ETTask.CompletedTask;
        }

        public static async ETTask BackButtonClick(this DlgGameWinUI self)
        {
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_GameWinUI);
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_GameUI);
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_GameLevelLayer);
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_ExitGameAlert);

            self.ZoneScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_GameLoaseUI);
            self.ZoneScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_GameUI);
            self.ZoneScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_GameLevelLayer);
            Session session = self.DomainScene().GetComponent<SessionComponent>().Session;
            long AccountId = self.DomainScene().GetComponent<AccountInfoComponent>().AccountId;
            long RoomId = self.DomainScene().GetComponent<PlayerComponent>().RoomId;
            M2C_BackGameToMainMenuResponse response =
                    (M2C_BackGameToMainMenuResponse) await session.Call(new C2M_BackGameToMainMenuRequest() { Account = AccountId, RoomId = RoomId });

            if (response.Error == ErrorCode.ERR_Success)
            {
                Log.Debug("game win back game success");
            }

            await ETTask.CompletedTask;
        }

        public static void ShowWindow(this DlgGameWinUI self, Entity contextData = null)
        {
        }
    }
}