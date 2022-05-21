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
            long accountId = request.AccountId;
            List<Item> items = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<Item>(a => a.OwnerId.Equals(accountId) && a.State.Equals(1));
            response.Error = ErrorCode.ERR_Success;
            List<ItemInfo> itemInfos = new List<ItemInfo>();

            List<KeyValuePair<int, ItemConfig>> itemConfigs = ItemConfigCategory.Instance.GetAll().ToList();
            List<Item> addItems = new List<Item>();
            foreach (var itemConfig in itemConfigs)
            {
                bool isEx = items.Exists(a => a.ConfigId.Equals(itemConfig.Key));
                if (!isEx)
                {
                    Item item = new Item()
                    {
                        Id = IdGenerater.Instance.GenerateId(),
                        OwnerId = accountId,
                        ConfigId = itemConfig.Key,
                        State = 1,
                        Count = itemConfig.Value.DefaultValue
                    };
                    addItems.Add(item);
                    await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(item);
                    // item.Dispose();
                }
            }

            // items.Concat(addItems);
            foreach (var item in addItems)
            {
                items.Add(item);
            }

            addItems.Clear();
            addItems = null;

            // items
            foreach (var item in items)
            {
                itemInfos.Add(item.GetInfo());
                item.Dispose();
            }

            items.Clear();
            items = null;
            response.Error = ErrorCode.ERR_Success;
            response.ItemInfos = itemInfos;

            reply();

            await ETTask.CompletedTask;
        }
    }
}