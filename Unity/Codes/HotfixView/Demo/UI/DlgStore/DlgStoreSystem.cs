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

        public static void OnScrollListEvennt(this DlgStore self, Transform transform, int i)
        {
            // transform.gameObject.GetComponent<>()
            Scroll_ItemWeapon itemHeroCard = self.ItemWeapons[i].BindTrans(transform);
            itemHeroCard.SetInfo(self.WeaponConfigs[i]);
        }

        /// <summary>
        /// 显示所有的武器
        /// </summary>
        public static void ShowAllWeapon(this DlgStore self)
        {
            self.WeaponConfigs = WeaponConfigCategory.Instance.GetAll().Values.ToList();
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
            var weaponSpriteAtlasStr = "Assets/Res/WeaponTextures/WeaponSpriteAtlas.spriteatlas";
            Sprite sp = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(weaponSpriteAtlasStr, "5s_ring_04");
            Log.Debug($"sp{sp.name}");
        }
    }
}