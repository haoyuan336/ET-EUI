﻿using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using MongoDB.Driver;

namespace ET
{
    public class C2M_UpdateOnWeaponRequestHandler: AMActorLocationRpcHandler<Unit, C2M_UpdateOnWeaponRequest, M2C_UpdateOnWeaponResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_UpdateOnWeaponRequest request, M2C_UpdateOnWeaponResponse response, Action reply)
        {
            long accountId = request.Account;
            long heroId = request.HeroId;
            long weaponId = request.WeaponId;

            //取出英雄来
            List<HeroCard> heroCards =
                    await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Query<HeroCard>(a =>
                            a.OwnerId.Equals(accountId) && a.Id.Equals(heroId) && a.State == (int) HeroCardState.Active);
            if (heroCards.Count == 0)
            {
                response.Error = ErrorCode.ERR_NotFoundHero;
                reply();
                return;
            }

            //取出装备来
            List<Weapon> weapons = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<Weapon>(a => a.OwnerId.Equals(accountId) && a.Id.Equals(weaponId) && a.State == (int) StateType.Active);
            if (weapons.Count == 0)
            {
                response.Error = ErrorCode.ERR_Not_Found_Weapon;
                reply();
                return;
            }

            Weapon targetWeapon = weapons[0];

            //找到已经装备的英雄
            List<Weapon> onWeapons = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<Weapon>(a => a.OnWeaponHeroId.Equals(heroId) && a.State == (int) StateType.Active);
            // Log.Warning($"已经装备了的装备信息{onWeapons.Count}");
            //取出来，所有已经装备了的装备， 找到与将要装备相同类型的装备
            WeaponsConfig targetConfig = WeaponsConfigCategory.Instance.Get(targetWeapon.ConfigId);
            List<Weapon> oldWeapons = onWeapons.FindAll(a =>
            {
                var config = WeaponsConfigCategory.Instance.Get(a.ConfigId);
                if (config.WeaponType == targetConfig.WeaponType && !a.Id.Equals(targetConfig.Id))
                {
                    return true;
                }

                return false;
            });

            onWeapons.RemoveAll(a =>
            {
                var config = WeaponsConfigCategory.Instance.Get(a.ConfigId);
                if (config.WeaponType == targetConfig.WeaponType)
                {
                    return true;
                }

                return false;
            });
            // Log.Warning($"old weapons  {oldWeapons.Count}");
            if (oldWeapons.Count != 0)
            {
                //说明原位置已经装备了武器了，需要将此装备移除
                List<ETTask> tasks = new List<ETTask>();
                foreach (var weapon in oldWeapons)
                {
                    weapon.OnWeaponHeroId = 0;
                    tasks.Add(DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(weapon));
                    weapon.Dispose();
                }
                await ETTaskHelper.WaitAll(tasks);
            }
            //然后储存当前装备
            targetWeapon.OnWeaponHeroId = heroId;
            onWeapons.Add(targetWeapon);
            List<WeaponInfo> weaponInfos = new List<WeaponInfo>();
            foreach (var weapon in onWeapons)
            {
                weaponInfos.Add(weapon.GetInfo());
                weapon.Dispose();
            }

            await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(targetWeapon);
            targetWeapon.Dispose();
            response.Error = ErrorCode.ERR_Success;
            response.WeaponInfos = weaponInfos;
            reply();
            await ETTask.CompletedTask;
        }
    }
}