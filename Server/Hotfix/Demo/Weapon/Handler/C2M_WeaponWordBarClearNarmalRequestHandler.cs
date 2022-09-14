using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ET
{
    public class C2M_WeaponWordBarClearNarmalRequestHandler: AMActorLocationRpcHandler<Unit, C2M_WeaponWordBarClearNormalRequest,
        M2C_WeaponWordBarClearNormalResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_WeaponWordBarClearNormalRequest request, M2C_WeaponWordBarClearNormalResponse response,
        Action reply)
        {
            long account = request.AccountId;
            List<long> wordBarIds = request.WordBarIds;
            long weaponId = request.WeaponId;
            //首先检查用户是否拥有次装备

            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.ClearWordBar, account.GetHashCode()))
            {
                List<Weapon> weapons = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                        .Query<Weapon>(a => a.OwnerId.Equals(account) && a.Id.Equals(weaponId) && a.State == (int) StateType.Active);
                if (weapons.Count == 0)
                {
                    response.Error = ErrorCode.ERR_Not_Found_Weapon;
                    reply();
                    return;
                }

                weapons[0].Dispose();

                List<WordBar> wordBars = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                        .Query<WordBar>(a => a.OwnerId.Equals(weaponId) && a.State == (int) StateType.Active);
                //从列表里面找出来，需要洗练的词条

                List<WordBar> chooseWordBars = wordBars.FindAll(a => { return wordBarIds.Exists(b => { return a.Id.Equals(b); }); });
                if (chooseWordBars.Count != wordBarIds.Count)
                {
                    response.Error = ErrorCode.ERR_Not_Found_Weapon;
                    reply();

                    foreach (var wordBar in wordBars)
                    {
                        wordBar.Dispose();
                    }

                    wordBars.Clear();
                    return;
                }

                List<ETTask> tasks = new List<ETTask>();
                foreach (var wordBar in chooseWordBars)
                {
                    List<WeaponWordBarsConfig> configs = WeaponWordBarsConfigCategory.Instance.GetAll().Values.ToList();
                    configs.RemoveAll(a => a.Id.Equals(wordBar.ConfigId));
                    WeaponWordBarsConfig config = configs.RandomArray();
                    Log.Debug($"random config {config.Id}");
                    wordBar.ConfigId = config.Id;
                    wordBar.Value = RandomHelper.RandomNumber(config.MinValue, config.MaxValue);
                    tasks.Add(DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(wordBar));
                }

                await ETTaskHelper.WaitAll(tasks);

                List<WordBarInfo> wordBarInfos = new List<WordBarInfo>();

                foreach (var wordBar in wordBars)
                {
                    wordBarInfos.Add(wordBar.GetInfo());
                    wordBar.Dispose();
                }

                wordBars = null;
                response.WordBarInfos = wordBarInfos;
                response.Error = ErrorCode.ERR_Success;
                reply();
            }
        }
    }
}