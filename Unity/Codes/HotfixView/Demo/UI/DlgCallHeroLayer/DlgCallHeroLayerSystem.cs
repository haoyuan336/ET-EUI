using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using ET.Account;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgCallHeroLayerSystem
    {
        public static void RegisterUIEvent(this DlgCallHeroLayer self)
        {
            self.View.ELoopScrollListHeroLoopVerticalScrollRect.AddItemRefreshListener((tr, index) =>
            {
                self.OnLoopListItemRefreshHandler(tr, index);
            });
            self.View.E_CallHeroButton.AddListenerAsync(() => { return self.CallHeroButtonClick(); });
            self.View.E_BackButton.AddListenerAsync(() => { return self.BackButtonClick(); });

            // self.View.oNL
        }

        public static async ETTask BackButtonClick(this DlgCallHeroLayer self)
        {
            // self.ZoneScene().GetComponent<UIBaseWindow>().
            self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_MainScene);
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_CallHeroLayer);

            await ETTask.CompletedTask;
        }

        public static async ETTask CallHeroButtonClick(this DlgCallHeroLayer self)
        {
            Log.Debug("Call hero button click");
            long AccountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            M2C_CallHeroCardResponse callHeroCardResponse;
            try
            {
                try
                {
                    callHeroCardResponse = (M2C_CallHeroCardResponse) await self.ZoneScene().GetComponent<SessionComponent>().Session
                            .Call(new C2M_CallHeroCardRequest() { Account = AccountId });
                    Log.Debug($"call hero card response  {callHeroCardResponse.Error}");
                    HeroCardInfo heroCardInfo = callHeroCardResponse.HeroCardInfo;
                    self.HeroCardInfos.Add(heroCardInfo);
                }
                finally
                {
                }
            }
            catch (Exception e)
            {
                Log.Error($"call hero request {e}");
            }

            self.AddUIScrollItems(ref self.ItemHeroCards, self.HeroCardInfos.Count);
            self.View.ELoopScrollListHeroLoopVerticalScrollRect.SetVisible(true, self.HeroCardInfos.Count);

            await ETTask.CompletedTask;
        }

        public static async void ShowWindow(this DlgCallHeroLayer self, Entity contextData = null)
        {
            Log.Debug("show call hero layer window");
            // self.AddUIScrollItems(ref  self.ItemHeroCards, 100);
            // self.View.ELoopScrollListHeroLoopVerticalScrollRect.SetVisible(true, 100);
            //todo 请求服务器当前玩家拥有那些卡牌
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

            await ETTask.CompletedTask;
        }

        public static void HideWindow(this DlgCallHeroLayer self)
        {
            self.RemoveUIScrollItems(ref self.ItemHeroCards);
        }

        public static void OnLoopListItemRefreshHandler(this DlgCallHeroLayer self, Transform transform, int index)
        {
            Scroll_ItemHeroCard scrollItemHeroCard = self.ItemHeroCards[index].BindTrans(transform);
            scrollItemHeroCard.E_TextText.text = $"{self.HeroCardInfos[index].HeroName}";
        }
    }
}