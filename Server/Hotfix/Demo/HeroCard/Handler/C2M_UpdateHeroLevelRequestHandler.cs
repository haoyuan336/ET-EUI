using System;
using System.Collections.Generic;

namespace ET
{
    public class C2M_UpdateHeroLevelRequestHandler: AMActorLocationRpcHandler<Unit, C2M_UpdateHeroLevelRequest, M2C_UpdateHeroLevelResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_UpdateHeroLevelRequest request, M2C_UpdateHeroLevelResponse response, Action reply)
        {
            HeroCardComponent heroCardComponent = unit.GetComponent<HeroCardComponent>();
            HeroCard heroCard = heroCardComponent.UpdateHeroCardLevel(request.HeroId);

            if (heroCard == null)
            {
                response.Error = ErrorCode.ERR_EXP_Not_Enough;
                reply();
                return;
            }

            response.HeroCardInfo = heroCard.GetMessageInfo();
            response.Error = ErrorCode.ERR_Success;
            reply();
            await ETTask.CompletedTask;
        }
    }
}