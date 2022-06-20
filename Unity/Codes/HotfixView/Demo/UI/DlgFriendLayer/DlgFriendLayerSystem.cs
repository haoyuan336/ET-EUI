using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgFriendLayerSystem
    {
        public static void RegisterUIEvent(this DlgFriendLayer self)
        {
            self.View.E_BackButton.AddListener(self.OnBackButtonClick);
            self.View.E_SearchButton.AddListenerAsync(self.OnSearchButtonClick);
        }

        public static async ETTask OnSearchButtonClick(this  DlgFriendLayer self)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_SearchUserLayer);
            
        }

        public static void OnBackButtonClick(this DlgFriendLayer self)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            uiComponent.HideWindow(WindowID.WindowID_FriendLayer);
        }

        public static void ShowWindow(this DlgFriendLayer self, Entity contextData = null)
        {
        }
    }
}