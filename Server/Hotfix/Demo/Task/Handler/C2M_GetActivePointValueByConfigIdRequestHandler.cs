using System;
using System.Collections.Generic;

namespace ET
{
    public class C2M_GetActivePointValueByConfigIdRequestHandler: AMActorLocationRpcHandler<Unit, C2M_GetActivePointValueByConfigIdRequest,
        M2C_GetActivePointValueByConfigIdResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_GetActivePointValueByConfigIdRequest request,
        M2C_GetActivePointValueByConfigIdResponse response,
        Action reply)
        {
            var configId = request.ConfigId;
            var accountId = request.AccountId;
            long currentTime = 0;
            switch (configId)
            {
                case 1011:
                    //日常任务
                    currentTime = CustomHelper.GetCurrentDayTime(); //当前天开始的时间
                    break;
                case 1012:
                    //周常任务
                    currentTime = CustomHelper.GetCurrentDayTime(); //当前周开始的时间
                    break;
            }

            List<Item> items = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<Item>(a =>
                            a.OwnerId.Equals(accountId) && a.ConfigId.Equals(configId) && a.State == (int) StateType.Active &&
                            a.CreateTime >= currentTime);
            if (items.Count > 0)
            {
                response.Value = items[0].Count;
            }
            else
            {
                response.Value = 0;
            }

            response.Error = ErrorCode.ERR_Success;
            reply();
            items[0]?.Dispose();

            await ETTask.CompletedTask;
        }
    }
}