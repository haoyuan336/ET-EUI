using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgAddSubPlaneSystem
    {
        public static void RegisterUIEvent(this DlgAddSubPlane self)
        {
            self.View.E_BgButton.AddListener(() => { self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_AddSubPlane); });
            // self.View.E_AddButton.AddListener();
            self.View.E_AddButton.AddListener(() =>
            {
                if (self.AddAction != null)
                {
                    self.AddAction();
                }
            });
            self.View.E_SubButton.AddListener(() =>
            {
                if (self.SubAction != null)
                {
                    self.SubAction();
                }
            });
        }

        public static void ShowWindow(this DlgAddSubPlane self, Entity contextData = null)
        {
            self.AddAction = null;
            self.SubAction = null;
            // self.View.E_countText
            Scroll_ItemWeapon itemWeapon = contextData as Scroll_ItemWeapon;
            self.View.E_ContentImage.transform.position =
                    itemWeapon.uiTransform.position;
        }

        public static void SetInfo(this DlgAddSubPlane self, WeaponInfo info)
        {
        }

        public static void SetFull(this DlgAddSubPlane self, bool full)
        {
            self.View.E_AddButton.interactable = !full;
        }

    }
}