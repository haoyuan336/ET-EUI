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
            self.View.E_ItemToggle.AddListener((result) =>
            {
                if (result)
                {
                    self.ShowAllItem();
                }
            });
            self.View.E_WeaponToggle.AddListener((result) =>
            {
                if (result)
                {
                    self.ShowAllWeapon();
                }
            });
            self.View.E_StoreToggle.AddListener((result) => { });
            self.View.ELoopScrollListLoopVerticalScrollRect.AddItemRefreshListener(self.OnScrollListEvennt);
            self.ShowAllWeapon();
        }

        public static void OnScrollListEvennt(this DlgStore self, Transform transform, int i)
        {
            Scroll_ItemGoods item = self.ItemGoods[i].BindTrans(transform);
            GoodsConfig config = self.GoodsConfigs[i];
            item.E_ChooseToggle.onValueChanged.RemoveAllListeners();

            item.E_LevelText.gameObject.SetActive(false);
            item.E_QualityIconImage.gameObject.SetActive(false);

            item.InitGoodsInfo(config);
            item.E_ChooseToggle.AddListener((value) =>
            {
                if (value)
                {
                    self.OnGoodsItemClick(config);
                    Log.Debug("道具被点击了");
                }

                item.E_ChooseToggle.isOn = false;
            });
            // var sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(ConstValue.WeaponAtlasPath, config.IconResName);
            // itemWeapon.E_WeaponImage.sprite = sprite;
        }

        public static async void OnGoodsItemClick(this DlgStore self, GoodsConfig config)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_BuyAlert);
            UIBaseWindow baseWindow = uiComponent.AllWindowsDic[(int) WindowID.WindowID_BuyAlert];
            baseWindow.GetComponent<DlgBuyAlert>().SetInfo(config);
            baseWindow.GetComponent<DlgBuyAlert>().OkButtonClickAction = self.OnBuyButtonClick;
        }

        public static async void OnBuyButtonClick(this DlgStore self, GoodsConfig config)
        {
            Log.Debug("ok buy button click");
            // // Log.Debug($"config id {self.Config.Id}");

            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            var accountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;

            switch (config.GoodsType)
            {
                case (int) GoodsType.Item:
                {
                    M2C_BuyGoodsResponse response = (M2C_BuyGoodsResponse) await session.Call(new C2M_BuyGoodsRequest()
                    {
                        Count = 1, ConfigId = config.Id, AccountId = accountId
                    });
                    if (response.Error == ErrorCode.ERR_Success)
                    {
                        Log.Debug("success ");
                        UIBaseWindow baseWindow = self.DomainScene().GetComponent<UIComponent>().GetUIBaseWindow(WindowID.WindowID_GoldInfoUI);
                        baseWindow.GetComponent<DlgGoldInfoUI>().ReferGoldInfo();
                    }
                }

                    break;
                case (int) GoodsType.Weapon:
                {
                    M2C_BuyWeaponsResponse response = (M2C_BuyWeaponsResponse) await session.Call(new C2M_BuyWeaponsRequest()
                    {
                        Count = 1, ConfigId = config.Id, AccountId = accountId
                    });
                    if (response.Error == ErrorCode.ERR_Success)
                    {
                        Log.Debug("buy weapon success ");
                        UIBaseWindow baseWindow = self.DomainScene().GetComponent<UIComponent>().GetUIBaseWindow(WindowID.WindowID_GoldInfoUI);
                        baseWindow.GetComponent<DlgGoldInfoUI>().ReferGoldInfo();
                    }
                }
                    break;
            }
        }

        /// <summary>
        /// 显示所有的武器
        /// </summary>
        public static void ShowAllWeapon(this DlgStore self)
        {
            List<GoodsConfig> configs = GoodsConfigCategory.Instance.GetAll().Values.ToList();
            self.GoodsConfigs.Clear();
            foreach (var config in configs)
            {
                if (config.GoodsType == (int) GoodsType.Weapon)
                {
                    self.GoodsConfigs.Add(config);
                }
            }

            self.AddUIScrollItems(ref self.ItemGoods, self.GoodsConfigs.Count);
            self.View.ELoopScrollListLoopVerticalScrollRect.SetVisible(true, self.GoodsConfigs.Count);
        }

        /// <summary>
        /// 显示所有的道具
        /// </summary>
        /// <param name="self"></param>
        public static void ShowAllItem(this DlgStore self)
        {
            List<GoodsConfig> configs = GoodsConfigCategory.Instance.GetAll().Values.ToList();
            self.GoodsConfigs.Clear();
            foreach (var config in configs)
            {
                if (config.GoodsType == (int) GoodsType.Item)
                {
                    self.GoodsConfigs.Add(config);
                }
            }

            Log.Debug($"show item goos {self.GoodsConfigs.Count}");
            self.AddUIScrollItems(ref self.ItemGoods, self.GoodsConfigs.Count);
            self.View.ELoopScrollListLoopVerticalScrollRect.SetVisible(true, self.GoodsConfigs.Count);
        }

        public static async void ShowWindow(this DlgStore self, Entity contextData = null)
        {
            //加载武器资源 
            await ETTask.CompletedTask;
        }
    }
}