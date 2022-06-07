using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ET
{
    public class C2M_StrengthenWeaponRequestHandler: AMActorLocationRpcHandler<Unit, C2M_StrengthenWeaponRequest, M2C_StrengthenWeaponResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_StrengthenWeaponRequest request, M2C_StrengthenWeaponResponse response, Action reply)
        {
            var account = request.AccountId;
            var targetInfo = request.TargetWeaponInfo;
            var chooseWeaponInfos = request.ChooseWeaponInfos;
            //首先目标道具不能在消耗道具的列表里面
            if (chooseWeaponInfos.Exists(a => a.WeaponId.Equals(targetInfo.WeaponId)))
            {
                response.Error = ErrorCode.ERR_Not_Found_Weapon;
                reply();
            }

            //取出来，用户的所有拥有的装备
            List<Weapon> weapons = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<Weapon>(a => a.OwnerId.Equals(account) && a.State == (int) StateType.Active);

            Weapon targetweapon = weapons.Find(a => a.Id.Equals(targetInfo.WeaponId));
            if (targetweapon == null)
            {
                response.Error = ErrorCode.ERR_Not_Found_Weapon;
                reply();
                return;
            }
            //然后查看所选则的资源是否足够

            List<Weapon> chooseWeapons = new List<Weapon>();
            foreach (var weaponInfo in chooseWeaponInfos)
            {
                var weapon = weapons.Find(a => a.Id.Equals(weaponInfo.WeaponId));
                if (weapon != null && weaponInfo.Count <= weapon.Count)
                {
                    chooseWeapons.Add(weapon);
                }
                else
                {
                    response.Error = ErrorCode.ERR_MaterialNotEnough;
                    reply();
                    return;
                }
            }

            List<ETTask> tasks = new List<ETTask>();
            //将材料销毁，或者减去原来的个数
            var totalExp = 0;
            foreach (var weapon in chooseWeapons)
            {
                var weaponInfo = chooseWeaponInfos.Find(a => a.WeaponId.Equals(weapon.Id));

                weapon.Count -= weaponInfo.Count;
                var exp = WeaponHelper.GetTotalExp(weaponInfo) + weapon.CurrentExp;
                totalExp += exp;

                if (weapon.Count == 0)
                {
                    weapon.State = (int) StateType.Destroy;
                }

                tasks.Add(DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(weapon));
                weapon.Dispose();
            }

            // Log.Warning($"add exp {totalExp}");

            var endLevel = WeaponHelper.GetEndLevelWithExp(targetweapon.GetInfo(), totalExp);
            var lastExp = WeaponHelper.GetUpdateLevelLastExp(targetweapon.GetInfo(), totalExp);

            targetweapon.Level = endLevel;
            targetweapon.CurrentExp = lastExp;

            // 激活词条 首先获取词条个数
            List<WordBar> wordBars = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<WordBar>(a => a.OwnerId.Equals(targetweapon.Id));
            int[] wordBarCount = { 0, 5, 10, 15, 30 };
            var count = 0;
            for (int i = 0; i < wordBarCount.Length - 1; i++)
            {
                if (endLevel >= wordBarCount[i] && endLevel < wordBarCount[i + 1])
                {
                    count = i;
                }
            }

            WeaponsConfig weaponsConfig = WeaponsConfigCategory.Instance.Get(targetweapon.ConfigId);
            var wordCount = weaponsConfig.WordBarCount;
            count = Mathf.Min(count, wordCount);
            //装备的最大词条数量

            //未激活的词条数量
            var lastCount = count - wordBars.Count + 1;
            List<WeaponWordBarsConfig> wordBarsConfigs = WeaponWordBarsConfigCategory.Instance.GetAll().Values.ToList();
            for (int i = 0; i < lastCount; i++)
            {
                var config = wordBarsConfigs.RandomArray();
                WordBar wordBar = new WordBar()
                {
                    Id = IdGenerater.Instance.GenerateId(),
                    ConfigId = config.Id,
                    IsMain = false,
                    OwnerId = targetweapon.Id,
                    State = (int) StateType.Active,
                    Value = RandomHelper.RandomNumber(config.MinValue, config.MaxValue)
                };
                //储存词条
                await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(wordBar);
                wordBar.Dispose();
            }
            // Log.Warning($"词条数量{count}");

            // Log.Warning($"end level {targetweapon.Level}");
            // Log.Warning($"last exp {lastExp}");
            await ETTaskHelper.WaitAll(tasks);
            await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(targetweapon);

            response.Error = ErrorCode.ERR_Success;
            response.WeaponInfo = targetweapon.GetInfo();
            reply();
            targetweapon.Dispose();
        }
    }
}