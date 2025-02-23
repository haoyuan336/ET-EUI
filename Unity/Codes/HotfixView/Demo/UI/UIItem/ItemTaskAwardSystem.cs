﻿using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ET
{
    public static class ItemTaskAwardSystem
    {
        public static void SetConfig(this Scroll_ItemTaskAward self, TaskConfig taskConfig)
        {
            self.E_TaskDesText.text = taskConfig.EnglishDes;
            self.E_AwardDesText.text = taskConfig.AwardDes;
            self.E_ActiveValueDesText.text = $"活跃度+{taskConfig.ActiveValue}";
            self.TaskConfig = taskConfig;

            self.RequestTaskInfo(taskConfig.Id);

            self.E_GetButton.onClick.RemoveAllListeners();
            self.E_GetButton.AddListenerAsync(self.OnGetButtonClick);
        }

        public static async ETTask OnGetButtonClick(this Scroll_ItemTaskAward self)
        {
            Log.Debug("获取按钮点击");
            switch (self.GameTaskInfo.TaskState)
            {
                case (int)TaskStateType.Completed:
                    Log.Debug("领取任务奖励");
                    await self.GetTaskAward();
                    break;
                case (int)TaskStateType.UnComplete:
                    Log.Debug("去完成游戏");
                    self.OnUnCompleteButtonClick();
                    break;
            }

            await ETTask.CompletedTask;
        }

        public static async void OnUnCompleteButtonClick(this Scroll_ItemTaskAward self)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            var configId = self.TaskConfig.ActionConfigId;
            uiComponent.HideWindow(WindowID.WindowID_TaskLabelLayer);

            switch (configId)
            {
                case 10002:
                    //进入pve 战前准备页面
                    uiComponent.HideWindow(WindowID.WindowID_MainScene);
                    uiComponent.HideWindow(WindowID.WindowID_MainSceneBg);
                    uiComponent.HideWindow(WindowID.WindowID_AccountInfo);
                    uiComponent.HideWindow(WindowID.WindowID_MessageTaskActiveInfo);
                    uiComponent.HideWindow(WindowID.WindowID_SettingUI);
                    uiComponent.HideWindow(WindowID.WindowID_FormationUI);
                    uiComponent.HideWindow(WindowID.WindowID_MainSceneMenu);
                    await uiComponent.ShowWindow(WindowID.WindowID_PVESceneLayer);

                    uiComponent.ShowWindow(WindowID.WindowID_GoldInfoUI).Coroutine();
                    break;

                case 10004:
                    await uiComponent.ShowWindow(WindowID.WindowID_FriendLayer);

                    break;
                case 10005:
                    uiComponent.HideWindow(WindowID.WindowID_MainScene);
                    uiComponent.HideWindow(WindowID.WindowID_MainSceneBg);
                    uiComponent.HideWindow(WindowID.WindowID_AccountInfo);
                    uiComponent.HideWindow(WindowID.WindowID_MessageTaskActiveInfo);
                    uiComponent.HideWindow(WindowID.WindowID_SettingUI);
                    uiComponent.HideWindow(WindowID.WindowID_FormationUI);
                    uiComponent.HideWindow(WindowID.WindowID_MainSceneMenu);
                    await uiComponent.ShowWindow(WindowID.WindowID_PVESceneLayer);
                    break;
            }
        }

        public static async ETTask GetTaskAward(this Scroll_ItemTaskAward self)
        {
            var accountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            var session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            Log.Debug($"game task id {self.GameTaskInfo.TaskId}");
            var request = new C2M_GetGameTaskAwardRequest() { AccountId = accountId, TaskId = self.GameTaskInfo.TaskId };
            var response = await session.Call(request) as M2C_GetGameTaskAwardResponse;
            if (response.Error == ErrorCode.ERR_Success)
            {
                if (self.GetTaskAwardAction != null)
                {
                    self.GetTaskAwardAction.Invoke();
                }

                self.ShowOwnAwardTipsLayer(response.ItemInfos);
                Log.Debug("领取任务奖励成功");
                response.GameTaskInfo.ActionCount = self.GameTaskInfo.ActionCount;
                self.SetGameTaskInfo(response.GameTaskInfo);
            }

            await ETTask.CompletedTask;
        }

        public static async void ShowOwnAwardTipsLayer(this Scroll_ItemTaskAward self, List<ItemInfo> itemInfos)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_OwnAwardTipsLayer);
            UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_OwnAwardTipsLayer);
            if (baseWindow != null)
            {
                baseWindow.GetComponent<DlgOwnAwardTipsLayer>().SetItemInfos(itemInfos);
            }
        }

        public static async void SetGameTaskInfo(this Scroll_ItemTaskAward self, GameTaskInfo gameTaskInfo)
        {
            self.GameTaskInfo = gameTaskInfo;
            // smbtnhuang smbtnlv
            string buttonImageName = "";
            switch (gameTaskInfo.TaskState)
            {
                case (int)TaskStateType.Awarded:
                    self.E_GetTipText.text = "完成";
                    self.E_GetButton.interactable = false;
                    buttonImageName = "smbtn_hui";
                    break;
                case (int)TaskStateType.UnComplete:
                    self.E_GetTipText.text = "去完成";
                    self.E_GetButton.interactable = true;
                    buttonImageName = "smbtnhuang";
                    break;
                case (int)TaskStateType.Completed:
                    self.E_GetTipText.text = "领取";
                    self.E_GetButton.interactable = true;
                    buttonImageName = "smbtnlv";
                    break;
            }

            // await CoroutineLockComponent.Instance.Wait(CoroutineLockType.ClearWordBar, self.uiTransform.GetHashCode());
            self.E_GetImage.sprite =
                    await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(ConstValue.CommonUIAtlasPath, buttonImageName);

            var count = gameTaskInfo.ActionCount;
            var needCount = self.TaskConfig.NeedActionCount;
            self.E_CountText.text = $"{count}/{needCount}";
        }

        //请求任务信息
        public static async void RequestTaskInfo(this Scroll_ItemTaskAward self, int configId)
        {
            var account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            var session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            var request = new C2M_GetTaskInfoWithConfigIdReqeust() { ConfigId = configId, AccountId = account };
            var response = await session.Call(request) as M2C_GetTaskInfoWithConfigIdResponse;
            if (response.Error == ErrorCode.ERR_Success)
            {
                self.SetGameTaskInfo(response.GameTaskInfo);
            }
        }
    }
}