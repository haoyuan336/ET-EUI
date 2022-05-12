using System;
using System.Collections.Generic;

namespace ET
{
    public class C2M_AddPowerRequestHandler: AMActorLocationRpcHandler<Unit, C2M_AddItemRequest, M2C_AddItemResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_AddItemRequest request, M2C_AddItemResponse response, Action reply)
        {
            long AccountId = request.AccountId;
            int count = request.Count;
            int configId = request.ConfigId;
            List<Item> items = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<Item>(a => a.OwnerId.Equals(AccountId) && a.ConfigId.Equals(configId));
            Log.Warning($"items count {items.Count}");
            if (items.Count == 0)
            {
                Item item = new Item() { Id = IdGenerater.Instance.GenerateId(), ConfigId = configId, Count = count, OwnerId = AccountId };
                items.Add(item);
            }
            else
            {
                items[0].Count += count;
            }

            await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(items[0]);
            response.ItemInfo = items[0].GetInfo();
            response.Error = ErrorCode.ERR_Success;
            reply();
            await ETTask.CompletedTask;
        }
    }
}