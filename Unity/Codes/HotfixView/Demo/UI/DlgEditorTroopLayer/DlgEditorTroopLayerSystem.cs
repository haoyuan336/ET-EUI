using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.CompilerServices;
using ET.Account;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgEditorTroopLayerSystem
    {
        public static void RegisterUIEvent(this DlgEditorTroopLayer self)
        {
            self.View.ELoopScrollList_HeroLoopVerticalScrollRect.AddItemRefreshListener((tr, index) =>
            {
                self.OnLoopItemListTroopHeroCardEvent(tr, index);
            });
            self.View.ELoopScrollList_TroopLoopHorizontalScrollRect.AddItemRefreshListener((tr, index) => { self.OnLoopItemScrollEvent(tr, index); });
            self.View.E_BackButton.AddListenerAsync(() => { return self.BackButtonClick(); });
        }

        public static async ETTask BackButtonClick(this DlgEditorTroopLayer self)
        {
            self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_MainScene);
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_EditorTroopLayer);
            await ETTask.CompletedTask;
        }

        public static void ShowWindow(this DlgEditorTroopLayer self, Entity contextData = null)
        {
            //todo 请求当前玩家拥有几支队伍
            self.ShowTropItems();
            self.ShowBagHeroItems();
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

        public static async void ShowTropItems(this DlgEditorTroopLayer self)
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

        public static void HideWindowd(this DlgEditorTroopLayer self)
        {
            self.RemoveUIScrollItems(ref self.ItemTroops);
        }

        public static void OnLoopItemScrollEvent(this DlgEditorTroopLayer self, Transform transform, int index)
        {
            Scroll_ItemHeroTroop scrollItemHeroTroop = self.ItemTroops[index].BindTrans(transform);
            if (index >= self.TroopInfos.Count)
            {
                scrollItemHeroTroop.E_LabelText.text = "+";
            }
            else
            {
                scrollItemHeroTroop.E_LabelText.text = self.TroopInfos[index].TroopId.ToString();
            }

            scrollItemHeroTroop.E_ToggleToggle.group = self.View.E_ContentToggleGroup;
            scrollItemHeroTroop.E_ToggleToggle.onValueChanged.AddListener((arg0 => self.TroopButtonClick(arg0, transform, index)));
            // scrollItemHeroTroop.E_ToggleToggle.AddListener(() => { self.TroopButtonClick(transform, index); });
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

            // Log.Debug($"trop button click {index}");
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
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            M2C_GetHeroInfosWithTroopIdResponse m2CGetHeroInfosWithTroopIdResponse;
            try
            {
                m2CGetHeroInfosWithTroopIdResponse =
                        (M2C_GetHeroInfosWithTroopIdResponse) await session.Call(new C2M_GetHeroInfosWithTroopIdRequest() { TroopId = TroopId });

                if (m2CGetHeroInfosWithTroopIdResponse.Error == ErrorCode.ERR_Success)
                {
                    Log.Debug("获取队伍英雄成功");

                    self.ShowTroopHeroCard(m2CGetHeroInfosWithTroopIdResponse.HeroCardInfos);
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

        public static void ShowTroopHeroCard(this DlgEditorTroopLayer self, List<HeroCardInfo> heroCardInfos)
        {
            self.View.ELoopScrollList_TroopHeroLoopHorizontalScrollRect.gameObject.SetActive(true);
            self.AddUIScrollItems(ref self.ItemTroopHeroCards, 3);
            self.View.ELoopScrollList_TroopHeroLoopHorizontalScrollRect.SetVisible(true, 3);
        }

        public static void OnLoopItemListTroopHeroCardEvent(this DlgEditorTroopLayer self, Transform transform, int index)
        {
            Scroll_ItemHeroCard itemHeroCard = self.ItemHeroCards[index].BindTrans(transform);
            itemHeroCard.E_TextText.text = self.HeroCardInfos[index].HeroName;
            itemHeroCard.E_ClickButton.AddListenerAsync(() => { return self.OnHeroCardClick(index); });
        }

        public static async ETTask OnHeroCardClick(this DlgEditorTroopLayer self,int index)
        {
            Log.Debug("hero card click" + index);
            await ETTask.CompletedTask;
        }
    }
}