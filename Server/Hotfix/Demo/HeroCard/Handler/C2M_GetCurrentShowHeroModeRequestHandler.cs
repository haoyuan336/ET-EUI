using System;

namespace ET
{
    public class C2M_GetCurrentShowHeroModeRequestHandler: AMActorLocationRpcHandler<Unit, C2M_GetCurrentShowHeroCardInfoRequest,
        M2C_GetCurrentShowHeroCardInfoResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_GetCurrentShowHeroCardInfoRequest request, M2C_GetCurrentShowHeroCardInfoResponse response,
        Action reply)
        {
            HeroCardComponent heroCardComponent = unit.GetComponent<HeroCardComponent>();

            HeroCard heroCard = await heroCardComponent.GetCurrentShowHeroAsync();
            if (heroCard == null)
            {
                response.Error = ErrorCode.ERR_NotFoundHero;
                reply();
                return;
            }
            response.Error = ErrorCode.ERR_Success;
            response.HeroCardInfo = heroCard.GetMessageInfo();
            reply();
        }
    }
}