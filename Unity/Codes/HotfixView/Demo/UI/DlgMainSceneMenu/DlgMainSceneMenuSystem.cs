using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgMainSceneMenuSystem
    {
        public static void RegisterUIEvent(this DlgMainSceneMenu self)
        {
            List<GameObject> list = new List<GameObject>();
            list.Add(self.View.E_MainImage.gameObject);
            list.Add(self.View.E_CallImage.gameObject);
            list.Add(self.View.E_HeroImage.gameObject);
            list.Add(self.View.E_BagImage.gameObject);
            list.Add(self.View.E_ShopImage.gameObject);
            self.InitAllToggleEventHandler(list);
        }

        public static void InitToggleEventHandler(this DlgMainSceneMenu self, GameObject obj)
        {
            obj.GetComponent<Toggle>().onValueChanged.AddListener((value) =>
            {
                Log.Debug($"init toggle event handler{value}");
                if (value)
                {
                    obj.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 240);
                    obj.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 240);
                    self.ClickMainMenu(obj.name);
                }
                else
                {
                    obj.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 220);
                    obj.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 220);
                }
            });
        }

        public static void ClickMainMenu(this DlgMainSceneMenu self, string buttonName)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            uiComponent.HideWindow(WindowID.WindowID_AccountInfo);
            uiComponent.HideWindow(WindowID.WindowID_MessageTaskActiveInfo);
            uiComponent.HideWindow(WindowID.WindowID_SettingUI);
            uiComponent.HideWindow(WindowID.WindowID_FormationUI);
            uiComponent.HideWindow(WindowID.WindowID_HeroInfoLayerUI);
            uiComponent.HideWindow(WindowID.WindowID_CallHeroLayer);
            uiComponent.HideWindow(WindowID.WindowID_ShowHeroInfoLayer);
            if (buttonName.Equals(self.View.E_MainImage.name))
            {
                Log.Debug("hero main");
                uiComponent.ShowWindow(WindowID.WindowID_AccountInfo);
                uiComponent.ShowWindow(WindowID.WindowID_MessageTaskActiveInfo);
                uiComponent.ShowWindow(WindowID.WindowID_SettingUI);
                uiComponent.ShowWindow(WindowID.WindowID_FormationUI);
            }
            else if (buttonName.Equals(self.View.E_HeroImage.name))
            {
                Log.Debug("hero label");
                uiComponent.ShowWindow(WindowID.WindowID_HeroInfoLayerUI);
            }
            else if (buttonName.Equals(self.View.E_CallImage.name))
            {
                Log.Debug("call label");
                uiComponent.ShowWindow(WindowID.WindowID_CallHeroLayer);
                uiComponent.ShowWindow(WindowID.WindowID_MainSceneMenu);
                uiComponent.ShowWindow(WindowID.WindowID_GoldInfoUI);
            }
            else if (buttonName.Equals(self.View.E_BagImage.name))
            {
                Log.Debug("bag label");
            }
            else
            {
                Log.Debug("Shop label");
            }
            // uiComponent.HideWindow(WindowID.WindowID_CallHeroLayer);
        }

        public static void InitAllToggleEventHandler(this DlgMainSceneMenu self, List<GameObject> list)
        {
            foreach (var obj in list)
            {
                self.InitToggleEventHandler(obj);
            }
        }

        public static async ETTask PvEButtonClick(this DlgMainSceneMenu self)
        {
            Log.Debug("pve button click");
            self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_EditorTroopLayer);
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_MainScene);

            await ETTask.CompletedTask;
        }

        public static void ShowWindow(this DlgMainSceneMenu self, Entity contextData = null)
        {
            Log.Debug("macin scene show window");
          
        }
    }
}