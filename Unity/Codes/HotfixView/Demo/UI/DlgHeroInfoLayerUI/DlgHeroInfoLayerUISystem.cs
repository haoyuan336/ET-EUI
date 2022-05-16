using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using ET.Account;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace ET
{
    public static class DlgHeroInfoLayerUISystem
    {
        public static void RegisterUIEvent(this DlgHeroInfoLayerUI self)
        {
        }

        public static void HideWindow(this DlgHeroInfoLayerUI self)
        {
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_AllHeroBagLayer);
        }

        public static async void ShowWindow(this DlgHeroInfoLayerUI self, Entity contextData = null)
        {
            await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_AllHeroBagLayer);
            await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_MainSceneMenu);
            await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_GoldInfoUI);

            UIBaseWindow baseWindow = self.DomainScene().GetComponent<UIComponent>().AllWindowsDic[(int) WindowID.WindowID_AllHeroBagLayer];
            baseWindow.GetComponent<DlgAllHeroBagLayer>().OnHeroItemInfoClick = self.OnClickHeroItem;
            baseWindow.uiTransform.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        }

        public static async void OnClickHeroItem(this DlgHeroInfoLayerUI self, HeroCardInfo heroCardInfo, Scroll_ItemHeroCard itemHeroCard, bool value)
        {
            var config = HeroConfigCategory.Instance.Get(heroCardInfo.ConfigId);
            if (config.MaterialType == 2)
            {
                itemHeroCard.E_ChooseToggle.isOn = false;
                return;
            }

            if (value)
            {
                itemHeroCard.E_ChooseToggle.isOn = false;
                self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_HeroInfoLayerUI);
                self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_MainSceneBg);
                // Log.Debug($"click hero {heroCard.HeroCardInfo.HeroId}");
                await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_ShowHeroInfoLayer);
                // self.DomainScene().GetComponent<UIComponent>().GetChild<UIBaseWindow>(WindowID.WindowID_ShowHeroInfoLayer);

                // UIEventComponent.Instance.GetUIEventHandler(WindowID.WindowID_ShowHeroInfoLayer).OnInitWindowCoreData(baseWindow);
                UIBaseWindow baseWindow = self.DomainScene().GetComponent<UIComponent>().AllWindowsDic[(int) WindowID.WindowID_ShowHeroInfoLayer];
                baseWindow.GetComponent<DlgShowHeroInfoLayer>().SetHeroInfo(heroCardInfo);
            }
            
           
        }
    }
}