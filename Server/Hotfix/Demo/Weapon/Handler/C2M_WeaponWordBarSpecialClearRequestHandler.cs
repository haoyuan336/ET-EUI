using System;
using System.Collections.Generic;
using System.Linq;
using SharpCompress.Writers;

namespace ET
{
    public class C2M_WeaponWordBarSpecialClearRequestHandler: AMActorLocationRpcHandler<Unit, C2M_WeaponWordBarSpeicalClearRequest,
        M2C_WeaponWordBarSpeicalClearResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_WeaponWordBarSpeicalClearRequest request, M2C_WeaponWordBarSpeicalClearResponse response,
        Action reply)
        {
            long accountId = request.AccountId;
            long weaponId = request.WeaponId;
            List<long> wordBarIds = request.WordBarIds;

            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.ClearWordBar, accountId.GetHashCode()))
            {
                //首先判断玩家是否拥有次道具
                List<Weapon> weapons = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Query<Weapon>(a =>
                        a.OwnerId.Equals(accountId) && a.Id.Equals(weaponId) && a.State == (int) StateType.Active);
                if (weapons.Count == 0)
                {
                    response.Error = ErrorCode.ERR_Not_Found_Weapon;
                    reply();
                    return;
                }

                foreach (var weapon in weapons)
                {
                    weapon.Dispose();
                }

                weapons = null;

                //取出来所有需要操作的词条
                List<WordBar> wordBars = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                        .Query<WordBar>(a => a.OwnerId.Equals(weaponId) && a.State == (int) StateType.Active);
                List<WordBar> chooseWordBars = wordBars.FindAll(a => wordBarIds.Exists(b => b.Equals(a.Id)));
                if (chooseWordBars.Count == 0)
                {
                    response.Error = ErrorCode.ERR_Not_Found_Weapon;
                    reply();
                    return;
                }

                List<WeaponWordBarsConfig> configs = WeaponWordBarsConfigCategory.Instance.GetAll().Values.ToList();

                List<ETTask> tasks = new List<ETTask>();
                foreach (var wordBar in chooseWordBars)
                {
                    //将选择到的词条的数值提升到最高值
                    //首先取出来配置
                    WeaponWordBarsConfig config = WeaponWordBarsConfigCategory.Instance.Get(wordBar.ConfigId);
                    var chooseConfigs = configs.FindAll(a => a.WordBarType == config.WordBarType);
                    //取出来相同类型
                    var value = chooseConfigs.Last().MaxValue;
                    wordBar.ConfigId = chooseConfigs.Last().Id;
                    wordBar.Value = value;
                    tasks.Add(DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(wordBar));
                }
                await ETTaskHelper.WaitAll(tasks);


                List<WordBarInfo> wordBarInfos = new List<WordBarInfo>();
                foreach (var wordBar in wordBars)
                {
                    wordBarInfos.Add(wordBar.GetInfo());
                }

                response.WordBarInfos = wordBarInfos;
                response.Error = ErrorCode.ERR_Success;
                reply();


                foreach (var wordBar in wordBars)
                {
                    wordBar.Dispose();
                }
                wordBars.Clear();
                wordBars = null;
                
            }

            
            await ETTask.CompletedTask;
        }
    }
}