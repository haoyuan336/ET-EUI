using System;
using System.Collections.Generic;

namespace ET
{
    public class C2M_GetItemInfoRequestHandler: AMActorLocationRpcHandler<Unit, C2M_GetItemInfoRequest, M2C_GetItemInfoResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_GetItemInfoRequest request, M2C_GetItemInfoResponse response, Action reply)
        {
            // long account = request.AccountId;
            int configId = request.ConfigId;
            ItemComponent itemComponent = unit.GetComponent<ItemComponent>();
            Item item = itemComponent.GetChildByConfigId(configId);
            if (item == null)
            {
                response.Error = ErrorCode.ERR_Not_Found_Item;
                reply();
                return;
            }

            response.Error = ErrorCode.ERR_Success;
            response.ItemInfo = item.GetInfo();
            reply();
            await ETTask.CompletedTask;

        }
    }
}