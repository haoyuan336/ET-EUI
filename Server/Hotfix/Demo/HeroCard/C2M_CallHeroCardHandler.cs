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
            int key = keys[randomIndex];
            HeroCard heroCard = unit.AddChild<HeroCard, int>(key);
            await heroCard.Call(unit.DomainZone(), request.Account);
            heroCard.OwnerId = request.Account;
            response.HeroCardInfo = heroCard.GetMessageInfo();

            reply();
            await ETTask.CompletedTask;
        }
    }
}