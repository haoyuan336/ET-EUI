using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
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
            self.View.E_QuickGetButton.AddListenerAsync(self.OnQuickButtonClick);
        }

        public static void SetChatInfoMap(this DlgFriendLayer self, Dictionary<long, ChatInfo> chatInfos)
        {
            self.ChatInfosMap = chatInfos;
            self.View.ELoopFriendListLoopVerticalScrollRect.RefillCells();
        }

        public static async ETTask OnQuickButtonClick(this DlgFriendLayer self)
        {
            long account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            var request = new C2M_OneKeyGiveAndGetRequest() { Account = account };
            M2C_OneKeyGiveAndGetResponse response = await session.Call(request) as M2C_OneKeyGiveAndGetResponse;

            if (response.Error == ErrorCode.ERR_Success)
            {
                List<FriendInfo> friendInfos = response.FriendInfos;

                foreach (var friendInfo in friendInfos)
                {
                    self.FriendInfos.Remove(friendInfo.FriendId);
                    self.FriendInfos.Add(friendInfo.FriendId, friendInfo);
                }

                self.View.ELoopFriendListLoopVerticalScrollRect.RefreshCells();

                // ItemInfo powerItemInfo = response.PowerItemInfo;
                // if (powerItemInfo != null)
                // {
                UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
                // await uiComponent.ShowWindow(WindowID.WindowID_GoldInfoUI);
                UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_GoldInfoUI);
                if (baseWindow != null)
                {
                    baseWindow.GetComponent<DlgGoldInfoUI>().ReferGoldInfo();
                }
                // }
            }

            if (response.Error == ErrorCode.Gift_Not_Enough)
            {
                self.ShowAlertText("今日赠送额度已经用完");
            }

            if (response.Error == ErrorCode.ERR_NotFoundPlayer)
            {
                self.ShowAlertText("没有可赠送的好友");
            }

            await ETTask.CompletedTask;
        }

        public static async void OnGiftButtonClick(this DlgFriendLayer self, AccountInfo accountInfo, Scroll_ItemFriend itemFriend)
        {
            Log.Debug("on gift button click");
            // itemFriend.E_GiftMarkImage.gameObject.SetActive(true);
            long accountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            //给此玩家赠送礼物
            C2M_GiveGiftToFriendRequest request = new C2M_GiveGiftToFriendRequest() { Account = accountId, AccountInfo = accountInfo };
            var response = await session.Call(request) as M2C_GiveGiftToFriendResponse;
            if (response.Error == ErrorCode.ERR_Success)
            {
                FriendInfo friendInfo = response.FriendInfo;
                self.FriendInfos.Remove(friendInfo.FriendId);
                self.FriendInfos.Add(friendInfo.FriendId, friendInfo);
                // self.FriendInfos
                self.View.ELoopFriendListLoopVerticalScrollRect.RefreshCells();
            }

            if (response.Error == ErrorCode.Gift_Gived)
            {
                Log.Debug("礼物已经赠送过了");
                self.ShowAlertText("已经赠送过了");
            }

            if (response.Error == ErrorCode.Gift_Not_Enough)
            {
                Log.Debug("赠送额度已经用完");
                self.ShowAlertText("今日赠送额度已经用完");
            }
        }

        public static async void ShowAlertText(this DlgFriendLayer self, string text)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_AlertLayer);
            UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_AlertLayer);
            baseWindow.GetComponent<DlgAlertLayer>().SetText(text);
        }

        public static async void OnItemFriendLoop(this DlgFriendLayer self, Transform transform, int index)
        {
            Scroll_ItemFriend itemFriend = self.ItemFriends[index].BindTrans(transform);
            if (index < self.AccountInfos.Count)
            {
                var accountInfo = self.AccountInfos[index];
                var friendInfo = self.FriendInfos[accountInfo.Account];
                var lastTime = TimeHelper.ServerNow() - accountInfo.LastLogonTime;
                var str = CustomHelper.GetLastTimeByMSecond(lastTime);
                itemFriend.E_TimeText.text = str;
                itemFriend.E_NameText.text = accountInfo.NickName;
                itemFriend.E_GiftMarkImage.gameObject.SetActive(friendInfo.IsGift);
                itemFriend.E_GiftButton.onClick.RemoveAllListeners();
                itemFriend.E_GiftButton.AddListener(() => { self.OnGiftButtonClick(accountInfo, itemFriend); });
                itemFriend.E_ChatButton.onClick.RemoveAllListeners();
                itemFriend.E_ChatButton.AddListener(() => { self.OnChatButtonClick(accountInfo, itemFriend); });
                itemFriend.E_NewChatMarkImage.gameObject.SetActive(false);
                itemFriend.E_HeadFrameButton.onClick.RemoveAllListeners();
                itemFriend.E_HeadFrameButton.AddListener(() =>
                {
                    self.OnHeadButtonClick(accountInfo);
                });
                var headImageConfig = PlayerHeadImageResConfigCategory.Instance.Get(accountInfo.HeadImageConfigId);
                var headFrameImageConfig = PlayerHeadImageResConfigCategory.Instance.Get(accountInfo.HeadFrameImageConfigId);
                itemFriend.E_HeadImage.sprite =
                        await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(headImageConfig.SpriteAtlasRes, headImageConfig.ImageRes);
                itemFriend.E_HeadFrameImage.sprite =
                        await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(headFrameImageConfig.SpriteAtlasRes,
                            headImageConfig.ImageRes);


                if (self.ChatInfosMap.Keys.Contains(accountInfo.Account))
                {
                    itemFriend.E_NewChatMarkImage.gameObject.SetActive(true);
                }
            }
        }

        public static async void OnHeadButtonClick(this DlgFriendLayer self, AccountInfo accountInfo)
        {
            Log.Debug("head frame button click");
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_UserInfoLayer);
            UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_UserInfoLayer);
            baseWindow.GetComponent<DlgUserInfoLayer>().SetUserInfo(accountInfo);
        }

        public static async void OnChatButtonClick(this DlgFriendLayer self, AccountInfo accountInfo, Scroll_ItemFriend itemFriend)
        {
            itemFriend.E_NewChatMarkImage.gameObject.SetActive(false);
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_FriendChatLayer);
            UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_FriendChatLayer);
            baseWindow.GetComponent<DlgFriendChatLayer>().SetAccountInfo(accountInfo);

            if (self.ChatInfosMap.Keys.Contains(accountInfo.Account))
            {
                baseWindow.GetComponent<DlgFriendChatLayer>().ReceiveChat(self.ChatInfosMap[accountInfo.Account]);
                self.ChatInfosMap.Remove(accountInfo.Account);

                UIBaseWindow setBaseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_SettingUI);
                if (setBaseWindow != null)
                {
                    setBaseWindow.GetComponent<DlgSettingUI>().SetChatInfos(self.ChatInfosMap);
                }
            }

            await ETTask.CompletedTask;
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
                self.FriendInfos = response.FriendInfos.ToDictionary(a => a.FriendId, b => b);
                self.AccountInfos = response.AccountInfos;
                self.AddUIScrollItems(ref self.ItemFriends, self.AccountInfos.Count);
                self.View.ELoopFriendListLoopVerticalScrollRect.SetVisible(true, self.AccountInfos.Count);
            }
        }
    }
}