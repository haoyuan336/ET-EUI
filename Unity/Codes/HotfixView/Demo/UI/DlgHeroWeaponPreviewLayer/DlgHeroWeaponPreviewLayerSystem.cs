using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgHeroWeaponPreviewLayerSystem
    {
        public static void RegisterUIEvent(this DlgHeroWeaponPreviewLayer self)
        {
            self.View.E_BackButton.AddListener(self.BackButtonClick);
            // self.View.E_EquipButton.AddListenerAsync(self.ShowChooseEquipLayer);
            // self.View.E_WeaponButton.AddListenerAsync(self.ShowWeaponLayer);
            // self.View.E_RingButton.AddListenerAsync(self.ShowAllRingLayer);
            // self.View.E_AccessoryButton.AddListenerAsync(self.ShowAllAccessory);

            Transform[] lists =
            {
                self.View.E_EquipImage.transform, self.View.E_WeaponImage.transform, self.View.E_RingImage.transform,
                self.View.E_AccessoryImage.transform
            };
            WeaponType[] weaponTypes = { WeaponType.Equip, WeaponType.Weapon, WeaponType.Ring, WeaponType.Accessory };

            for (var i = 0; i < lists.Length; i++)
            {
                Transform transform = lists[i];
                ESCommonWeaponItem item = self.AddChildWithId<ESCommonWeaponItem, WeaponType, Transform>(IdGenerater.Instance.GenerateId(),
                    weaponTypes[i],
                    transform);
                self.WeaponItems.Add(item);
                item.OnWeaponItemClickAction = self.OnWeaponItemClick;
            }
        }

        public static async void OnWeaponItemClick(this DlgHeroWeaponPreviewLayer self, WeaponType type,ESCommonWeaponItem weaponItem, bool value)
        {
            if (value)
            {
                weaponItem.E_Choose.GetComponent<Toggle>().isOn = false;
                UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
                await uiComponent.ShowWindow(WindowID.WindowID_ChooseWeaponLayer);
                UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_ChooseWeaponLayer);
                baseWindow.GetComponent<DlgChooseWeaponLayer>().ShowAllWeaponType(type);
                baseWindow.GetComponent<DlgChooseWeaponLayer>().SetAlChooseWeaponInfo(weaponItem.WeaponInfo);
                self.RegisterWeaponClickEvent();
            }
     
        }

        public static async void RegisterWeaponClickEvent(this DlgHeroWeaponPreviewLayer self)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_ChooseWeaponLayer);
            // baseWindow.GetComponent<DlgChooseWeaponLayer>().OnItemWeaponClick();
            baseWindow.GetComponent<DlgChooseWeaponLayer>().OnWeaponItemClickAction = null;

            baseWindow.GetComponent<DlgChooseWeaponLayer>().OnWeaponItemClickAction = self.OnChooseOneWeaponItem;

            await ETTask.CompletedTask;
        }

        public static async void OnChooseOneWeaponItem(this DlgHeroWeaponPreviewLayer self, WeaponInfo weaponInfo)
        {
            WeaponsConfig config = WeaponsConfigCategory.Instance.Get(weaponInfo.ConfigId);
            ESCommonWeaponItem item = self.WeaponItems.Find(a => (int) a.CurrentType == config.WeaponType);
            if (item != null)
            {
                await self.SetWeaponOnHeroRequest(weaponInfo);
                item.SetWeaponInfo(weaponInfo);
            }
        }

        public static async ETTask SetWeaponOnHeroRequest(this DlgHeroWeaponPreviewLayer self, WeaponInfo weaponInfo)
        {
            //将道具装备到英雄身上

            long accountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            C2M_UpdateOnWeaponRequest request = new C2M_UpdateOnWeaponRequest()
            {
                Account = accountId, HeroId = self.HeroCardInfo.HeroId, WeaponId = weaponInfo.WeaponId
            };

            M2C_UpdateOnWeaponResponse response = (M2C_UpdateOnWeaponResponse) await session.Call(request);

            if (response.Error == ErrorCode.ERR_Success)
            {
                Log.Debug("设置装备成功");
            }

            await ETTask.CompletedTask;
        }

        public static void BackButtonClick(this DlgHeroWeaponPreviewLayer self)
        {
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_HeroWeaponPreviewLayer);
        }

        public static void ShowWindow(this DlgHeroWeaponPreviewLayer self, Entity contextData = null)
        {
        }

        public static async void SetTargetInfo(this DlgHeroWeaponPreviewLayer self, HeroCardInfo heroCardInfo)
        {
            self.HeroCardInfo = heroCardInfo;
            long accountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            C2M_GetOnWeaponsRequest request = new C2M_GetOnWeaponsRequest() { Account = accountId, HeroId = self.HeroCardInfo.HeroId };
            M2C_GetOnWeaponsResponse response = (M2C_GetOnWeaponsResponse) await session.Call(request);
            if (response.Error == ErrorCode.ERR_Success)
            {
                List<WeaponInfo> weaponInfos = response.WeaponInfos;

                Dictionary<int, ESCommonWeaponItem> items = new Dictionary<int, ESCommonWeaponItem>();
                
                foreach (var item in self.WeaponItems)
                {
                    items.Add((int) item.CurrentType, item);
                }
                Log.Debug($"获取装备的装备成功{weaponInfos.Count}");

                foreach (var weaponInfo in weaponInfos)
                {
                    WeaponsConfig config = WeaponsConfigCategory.Instance.Get(weaponInfo.ConfigId);
                    items[config.WeaponType].SetWeaponInfo(weaponInfo);
                }
            }
        }
        
        
    }
}