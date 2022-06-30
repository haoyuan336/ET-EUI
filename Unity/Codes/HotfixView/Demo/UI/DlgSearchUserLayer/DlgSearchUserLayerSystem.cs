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
            self.View.ELoopApplyLoopVerticalScrollRect.AddItemRefreshListener(self.OnApplyLoop);
            self.View.E_BackButton.AddListener(self.BackButtonClick);
            self.View.E_SearchButton.AddListenerAsync(self.OnSearchButtonClick);
        }

        public static async ETTask OnSearchButtonClick(this DlgSearchUserLayer self)
        {
            if (string.IsNullOrEmpty(self.View.E_SearchContentText.text))
            {
                return;
            }

            long accountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            var request = new C2M_SearchAccountWithNameRequest() { Name = self.View.E_SearchContentText.text, AccountId = accountId };
            var response = await session.Call(request) as M2C_SearchAccountWithNameResponse;

            if (response.Error == ErrorCode.ERR_Success)
            {
                // self.RecommendAccountInfos = response.AccountInfos;
                // self.View.ELoopRecommendLoopVerticalScrollRect.RefreshCells();
            }

            await ETTask.CompletedTask;
        }

        public static void BackButtonClick(this DlgSearchUserLayer self)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            uiComponent.HideWindow(WindowID.WindowID_SearchUserLayer);
        }

        public static async void OnApplyLoop(this DlgSearchUserLayer self, Transform transform, int index)
        {
            Scroll_ItemAddFriendApply itemAddFriendApply = self.ItemAddFriendApplies[index].BindTrans(transform);
            if (index < self.ApplyAccountInfos.Count)
            {
                var mailInfo = self.ApplyMailInfos[index];
                var info = self.ApplyAccountInfos[index];
                var lastTime = TimeHelper.ServerNow() - mailInfo.SendTime;
                var str = CustomHelper.GetLastTimeByMSecond(lastTime);
                itemAddFriendApply.E_TimeText.text = str;
                itemAddFriendApply.E_NameText.text = info.NickName;

                itemAddFriendApply.E_AcceptButton.AddListener(() => { self.OnAcceptButtonClick(info, true); });
                itemAddFriendApply.E_RefuseButton.AddListener(() => { self.OnAcceptButtonClick(info, false); });
                var headImageConfig = PlayerHeadImageResConfigCategory.Instance.Get(info.HeadImageConfigId);
                var headImageFrameConfig = PlayerHeadImageResConfigCategory.Instance.Get(info.HeadFrameImageConfigId);

                itemAddFriendApply.E_HeadImage.sprite =
                        await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(headImageConfig.SpriteAtlasRes, headImageConfig.ImageRes);

                itemAddFriendApply.E_HeadFrameImage.sprite =
                        await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(headImageFrameConfig.SpriteAtlasRes,
                            headImageFrameConfig.ImageRes);

                itemAddFriendApply.E_HeadFrameButton.onClick.RemoveAllListeners();
                itemAddFriendApply.E_HeadFrameButton.AddListener(async () =>
                {
                    UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
                    await uiComponent.ShowWindow(WindowID.WindowID_UserInfoLayer);
                    UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_UserInfoLayer);
                    baseWindow.GetComponent<DlgUserInfoLayer>().SetUserInfo(info);
                });
            }
        }

        public static async void OnAcceptButtonClick(this DlgSearchUserLayer self, AccountInfo accountInfo, bool procss)
        {
            //接受拒绝按钮回调
            long accountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            C2M_ProcessFriendApplyRequest request = new C2M_ProcessFriendApplyRequest()
            {
                AccountId = accountId,
                AccountInfo = accountInfo,
                ApplyProcessType = procss? (int) ApplyProcessType.Accept : (int) ApplyProcessType.Refuse
            };
            var response = await session.Call(request) as M2C_ProcessFriendApplyResponse;
            if (response.Error == ErrorCode.ERR_Success)
            {
                self.ApplyAccountInfos.Remove(accountInfo);
                self.View.ELoopApplyLoopVerticalScrollRect.SetVisible(true, self.ApplyAccountInfos.Count);

                UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
                UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_FriendLayer);
                baseWindow.GetComponent<DlgFriendLayer>().ReferInfo();
            }
        }

        public static async void OnRecommendLoop(this DlgSearchUserLayer self, Transform transform, int index)
        {
            Scroll_ItemRecommend itemRecommend = self.ItemRecommends[index].BindTrans(transform);
            if (index < self.RecommendAccountInfos.Count)
            {
                AccountInfo accountInfo = self.RecommendAccountInfos[index];
                itemRecommend.E_AddButton.AddListener(() => { self.OnAddFriendButtonClick(accountInfo); });
                itemRecommend.E_NameText.text = accountInfo.NickName;
                var disTime = TimeHelper.ServerNow() - accountInfo.LastLogonTime;
                var str = CustomHelper.GetLastTimeByMSecond(disTime);
                itemRecommend.E_TimeText.text = str;
                var headImageConfig = PlayerHeadImageResConfigCategory.Instance.Get(accountInfo.HeadImageConfigId);
                var headImageFrameConfig = PlayerHeadImageResConfigCategory.Instance.Get(accountInfo.HeadFrameImageConfigId);

                itemRecommend.E_HeadImage.sprite =
                        await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(headImageConfig.SpriteAtlasRes, headImageConfig.ImageRes);

                itemRecommend.E_HeadFrameImage.sprite =
                        await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(headImageFrameConfig.SpriteAtlasRes,
                            headImageFrameConfig.ImageRes);

                itemRecommend.E_HeadFrameButton.onClick.RemoveAllListeners();
                itemRecommend.E_HeadFrameButton.AddListener(async () =>
                {
                    UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
                    await uiComponent.ShowWindow(WindowID.WindowID_UserInfoLayer);
                    UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_UserInfoLayer);
                    baseWindow.GetComponent<DlgUserInfoLayer>().SetUserInfo(accountInfo);
                });
            }
        }

        public static async void OnApplyToggleClick(this DlgSearchUserLayer self, bool value)
        {
            if (value)
            {
                Log.Debug("好友申请列表");
                self.View.ELoopApplyLoopVerticalScrollRect.gameObject.SetActive(true);
                //获取玩家的所有邮件
                long account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
                Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
                C2M_GetFriendApplyListRequest request = new C2M_GetFriendApplyListRequest() { AccountId = account };
                M2C_GetFriendApplyListResponse response = await session.Call(request) as M2C_GetFriendApplyListResponse;
                if (response.Error == ErrorCode.ERR_Success)
                {
                    self.ApplyAccountInfos = response.AccountInfo;
                    self.ApplyMailInfos = response.MailInfo;
                    self.AddUIScrollItems(ref self.ItemAddFriendApplies, self.ApplyAccountInfos.Count);
                    self.View.ELoopApplyLoopVerticalScrollRect.SetVisible(true, self.ApplyAccountInfos.Count);
                }
            }
            else
            {
                self.View.ELoopApplyLoopVerticalScrollRect.gameObject.SetActive(false);
            }
        }

        public static async void OnRecommendToggleClick(this DlgSearchUserLayer self, bool value)
        {
            if (value)
            {
                self.View.ELoopRecommendLoopVerticalScrollRect.gameObject.SetActive(true);
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
            else
            {
                self.View.ELoopRecommendLoopVerticalScrollRect.gameObject.SetActive(false);
            }

            await ETTask.CompletedTask;
        }

        public static async void OnAddFriendButtonClick(this DlgSearchUserLayer self, AccountInfo accountInfo)
        {
            long account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;

            C2M_AddFriendRequest request = new C2M_AddFriendRequest() { Account = account, TargetInfo = accountInfo };

            M2C_AddFriendResponse response = await session.Call(request) as M2C_AddFriendResponse;

            self.RecommendAccountInfos.Remove(accountInfo);
            self.AddUIScrollItems(ref self.ItemRecommends, self.RecommendAccountInfos.Count);
            self.View.ELoopRecommendLoopVerticalScrollRect.SetVisible(true, self.RecommendAccountInfos.Count);
            if (response.Error == ErrorCode.ERR_Success)
            {
                Log.Debug("添加好友请求发送成功");
            }
        }

        public static void ShowWindow(this DlgSearchUserLayer self, Entity contextData = null)
        {
            self.OnRecommendToggleClick(true);
            self.View.E_RecommendToggle.isOn = true;
        }

        public static void ReceiveAddFriendMessage(this DlgSearchUserLayer self, List<MailInfo> mailInfos)
        {
            Log.Debug("收到了 添加好友申请的消息");
        }
    }
}