using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace ET.MainScene
{
    public class C2M_BugWeaponRequestHandler: AMActorLocationRpcHandler<Unit, C2M_BuyWeaponRequest, M2C_BuyWeaponResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_BuyWeaponRequest request, M2C_BuyWeaponResponse response, Action reply)
        {
            Log.Warning("收到用户发来的购买装备的消息");
            var configId = request.ConfigId;
            var count = request.Count;
            var accountId = request.AccountId;
            //首先取出这个id对应的配置
            WeaponConfig weaponConfig = WeaponConfigCategory.Instance.Get(configId);
            var price = weaponConfig.Price;

            //取出这个玩家拥有的这个道具的数量
            List<Item> items = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<Item>(a => a.OwnerId.Equals(accountId) && a.ConfigId.Equals(weaponConfig.MoneyType));

            if (items.Count > 0)
            {
                if (items[0].Count >= price)
                {
                    response.Error = ErrorCode.ERR_Success;
                    //todo money足够，那么启动购买程序
                    //首先扣除用户相应数量的道具数量
                    items[0].Count -= price;
                    //然后保存道具数量
                    await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(items[0]);
                    //然后创建武器
                    Weapon weapon = new Weapon() { Id = IdGenerater.Instance.GenerateId(), ConfigId = configId, Count = 1, OwnerId = accountId };
                    //然后保存此武器
                    await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(weapon);
                    response.WeaponInfo = weapon.GetInfo();
                }
                else
                {
                    switch (weaponConfig.MoneyType)
                    {
                        case 1001:
                            response.Error = ErrorCode.ERR_GoldNotEnough;
                            break;
                        case 1002:
                            response.Error = ErrorCode.ERR_DiamondNotEnough;
                            break;
                    }
                }
            }
            else
            {
                switch (weaponConfig.MoneyType)
                {
                    case 1001:
                        response.Error = ErrorCode.ERR_GoldNotEnough;
                        break;
                    case 1002:
                        response.Error = ErrorCode.ERR_DiamondNotEnough;
                        break;
                }
            }

            reply();
            await ETTask.CompletedTask;
        }
    }
}