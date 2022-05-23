using System;
using System.Collections.Generic;

namespace ET
{
    public class C2M_GetAllWeaponsRequestHandler: AMActorLocationRpcHandler<Unit, C2M_GetAllWeaponsRequest, M2C_GetAllWeaponsResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_GetAllWeaponsRequest request, M2C_GetAllWeaponsResponse response, Action reply)
        {
            var account = request.AccountId;
            List<Weapon> weapons = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<Weapon>(a => a.OwnerId.Equals(account) && a.State.Equals((int) StateType.Active));
            List<WeaponInfo> weaponInfos = new List<WeaponInfo>();
            foreach (var weapon in weapons)
            {
                weaponInfos.Add(weapon.GetInfo());
            }

            response.Error = ErrorCode.ERR_Success;
            response.WeaponInfos = weaponInfos;
            reply();
        }
    }
}