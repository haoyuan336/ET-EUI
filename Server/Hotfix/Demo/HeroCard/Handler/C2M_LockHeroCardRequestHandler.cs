using System;
using System.Collections.Generic;

namespace ET
{
    public class C2M_LockHeroCardRequestHandler: AMActorLocationRpcHandler<Unit, C2M_LockHeroCardRequest, M2C_LockHeroCardResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_LockHeroCardRequest request, M2C_LockHeroCardResponse response, Action reply)
        {
            //锁定英雄卡牌的请求消息 ， 首先取出来英雄

            HeroCardComponent heroCardComponent = unit.GetComponent<HeroCardComponent>();
            HeroCard heroCard = heroCardComponent.LockHeroCard(request.HeroId, request.Lock);

            if (heroCard == null)
            {
                response.Error = ErrorCode.ERR_NotFoundHero;
                reply();
                return;
            }

            response.Error = ErrorCode.ERR_Success;
            reply();
            await ETTask.CompletedTask;
        }
    }
}