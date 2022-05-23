using System.Collections;
using System.Collections.Generic;
using System;
using ET.Account;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgGameLevelInfoLayerSystem
    {
        public static void RegisterUIEvent(this DlgGameLevelInfoLayer self)
        {
            self.View.E_EditorTroopButton.AddListenerAsync(self.ShowEditorTroopLayer);
            self.View.E_StartGameButton.AddListenerAsync(self.StartGameClickAction);
            self.View.ESTroopHeroCards.RegisterUIEvent();
        }

        // public static async ETTask StartGame(this DlgGameLevelInfoLayer self)
        // {
        //     
        //     await ETTask.CompletedTask;
        // }
        /// <summary>
        /// 检查体力是否足够
        /// </summary>
        /// <param name="self"></param>
        public static async ETTask<bool> CheckPowerIsEnough(this DlgGameLevelInfoLayer self)
        {
            long AccountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            M2C_GetGoldInfoResponse response = (M2C_GetGoldInfoResponse) await session.Call(new C2M_GetGoldInfoRequest() { AccountId = AccountId });
            if (response.Error == ErrorCode.ERR_Success)
            {
                // if (response.PowerCount > 0)
                // {
                //     return true;
                // }
                var powerCount = 0;
                foreach (var itemCountInfo in response.ItemInfos)
                {
                    if (itemCountInfo.ConfigId == 1003)
                    {
                        powerCount = itemCountInfo.Count;
                    }
                }

                if (powerCount > 0)
                {
                    return true;
                }
            }

            // return false;
            return false;
            // await ETTask.CompletedTask;
        }

        public static async ETTask StartGameClickAction(this DlgGameLevelInfoLayer self)
        {
            bool isPowerEnough = await self.CheckPowerIsEnough();
            if (!isPowerEnough)
            {
                await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_PowerNotEnoughAlert);
                return;
            }

            var Account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            M2C_StartPVEGameResponse m2CStartPveGameResponse =
                    (M2C_StartPVEGameResponse) await session.Call(new C2M_StartPVEGameRequest()
                    {
                        AccountId = Account, TroopId = self.CurrentChooseTroopId
                    });
            if (m2CStartPveGameResponse.Error == ErrorCode.ERR_Success)
            {
                self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_GameLevelInfoLayer);
                self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_GoldInfoUI);
            }
        }

        public static async ETTask ShowEditorTroopLayer(this DlgGameLevelInfoLayer self)
        {
            //显示编辑队伍的页面
            await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_EditorTroopLayer);

            UIBaseWindow baseWindow = self.DomainScene().GetComponent<UIComponent>().GetUIBaseWindow(WindowID.WindowID_EditorTroopLayer);
            var troopLayer = baseWindow.GetComponent<DlgEditorTroopLayer>();
            troopLayer.HideEditorTroopLayerAction = () =>
            {
                self.ShowTroopHeroCardInfo();

                UIBaseWindow backWindow = self.DomainScene().GetComponent<UIComponent>().GetUIBaseWindow(WindowID.WindowID_BackButton);
                backWindow.uiTransform.GetComponent<RectTransform>().offsetMax = new Vector2(300, -400);
                DlgBackButton dlgBackButton = backWindow.GetComponent<DlgBackButton>();
                dlgBackButton.BackButtonClickAction = self.OnBackButtonClick;
            };
            await ETTask.CompletedTask;
        }

        public static async void ShowWindow(this DlgGameLevelInfoLayer self, Entity contextData = null)
        {
            await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_BackButton);
            UIBaseWindow baseWindow = self.DomainScene().GetComponent<UIComponent>().GetUIBaseWindow(WindowID.WindowID_BackButton);
            baseWindow.uiTransform.GetComponent<RectTransform>().offsetMax = new Vector2(300, -400);
            DlgBackButton dlgBackButton = baseWindow.GetComponent<DlgBackButton>();
            dlgBackButton.BackButtonClickAction = self.OnBackButtonClick;
            self.ShowTroopHeroCardInfo();

            //显示游戏详情页面
            self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_GameLevelEnemyInfoLayer).Coroutine();
        }

        public static async void ShowTroopHeroCardInfo(this DlgGameLevelInfoLayer self)
        {
            List<HeroCardInfo> heroCardInfos = await self.RequestCurrentTroopInfo();
            self.View.E_StartGameButton.gameObject.SetActive(heroCardInfos.Count == 3);
            // await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_TroopHeroCardLayer);
            // UIBaseWindow heroCardBaseWindos = self.DomainScene().GetComponent<UIComponent>().GetUIBaseWindow(WindowID.WindowID_TroopHeroCardLayer);
            // heroCardBaseWindos.GetComponent<DlgTroopHeroCardLayer>().UpdateHeroCardInfo(heroCardInfos);
            self.View.ESTroopHeroCards.UpdateHeroCardInfos(heroCardInfos);

            // heroCardBaseWindos.uiTransform.GetComponent<RectTransform>().offsetMax = new vec;
            // var heroCardBaseWindowRect = heroCardBaseWindos.uiTransform.GetComponent<RectTransform>();
            // heroCardBaseWindowRect.anchorMax = new Vector2(0.5f, 1);
            // heroCardBaseWindowRect.anchorMin = new Vector2(0.5f, 1);
            // heroCardBaseWindowRect.offsetMax = new Vector2(0, -700);
            // heroCardBaseWindowRect.offsetMin = new Vector2(0, -700);
            // heroCardBaseWindos.GetComponent<DlgTroopHeroCardLayer>().acti
        }

        public static async ETTask<List<HeroCardInfo>> RequestCurrentTroopInfo(this DlgGameLevelInfoLayer self)
        {
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            long account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            C2M_GetAllTroopInfosRequest troopInfosRequest = new C2M_GetAllTroopInfosRequest() { Account = account };
            M2C_GetAllTroopInfosResponse response = (M2C_GetAllTroopInfosResponse) await session.Call(troopInfosRequest);
            if (response.Error == ErrorCode.ERR_Success)
            {
                // Log.Debug($"response {response.TroopInfos.Count}");
                // TroopInfo troopInfo = response.TroopInfos[0];
                if (response.TroopInfos.Count > 0)
                {
                    long troopId = response.TroopInfos[0].TroopId;
                    //获取此id下的玩家列表
                    self.CurrentChooseTroopId = troopId;
                    C2M_GetHeroInfosWithTroopIdRequest c2m = new C2M_GetHeroInfosWithTroopIdRequest() { TroopId = troopId };
                    M2C_GetHeroInfosWithTroopIdResponse getHeroInfosWithTroopIdResponse =
                            (M2C_GetHeroInfosWithTroopIdResponse) await session.Call(c2m);
                    if (getHeroInfosWithTroopIdResponse.Error == ErrorCode.ERR_Success)
                    {
                        List<HeroCardInfo> heroCardInfos = getHeroInfosWithTroopIdResponse.HeroCardInfos;
                        return heroCardInfos;
                    }
                }
            }

            return null;
        }

        public static void OnBackButtonClick(this DlgGameLevelInfoLayer self)
        {
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_GameLevelInfoLayer);
            self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_MainScene).Coroutine();
        }

        public static void HideWindow(this DlgGameLevelInfoLayer self)
        {
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_BackButton);
            // self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_TroopHeroCardLayer);
        }
    }
}