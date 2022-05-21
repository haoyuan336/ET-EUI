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
            self.View.E_BackButton.AddListener(self.BackButtonClick);
            self.View.E_ComposeButton.AddListener(self.OnComposeButtonClick);
        }

        public static async void OnComposeButtonClick(this DlgShowHeroInfoLayer self)
        {
            // 点击强化按钮，显示强化页面
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_HeroStrengthenLayer);
            UIBaseWindow baseWindow = uiComponent.AllWindowsDic[(int)WindowID.WindowID_HeroStrengthenLayer];
            baseWindow.GetComponent<DlgHeroStrengthenLayer>().SetTargetInfo(self.HeroCardInfo);
        }

        public static async void BackButtonClick(this DlgShowHeroInfoLayer self)
        {
            await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_HeroInfoLayerUI);
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_ShowHeroInfoLayer);
        }

        public static async void SetHeroInfo(this DlgShowHeroInfoLayer self, HeroCardInfo heroCardInfo)
        {
            Log.Debug("Set hero info");
            self.HeroCardInfo = heroCardInfo;
            self.View.E_LevelText.text = $"Lv:{heroCardInfo.Level}";
            HeroConfig config = HeroConfigCategory.Instance.Get(heroCardInfo.ConfigId);
            string heroModeStr = config.HeroMode;
            self.View.E_HeroNameText.text = config.HeroName;

            self.HeroModeShow = await AddressableComponent.Instance.LoadGameObjectAndInstantiateByPath(heroModeStr);
            // Transform shadow = UIFindHelper.FindDeepChild(self.HeroModeShow.transform.parent.gameObject, "Shadow");
            // shadow.gameObject.SetActive(true);
            // Vector3 center = self.HeroModeShow.GetComponentInChildren<SkinnedMeshRenderer>().bounds.center;
            // GameObject obj = GameObject.Find("MainCameraLockLook");
            // obj.transform.position = center;
            // GameObject mainSceneHeroBg = GameObject.Find("MainSceneHeroBG");
            // mainSceneHeroBg.transform.position = center + self.HeroModeShow.transform.forward * -10;

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