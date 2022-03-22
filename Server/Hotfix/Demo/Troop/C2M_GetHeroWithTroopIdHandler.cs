using System;
using System.Collections.Generic;

namespace ET
{
    public class C2M_GetHeroWithTroopIdHandler: AMActorLocationRpcHandler<Unit, C2M_GetHeroInfosWithTroopIdRequest, M2C_GetHeroInfosWithTroopIdResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_GetHeroInfosWithTroopIdRequest request, M2C_GetHeroInfosWithTroopIdResponse response,
        Action reply)
        {
            
            long TroopId = request.TroopId;
            List<HeroCard> heroCards =
                    await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Query<HeroCard>((a) => a.TroopId.Equals(TroopId));
            List<HeroCardInfo> heroCardInfos = new List<HeroCardInfo>();
            foreach (var heroCard in heroCards)
            {
                heroCardInfos.Add(heroCard.GetMessageInfo());
            }

            response.HeroCardInfos = heroCardInfos;
            response.Error = ErrorCode.ERR_Success;
            reply();
            await ETTask.CompletedTask;
        }
    }
}