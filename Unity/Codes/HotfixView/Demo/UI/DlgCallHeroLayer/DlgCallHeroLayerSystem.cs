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
            self.View.E_CallButton.AddListenerAsync(() => { return self.CallHeroButtonClick(); });
            // self.View.oNL
            
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

            await ETTask.CompletedTask;
        }

        public static async void ShowWindow(this DlgCallHeroLayer self, Entity contextData = null)
        {
            Log.Debug("show call hero layer window");
            // self.AddUIScrollItems(ref  self.ItemHeroCards, 100);
            // self.View.ELoopScrollListHeroLoopVerticalScrollRect.SetVisible(true, 100);
            await ETTask.CompletedTask;
        }

        public static void HideWindow(this DlgCallHeroLayer self)
        {
        }
    }
}