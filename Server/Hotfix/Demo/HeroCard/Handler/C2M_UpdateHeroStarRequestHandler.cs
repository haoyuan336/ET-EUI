using System;
using System.Collections.Generic;

namespace ET
{
    public class C2M_UpdateHeroStarRequestHandler: AMActorLocationRpcHandler<Unit, C2M_UpdateHeroStarRequest, M2C_UpdateHeroStarResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_UpdateHeroStarRequest request, M2C_UpdateHeroStarResponse response, Action reply)
        {
            HeroCardComponent heroCardComponent = unit.GetComponent<HeroCardComponent>();
            HeroCard heroCard = heroCardComponent.UpdateHeroStar(request.HeroId, request.MaterialHeroId);
            if (heroCard == null)
            {
                response.Error = ErrorCode.ERR_NotFoundHero;
                reply();
                return;
            }
            response.Error = ErrorCode.ERR_Success;
            response.HeroCardInfo = heroCard.GetMessageInfo();
            reply();
            await ETTask.CompletedTask;
        }
        
    }
}