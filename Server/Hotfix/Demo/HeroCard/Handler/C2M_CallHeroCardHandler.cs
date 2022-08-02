using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

namespace ET
{
    public class C2M_CallHeroCardHandler: AMActorLocationRpcHandler<Unit, C2M_CallHeroCardRequest, M2C_CallHeroCardResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_CallHeroCardRequest request, M2C_CallHeroCardResponse response, Action reply)
        {
            Log.Debug("call hero message");
            response.Error = ErrorCode.ERR_Success;

            // heroConfigs.RemoveAll(a => a.Value.MaterialType == (int)HeroBagType.Materail);
            // heroConfigs.Remove()

            List<HeroConfig> heroConfigs = HeroConfigCategory.Instance.GetAll().Values.ToList(); 

            heroConfigs.RemoveAll(a=>a.MaterialType == (int)HeroBagType.Materail);
            int randomIndex = RandomHelper.RandomNumber(0, heroConfigs.Count);
            
            // if (config.MaterialType == (int)HeroBagType.Materail)
            // {
                // Log.Warning("召唤到的是材料");
                //召唤出来的是 材料
                //首先从数据库里面查询一下 ，玩家是否拥有此类型的英雄材料
                // List<HeroCard> heroCards = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                //         .Query<HeroCard>(a => a.OwnerId.Equals(request.Account) && a.ConfigId.Equals(key));
                // if (heroCards.Count > 0)
                // {
                //     //说明数据库里面存在此类型的英雄材料，那么数目自加并且保存
                //     heroCards[0].Count++;
                //     // Log.Warning($"存在此材料 自加1 {key}");
                //     await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(heroCards[0]);
                // }
                // else
                // {
                //     // Log.Warning("不存在此材料，需要重新创建");
                //     HeroCard heroCard = new HeroCard();
                //     heroCard.Id = IdGenerater.Instance.GenerateId();
                //     heroCard.ConfigId = key;
                //     unit.AddChild(heroCard);
                //     await heroCard.Call(unit.DomainZone(), request.Account);
                // }
            // }
            // else
            // {
                HeroCard heroCard = new HeroCard();
                heroCard.Id = IdGenerater.Instance.GenerateId();
                heroCard.ConfigId = heroConfigs[randomIndex].Id;
                unit.AddChild(heroCard);
                // HeroCard heroCard = unit.AddChild<HeroCard, int>(key);
                await heroCard.Call(unit.DomainZone(), request.Account);
                response.HeroCardInfo = heroCard.GetMessageInfo();
            // }

            reply();
            await ETTask.CompletedTask;
        }
    }
}