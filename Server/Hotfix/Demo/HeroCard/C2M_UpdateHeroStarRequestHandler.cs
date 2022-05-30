using System;
using System.Collections.Generic;

namespace ET
{
    public class C2M_UpdateHeroStarRequestHandler: AMActorLocationRpcHandler<Unit, C2M_UpdateHeroStarRequest, M2C_UpdateHeroStarResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_UpdateHeroStarRequest request, M2C_UpdateHeroStarResponse response, Action reply)
        {
            long account = request.Account;
            long heroId = request.HeroId;

            List<HeroCard> heroCards = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Query<HeroCard>(a =>
                    a.OwnerId.Equals(account) && a.Id.Equals(heroId) && a.State.Equals((int) HeroCardState.Active));
            if (heroCards.Count > 0)
            {
                HeroCard heroCard = heroCards[0];
                if (heroCard.Star >= 5)
                {
                    response.Error = ErrorCode.ERR_MAX_Star;
                }
                else
                {
                    response.Error = ErrorCode.ERR_Success;
                    heroCard.Star += 1;
                    await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(heroCard);
                    response.HeroCardInfo = heroCard.GetMessageInfo();
                }

                heroCard.Dispose();
            }
            else
            {
                response.Error = ErrorCode.ERR_NotFoundHero;
            }
            
            reply();
            await ETTask.CompletedTask;
        }
        
    }
}