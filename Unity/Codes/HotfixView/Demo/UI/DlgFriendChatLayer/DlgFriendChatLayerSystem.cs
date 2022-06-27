using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgFriendChatLayerSystem
    {
        public static void RegisterUIEvent(this DlgFriendChatLayer self)
        {
            // self.AddUIScrollItems(ref  self.ItemChats);
            self.View.E_BackButton.AddListener(() =>
            {
                UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
                uiComponent.HideWindow(WindowID.WindowID_FriendChatLayer);
            });

            self.View.ELoopScrollListLoopVerticalScrollRect.AddItemRefreshListener(self.OnLoopEventListener);

            self.View.E_SendButton.AddListenerAsync(self.OnSendButtonClick);
        }

        public static async ETTask OnSendButtonClick(this DlgFriendChatLayer self)
        {
            string inputText = self.View.E_InputInputField.text;
            self.View.E_InputInputField.text = "";
            if (inputText.Length == 0)
            {
                return;
            }

            long accountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;

            C2M_ChatToFriendRequest request =
                    new C2M_ChatToFriendRequest() { AccountId = accountId, ChatText = inputText, AccountInfo = self.ChatToAccountInfo };
            var response = await session.Call(request) as M2C_ChatToFriendResponse;
            if (response.Error == ErrorCode.ERR_Success)
            {
                ChatInfo characterInfo = response.ChatInfo;
                self.AddChat(characterInfo);
            }

            await ETTask.CompletedTask;
        }

        public static void OnLoopEventListener(this DlgFriendChatLayer self, Transform transform, int index)
        {
            Scroll_ItemChat itemChat = self.ItemChats[index].BindTrans(transform);
            if (index < self.ChatInfos.Count)
            {
                var chatInfo = self.ChatInfos[index];
                self.SetChatitemInfo(chatInfo, itemChat);
            }
        }

        public static void SetChatitemInfo(this DlgFriendChatLayer self, ChatInfo chatInfo, Scroll_ItemChat itemChat)
        {
            long accountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            AccountInfo accountInfo = chatInfo.AccountInfo;
            string chatText = chatInfo.ChatText;
            Log.Debug($"chat Text {chatText}");
            itemChat.E_MyItemImage.gameObject.SetActive(accountId == accountInfo.Account);
            itemChat.E_FriendItemImage.gameObject.SetActive(accountId != accountInfo.Account);
            itemChat.E_MyChatText.text = chatText;
            itemChat.E_FriendChatText.text = chatText;
            if (accountId == accountInfo.Account)
            {
                itemChat.E_MyNameText.text = accountInfo.NickName;
            }
            else
            {
                itemChat.E_FriendNameText.text = accountInfo.NickName;
            }
        }

        public static void HideWindow(this DlgFriendChatLayer self)
        {
            self.View.ELoopScrollListLoopVerticalScrollRect.ClearCells();
            self.ChatInfos.Clear();
        }

        public static void ShowWindow(this DlgFriendChatLayer self, Entity contextData = null)
        {
        }

        public static void SetAccountInfo(this DlgFriendChatLayer self, AccountInfo accountInfo)
        {
            self.ChatToAccountInfo = accountInfo;
            self.View.E_NameText.text = accountInfo.NickName;
        }

        public static async void AddChat(this DlgFriendChatLayer self, ChatInfo chatInfo)
        {
            self.ChatInfos.Add(chatInfo);
            self.AddUIScrollItems(ref self.ItemChats, self.ChatInfos.Count);
            self.View.ELoopScrollListLoopVerticalScrollRect.SetVisible(true, self.ChatInfos.Count);
            // self.View.ELoopScrollListLoopVerticalScrollRect.totalCount = self.ChatInfos.Count;
            // self.View.ELoopScrollListLoopVerticalScrollRect.RefreshCells();
            await TimerComponent.Instance.WaitFrameAsync();
            self.View.ELoopScrollListLoopVerticalScrollRect.SrollToCellWithinTime(self.ChatInfos.Count - 1, 0);
        }

        public static bool ReceiveChat(this DlgFriendChatLayer self, ChatInfo chatInfo)
        {
            if (!chatInfo.AccountInfo.Account.Equals(self.ChatToAccountInfo.Account))
            {
                return false;
            }

            self.AddChat(chatInfo);

            return true;
        }
    }
}