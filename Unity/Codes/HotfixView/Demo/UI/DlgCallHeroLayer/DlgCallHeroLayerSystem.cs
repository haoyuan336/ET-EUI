using System.Collections;
using System.Collections.Generic;
using System;
using ET.Account;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgCallHeroLayerSystem
    {
        public static void RegisterUIEvent(this DlgCallHeroLayer self)
        {
            self.View.ELoopScrollListHeroLoopVerticalScrollRect.AddItemRefreshListener((tr, index) => { self.RefershListener(tr, index); });
        }

        public static void RefershListener(this DlgCallHeroLayer self, Transform transform, int index)
        {
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
                if (m2CGetAllHeroCardListResponse.Error != ErrorCode.ERR_Success)
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
        }
    }
}