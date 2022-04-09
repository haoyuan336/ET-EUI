using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgMainSceneSystem
    {
        public static void RegisterUIEvent(this DlgMainScene self)
        {
            // self.View.E_BagButton.onClick.AddListener(() => { self.BagButtonClick();});
         
            self.View.E_PVEButton.AddListenerAsync(() => { return self.PvEButtonClick(); });
            List<GameObject> list = new List<GameObject>();
            list.Add(self.View.E_MainImage.gameObject);
            list.Add(self.View.E_CallImage.gameObject);
            list.Add(self.View.E_HeroImage.gameObject);
            list.Add(self.View.E_BagImage.gameObject);
            list.Add(self.View.E_ShopImage.gameObject);
            self.InitAllToggleEventHandler(list);
            
            // self.View.E_b.AddListenerAsync(() => { return self.BagButtonClick(); });
            // self.View.E_CallHeroButton.AddListenerAsync(() => { return self.CallHeroButtonClick(); });
        }
        public static async ETTask CallHeroButtonClick(this DlgMainScene self)
        {
            Log.Debug("call hero button click");
            self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_CallHeroLayer);
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_MainScene);
            await ETTask.CompletedTask;
        }

        public static async ETTask BagButtonClick(this DlgMainScene self)
        {
            Log.Debug("Bag button click");
            self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_BagLayer);
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_MainScene);

            await ETTask.CompletedTask;
        }
        public static void InitToggleEventHandler(this DlgMainScene self, GameObject obj)
        {
            obj.GetComponent<Toggle>().onValueChanged.AddListener((value) =>
            {
                Log.Debug($"init toggle event handler{value}");
                if (value)
                {
                    obj.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 220);
                    obj.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 220);
                }
                else
                {
                    obj.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200);
                    obj.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 200);
                }
            });
        }

        public static void InitAllToggleEventHandler(this DlgMainScene self, List<GameObject> list)
        {
            foreach (var obj in list)
            {
                self.InitToggleEventHandler(obj);
            }
        }
        public static async ETTask PvEButtonClick(this DlgMainScene self)
        {
            Log.Debug("pve button click");
            self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_EditorTroopLayer);
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_MainScene);
            
            await ETTask.CompletedTask;
        }

      

        public static void ShowWindow(this DlgMainScene self, Entity contextData = null)
        {
            Log.Debug("macin scene show window");
            self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_GoldInfoUI);
            self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_AccountInfo);
            self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_MessageTaskActiveInfo);
            self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_SettingUI);
        }
    }
}