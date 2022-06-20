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
            // List<GameObject> list = new List<GameObject>();
            // list.Add(self.View.E_MainImage.gameObject);
            // list.Add(self.View.E_CallImage.gameObject);
            // list.Add(self.View.E_HeroImage.gameObject);
            // list.Add(self.View.E_BagImage.gameObject);
            // list.Add(self.View.E_ShopImage.gameObject);
            // self.InitAllToggleEventHandler(list);
            self.View.E_MainToggle.AddListener((value) =>
            {
                if (value)
                {
                    self.ClickMainMenu(self.View.E_MainToggle.name);
                }
            });
            self.View.E_CallToggle.AddListener((value) =>
            {
                if (value)
                {
                    self.ClickMainMenu(self.View.E_CallToggle.name);
                }
            });
            self.View.E_HeroToggle.AddListener((value) =>
            {
                if (value)
                {
                    self.ClickMainMenu(self.View.E_HeroToggle.name);
                }
            });
            self.View.E_BagToggle.AddListener((value) =>
            {
                if (value)
                {
                    self.ClickMainMenu(self.View.E_BagToggle.name);
                }
            });
            self.View.E_ShopToggle.AddListener((value) =>
            {
                if (value)
                {
                    self.ClickMainMenu(self.View.E_ShopToggle.name);
                }
            });
        }

        // public static void InitToggleEventHandler(this DlgMainSceneMenu self, GameObject obj)
        // {
            // obj.GetComponent<Toggle>().onValueChanged.AddListener((value) =>
            // {
            //     // if (value && obj.GetComponent<Toggle>().isOn)
            //     // {
            //     //     return;
            //     // }
            //
            //     Log.Debug($"init toggle event handler{value}");
            //     if (value)
            //     {
            //         // obj.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 240);
            //         // obj.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 240);
            //         self.ClickMainMenu(obj.name);
            //     }
            //     // else
            //     // {
            //     //     obj.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 220);
            //     //     obj.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 220);
            //     // }
            // });
        // }

        public static async void ClickMainMenu(this DlgMainSceneMenu self, string buttonName)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            uiComponent.HideWindow(WindowID.WindowID_AccountInfo);
            uiComponent.HideWindow(WindowID.WindowID_MessageTaskActiveInfo);
            uiComponent.HideWindow(WindowID.WindowID_SettingUI);
            uiComponent.HideWindow(WindowID.WindowID_FormationUI);
            uiComponent.HideWindow(WindowID.WindowID_HeroInfoLayerUI);
            uiComponent.HideWindow(WindowID.WindowID_CallHeroLayer);
            uiComponent.HideWindow(WindowID.WindowID_ShowHeroInfoLayer);
            uiComponent.HideWindow(WindowID.WindowID_Store);
            uiComponent.HideWindow(WindowID.WindowID_MainScene);
            uiComponent.HideWindow(WindowID.WindowID_BagLayer);
            // uiComponent.HideWindow(WindowID.WindowID_MainSceneBg);
            if (buttonName.Equals(self.View.E_MainToggle.name))
            {
                Log.Debug("hero main");
                await uiComponent.ShowWindow(WindowID.WindowID_AccountInfo);
                await uiComponent.ShowWindow(WindowID.WindowID_MessageTaskActiveInfo);
                await uiComponent.ShowWindow(WindowID.WindowID_SettingUI);
                await uiComponent.ShowWindow(WindowID.WindowID_FormationUI);
                await uiComponent.ShowWindow(WindowID.WindowID_MainScene);
            }
            else if (buttonName.Equals(self.View.E_HeroToggle.name))
            {
                Log.Debug("hero label");
                await uiComponent.ShowWindow(WindowID.WindowID_HeroInfoLayerUI);
            }
            else if (buttonName.Equals(self.View.E_CallToggle.name))
            {
                Log.Debug("call label");
                await uiComponent.ShowWindow(WindowID.WindowID_CallHeroLayer);
                await uiComponent.ShowWindow(WindowID.WindowID_MainSceneMenu);
                await uiComponent.ShowWindow(WindowID.WindowID_GoldInfoUI);
            }
            else if (buttonName.Equals(self.View.E_BagToggle.name))
            {
                Log.Debug("bag label");
                await uiComponent.ShowWindow(WindowID.WindowID_BagLayer);
                await uiComponent.ShowWindow(WindowID.WindowID_MainSceneMenu);
                await uiComponent.ShowWindow(WindowID.WindowID_GoldInfoUI);
            }
            else if (buttonName.Equals(self.View.E_ShopToggle.name))

            {
                await uiComponent.ShowWindow(WindowID.WindowID_Store);
                Log.Debug("Shop label");
            }
            // uiComponent.HideWindow(WindowID.WindowID_CallHeroLayer);
        }

        // public static void InitAllToggleEventHandler(this DlgMainSceneMenu self, List<GameObject> list)
        // {
        //     foreach (var obj in list)
        //     {
        //         self.InitToggleEventHandler(obj);
        //     }
        // }

        public static async ETTask PvEButtonClick(this DlgMainSceneMenu self)
        {
            Log.Debug("pve button click");
            await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_EditorTroopLayer);
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_MainScene);

            await ETTask.CompletedTask;
        }

        public static void ShowWindow(this DlgMainSceneMenu self, Entity contextData = null)
        {
            Log.Debug("macin scene show window");
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_GameLevelLayer);
        }
    }
}