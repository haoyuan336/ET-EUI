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
            // response.Error = ErrorCode.ERR_Success;
            // List<HeroConfig> heroConfigs = HeroConfigCategory.Instance.GetAll().Values.ToList();
            // heroConfigs.RemoveAll(a => a.MaterialType == (int)HeroBagType.Materail);
            // int randomIndex = RandomHelper.RandomNumber(0, heroConfigs.Count);
            //
            // HeroCard heroCard = new HeroCard();
            // heroCard.Id = IdGenerater.Instance.GenerateId();
            // heroCard.ConfigId = heroConfigs[randomIndex].Id;
            // unit.AddChild(heroCard);
            // // HeroCard heroCard = unit.AddChild<HeroCard, int>(key);
            // await heroCard.Call(unit.DomainZone(), request.Account);
            // response.HeroCardInfo = heroCard.GetMessageInfo();

            
            HeroCardComponent heroCardComponent =  unit.GetComponent<HeroCardComponent>();
            HeroCard heroCard = heroCardComponent.CallOneHero();
            response.HeroCardInfo = heroCard.GetMessageInfo();
            response.Error = ErrorCode.ERR_Success;
            reply();
            await ETTask.CompletedTask;
        }
    }
}