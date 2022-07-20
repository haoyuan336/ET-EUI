using System;
using System.Collections.Generic;
using System.Security.Cryptography.Pkcs;

namespace ET
{
    public class C2M_GetTaskInfoWidthConfigRequestHandler: AMActorLocationRpcHandler<Unit, C2M_GetTaskInfoWithConfigIdReqeust,
        M2C_GetTaskInfoWithConfigIdResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_GetTaskInfoWithConfigIdReqeust request, M2C_GetTaskInfoWithConfigIdResponse response,
        Action reply)
        {
            //首先查找对应的任务，在规定的时间内
            var accountId = request.AccountId;
            var configId = request.ConfigId;

            TaskConfig taskConfig = TaskConfigCategory.Instance.Get(configId);

            long currentTime = 0;

            int taskType = taskConfig.Type;
            switch (taskType)
            {
                case (int) TaskType.DayTask:
                    //日常任务
                    currentTime = CustomHelper.GetCurrentDayTime(); //当前天开始的时间

                    break;
                case (int) TaskType.WeekTask:
                    currentTime = CustomHelper.GetCurrentDayTime(); //当前周开始的时间
                    break;
                case (int) TaskType.GrowUpTask:
                    //从0 开始
                    break;
            }
            //取出玩家的任务列表，
            //todo 1自己的任务，2任务的configid匹配3，激活状态 4,创建时间大于当前 日起始时间或周起始时间

            List<GameTask> gameTasks = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Query<GameTask>(a =>
                    a.OwnerId.Equals(accountId) && a.ConfigId.Equals(configId) && a.State == (int) StateType.Active && a.CreateTime >= currentTime);
            GameTask gameTask;
            if (gameTasks.Count > 0)
            {
                gameTask = gameTasks[0];
                if (gameTask.TaskState == (int) TaskStateType.UnComplete)
                {
                    //检查一下是否完成
                }
            }
            else
            {
                gameTask = new GameTask() { OwnerId = accountId, ConfigId = configId, CreateTime = TimeHelper.ServerNow() };
            }

            //如果是登录任务，那么默认完成
            if (gameTask.ConfigId == 2001)
            {
                gameTask.TaskState = (int) TaskStateType.Completed;
            }

            response.GameTaskInfo = gameTask.GetInfo();
            response.Error = ErrorCode.ERR_Success;
            reply();
            // gameTask.Dispose();
            await ETTask.CompletedTask;
        }
    }
}