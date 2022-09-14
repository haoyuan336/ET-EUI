using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;

namespace ET
{
    public class C2M_GetAllItemRequestHandler: AMActorLocationRpcHandler<Unit, C2M_GetAllItemRequest, M2C_GetAllItemResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_GetAllItemRequest request, M2C_GetAllItemResponse response, Action reply)
        {
            List<ItemInfo> itemInfos = new List<ItemInfo>();
            List<Item> items = await unit.GetComponent<ItemComponent>().GetAllItems();
            foreach (var item in items)
            {
                itemInfos.Add(item.GetInfo());
            }
            response.Error = ErrorCode.ERR_Success;
            response.ItemInfos = itemInfos;
            reply();

            await ETTask.CompletedTask;
        }
    }
}