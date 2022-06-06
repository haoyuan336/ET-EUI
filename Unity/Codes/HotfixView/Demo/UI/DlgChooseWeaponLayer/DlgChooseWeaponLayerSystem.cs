using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgChooseWeaponLayerSystem
    {
        public static void RegisterUIEvent(this DlgChooseWeaponLayer self)
        {
            self.View.E_BackButton.AddListener(() =>
            {
                self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_ChooseWeaponLayer);
            });

            // self.es

            self.View.ESWeaponBagCommon.RegisterUIEvent();
            self.View.ESWeaponBagCommon.WeaponItemClickAction = self.OnItemWeaponClick;
        }

        public static void OnItemWeaponClick(this DlgChooseWeaponLayer self, WeaponInfo weaponInfo, Scroll_ItemWeapon itemWeapon, bool value)
        {
            if (value)
            {
                itemWeapon.E_ChooseToggle.isOn = false;
                self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_ChooseWeaponLayer);

                if (self.OnWeaponItemClickAction != null)
                {
                    self.OnWeaponItemClickAction(weaponInfo);
                }
            }
        }

        public static void ShowWindow(this DlgChooseWeaponLayer self, Entity contextData = null)
        {
            // self.View.ESWeaponBagCommon.ShowWindow();
        }

        public static void ShowAllWeaponType(this DlgChooseWeaponLayer self, WeaponType type)
        {
            // self.View.ESWeaponBagCommon.
            self.View.ESWeaponBagCommon.ShowWindowWidthLockType(type);
        }

        public static void HideWindow(this DlgChooseWeaponLayer self)
        {
            self.AlChooseWeaponInfo = null;
        }

        public static void SetAlChooseWeaponInfo(this DlgChooseWeaponLayer self, WeaponInfo weaponInfo)
        {
            //todo 显示当前装备的武器
            // self.AlChooseWeaponInfo = weaponInfo;
            // self.View.ESWeaponBagCommon.SetAlChooseWeaponInfo();
        }
    }
}