using System;
using System.Collections.Generic;

namespace ET
{
    public class C2M_UpdateHeroRankRequestHandler: AMActorLocationRpcHandler<Unit, C2M_UpdateHeroRankRequest, M2C_UpdateHeroRankResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_UpdateHeroRankRequest request, M2C_UpdateHeroRankResponse response, Action reply)
        {
            HeroCardComponent heroCardComponent = unit.GetComponent<HeroCardComponent>();

            HeroCard heroCard = heroCardComponent.UpdateHeroRank(request.HeroId);
            if (heroCard == null)
            {
                response.Error = ErrorCode.ERR_EXP_Not_Enough;
                reply();
                return;
            }
            response.HeroCardInfo = heroCard.GetMessageInfo();
            reply();
            await ETTask.CompletedTask;
            reply();
        }
    }
}