using System;
using System.Collections.Generic;

namespace ET
{
    public class C2M_UpdateHeroRankRequestHandler: AMActorLocationRpcHandler<Unit, C2M_UpdateHeroRankRequest, M2C_UpdateHeroRankResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_UpdateHeroRankRequest request, M2C_UpdateHeroRankResponse response, Action reply)
        {
            long account = request.Account;
            long heroId = request.HeroId;

            List<HeroCard> heroCards = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<HeroCard>(a => a.OwnerId.Equals(account) && a.Id.Equals(heroId) && a.State.Equals((int) HeroCardState.Active));
            if (heroCards.Count == 0)
            {
                response.Error = ErrorCode.ERR_NotFoundHero;
            }
            else
            {
                HeroCard heroCard = heroCards[0];
                HeroConfig heroConfig = HeroConfigCategory.Instance.Get(heroCard.ConfigId);
                var currentRank = heroCard.Rank;
                if (currentRank >= heroConfig.MaxRank)
                {
                    response.Error = ErrorCode.ERR_MAX_Rank;
                }
                else
                {
                    response.Error = ErrorCode.ERR_Success;
                    heroCard.Rank++;
                    await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(heroCard);
                    response.HeroCardInfo = heroCard.GetMessageInfo();
                    heroCard.Dispose();
                }
            }

            reply();
        }
    }
}