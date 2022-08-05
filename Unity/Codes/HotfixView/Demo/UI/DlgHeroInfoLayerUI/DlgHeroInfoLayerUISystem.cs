using UnityEngine;

namespace ET
{
    public static class DlgHeroInfoLayerUISystem
    {
        public static void RegisterUIEvent(this DlgHeroInfoLayerUI self)
        {
        }

        public static void HideWindow(this DlgHeroInfoLayerUI self)
        {
            var uiComponent = self.DomainScene().GetComponent<UIComponent>();
            uiComponent.HideWindow(WindowID.WindowID_AllHeroBagLayer);

            var goldInfoUIBaseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_GoldInfoUI);
            goldInfoUIBaseWindow.GetComponent<DlgGoldInfoUI>().ShowWidgetWithType(GoldInfoUIType.MainScene);
        }

        public static async void ShowWindow(this DlgHeroInfoLayerUI self, Entity contextData = null)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_AllHeroBagLayer);
            await uiComponent.ShowWindow(WindowID.WindowID_MainSceneMenu);
            await uiComponent.ShowWindow(WindowID.WindowID_GoldInfoUI);

            UIBaseWindow baseWindow = uiComponent.AllWindowsDic[(int)WindowID.WindowID_AllHeroBagLayer];
            baseWindow.GetComponent<DlgAllHeroBagLayer>().OnHeroItemInfoClick = self.OnClickHeroItem;
            baseWindow.GetComponent<DlgAllHeroBagLayer>().SetShowHeroType(HeroBagType.HeroAndMaterial);
            baseWindow.uiTransform.GetComponent<RectTransform>().offsetMax = new Vector2(0, -400);
            baseWindow.uiTransform.GetComponent<RectTransform>().offsetMin = new Vector2(0, 200);

            UIBaseWindow goldInfoBaseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_GoldInfoUI);
            goldInfoBaseWindow.GetComponent<DlgGoldInfoUI>().ShowWidgetWithType(GoldInfoUIType.HeroInfo);
        }

        public static async void OnClickHeroItem(this DlgHeroInfoLayerUI self, HeroCardInfo heroCardInfo, Scroll_ItemHeroCard itemHeroCard,
        bool value)
        {
            var config = HeroConfigCategory.Instance.Get(heroCardInfo.ConfigId);
            if (config.MaterialType == (int)HeroBagType.Materail)
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
                UIBaseWindow baseWindow = self.DomainScene().GetComponent<UIComponent>().AllWindowsDic[(int)WindowID.WindowID_ShowHeroInfoLayer];
                baseWindow.GetComponent<DlgShowHeroInfoLayer>().SetHeroInfo(heroCardInfo);
            }
        }
    }
}