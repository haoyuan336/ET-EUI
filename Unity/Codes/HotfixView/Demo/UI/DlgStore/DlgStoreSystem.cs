using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgStoreSystem
    {
        public static void RegisterUIEvent(this DlgStore self)
        {
            self.View.E_ItemToggle.onValueChanged.AddListener((result) =>
            {
                if (result)
                {
                    self.ShowAllItem();
                    self.View.ELoopScrollListLoopVerticalScrollRect.gameObject.SetActive(false);
                }
            });
            self.View.E_WeaponToggle.onValueChanged.AddListener((result) =>
            {
                if (result)
                {
                    self.ShowAllWeapon();
                }
            });
            self.View.E_StoreToggle.onValueChanged.AddListener((result) =>
            {
                self.View.ELoopScrollListLoopVerticalScrollRect.gameObject.SetActive(false);
            });
            self.View.ELoopScrollListLoopVerticalScrollRect.AddItemRefreshListener(self.OnScrollListEvennt);
            self.ShowAllWeapon();
        }

        public static async void OnWeaponIconClick(this DlgStore self, WeaponsConfig config)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_BuyAlert);
            UIBaseWindow baseWindow = uiComponent.AllWindowsDic[(int) WindowID.WindowID_BuyAlert];
            baseWindow.GetComponent<DlgBuyAlert>().SetInfo(config);
        }

        public static async void OnScrollListEvennt(this DlgStore self, Transform transform, int i)
        {
            Scroll_ItemWeapon itemWeapon = self.ItemWeapons[i].BindTrans(transform);
            WeaponsConfig config = self.WeaponConfigs[i];
            itemWeapon.E_ClickToggle.onValueChanged.RemoveAllListeners();
            itemWeapon.E_ClickToggle.onValueChanged.AddListener((value) =>
            {
                Log.Debug($"index {i}");
                Log.Debug($"config materialtype {config.Type}");
                if (value)
                {
                    self.OnWeaponIconClick(config);
                }
                itemWeapon.E_ClickToggle.isOn = false;
            });
            var sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(ConstValue.WeaponAtlasPath, config.IconResName);
            itemWeapon.E_WeaponImage.sprite = sprite;
        }

        /// <summary>
        /// 显示所有的武器
        /// </summary>
        public static void ShowAllWeapon(this DlgStore self)
        {
            self.WeaponConfigs = WeaponsConfigCategory.Instance.GetAll().Values.ToList();
            self.AddUIScrollItems(ref self.ItemWeapons, self.WeaponConfigs.Count);
            self.View.ELoopScrollListLoopVerticalScrollRect.SetVisible(true, self.WeaponConfigs.Count);
        }

        /// <summary>
        /// 显示所有的道具
        /// </summary>
        /// <param name="self"></param>
        public static void ShowAllItem(this DlgStore self)
        {
        }

        public static async void ShowWindow(this DlgStore self, Entity contextData = null)
        {
            //加载武器资源 
            await ETTask.CompletedTask;
        }
    }
}