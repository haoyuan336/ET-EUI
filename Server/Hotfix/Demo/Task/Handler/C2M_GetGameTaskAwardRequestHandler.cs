using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ET
{
    public class C2M_GetGameTaskAwardRequestHandler: AMActorLocationRpcHandler<Unit, C2M_GetGameTaskAwardRequest, M2C_GetGameTaskAwardResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_GetGameTaskAwardRequest request, M2C_GetGameTaskAwardResponse response, Action reply)
        {
            var account = request.AccountId;
            var taskId = request.TaskId;

            List<ItemInfo> itemInfos = new List<ItemInfo>();

            // 取出来，此玩家的任务
            List<GameTask> gameTasks = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<GameTask>(a => a.OwnerId.Equals(account) && a.Id.Equals(taskId) && a.State == (int) StateType.Active);
            if (gameTasks.Count == 0)
            {
                response.Error = ErrorCode.ERR_Not_Fount_GameTask; //未找到任务
                reply();
                return;
            }

            GameTask gameTask = gameTasks[0];

            if (gameTask.TaskState == (int) TaskStateType.Awarded)
            {
                response.Error = ErrorCode.ERR_GameTask_Award_AlGet; //任务奖励已经领取
                reply();
                return;
            }

            if (gameTask.TaskState == (int) TaskStateType.UnComplete)
            {
                response.Error = ErrorCode.ERR_GameTask_UnComplete; //任务还未完成
                reply();
                return;
            }

            var configId = gameTask.ConfigId;
            //取出来任务配置
            TaskConfig taskConfig = TaskConfigCategory.Instance.Get(configId);

            long currentTime = 0;
            int itemConfigId = 0;
            switch (taskConfig.Type)
            {
                case (int) TaskType.DayTask:
                    //日常任务
                    currentTime = CustomHelper.GetCurrentDayTime(); //当前天开始的时间
                    itemConfigId = 1011;
                    break;
                case (int) TaskType.WeekTask:
                    currentTime = CustomHelper.GetCurrentWeekTime(); //当前周开始的时间
                    itemConfigId = 1012;
                    break;
                case (int) TaskType.GrowUpTask:
                    //从0 开始
                    break;
            }
            //todo-----------------------------------------增加活跃度值--------------------------------------------
            //取出来相应的 活跃度值
            //todo 过滤条件 1.自己的道具2，配置id相同3，状态为未销毁4，创建时间 大于当前起始时间

            List<Item> activePointItems = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Query<Item>(a => a.OwnerId.Equals(account) &&
                    a.ConfigId.Equals(itemConfigId) && a.State == (int) StateType.Active &&
                    a.CreateTime >= currentTime);

            Item activePointItem;
            if (activePointItems.Count == 0)
            {
                activePointItem = new Item() { OwnerId = account, ConfigId = itemConfigId };
            }
            else
            {
                activePointItem = activePointItems[0];
            }

            activePointItem.Count += taskConfig.ActiveValue;

            await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(activePointItem);
            // itemInfos.Add(activePointItem.GetInfo());
            activePointItem?.Dispose();

            //todo-----------------------------------------增加活跃度值-------------------------------------------- 

            //todo----------------------------------------增加道具奖励---------------------------------------------

            //取出来奖励configId
            var awardConfigId = taskConfig.AwarditemConfigId; //奖励的id 
            var awardCount = taskConfig.AwardCount; //奖励的count
            // var awardConfig = ItemConfigCategory.Instance.Get(awardConfigId);

            //取出来，玩家拥有的此道具的数据
            List<Item> items = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Query<Item>(a =>
                    a.OwnerId.Equals(account) && a.ConfigId.Equals(awardConfigId) && a.State == (int) StateType.Active);
            Item awardItem;
            if (items.Count == 0)
            {
                awardItem = new Item() { OwnerId = account, ConfigId = configId };
            }
            else
            {
                awardItem = items[0];
            }

            awardItem.Count += awardCount;

            await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(awardItem);

            var iteminfo = awardItem.GetInfo();
            iteminfo.Count = awardCount;

            itemInfos.Add(iteminfo);
            
            awardItem?.Dispose();

            //todo----------------------------------------增加道具奖励---------------------------------------------

            //todo----------------------------------------改变当前任务状态-----------------------------------------
            gameTask.TaskState = (int)TaskStateType.Awarded;

            await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(gameTask);
            
            
            response.Error = ErrorCode.ERR_Success;
            response.ItemInfos = itemInfos;
            response.GameTaskInfo = gameTask.GetInfo();
            gameTask.Dispose();
            reply();

            await ETTask.CompletedTask;
        }
    }
}