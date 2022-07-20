using System.Collections.Generic;
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
            self.RequestTaskInfo(taskConfig.Id);

            self.E_GetButton.onClick.RemoveAllListeners();
            self.E_GetButton.AddListenerAsync(self.OnGetButtonClick);
        }

        public static async ETTask OnGetButtonClick(this Scroll_ItemTaskAward self)
        {
            Log.Debug("获取按钮点击");
            switch (self.GameTaskInfo.TaskState)
            {
                case (int) TaskStateType.Completed:
                    Log.Debug("领取任务奖励");
                    await self.GetTaskAward();
                    break;
                case (int) TaskStateType.UnComplete:
                    Log.Debug("去完成游戏");

                    break;
            }

            await ETTask.CompletedTask;
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
                case (int) TaskStateType.Awarded:
                    self.E_GetTipText.text = "完成";
                    self.E_GetButton.interactable = false;
                    buttonImageName = "smbtn_hui";
                    break;
                case (int) TaskStateType.UnComplete:
                    self.E_GetTipText.text = "去完成";
                    self.E_GetButton.interactable = true;
                    buttonImageName = "smbtnhuang";
                    break;
                case (int) TaskStateType.Completed:
                    self.E_GetTipText.text = "领取";
                    self.E_GetButton.interactable = true;
                    buttonImageName = "smbtnlv";
                    break;
            }

            self.E_GetImage.sprite =
                    await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(ConstValue.CommonUIAtlasPath, buttonImageName);
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