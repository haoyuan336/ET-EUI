using System;
using System.Collections.Generic;

namespace ET
{
    public class C2M_GetItemInfoRequestHandler: AMActorLocationRpcHandler<Unit, C2M_GetItemInfoRequest, M2C_GetItemInfoResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_GetItemInfoRequest request, M2C_GetItemInfoResponse response, Action reply)
        {
            long account = request.AccountId;
            int configId = request.ConfigId;
            //取出此玩家的相关item
            //状态1为可以使用的状态
            Item item;
            List<Item> items = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<Item>(a => a.OwnerId.Equals(account) && a.ConfigId.Equals(configId) && a.State == 1);
            if (items.Count == 0)
            {
                ItemConfig config = ItemConfigCategory.Instance.Get(configId);
                item = new Item()
                {
                    Id = IdGenerater.Instance.GenerateId(),
                    OwnerId = account,
                    ConfigId = configId,
                    Count = config.DefaultValue,
                    State = 1
                };
            }
            else
            {
                item = items[0];
            }

            ItemInfo itemInfo = item.GetInfo();
            response.Error = ErrorCode.ERR_Success;
            response.ItemInfo = itemInfo;
            reply();
            item.Dispose();

        }
    }
}