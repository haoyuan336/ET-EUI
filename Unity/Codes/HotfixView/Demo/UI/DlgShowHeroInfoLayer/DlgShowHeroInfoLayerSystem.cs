using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace ET
{
    public static class DlgShowHeroInfoLayerSystem
    {
        public static void RegisterUIEvent(this DlgShowHeroInfoLayer self)
        {
            self.View.E_BackButton.AddListener(() => { self.BackButtonClick(); });
        }

        public static async void BackButtonClick(this DlgShowHeroInfoLayer self)
        {
            await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_MainSceneBg);
            // await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_MainSceneMenu);
            await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_HeroInfoLayerUI);
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_ShowHeroInfoLayer);
        }

        public static async void ShowWindow(this DlgShowHeroInfoLayer self, Entity contextData = null)
        {
            var heroCardItem = (Scroll_ItemHeroCard) contextData;
            // self.View.E_ATKText.text = $"ATK:{heroCardItem.HeroCardInfo.Attack}";
            // self.View.E_HPText.text = $"HP:{heroCardItem.HeroCardInfo.HP}";
            // self.View.E_DEFText.text = $"DEF:{heroCardItem.HeroCardInfo.Defence}";
            self.View.E_HeroNameText.text = heroCardItem.HeroCardInfo.HeroName;
            self.View.E_LevelText.text = $"Lv:{heroCardItem.HeroCardInfo.Level}";

            HeroConfig config = HeroConfigCategory.Instance.Get(heroCardItem.HeroCardInfo.ConfigId);
            string heroModeStr = config.HeroMode;
            GameObject Prefab =  await AddressableComponent.Instance.LoadAssetByPathAsync<GameObject>(heroModeStr);
            self.HeroModeShow = GameObject.Instantiate(Prefab);

            Vector3 center = self.HeroModeShow.GetComponentInChildren<SkinnedMeshRenderer>().bounds.center;
            
            GameObject obj = GameObject.Find("MainCameraLockLook");
            obj.transform.position = center;
            GameObject mainSceneHeroBg = GameObject.Find("MainSceneHeroBG");
            mainSceneHeroBg.transform.position = center + self.HeroModeShow.transform.forward * -10;
        }

        public static void HideWindow(this DlgShowHeroInfoLayer self)
        {
            // Log.Debug("hide window show hero info layer");
            GameObject.Destroy(self.HeroModeShow);
        }
    }
}