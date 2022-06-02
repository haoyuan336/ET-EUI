using System;
using System.Collections.Generic;

namespace ET
{
    public class C2M_GetOnWeaponsRequestHandler: AMActorLocationRpcHandler<Unit, C2M_GetOnWeaponsRequest, M2C_GetOnWeaponsResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_GetOnWeaponsRequest request, M2C_GetOnWeaponsResponse response, Action reply)
        {
            long accountId = request.Account;
            long heroId = request.HeroId;
            List<HeroCard> heroCards = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<HeroCard>(a => a.OwnerId.Equals(accountId) && a.Id.Equals(heroId) && a.State == (int) StateType.Active);
            if (heroCards.Count == 0)
            {
                response.Error = ErrorCode.ERR_NotFoundHero;
                reply();
                return;
            }

            heroCards[0].Dispose();

            List<Weapon> weapons = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Query<Weapon>(a => a.OwnerId.Equals(accountId) &&
                    a.OnWeaponHeroId.Equals(heroId) && a.State == (int) StateType.Active);
            List<WeaponInfo> weaponInfos = new List<WeaponInfo>();
            foreach (var weapon in weapons)
            {
                weaponInfos.Add(weapon.GetInfo());
                weapon.Dispose();
            }

            response.Error = ErrorCode.ERR_Success;
            response.WeaponInfos = weaponInfos;
            reply();

            await ETTask.CompletedTask;
        }
    }
}