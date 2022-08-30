using System;
using System.Collections.Generic;
using System.Linq;

namespace ET
{
    public class C2M_BuyWeaponRequestHandler: AMActorLocationRpcHandler<Unit, C2M_BuyWeaponsRequest, M2C_BuyWeaponsResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_BuyWeaponsRequest request, M2C_BuyWeaponsResponse response, Action reply)
        {
            var accountId = request.AccountId;
            GoodsConfig goodsConfig = GoodsConfigCategory.Instance.Get(request.ConfigId);
            var moneyType = goodsConfig.MoneyType;
            var price = goodsConfig.Price;
            var count = request.Count;

            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.BuyWeapon, request.AccountId.GetHashCode()))
            {
                List<Item> moneyItems = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                        .Query<Item>(a =>
                                a.OwnerId.Equals(accountId) && a.State.Equals(StateType.Active) && a.ConfigId.Equals(moneyType) && a.Count >= price);

                if (moneyItems.Count > 0)
                {
                    //扣除相应的货币数量
                    moneyItems[0].Count -= price;
                    await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(moneyItems[0]);
                    //添加相应的道具
                    WeaponsConfig itemConfig = WeaponsConfigCategory.Instance.Get(goodsConfig.ConfigId);
                    //获取玩家是否拥有此类型的道具

                    // List<Weapon> weapons = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Query<Weapon>(a=>a.OwnerId.Equals(accountId) && a.State ==(int)StateType.Active)

                    if (itemConfig.MaterialType == (int) WeaponBagType.Weapon)
                    {
                        //储存道具
                        Weapon item = new Weapon()
                        {
                            Id = IdGenerater.Instance.GenerateId(),
                            OwnerId = accountId,
                            ConfigId = itemConfig.Id,
                            Count = 1,
                            State = (int) StateType.Active
                        };
                        response.WeaponInfo = item.GetInfo();
                        response.Error = ErrorCode.ERR_Success;
                        // Log.Debug("装备创建完成");
                        await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(item);
                        var weaponType = itemConfig.WeaponType;
                        //从配置里面取出来所有武器类型的词条

                        // Log.Debug("装备保存成功");
                        List<WeaponWordBarsConfig> wordBarsConfigs = WeaponWordBarsConfigCategory.Instance.GetAll().Values.ToList()
                                .FindAll(a => a.WeaponType == weaponType && a.Star == itemConfig.Star);
                        // Log.Debug($"取出词条配置成功 {wordBarsConfigs.Count}");
                        //从列表里面随机一条数据出来
                        WeaponWordBarsConfig wordBarsConfig = wordBarsConfigs.RandomArray();
                        // Log.Debug($"随机获取配置成功{wordBarsConfig.Id}");
                        var wordBar = new WordBar()
                        {
                            Id = IdGenerater.Instance.GenerateId(),
                            OwnerId = item.Id,
                            ConfigId = wordBarsConfig.Id,
                            IsMain = true,
                            State = (int) StateType.Active,
                            Value = RandomHelper.RandomNumber(wordBarsConfig.MinValue, wordBarsConfig.MaxValue)
                        };
                        // Log.Debug($"创建词条{item.Id}");
                        //给新创建的装备叠加词条
                        //
                        await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(wordBar);
                        // Log.Debug($"保存词条成功{item.Id}");

                        wordBar.Dispose();
                        item.Dispose();
                    }
                    else if (itemConfig.MaterialType == (int) WeaponBagType.Materail)
                    {
                        List<Weapon> items = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Query<Weapon>(a =>
                                a.OwnerId.Equals(accountId) && a.State.Equals(StateType.Active) && a.ConfigId.Equals(itemConfig.Id));
                        //todo 道具属于叠加道具，需要在原有道具基础上增加数目
                        // Log.Warning($"items {items.Count}");
                        if (items.Count > 0)
                        {
                            items[0].Count += count;
                            response.WeaponInfo = items[0].GetInfo();
                            response.Error = ErrorCode.ERR_Success;
                            await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(items[0]);
                        }
                        else
                        {
                            Weapon item = new Weapon()
                            {
                                Id = IdGenerater.Instance.GenerateId(),
                                OwnerId = accountId,
                                ConfigId = itemConfig.Id,
                                Count = count,
                                State = (int) StateType.Active
                            };
                            response.WeaponInfo = item.GetInfo();
                            response.Error = ErrorCode.ERR_Success;
                            await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(item);
                        }
                    }
                }
                else
                {
                    response.Error = ErrorCode.ERR_GoldNotEnough;
                }

                reply();
                await ETTask.CompletedTask;
            }
        }
    }
}