using System;
using System.Collections.Generic;

namespace ET
{
    public class C2M_GetWeaponWordBarsRequestHandler: AMActorLocationRpcHandler<Unit, C2M_GetWeaponWordBarsRequest, M2C_GetWeaponWordBarsResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_GetWeaponWordBarsRequest request, M2C_GetWeaponWordBarsResponse response, Action reply)
        {
            var accountId = request.Account;
            var weaponId = request.WeaponId;

            //取出来玩家的装备id
            List<Weapon> weapons = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Query<Weapon>(a =>
                    a.OwnerId.Equals(accountId) && a.Id.Equals(weaponId) && a.State == (int) StateType.Active);

            if (weapons.Count == 0)
            {
                response.Error = ErrorCode.ERR_Not_Found_Weapon;
                reply();
                return;
            }

            weapons[0].Dispose();

            //取出来此装备的所有词条
            List<WordBar> wordbars = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<WordBar>(a => a.OwnerId.Equals(weaponId) && a.State == (int) StateType.Active);
            List<WordBarInfo> weaponInfos = new List<WordBarInfo>();
            foreach (var wordbar in wordbars)
            {
                weaponInfos.Add(wordbar.GetInfo());
            }

            response.WordBarInfos = weaponInfos;
            response.Error = ErrorCode.ERR_Success;
            reply();

            await ETTask.CompletedTask;
        }
    }
}