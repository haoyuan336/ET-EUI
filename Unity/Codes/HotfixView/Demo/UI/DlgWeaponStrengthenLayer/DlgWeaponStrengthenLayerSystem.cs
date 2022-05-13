using System.Collections;
using System.Collections.Generic;
using System;
using ET.Account;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgWeaponStrengthenLayerSystem
    {
        public static void RegisterUIEvent(this DlgWeaponStrengthenLayer self)
        {
            self.View.E_BackButton.onClick.AddListener(() =>
            {
                self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_WeaponStrengthenLayer);
            });
            // self.AddUIScrollItems(ref self.TargetItemWeapons, 10);
            self.View.E_TargetWeaponContentLoopVerticalScrollRect.AddItemRefreshListener(self.OnTargetLoopScrollview);
            self.View.E_BagContentLoopVerticalScrollRect.AddItemRefreshListener(self.OnWeaponsLoopScrollview);
        }

        public static async void RequestBagInfo(this DlgWeaponStrengthenLayer self)
        {
            M2C_GetAllWeaponsResponse response;
            var session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            var account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            response = (M2C_GetAllWeaponsResponse) await session.Call(new C2M_GetAllWeaponsRequest() { AccountId = account });
            if (response.Error == ErrorCode.ERR_Success)
            {
                self.WeaponInfos = response.WeaponInfos;

                self.AddUIScrollItems(ref self.ItemWeapons, self.WeaponInfos.Count);
                self.View.E_BagContentLoopVerticalScrollRect.SetVisible(true, self.WeaponInfos.Count);
            }
        }

        public static void OnTargetLoopScrollview(this DlgWeaponStrengthenLayer self, Transform tr, int index)
        {
            Scroll_ItemWeapon itemWeapon = self.TargetItemWeapons[index].BindTrans(tr);
            self.InitWeaponItem(self.TargetWeaponInfos[index], itemWeapon);
        }

        public static void OnWeaponsLoopScrollview(this DlgWeaponStrengthenLayer self, Transform tr, int index)
        {
            Scroll_ItemWeapon itemWeapon = self.ItemWeapons[index].BindTrans(tr);
            self.InitWeaponItem(self.WeaponInfos[index], itemWeapon);
            itemWeapon.E_ClickToggle.interactable = true;
            if (self.WeaponInfos[index].WeaponId.Equals(self.TargetWeaponInfos[0].WeaponId))
            {
                itemWeapon.E_ClickToggle.interactable = false;
            }

            itemWeapon.E_ClickToggle.onValueChanged.RemoveAllListeners();
            itemWeapon.E_ClickToggle.onValueChanged.AddListener((value) => { self.OnWeaponItemClick(self.WeaponInfos[index], itemWeapon); });
        }

        public static async void OnWeaponItemClick(this DlgWeaponStrengthenLayer self, WeaponInfo info, Scroll_ItemWeapon itemWeapon)
        {
            Log.Debug($"on click config id {info.ConfigId}");
            WeaponsConfig config = WeaponsConfigCategory.Instance.Get(info.ConfigId);
            // Log.Debug($"on weapon team clic{config.MaterialTypes}");
            // if (config.MaterialTypes == "1")
            // {
            //     //如果是装备类型的材料，那么直接选择
            // }
            // else
            // {
                //如果是普通材料。那么需要选择个数
                UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
                await uiComponent.ShowWindow(WindowID.WindowID_AddSubPlane, WindowID.WindowID_Invaild,
                    new ShowWindowData() { contextData = itemWeapon });

                UIBaseWindow baseWindow = uiComponent.AllWindowsDic[(int) WindowID.WindowID_AddSubPlane];
                baseWindow.GetComponent<DlgAddSubPlane>().SetInfo(info);
            // }
        }

        public static async void InitWeaponItem(this DlgWeaponStrengthenLayer self, WeaponInfo info, Scroll_ItemWeapon weapon)
        {
            WeaponsConfig config = WeaponsConfigCategory.Instance.Get(info.ConfigId);
            Sprite sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(ConstValue.WeaponAtlasPath, config.IconResName);
            weapon.E_WeaponImage.sprite = sprite;
            weapon.E_LevelText.text = $"Lv:{info.Level}";
        }

        public static void ShowWindow(this DlgWeaponStrengthenLayer self, Entity contextData = null)
        {
        }

        public static void SetInfo(this DlgWeaponStrengthenLayer self, WeaponInfo info)
        {
            self.TargetWeaponInfos = new List<WeaponInfo>() { info };
            Log.Debug($"set target weapon infos {self.TargetWeaponInfos.Count}");
            self.AddUIScrollItems(ref self.TargetItemWeapons, self.TargetWeaponInfos.Count);
            self.View.E_TargetWeaponContentLoopVerticalScrollRect.SetVisible(true, self.TargetWeaponInfos.Count);
            self.RequestBagInfo();
        }
    }
}