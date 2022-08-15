using System.Collections.Generic;
using System.Linq;

namespace ET
{
#if SERVER
    public class ItemComponentAwakeSystem: AwakeSystem<ItemComponent>
    {
        public override async void Awake(ItemComponent self)
        {
            //取出来 用户背包里面的所有道具
            // Log.Warning("item component awake system ");
            await ETTask.CompletedTask;
        }
    }

    public class ItemComponentDestorySystem: DestroySystem<ItemComponent>
    {
        public override void Destroy(ItemComponent self)
        {
            // Log.Warning("组件销毁时候 ，储存一次数据");
            // self.SaveData();
        }
    }

    public class ItemComponentBeforeDestroySystem: BeforeDestroySystem<ItemComponent>
    {
        public override void BeforeDestroy(ItemComponent self)
        {
            self.SaveData();
        }
    }

    public class ItemComponentUpdateSystem: UpdateSystem<ItemComponent>
    {
        public override void Update(ItemComponent self)
        {
            //每10分钟储存一次数据
            self.CurrentTime += 1;
            if (self.CurrentTime >= ConstValue.AutoSaveTime)
            {
                self.SaveData();
                self.CurrentTime = 0;
            }
        }
    }
#endif

    public static class ItemComponentSystem
    {
#if SERVER

        public static Item GetChildByConfigId(this ItemComponent self, int configId)
        {
            List<Item> items = self.GetChilds<Item>();
            if (items == null || items.Count == 0)
            {
                return null;
            }

            Item item = items.Find(a => a.ConfigId.Equals(configId));
            return item;
        }

        public static async ETTask<List<Item>> GetAllItems(this ItemComponent self)
        {
            List<Item> items = self.GetChilds<Item>();
            long accountId = self.GetParent<Unit>().AccountId;
            if (items == null || items.Count == 0)
            {
                //从数据库里面 取出来所有的 item
                items = await DBManagerComponent.Instance.GetZoneDB(self.DomainZone())
                        .Query<Item>(a => a.OwnerId.Equals(accountId) && a.State == (int)StateType.Active);
                //然后创建配置表里面，不存在的值
                foreach (var item in items)
                {
                    self.AddChild(item);
                }
            }

            List<ItemConfig> itemConfigs = ItemConfigCategory.Instance.GetAll().Values.ToList();
            foreach (var itemConfig in itemConfigs)
            {
                if (itemConfig.DefaultValue != 0 && !items.Exists(a => a.ConfigId.Equals(itemConfig.Id)))
                {
                    Item item = new Item()
                    {
                        OwnerId = accountId, Id = IdGenerater.Instance.GenerateId(), ConfigId = itemConfig.Id, Count = itemConfig.DefaultValue
                    };
                    items.Add(item);
                    self.AddChild(item);
                    //保存一下
                    DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Save(item).Coroutine();
                }
            }

            return items;
            // await ETTask.CompletedTask;
        }

        public static void SaveData(this ItemComponent self)
        {
            List<Item> items = self.GetChilds<Item>();
            if (items != null)
            {
                foreach (var item in items)
                {
                    DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Save(item).Coroutine();
                }
            }
        }

        public static Item AddItemCount(this ItemComponent self, int configId, int count)
        {
            List<Item> items = self.GetChilds<Item>();
            Item item = items.Find(a => a.ConfigId.Equals(configId));
            item.Count += count;
            return item;
        }
#endif
    }
}