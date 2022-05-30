using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace ET
{
    public static class DlgUpdateHeroStarLayerSystem
    {
        public static void RegisterUIEvent(this DlgUpdateHeroStarLayer self)
        {
            self.View.E_BackButton.AddListener(() =>
            {
                self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_UpdateHeroStarLayer);
            });
            self.View.E_OkButtonButton.AddListenerAsync(self.OkButtonClick);
        }

        public static async ETTask OkButtonClick(this DlgUpdateHeroStarLayer self)
        {
            long accountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            C2M_UpdateHeroStarRequest request = new C2M_UpdateHeroStarRequest() { Account = accountId, HeroId = self.HeroCardInfo.HeroId };

            M2C_UpdateHeroStarResponse response = (M2C_UpdateHeroStarResponse) await session.Call(request);
            if (response.Error == ErrorCode.ERR_Success)
            {
                self.SetTargetInfo(response.HeroCardInfo);
                UIBaseWindow baseWindow = self.DomainScene().GetComponent<UIComponent>().GetUIBaseWindow(WindowID.WindowID_ShowHeroInfoLayer);
                baseWindow.GetComponent<DlgShowHeroInfoLayer>().SetHeroInfo(response.HeroCardInfo);
            }

            await ETTask.CompletedTask;
        }

        public static void ShowWindow(this DlgUpdateHeroStarLayer self, Entity contextData = null)
        {
        }

        public static async void InitHeadImage(this DlgUpdateHeroStarLayer self, HeroCardInfo heroCardInfo)
        {
            HeroConfig config = HeroConfigCategory.Instance.Get(heroCardInfo.ConfigId);
            var spriteAltas = ConstValue.HeroCardAtlasPath;
            Sprite sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(spriteAltas, config.HeroIconImage);
            self.View.E_HeadImage.sprite = sprite;
        }

        public static async void InitStarView(this DlgUpdateHeroStarLayer self, HeroCardInfo heroCardInfo)
        {
            var starCount = heroCardInfo.Star;

            // var starGroup = UIFindHelper.FindDeepChild(self.View.E_StarGroupButton.gameObject, "StarGroup");

            var spriteAtlas = ConstValue.CommonUIAtlasPath;
            Transform tr = self.View.E_StarGroupButton.transform;
            for (int i = 0; i < 5; i++)
            {
                var child = tr.GetChild(i);
                if (i < starCount)
                {
                    var sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(spriteAtlas, "star");
                    child.GetComponent<Image>().sprite = sprite;
                }
                else
                {
                    var sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(spriteAtlas, "StarTexture");
                    child.GetComponent<Image>().sprite = sprite;
                }
            }
        }

        public static async void InitBaseInfo(this DlgUpdateHeroStarLayer self, HeroCardInfo heroCardInfo)
        {
            //初始化基础数据

            // TimerComponent
            // UIFindHelper.FindDeepChild()
            if (heroCardInfo.Star >= 5)
            {
                self.View.E_MaxText.gameObject.SetActive(true);
                self.View.E_BaseInfoButton.gameObject.SetActive(false);
                return;
            }

            self.View.E_MaxText.gameObject.SetActive(false);
            self.View.E_BaseInfoButton.gameObject.SetActive(true);
            self.View.E_CurrentAttackText.text = $"{HeroHelper.GetHeroBaseAttack(heroCardInfo)}";
            self.View.E_CurrentHPText.text = $"{HeroHelper.GetHeroBaseHP(heroCardInfo)}";
            self.View.E_CurrentDefenceText.text = $"{HeroHelper.GetHeroBaseDefence(heroCardInfo)}";

            var addAttack = HeroHelper.GetNextStarHeroBaseAttack(heroCardInfo) - HeroHelper.GetHeroBaseAttack(heroCardInfo);
            var addDefence = HeroHelper.GetNextStarHeroBaseDefence(heroCardInfo) - HeroHelper.GetHeroBaseDefence(heroCardInfo);
            var addHP = HeroHelper.GetNextStarHeroBaseHP(heroCardInfo) - HeroHelper.GetHeroBaseHP(heroCardInfo);

            self.View.E_NextAttackText.text = $"{HeroHelper.GetNextStarHeroBaseAttack(heroCardInfo)}(+{addAttack})";
            self.View.E_NextDefenceText.text = $"{HeroHelper.GetNextStarHeroBaseDefence(heroCardInfo)}(+{addDefence})";
            self.View.E_NextHPText.text = $"{HeroHelper.GetNextStarHeroBaseHP(heroCardInfo)}(+{addHP})";

            await ETTask.CompletedTask;
        }

        public static void SetTargetInfo(this DlgUpdateHeroStarLayer self, HeroCardInfo heroCardInfo)
        {
            self.HeroCardInfo = heroCardInfo;
            self.InitHeadImage(heroCardInfo);
            self.InitBaseInfo(heroCardInfo);
            self.InitStarView(heroCardInfo);
        }
    }
}