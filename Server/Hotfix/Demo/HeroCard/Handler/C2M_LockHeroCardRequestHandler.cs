using System;
using System.Collections.Generic;

namespace ET
{
    public class C2M_LockHeroCardRequestHandler: AMActorLocationRpcHandler<Unit, C2M_LockHeroCardRequest, M2C_LockHeroCardResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_LockHeroCardRequest request, M2C_LockHeroCardResponse response, Action reply)
        {
            //锁定英雄卡牌的请求消息 ， 首先取出来英雄

            var account = request.Account;
            var herocardId = request.HeroId;
            List<HeroCard> heroCards = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<HeroCard>(a => a.OwnerId.Equals(account) && a.Id.Equals(herocardId) && a.State == (int)StateType.Active);

            if (heroCards.Count == 0)
            {
                response.Error = ErrorCode.ERR_NotFoundHero;
                reply();
                return;
            }

            HeroCard heroCard = heroCards[0];
            heroCard.IsLock = request.Lock;
            await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(heroCard);
            response.Error = ErrorCode.ERR_Success;
            reply();

            await ETTask.CompletedTask;
        }
    }
}