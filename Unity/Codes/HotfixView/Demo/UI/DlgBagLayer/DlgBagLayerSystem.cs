using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.CompilerServices;
using ET.Account;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace ET
{
    public static class DlgBagLayerSystem
    {
        public static void RegisterUIEvent(this DlgBagLayer self)
        {
            self.View.E_BackButton.AddListenerAsync(() => { return self.BackButtonClick(); });
            self.View.ELoopScrollListHeroLoopVerticalScrollRect.AddItemRefreshListener((Transform tr, int index) =>
            {
                self.OnLoopListItemRefreshHandler(tr, index);
            });
        }

        public static void OnLoopListItemRefreshHandler(this DlgBagLayer self, Transform transform, int index)
        {
            Scroll_ItemHeroCard scrollItemHeroCard = self.ItemHeroCards[index].BindTrans(transform);
            // scrollItemHeroCard.E_TextText.text = $"{self.HeroCardInfos[index].HeroName}";
        }

        public static async ETTask BackButtonClick(this DlgBagLayer self)
        {
            await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_MainScene);
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_BagLayer);
            await ETTask.CompletedTask;
        }

        public static async void ShowWindow(this DlgBagLayer self, Entity contextData = null)
        {
            long Account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Log.Debug($"account = {Account}");
            M2C_GetAllHeroCardListResponse m2CGetAllHeroCardListResponse;
            try
            {
                m2CGetAllHeroCardListResponse = (M2C_GetAllHeroCardListResponse) await self.ZoneScene().GetComponent<SessionComponent>().Session
                        .Call(new C2M_GetAllHeroCardListRequest() { Account = Account });
                if (m2CGetAllHeroCardListResponse.Error == ErrorCode.ERR_Success)
                {
                    List<HeroCardInfo> heroCardInfos = m2CGetAllHeroCardListResponse.HeroCardInfos;
                    self.HeroCardInfos.Clear();
                    foreach (var heroCardInfo in heroCardInfos)
                    {
                        self.HeroCardInfos.Add(heroCardInfo);
                    }

                    self.AddUIScrollItems(ref self.ItemHeroCards, self.HeroCardInfos.Count);
                    self.View.ELoopScrollListHeroLoopVerticalScrollRect.SetVisible(true, self.HeroCardInfos.Count);
                }
                else
                {
                    Log.Error($"c2c get all hero card list error {m2CGetAllHeroCardListResponse.Error}");
                }
            }
            catch (Exception e)
            {
                Log.Error($"get all hero card list error {e}");
            }
        }
    }
}