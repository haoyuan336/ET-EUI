using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.CompilerServices;
using ET.Account;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ET
{
    public static class DlgEditorTroopLayerSystem
    {
        public static void RegisterUIEvent(this DlgEditorTroopLayer self)
        {
            self.View.ELoopScrollList_HeroLoopVerticalScrollRect.AddItemRefreshListener((tr, index) =>
            {
                self.OnLoopItemListHeroCardEvent(tr, index);
            });
            self.View.ELoopScrollList_TroopLoopHorizontalScrollRect.AddItemRefreshListener((tr, index) => { self.OnLoopItemScrollEvent(tr, index); });

            self.View.ELoopScrollList_TroopHeroLoopHorizontalScrollRect.AddItemRefreshListener((tr, index) =>
            {
                self.OnLoopItemListTroopHeroCardEvent(tr, index);
            });
            self.View.E_BackButton.AddListenerAsync(() => { return self.BackButtonClick(); });
            self.View.E_StartGameButton.AddListenerAsync(() => { return self.StartGameButtonClick(); });
        }

        public static async ETTask BackButtonClick(this DlgEditorTroopLayer self)
        {
            await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_MainScene);
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_EditorTroopLayer);
            await ETTask.CompletedTask;
        }

        public static void ShowWindow(this DlgEditorTroopLayer self, Entity contextData = null)
        {
            //todo 请求当前玩家拥有几支队伍
            self.ShowTroopItems();
            self.ShowBagHeroItems();
            self.ShowTroopHeroCard();
        }

        public static void HideWindow(this DlgEditorTroopLayer self)
        {
            Log.Debug("hide window");
            self.RemoveUIScrollItems(ref self.ItemTroops);
            self.RemoveUIScrollItems(ref self.ItemTroopHeroCards);
            self.RemoveUIScrollItems(ref self.ItemHeroCards);
            self.TroopHeroCardInfos.Clear();
            self.HeroCardInfos.Clear();
        }

        public static async void ShowBagHeroItems(this DlgEditorTroopLayer self)
        {
            M2C_GetAllHeroCardListResponse m2CGetAllHeroCardListResponse;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            long AccoundId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            m2CGetAllHeroCardListResponse =
                    (M2C_GetAllHeroCardListResponse) await session.Call(new C2M_GetAllHeroCardListRequest() { Account = AccoundId });
            if (m2CGetAllHeroCardListResponse.Error == ErrorCode.ERR_Success)
            {
                self.HeroCardInfos = m2CGetAllHeroCardListResponse.HeroCardInfos;
                self.AddUIScrollItems(ref self.ItemHeroCards, m2CGetAllHeroCardListResponse.HeroCardInfos.Count);
                self.View.ELoopScrollList_HeroLoopVerticalScrollRect.SetVisible(true, m2CGetAllHeroCardListResponse.HeroCardInfos.Count);
            }
        }

        public static async void ShowTroopItems(this DlgEditorTroopLayer self)
        {
            long AccountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            M2C_GetAllTroopInfosResponse m2CGetAllTroopInfosResponse;
            m2CGetAllTroopInfosResponse = (M2C_GetAllTroopInfosResponse) await self.ZoneScene().GetComponent<SessionComponent>().Session
                    .Call(new C2M_GetAllTroopInfosRequest() { Account = AccountId });
            if (m2CGetAllTroopInfosResponse.Error == ErrorCode.ERR_Success)
            {
                self.TroopInfos = m2CGetAllTroopInfosResponse.TroopInfos;
                self.ShowTroopItemList();
            }
        }

        public static void OnLoopItemScrollEvent(this DlgEditorTroopLayer self, Transform transform, int index)
        {
            Log.Debug($"OnLoopItemScrollEvent{index}");
            Scroll_ItemHeroTroop scrollItemHeroTroop = self.ItemTroops[index].BindTrans(transform);
            if (index >= self.TroopInfos.Count)
            {
                scrollItemHeroTroop.E_LabelText.text = "+";
            }
            else
            {
                scrollItemHeroTroop.E_LabelText.text = self.TroopInfos[index].TroopId.ToString();
            }

            scrollItemHeroTroop.E_ToggleToggle.group = self.View.E_Content_TroopToggleGroup;
            scrollItemHeroTroop.E_ToggleToggle.onValueChanged.RemoveAllListeners();
            scrollItemHeroTroop.E_ToggleToggle.onValueChanged.AddListener((arg0 => self.TroopButtonClick(arg0, transform, index)));
            if (index == 0)
            {
                scrollItemHeroTroop.E_ToggleToggle.isOn = true;
            }
        }

        public static async void TroopButtonClick(this DlgEditorTroopLayer self, bool isClick, Transform transform, int index)
        {
            if (!isClick)
            {
                return;
            }

            Log.Debug($"trop toggle value change {index}");
            if (index >= self.TroopInfos.Count)
            {
                M2C_CreateTroopResponse createTroopResponse;
                //todo 请求创建队伍的接口
                long Account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
                Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
                createTroopResponse = (M2C_CreateTroopResponse) await session.Call(new C2M_CreateTroopRequest() { AccountId = Account });
                if (createTroopResponse.Error == ErrorCode.ERR_Success)
                {
                    self.TroopInfos.Add(createTroopResponse.TroopInfo);
                    self.ShowTroopItemList();
                }
            }
            else
            {
                Log.Debug("troop button click" + self.TroopInfos[index].TroopId);
                self.ChooseTroop(self.TroopInfos[index].TroopId);
            }
        }

        public static async void ChooseTroop(this DlgEditorTroopLayer self, long TroopId)
        {
            self.View.E_TroopNameText.text = $"current troop :{TroopId}";
            self.CurrentChooseTroopId = TroopId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            M2C_GetHeroInfosWithTroopIdResponse m2CGetHeroInfosWithTroopIdResponse;
            try
            {
                m2CGetHeroInfosWithTroopIdResponse =
                        (M2C_GetHeroInfosWithTroopIdResponse) await session.Call(new C2M_GetHeroInfosWithTroopIdRequest() { TroopId = TroopId });

                if (m2CGetHeroInfosWithTroopIdResponse.Error == ErrorCode.ERR_Success)
                {
                    Log.Debug("获取队伍英雄成功");

                    self.TroopHeroCardInfos = m2CGetHeroInfosWithTroopIdResponse.HeroCardInfos;
                    foreach (var heroCardInfo in self.TroopHeroCardInfos)
                    {
                        Log.Debug($"hero card info {heroCardInfo.ConfigId}");
                        Log.Debug($"hero index {heroCardInfo.InTroopIndex}");
                    }

                    // self.ShowTroopHeroCard();
                    self.View.ELoopScrollList_TroopHeroLoopHorizontalScrollRect.gameObject.SetActive(true);
                    self.View.ELoopScrollList_TroopHeroLoopHorizontalScrollRect.SetVisible(true, 3);
                }
            }
            catch (Exception e)
            {
                Log.Debug($"choose troop {e}");
            }
        }

        public static void ShowTroopItemList(this DlgEditorTroopLayer self)
        {
            TroopCountConfig troopCountConfig = TroopCountConfigCategory.Instance.Get(1);
            var count = self.TroopInfos.Count;
            if (self.TroopInfos.Count < troopCountConfig.TroopCount)
            {
                count = self.TroopInfos.Count + 1;
            }

            self.AddUIScrollItems(ref self.ItemTroops, count);
            self.View.ELoopScrollList_TroopLoopHorizontalScrollRect.SetVisible(true, count);
        }

        public static void ShowTroopHeroCard(this DlgEditorTroopLayer self)
        {
            self.AddUIScrollItems(ref self.ItemTroopHeroCards, 3);
            self.View.ELoopScrollList_TroopHeroLoopHorizontalScrollRect.SetVisible(true, 3);
            // self.View.E_StartGameButton.SetVisible(self.TroopInfos.Count == 2);
        }

        public static void OnLoopItemListHeroCardEvent(this DlgEditorTroopLayer self, Transform transform, int index)
        {
            Log.Debug($"OnLoopItemListHeroCardEvent{index}");
            Scroll_ItemHeroCard itemHeroCard = self.ItemHeroCards[index].BindTrans(transform);
            itemHeroCard.SetHeroInfo(self.HeroCardInfos[index]);
            // itemHeroCard.E_TextText.text = self.HeroCardInfos[index].HeroName + self.HeroCardInfos[index].ConfigId;
            // itemHeroCard.E_ClickButton.onClick.RemoveAllListeners();
            if (self.HeroCardInfos[index].TroopId != 0)
            {
                itemHeroCard.E_InTroopMarkImage.gameObject.SetActive(true);
            }

            itemHeroCard.E_ClickButton.AddListenerAsync(() => { return self.OnHeroCardClick(index); });
        }

        public static async ETTask OnHeroCardClick(this DlgEditorTroopLayer self, int index)
        {
            Log.Debug("hero card click" + index);
            //todo 请求将此英雄配置到队伍里面
            try
            {
                Log.Debug($"current choose troop  ={self.CurrentChooseTroopId}");
                Log.Debug($"current choose index = {self.CurrentChooseInTroopIndex}");
                HeroCardInfo heroCardInfo = self.HeroCardInfos[index];
                long HeroId = heroCardInfo.HeroId;
                Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
                M2C_SetHeroToTroopResponse m2CSetHeroToTroopResponse = (M2C_SetHeroToTroopResponse) await session.Call(new C2M_SetHeroToTroopRequest()
                {
                    HeroId = HeroId, TroopId = self.CurrentChooseTroopId, InTroopIndex = self.CurrentChooseInTroopIndex
                });

                if (m2CSetHeroToTroopResponse.Error == ErrorCode.ERR_Success)
                {
                    Log.Debug("设置英雄进队伍成功");
                    HeroCardInfo cardInfo = m2CSetHeroToTroopResponse.HeroCardInfo;

                    foreach (var value in self.TroopHeroCardInfos)
                    {
                        if (value.HeroId.Equals(cardInfo.HeroId))
                        {
                            self.TroopHeroCardInfos.Remove(value);
                            break;
                        }

                        if (value.InTroopIndex.Equals(cardInfo.InTroopIndex))
                        {
                            self.TroopHeroCardInfos.Remove(value);
                            break;
                        }
                    }

                    self.TroopHeroCardInfos.Add(cardInfo);
                    self.View.ELoopScrollList_TroopHeroLoopHorizontalScrollRect.SetVisible(true, 3);
                }
                else
                {
                    Log.Error($"{m2CSetHeroToTroopResponse.Error}");
                }
            }
            catch (Exception e)
            {
                Log.Error($"{e}");
            }

            await ETTask.CompletedTask;
        }

        public static void OnLoopItemListTroopHeroCardEvent(this DlgEditorTroopLayer self, Transform transform, int index)
        {
            Log.Debug($"on loop item list {index}");

            HeroCardInfo heroCardInfo = null;
            foreach (var info in self.TroopHeroCardInfos)
            {
                if (info.InTroopIndex == index)
                {
                    heroCardInfo = info;
                }
            }

            Scroll_ItemTroopHeroCard itemTroopHeroCard = self.ItemTroopHeroCards[index].BindTrans(transform);
            itemTroopHeroCard.E_ToggleToggle.group = self.View.E_Content_TroopHeroToggleGroup;

            if (heroCardInfo != null)
            {
                // itemTroopHeroCard.E_TextText.text = heroCardInfo.HeroName + heroCardInfo.ConfigId;

                itemTroopHeroCard.SetHeroInfo(heroCardInfo);
            }
            else
            {
                itemTroopHeroCard.E_TextText.text = "+";
            }

            itemTroopHeroCard.E_ToggleToggle.onValueChanged.RemoveAllListeners();
            itemTroopHeroCard.E_ToggleToggle.onValueChanged.AddListener((value) => { self.OnTroopHeroClick(value, index); });
            if (index == 0)
            {
                itemTroopHeroCard.E_ToggleToggle.isOn = true;
            }

            Log.Debug($"player troop hero count ={self.TroopHeroCardInfos.Count}");
            self.View.E_StartGameButton.SetVisible(self.TroopHeroCardInfos.Count == 3);
        }

        public static void OnTroopHeroClick(this DlgEditorTroopLayer self, bool value, int index)
        {
            if (value)
            {
                Log.Debug($"on troop hero click{index}");
                self.CurrentChooseInTroopIndex = index;
            }
        }

        public static async ETTask StartGameButtonClick(this DlgEditorTroopLayer self)
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
                        AccoundId = Account, TroopId = self.CurrentChooseTroopId
                    });
            if (m2CStartPveGameResponse.Error == ErrorCode.ERR_Success)
            {
                self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_EditorTroopLayer);
            }

            await ETTask.CompletedTask;
        }

        /// <summary>
        /// 检查体力是否足够
        /// </summary>
        /// <param name="self"></param>
        public static async ETTask<bool> CheckPowerIsEnough(this DlgEditorTroopLayer self)
        {
            long AccountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            M2C_GetGoldInfoResponse response = (M2C_GetGoldInfoResponse) await session.Call(new C2M_GetGoldInfoRequest() { AccountId = AccountId });
            if (response.Error == ErrorCode.ERR_Success)
            {
                if (response.PowerCount > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}