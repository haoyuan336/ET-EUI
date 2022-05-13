using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgAddSubPlaneSystem
    {
        public static void RegisterUIEvent(this DlgAddSubPlane self)
        {
            self.View.E_BgButton.AddListener(() => { self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_AddSubPlane); });
        }

        public static void ShowWindow(this DlgAddSubPlane self, Entity contextData = null)
        {
            // self.View.E_countText
            Scroll_ItemWeapon itemWeapon = contextData as Scroll_ItemWeapon;
            self.View.E_ContentImage.transform.position =
                    itemWeapon.uiTransform.position;
        }

        public static void SetInfo(this DlgAddSubPlane self, WeaponInfo info)
        {
            
        }
    }
}