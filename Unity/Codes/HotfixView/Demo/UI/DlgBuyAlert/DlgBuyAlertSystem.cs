using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics.Eventing.Reader;
using ET.Account;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace ET
{
    public static class DlgBuyAlertSystem
    {
        public static void RegisterUIEvent(this DlgBuyAlert self)
        {
            self.View.E_CancelButton.AddListener(() => { self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_BuyAlert); });
            self.View.E_OKButton.AddListener(self.OkBuy);
        }

        public static void OkBuy(this DlgBuyAlert self)
        {
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_BuyAlert);
            //todo 向服务器发起购买武器的请求
            if (self.OkButtonClickAction != null)
            {
                self.OkButtonClickAction(self.Config);
            }

            // Log.Debug($"config id {self.Config.Id}");
            // // M2C_BuyWeaponResponse response
            // Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            // var accountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            // M2C_BuyWeaponResponse response = (M2C_BuyWeaponResponse) await session.Call(new C2M_BuyWeaponRequest()
            // {
            //     Count = 1, ConfigId = self.Config.Id, AccountId = accountId
            // });
            // if (response.Error == ErrorCode.ERR_Success)
            // {
            //     Log.Debug("购买成功");
            //     self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_BuyAlert);
            //     self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_GoldInfoUI).Coroutine();
            // }
            //
            // await ETTask.CompletedTask;
        }

        public static void ShowWindow(this DlgBuyAlert self, Entity contextData = null)
        {
        }

        public static void InitWithItemConfig(this DlgBuyAlert self, ItemConfig config)
        {
            self.View.E_IconImage.sprite = null;
            self.View.E_NameText.text = config.Des;
        }

        public static async void InitWithWeaponConfig(this DlgBuyAlert self, WeaponsConfig config)
        {
            self.View.E_NameText.text = config.Name;

            var sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(ConstValue.WeaponAtlasPath, config.IconResName);
            self.View.E_IconImage.sprite = sprite;
        }

        public static async void SetInfo(this DlgBuyAlert self, GoodsConfig config)
        {
            self.Config = config;

            switch (config.GoodsType)
            {
                case (int) GoodsType.Item:
                    ItemConfig itemConfig = ItemConfigCategory.Instance.Get(config.ConfigId);
                    self.InitWithItemConfig(itemConfig);
                    break;
                case (int) GoodsType.Weapon:
                    WeaponsConfig weaponsConfig = WeaponsConfigCategory.Instance.Get(config.ConfigId);
                    self.InitWithWeaponConfig(weaponsConfig);
                    break;
            }

            // var 

            var moneyType = config.MoneyType;
            var moneyConfig = ItemConfigCategory.Instance.Get(moneyType);
            var moneySprite =
                    await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(ConstValue.CommonUIAtlasPath, moneyConfig.IconImageStr);
            self.View.E_MoneyIconImage.sprite = moneySprite;
            self.View.E_MoneyText.text = $"X{config.Price}";

            var goldInfo = await self.GetSelfGoldInfo();
            if (goldInfo != null)
            {
                List<ItemInfo> items = goldInfo.ItemInfos;
                var itemInfo = items.Find((a) => { return a.ConfigId.Equals(moneyType); });
                if (itemInfo != null)
                {
                    Log.Debug($"item info count {itemInfo.Count}");
                    Log.Debug($"prece count{config.Price}");
                    if (itemInfo.Count < config.Price)
                    {
                        self.View.E_MoneyText.color = Color.red;
                    }
                    else
                    {
                        self.View.E_MoneyText.color = Color.white;
                    }
                }
            }
            //获取自己的钻石数量
        }

        public static async ETTask<M2C_GetGoldInfoResponse> GetSelfGoldInfo(this DlgBuyAlert self)
        {
            long AccountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            M2C_GetGoldInfoResponse response = (M2C_GetGoldInfoResponse) await session.Call(new C2M_GetGoldInfoRequest() { AccountId = AccountId });
            if (response.Error == ErrorCode.ERR_Success)
            {
                // Log.Debug("增加体力值成功");
                // self.View.E_PowerText.text = $"{response.Count}";

                return response;
            }

            return null;
        }
    }
}