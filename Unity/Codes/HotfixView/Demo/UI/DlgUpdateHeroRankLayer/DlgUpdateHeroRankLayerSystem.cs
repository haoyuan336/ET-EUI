using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

namespace ET
{
    public static class DlgUpdateHeroRankLayerSystem
    {
        public static void RegisterUIEvent(this DlgUpdateHeroRankLayer self)
        {
            self.View.E_BackButton.AddListener(() =>
            {
                self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_UpdateHeroRankLayer);
            });
            self.View.E_OkButtonButton.AddListenerAsync(self.OnUpdateRankButtonClick);
        }

        public static async ETTask OnUpdateRankButtonClick(this DlgUpdateHeroRankLayer self)
        {
            HeroConfig heroConfig = HeroConfigCategory.Instance.Get(self.HeroCardInfo.ConfigId);
            if (self.HeroCardInfo.Rank >= heroConfig.MaxRank)
            {
                return;
            }
            long accountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            C2M_UpdateHeroRankRequest request = new C2M_UpdateHeroRankRequest() { Account = accountId, HeroId = self.HeroCardInfo.HeroId };

            M2C_UpdateHeroRankResponse response = (M2C_UpdateHeroRankResponse) await session.Call(request);

            if (response.Error == ErrorCode.ERR_Success)
            {
                self.HeroCardInfo = response.HeroCardInfo;

                self.SetTargetInfo(self.HeroCardInfo);

                UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
                UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_ShowHeroInfoLayer);
                baseWindow.GetComponent<DlgShowHeroInfoLayer>().SetHeroInfo(self.HeroCardInfo);
            }

            await ETTask.CompletedTask;
        }

        public static async void InitHeroHeadImage(this DlgUpdateHeroRankLayer self, HeroCardInfo heroCardInfo)
        {
            var spriteAtlas = ConstValue.HeroCardAtlasPath;
            HeroConfig cardInfo = HeroConfigCategory.Instance.Get(heroCardInfo.ConfigId);
            var sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(spriteAtlas, cardInfo.HeroIconImage);
            self.View.E_HeadImage.sprite = sprite;
        }

        public static void SetTargetInfo(this DlgUpdateHeroRankLayer self, HeroCardInfo heroCardInfo)
        {
            self.HeroCardInfo = heroCardInfo;
            self.InitHeroHeadImage(heroCardInfo);

            HeroConfig heroConfig = HeroConfigCategory.Instance.Get(heroCardInfo.ConfigId);
            if (heroCardInfo.Rank >= heroConfig.MaxRank)
            {
                // self.View.E_CurrentRankText.gameObject.SetActive();
                self.View.E_CurrentRankText.text = $"MAX阶";
                self.View.E_NextRankText.text = $"--阶";
            }
            else
            {
                self.View.E_CurrentRankText.text = $"{heroCardInfo.Rank}阶";
                self.View.E_NextRankText.text = $"{heroCardInfo.Rank + 1}阶";
            }
        }

        public static void ShowWindow(this DlgUpdateHeroRankLayer self, Entity contextData = null)
        {
        }
    }
}