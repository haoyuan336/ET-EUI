using System;
using System.Collections.Generic;

namespace ET
{
    public class C2M_OffWeaponRequestHandler: AMActorLocationRpcHandler<Unit, C2M_OffWeaponRequest, M2C_OffWeaponResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_OffWeaponRequest request, M2C_OffWeaponResponse response, Action reply)
        {
            long account = request.Account;
            long heroId = request.HeroId;
            long weaponId = request.WeaponId;

            List<Weapon> weapons = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<Weapon>(a => a.Id.Equals(weaponId) && a.OwnerId.Equals(account) && a.State == (int) StateType.Active);
            if (weapons.Count == 0)
            {
                response.Error = ErrorCode.ERR_Success;
                reply();
                return;
            }
            //将weapon的onweaponid 设置为0

            Weapon weapon = weapons[0];
            weapon.OnWeaponHeroId = 0;
            await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(weapon);
            response.Error = ErrorCode.ERR_Success;
            reply();
            weapon.Dispose();
        }
    }
}