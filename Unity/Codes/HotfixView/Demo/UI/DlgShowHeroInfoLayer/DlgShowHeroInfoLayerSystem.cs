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
            await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_HeroInfoLayerUI);
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_ShowHeroInfoLayer);
        }

        public static async void SetHeroInfo(this DlgShowHeroInfoLayer self, HeroCardInfo heroCardInfo)
        {
            Log.Debug("Set hero info");

            self.View.E_HeroNameText.text = heroCardInfo.HeroName;
            self.View.E_LevelText.text = $"Lv:{heroCardInfo.Level}";
            HeroConfig config = HeroConfigCategory.Instance.Get(heroCardInfo.ConfigId);
            string heroModeStr = config.HeroMode;
            self.HeroModeShow = await AddressableComponent.Instance.LoadGameObjectAndInstantiateByPath(heroModeStr);
            Vector3 center = self.HeroModeShow.GetComponentInChildren<SkinnedMeshRenderer>().bounds.center;
            GameObject obj = GameObject.Find("MainCameraLockLook");
            obj.transform.position = center;
            GameObject mainSceneHeroBg = GameObject.Find("MainSceneHeroBG");
            mainSceneHeroBg.transform.position = center + self.HeroModeShow.transform.forward * -10;
        }

        public static async void ShowWindow(this DlgShowHeroInfoLayer self, Entity contextData = null)
        {
            await ETTask.CompletedTask;
        }

        public static void HideWindow(this DlgShowHeroInfoLayer self)
        {
            // Log.Debug("hide window show hero info layer");
            GameObject.Destroy(self.HeroModeShow);
        }
    }
}