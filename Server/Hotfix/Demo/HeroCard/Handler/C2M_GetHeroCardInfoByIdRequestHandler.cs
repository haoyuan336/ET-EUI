using System;
using System.Collections.Generic;

namespace ET
{
    public class C2M_GetHeroCardInfoByIdRequestHandler: AMActorLocationRpcHandler<Unit, C2M_GetHeroCardByIdRequest, M2C_GetHeroCardByIdResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_GetHeroCardByIdRequest request, M2C_GetHeroCardByIdResponse response, Action reply)
        {
            // long account = request.Account;
            // long heroId = request.HeroId;
            // List<HeroCard> heroCards = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
            //         .Query<HeroCard>(a => a.Id.Equals(heroId) && a.OwnerId.Equals(account) && a.State == (int) StateType.Active);
            // if (heroCards.Count == 0)
            // {
            //     response.Error = ErrorCode.ERR_NotFoundHero;
            //     reply();
            //     return;
            // }
            //
            // HeroCardInfo info = heroCards[0].GetMessageInfo();
            // response.HeroCardInfo = info;
            // response.Error = ErrorCode.ERR_Success;

            HeroCardComponent heroCardsComponent = unit.GetComponent<HeroCardComponent>();

            HeroCard heroCard = heroCardsComponent.GetChild<HeroCard>(request.HeroId);
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