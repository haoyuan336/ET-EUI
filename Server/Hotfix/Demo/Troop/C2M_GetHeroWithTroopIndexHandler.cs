using System;
using System.Collections.Generic;

namespace ET
{
    public class C2M_GetHeroWithTroopIndexHandler: AMActorLocationRpcHandler<Unit, C2M_GetCurrentTroopHeroInfosRequest, M2C_GetCurrentTroopHeroInfosResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_GetCurrentTroopHeroInfosRequest request, M2C_GetCurrentTroopHeroInfosResponse response,
        Action reply)
        {

            TroopComponent troopComponent = unit.GetComponent<TroopComponent>();

            List<HeroCardInfo> heroCardInfos = await troopComponent.GetHeroCardInfosByIndexAsync();

            response.Error = ErrorCode.ERR_Success;
            response.HeroCardInfos = heroCardInfos;
            reply();

          
            await ETTask.CompletedTask;
        }
    }
}