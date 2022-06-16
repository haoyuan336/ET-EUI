using System;
using System.Collections.Generic;

namespace ET
{
    public class C2M_GetAllHeroListInfoRequestHandler: AMActorLocationRpcHandler<Unit, C2M_GetAllHeroCardListRequest, M2C_GetAllHeroCardListResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_GetAllHeroCardListRequest request, M2C_GetAllHeroCardListResponse response, Action reply)
        {
            M2C_GetAllHeroCardListResponse m2CGetAllHeroCardListResponse = new M2C_GetAllHeroCardListResponse();
            m2CGetAllHeroCardListResponse.Error = ErrorCode.ERR_Success;
            long AccountId = request.Account;
            int bagType = request.BagType;
            List<HeroCard> heroCards = new List<HeroCard>();
            heroCards = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<HeroCard>(a => a.OwnerId.Equals(AccountId) && a.State == (int) HeroCardState.Active && a.Count > 0);

            List<HeroCardInfo> heroCardInfos = new List<HeroCardInfo>();
            foreach (var heroCard in heroCards)
            {
                heroCardInfos.Add(heroCard.GetMessageInfo());
            }

            response.HeroCardInfos = heroCardInfos;
            reply();
            await ETTask.CompletedTask;
        }
    }
}