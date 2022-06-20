using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgSearchUserLayerSystem
    {
        public static void RegisterUIEvent(this DlgSearchUserLayer self)
        {
            self.View.E_RecommendToggle.onValueChanged.AddListener(self.OnRecommendToggleClick);
            self.View.E_ApplyToggle.onValueChanged.AddListener(self.OnApplyToggleClick);
            self.View.ELoopRecommendLoopVerticalScrollRect.AddItemRefreshListener(self.OnRecommendLoop);
            self.View.E_BackButton.AddListener(self.BackButtonClick);
        }

        public static void BackButtonClick(this DlgSearchUserLayer self)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            uiComponent.HideWindow(WindowID.WindowID_SearchUserLayer);
        }

        public static void OnRecommendLoop(this DlgSearchUserLayer self, Transform transform, int index)
        {
            Scroll_ItemRecommend itemRecommend = self.ItemRecommends[index].BindTrans(transform);
            AccountInfo accountInfo = self.RecommendAccountInfos[index];
            if (accountInfo != null)
            {
                itemRecommend.E_AddButton.AddListener(() => { self.OnAddFriendButtonClick(accountInfo); });
                itemRecommend.E_NameText.text = accountInfo.NickName;
                var disTime = TimeHelper.ServerNow() - accountInfo.LastLogonTime;
                var year = disTime / 1000 / (60 * 60 * 24 * 365);
                var day = disTime / 1000 / (60 * 60 * 24);
                var hour = disTime / 1000 / (60 * 60);
                var min = disTime / 1000 / 60;
                var second = disTime / 1000;
                if (year != 0)
                {
                    itemRecommend.E_TimeText.text = $"{year}年前";
                }
                else if (day != 0)
                {
                    itemRecommend.E_TimeText.text = $"{day}天前";
                }
                else if (hour != 0)
                {
                    itemRecommend.E_TimeText.text = $"{hour}小时前";
                }
                else if (min != 0)
                {
                    itemRecommend.E_TimeText.text = $"{min}分钟前";
                }
                else if (second != 0)
                {
                    itemRecommend.E_TimeText.text = $"{second}秒前";
                }
            }
        }

        public static void OnApplyToggleClick(this DlgSearchUserLayer self, bool value)
        {
            if (value)
            {
                Log.Debug("好友申请列表");
            }
        }

        public static async void OnRecommendToggleClick(this DlgSearchUserLayer self, bool value)
        {
            if (value)
            {
                long account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
                Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
                Log.Debug("显示推荐列表");
                C2M_GetFriendRecommendListRequest request = new C2M_GetFriendRecommendListRequest() { AccountId = account };
                var response = await session.Call(request) as M2C_GetFriendRecommendListResponse;
                if (response.Error == ErrorCode.ERR_Success)
                {
                    Log.Debug($"response count  {response.AccountInfos.Count}");

                    self.RecommendAccountInfos = response.AccountInfos;
                    self.RecommendAccountInfos.Sort((a, b) => { return (int) (b.LastLogonTime - a.LastLogonTime); });
                    self.AddUIScrollItems(ref self.ItemRecommends, self.RecommendAccountInfos.Count);
                    self.View.ELoopRecommendLoopVerticalScrollRect.SetVisible(true, self.RecommendAccountInfos.Count);
                }
            }

            await ETTask.CompletedTask;
        }

        public static async void OnAddFriendButtonClick(this DlgSearchUserLayer self, AccountInfo accountInfo)
        {
            long account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;

            C2M_AddFriendRequest request = new C2M_AddFriendRequest() { Account = account, TargetInfo = accountInfo };

            M2C_AddFriendResponse response = await session.Call(request) as M2C_AddFriendResponse;

            if (response.Error == ErrorCode.ERR_Success)
            {
                Log.Debug("添加好友请求发送成功");
            }
        }

        public static void ShowWindow(this DlgSearchUserLayer self, Entity contextData = null)
        {
            self.OnRecommendToggleClick(true);
        }

        public static void ReceiveAddFriendMessage(this DlgSearchUserLayer self, List<MailInfo> mailInfos)
        {
            Log.Debug("收到了 添加好友申请的消息");
        }
    }
}