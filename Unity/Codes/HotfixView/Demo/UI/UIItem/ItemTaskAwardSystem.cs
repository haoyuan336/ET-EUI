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
            self.E_GetButton.onClick.AddListener(self.OnGetButtonClick);
        }

        
        public static void OnGetButtonClick(this Scroll_ItemTaskAward self)
        {
            Log.Debug("获取按钮点击");
            
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
                switch (response.GameTaskInfo.TaskState)
                {
                    case (int) TaskStateType.Awarded:
                        self.E_GetTipText.text = "完成";
                        self.E_GetButton.interactable = false;

                        break;
                    case (int) TaskStateType.UnComplete:
                        self.E_GetTipText.text = "去完成";
                        self.E_GetButton.interactable = true;
                        break;
                    case (int) TaskStateType.Completed:
                        self.E_GetTipText.text = "领取";
                        self.E_GetButton.interactable = true;
                        break;
                }
            }
        }
    }
}