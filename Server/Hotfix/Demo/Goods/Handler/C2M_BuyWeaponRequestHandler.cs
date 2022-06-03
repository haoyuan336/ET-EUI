using System;
using System.Collections.Generic;

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
                    await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(item);
                    item.Dispose();
                }
                else
                {
                    List<Weapon> items = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Query<Weapon>(a =>
                            a.OwnerId.Equals(accountId) && a.State.Equals(StateType.Active) && a.ConfigId.Equals(itemConfig.Id));
                    //todo 道具属于叠加道具，需要在原有道具基础上增加数目
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