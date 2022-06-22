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
            self.View.ELoopFriendListLoopVerticalScrollRect.AddItemRefreshListener(self.OnItemFriendLoop);
        }

        public static async void OnGiftButtonClick(this DlgFriendLayer self, AccountInfo accountInfo, Scroll_ItemFriend itemFriend)
        {
            Log.Debug("on gift button click");
            if (!itemFriend.E_GiftMarkImage.gameObject.activeSelf)
            {
                itemFriend.E_GiftMarkImage.gameObject.SetActive(true);
                long accountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
                Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
                //给此玩家赠送礼物
                C2M_GiveGiftToFriendListRequest request = new C2M_GiveGiftToFriendListRequest() { Account = accountId, AccountInfo = accountInfo };
                var response = await session.Call(request) as M2C_GiveGiftToFriendListResponse;
                if (response.Error == ErrorCode.ERR_Success)
                {
                    self.AccountInfos = response.AccountInfos;
                    self.FriendInfos = response.FriendInfos;
                    self.View.ELoopFriendListLoopVerticalScrollRect.RefreshCells();
                }
            }
        }

        public static void OnItemFriendLoop(this DlgFriendLayer self, Transform transform, int index)
        {
            Scroll_ItemFriend itemFriend = self.ItemFriends[index].BindTrans(transform);
            if (index < self.AccountInfos.Count)
            {
                var accountInfo = self.AccountInfos[index];
                var friendInfo = self.FriendInfos[index];
                var lastTime = TimeHelper.ServerNow() - accountInfo.LastLogonTime;
                var str = CustomHelper.GetLastTimeByMSecond(lastTime);
                itemFriend.E_TimeText.text = str;
                itemFriend.E_NameText.text = accountInfo.NickName;
                itemFriend.E_GiftMarkImage.gameObject.SetActive(friendInfo.IsGift);
                itemFriend.E_GiftButton.AddListener(() => { self.OnGiftButtonClick(accountInfo, itemFriend); });
            }
        }

        public static async ETTask OnSearchButtonClick(this DlgFriendLayer self)
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
            self.ReferInfo();
        }

        public static async void ReferInfo(this DlgFriendLayer self)
        {
            long account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            C2M_GetAllFriendsRequest request = new C2M_GetAllFriendsRequest() { AccountId = account };
            var response = await session.Call(request) as M2C_GetAllFriendsResponse;
            if (response.Error == ErrorCode.ERR_Success)
            {
                self.FriendInfos = response.FriendInfos;
                self.AccountInfos = response.AccountInfos;
                self.AddUIScrollItems(ref self.ItemFriends, self.FriendInfos.Count);
                self.View.ELoopFriendListLoopVerticalScrollRect.SetVisible(true, self.FriendInfos.Count);
            }
        }
    }
}