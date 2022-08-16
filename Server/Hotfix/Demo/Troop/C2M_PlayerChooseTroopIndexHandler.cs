using System;
using System.Collections.Generic;

namespace ET
{
    public class C2M_PlayerChooseTroopIndexHandler: AMActorLocationRpcHandler<Unit, C2M_PlayerChooseTroopIndexRequest,
        M2C_PlayerChooseTroopIndexResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_PlayerChooseTroopIndexRequest request, M2C_PlayerChooseTroopIndexResponse response,
        Action reply)
        {
            TroopComponent troopComponent = unit.GetComponent<TroopComponent>();
            List<HeroCard> heroCards = await troopComponent.PlayerChooseTroopIndex(request.Index);

            List<HeroCardInfo> heroCardInfos = new List<HeroCardInfo>();
            foreach (var heroCard in heroCards)
            {
                heroCardInfos.Add(heroCard.GetMessageInfo());
            }
            response.HeroCardInfo = heroCardInfos;
            response.Error = ErrorCode.ERR_Success;
            reply();
            await ETTask.CompletedTask;
        }
    }
}