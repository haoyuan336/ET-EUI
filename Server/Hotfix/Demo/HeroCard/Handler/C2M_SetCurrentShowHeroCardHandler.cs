using System;

namespace ET
{
    public class C2M_SetCurrentShowHeroCardHandler: AMActorLocationRpcHandler<Unit, C2M_SetCurrentShowHeroCardInfoRequest,
        M2C_SetCurrentShowHeroCardInfoResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_SetCurrentShowHeroCardInfoRequest request, M2C_SetCurrentShowHeroCardInfoResponse response,
        Action reply)
        {
            HeroCardComponent heroCardComponent = unit.GetComponent<HeroCardComponent>();
            bool success = await heroCardComponent.SetHeroCardShowAsync(request.HeroId);

            if (success)
            {
                response.Error = ErrorCode.ERR_Success;
                reply();
            }
            else
            {
                response.Error = ErrorCode.ERR_NotFoundHero;
                reply();
            }

            await ETTask.CompletedTask;
        }
    }
}