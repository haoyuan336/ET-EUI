using System;
using System.Collections.Generic;

namespace ET
{
    public class C2M_AddPowerRequestHandler: AMActorLocationRpcHandler<Unit, C2M_AddItemRequest, M2C_AddItemResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_AddItemRequest request, M2C_AddItemResponse response, Action reply)
        {
            int count = request.Count;
            int configId = request.ConfigId;
            Item item = unit.GetComponent<ItemComponent>().AddItemCount(configId, count);
            response.ItemInfo = item.GetInfo();
            reply();
            await ETTask.CompletedTask;
        }
    }
}