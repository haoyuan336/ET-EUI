using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.CompilerServices;
using ET.Account;
using ILRuntime.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ET
{
    public static class DlgEditorTroopLayerSystem
    {
        public static void RegisterUIEvent(this DlgEditorTroopLayer self)
        {
            self.View.ESTroopHeroCards.RegisterUIEvent();
        }

        public static async void ShowBackButton(this DlgEditorTroopLayer self)
        {

            await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_BackButton);
            UIBaseWindow baseWindow = self.DomainScene().GetComponent<UIComponent>().GetUIBaseWindow(WindowID.WindowID_BackButton);
            baseWindow.GetComponent<DlgBackButton>().BackButtonClickAction = () =>
            {
                self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_AllHeroBagLayer);
                self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_EditorTroopLayer);
            };
        }

        public static async void ShowTroopHeroCardInfo(this DlgEditorTroopLayer self)
        {
            //显示当前的队伍信息
            //首先获取到当前的玩家队伍id
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            long accountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;

            M2C_GetAllTroopInfosResponse troopInfosResponse =
                    (M2C_GetAllTroopInfosResponse) await session.Call(new C2M_GetAllTroopInfosRequest() { Account = accountId });
            if (troopInfosResponse.Error == ErrorCode.ERR_Success)
            {
                Log.Debug($"troop info {troopInfosResponse.TroopInfos.Count}");

                self.CurrentChooseTroopId = troopInfosResponse.TroopInfos[0].TroopId;
                //获取此id下的所有英雄
                Log.Debug($"current choose troop id{self.CurrentChooseTroopId}");
                M2C_GetHeroInfosWithTroopIdResponse troopIdResponse = (M2C_GetHeroInfosWithTroopIdResponse) await session.Call(
                    new C2M_GetHeroInfosWithTroopIdRequest() { TroopId = troopInfosResponse.TroopInfos[0].TroopId });
                if (troopIdResponse.Error == ErrorCode.ERR_Success)
                {
                    self.TroopHeroCardInfos = troopIdResponse.HeroCardInfos;
                    Log.Debug($"self troop card info {self.TroopHeroCardInfos.Count}");
                    self.UpdateTroopHeroCardInfoAsync(self.TroopHeroCardInfos);
                    // self.View.E_TroopCardContentLoopVerticalScrollRect.RefreshCells();
                    self.SetAlChooseHeroCardInfo();
                }
            }
        }

        public static async ETTask UnSetHeroToTroopAsync(this DlgEditorTroopLayer self, HeroCardInfo heroCardInfo)
        {
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            long accountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            M2C_UnSetHeroToTroopResponse response = (M2C_UnSetHeroToTroopResponse) await session.Call(new C2M_UnSetHeroToTroopRequest()
            {
                AccountId = accountId, TroopId = self.CurrentChooseTroopId, HeroId = heroCardInfo.HeroId
            });
            if (response.Error == ErrorCode.ERR_Success)
            {
                Log.Debug("从队伍里面移除英雄成功");
                self.TroopHeroCardInfos = response.HeroCardInfos;
                // self.View.E_TroopCardContentLoopVerticalScrollRect.RefreshCells();
                // SetAllChooseHeroCardInfos
                self.UpdateTroopHeroCardInfoAsync(self.TroopHeroCardInfos);

                self.SetAlChooseHeroCardInfo();
            }
        }

        public static async void OnTroopHeroCardItemClickAction(this DlgEditorTroopLayer self, HeroCardInfo heroCardInfo, bool value)
        {
            if (value)
            {
                if (heroCardInfo != null)
                {
                    await self.UnSetHeroToTroopAsync(heroCardInfo);
                }
            }
        }

        public static void SetAlChooseHeroCardInfo(this DlgEditorTroopLayer self)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            UIBaseWindow baseWindow = uiComponent.AllWindowsDic[(int) WindowID.WindowID_AllHeroBagLayer];
            baseWindow.GetComponent<DlgAllHeroBagLayer>().SetAllChooseHeroCardInfos(self.TroopHeroCardInfos);
        }

        public static void UpdateTroopHeroCardInfoAsync(this DlgEditorTroopLayer self, List<HeroCardInfo> heroCardInfos)
        {
            self.View.ESTroopHeroCards.UpdateHeroCardInfo(heroCardInfos);
        }

        public static async void OnHeroCardItemClickAction(this DlgEditorTroopLayer self, HeroCardInfo info, Scroll_ItemHeroCard heroCard, bool value)
        {
            Log.Debug("on hero card click");
            if (value)
            {
                if (self.TroopHeroCardInfos.Count == 3)
                {
                    heroCard.E_ChooseToggle.isOn = false;
                    return;
                }

                Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
                long accountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
                M2C_SetHeroToTroopResponse response = (M2C_SetHeroToTroopResponse) await
                        session.Call(new C2M_SetHeroToTroopRequest()
                        {
                            HeroId = info.HeroId, TroopId = self.CurrentChooseTroopId, AccountId = accountId
                        });
                if (response.Error == ErrorCode.ERR_Success)
                {
                    // Log.Debug($"设置队伍成功{response.HeroCardInfos.Count}");
                    self.TroopHeroCardInfos = response.HeroCardInfos;
                    self.UpdateTroopHeroCardInfoAsync(self.TroopHeroCardInfos);
                }
                else
                {
                    heroCard.E_ChooseToggle.isOn = false;
                }
            }

            if (!value)
            {
                await self.UnSetHeroToTroopAsync(info);
            }

            self.SetAlChooseHeroCardInfo();
        }

        public static async void ShowWindow(this DlgEditorTroopLayer self, Entity contextData = null)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_AllHeroBagLayer);
            UIBaseWindow baseWindow = uiComponent.AllWindowsDic[(int) WindowID.WindowID_AllHeroBagLayer];
            baseWindow.uiTransform.GetComponent<RectTransform>().offsetMax = new Vector2(0, -600);
            baseWindow.uiTransform.GetComponent<RectTransform>().offsetMin = new Vector2(0, 250);
            DlgAllHeroBagLayer allHeroBagLayer = baseWindow.GetComponent<DlgAllHeroBagLayer>();
            allHeroBagLayer.OnHeroItemInfoClick = self.OnHeroCardItemClickAction;
            await allHeroBagLayer.SetShowHeroTypeAsync(HeroBagType.Hero);
            self.View.ESTroopHeroCards.ItemCardClickAction = self.OnTroopHeroCardItemClickAction;
            self.ShowTroopHeroCardInfo();
            self.ShowBackButton();
        }

        public static void HideWindow(this DlgEditorTroopLayer self)
        {
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_AllHeroBagLayer);
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_TroopHeroCardLayer);
            // self.View.E_StartGameButton.gameObject.SetActive(false);
            if (self.HideEditorTroopLayerAction != null)
            {
                self.HideEditorTroopLayerAction();
            }
        }
    }
}