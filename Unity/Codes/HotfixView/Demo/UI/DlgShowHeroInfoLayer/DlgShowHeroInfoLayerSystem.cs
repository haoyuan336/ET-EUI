using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace ET
{
    public static class DlgShowHeroInfoLayerSystem
    {
        public static void RegisterUIEvent(this DlgShowHeroInfoLayer self)
        {
            self.View.E_BackButton.AddListener(self.BackButtonClick);
            self.View.E_ComposeButton.AddListener(self.OnComposeButtonClick);
            self.View.E_UpRankButton.AddListener(self.OnUpRankButtonClick);
            self.View.E_UpStarButton.AddListener(self.OnUpStarButtonClick);
        }

        public static async void OnUpStarButtonClick(this DlgShowHeroInfoLayer self)
        {
            //升星系统
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_UpdateHeroStarLayer);
            UIBaseWindow uiBaseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_UpdateHeroStarLayer);
            uiBaseWindow.GetComponent<DlgUpdateHeroStarLayer>().SetTargetInfo(self.HeroCardInfo);
            await ETTask.CompletedTask;
        }

        public static async  void OnUpRankButtonClick(this DlgShowHeroInfoLayer self)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_UpdateHeroRankLayer);
            UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_UpdateHeroRankLayer);
            baseWindow.GetComponent<DlgUpdateHeroRankLayer>().SetTargetInfo(self.HeroCardInfo);
        }

        public static async void OnComposeButtonClick(this DlgShowHeroInfoLayer self)
        {
            // 点击强化按钮，显示强化页面
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_HeroStrengthenPreviewLayer);
            UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_HeroStrengthenPreviewLayer);
            baseWindow.GetComponent<DlgHeroStrengthenPreviewLayer>().SetTargetInfo(self.HeroCardInfo);

        }

        public static async void BackButtonClick(this DlgShowHeroInfoLayer self)
        {
            await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_HeroInfoLayerUI);
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_ShowHeroInfoLayer);
        }

        public static void ReferHeroCardView(this DlgShowHeroInfoLayer self, HeroCardInfo heroCardInfo)
        {
            // C2M_GetHeroInfosWithTroopIdRequest
            self.SetHeroInfo(heroCardInfo);
        }

        public static async void SetHeroInfo(this DlgShowHeroInfoLayer self, HeroCardInfo heroCardInfo)
        {
            Log.Debug("Set hero info");
            self.HeroCardInfo = heroCardInfo;
            self.View.E_LevelText.text = $"Lv:{heroCardInfo.Level}";
            HeroConfig config = HeroConfigCategory.Instance.Get(heroCardInfo.ConfigId);
            string heroModeStr = config.HeroMode;
            self.View.E_HeroNameText.text = config.HeroName;
            if (self.HeroModeShow == null)
            {
                self.HeroModeShow = await AddressableComponent.Instance.LoadGameObjectAndInstantiateByPath(heroModeStr);
            }
            self.View.E_RankText.text = $"{heroCardInfo.Rank}阶";
            var baseAttack = HeroHelper.GetHeroBaseAttack(heroCardInfo);
            var baseHP = HeroHelper.GetHeroBaseHP(heroCardInfo);
            var baseDefence = HeroHelper.GetHeroBaseDefence(heroCardInfo);
            self.View.E_BaseAttackText.text = $"{baseAttack}";
            self.View.E_HPText.text = $"{baseHP}";
            self.View.E_DefenceText.text = $"{baseDefence}";
            self.SethetoStar(heroCardInfo);
            self.SetElementInfo(heroCardInfo);
        }

        public static async void SetElementInfo(this DlgShowHeroInfoLayer self, HeroCardInfo heroCardInfo)
        {
            var configId = heroCardInfo.ConfigId;
            var config = HeroConfigCategory.Instance.Get(configId);
            var elementConfig = ElementConfigCategory.Instance.Get(config.HeroColor);
            var elementStr = elementConfig.IconImage;
            Sprite sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(ConstValue.HeroCardAtlasPath, elementStr);
            self.View.E_ElementImage.sprite = sprite;
        }

        public static void SethetoStar(this DlgShowHeroInfoLayer self, HeroCardInfo heroCardInfo)
        {
            for (int i = 0; i < 5; i++)
            {
                var starStr = $"Star_{i}";
                Transform tr = UIFindHelper.FindDeepChild(self.View.uiTransform.gameObject, starStr);
                tr.gameObject.SetActive(i < heroCardInfo.Star);
            }
        }

        public static async void ShowWindow(this DlgShowHeroInfoLayer self, Entity contextData = null)
        {
            await ETTask.CompletedTask;
        }

        public static void HideWindow(this DlgShowHeroInfoLayer self)
        {
            // Log.Debug("hide window show hero info layer");
            GameObject.Destroy(self.HeroModeShow);
            Transform shadow = UIFindHelper.FindDeepChild(GlobalComponent.Instance.Global.gameObject, "Shadow");
            shadow.gameObject.SetActive(false);
        }
    }
}