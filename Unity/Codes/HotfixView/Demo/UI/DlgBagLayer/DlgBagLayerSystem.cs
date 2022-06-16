using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.CompilerServices;
using ET.Account;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace ET
{
    public static class DlgBagLayerSystem
    {
        public static void RegisterUIEvent(this DlgBagLayer self)
        {
            self.View.ESWeaponBagCommon.RegisterUIEvent();
            self.View.ESWeaponBagCommon.WeaponItemClickAction = self.OnItemWeaponClick;
        }

        public static async void ShowWeaponInfoLayer(this DlgBagLayer self, WeaponInfo weaponInfo)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_WeaponInfoLayer);

            UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_WeaponInfoLayer);
            baseWindow.GetComponent<DlgWeaponInfoLayer>().SetTargetInfo(weaponInfo);
        }

        public static void OnItemWeaponClick(this DlgBagLayer self, WeaponInfo weaponInfo, Scroll_ItemWeapon itemWeapon)
        {
            // UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            // await uiComponent.ShowWindow(WindowID.WindowID_WeaponStrengthenLayer);
            // UIBaseWindow baseWindow = uiComponent.AllWindowsDic[(int) WindowID.WindowID_WeaponStrengthenLayer];
            // DlgWeaponStrengthenLayer strengthenLayer = baseWindow.GetComponent<DlgWeaponStrengthenLayer>();
            // strengthenLayer.SetInfo(weaponInfo);
            // await ETTask.CompletedTask;

            // itemWeapon.E_ChooseButton.isOn = false;
            WeaponsConfig config = WeaponsConfigCategory.Instance.Get(weaponInfo.ConfigId);
            if (config.MaterialType == (int) WeaponBagType.Weapon)
            {
                self.ShowWeaponInfoLayer(weaponInfo);
            }
        }

        public static void ReferBagInfo(this DlgBagLayer self)
        {
            self.View.ESWeaponBagCommon.ShowWindow();
        }

        //
        public static async void ShowWindow(this DlgBagLayer self, Entity contextData = null)
        {
            self.View.ESWeaponBagCommon.ShowWindow();

            await ETTask.CompletedTask;
        }
    }
}