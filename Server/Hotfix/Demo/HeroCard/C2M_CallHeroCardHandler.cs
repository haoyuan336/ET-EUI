using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ET
{
    public class C2M_CallHeroCardHandler: AMActorLocationRpcHandler<Unit, C2M_CallHeroCardRequest, M2C_CallHeroCardResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_CallHeroCardRequest request, M2C_CallHeroCardResponse response, Action reply)
        {
            Log.Debug("call hero message");
            response.Error = ErrorCode.ERR_Success;
            var keys = HeroConfigCategory.Instance.GetAll().Keys.ToArray();
            Log.Debug($"keys = {keys.Length}");
            int randomIndex = RandomHelper.RandomNumber(0, keys.Length);
            var key = keys[randomIndex];
            HeroConfig heroConfig = HeroConfigCategory.Instance.Get(key);

            HeroCard heroCard = new HeroCard();
            heroCard.Id = IdGenerater.Instance.GenerateId();
            heroCard.ConfigId = key;
            heroCard.OwnerId = request.Account;
            heroCard.InitWithConfig(heroConfig, heroCard.Id);
            
            response.HeroCardInfo = heroCard.GetMessageInfo();
            
            await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(heroCard);
            
            reply();
            await ETTask.CompletedTask;
        }
    }
}