using System;
using System.Collections.Generic;

namespace ET
{
    public class C2M_UnSetHeroToTroopHandler: AMActorLocationRpcHandler<Unit, C2M_UnSetHeroFromTroopRequest, M2C_UnSetHeroFromTroopResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_UnSetHeroFromTroopRequest request, M2C_UnSetHeroFromTroopResponse response, Action reply)
        {
            TroopComponent troopComponent = unit.GetComponent<TroopComponent>();
            List<HeroCard> heroCards = await troopComponent.UnSetHeroFormTroop(request.HeroId);
            if (heroCards == null)
            {
                response.Error = ErrorCode.ERR_NotFoundHero;
                reply();
                return;
            }

            List<HeroCardInfo> heroCardInfos = new List<HeroCardInfo>();
            foreach (var heroCard in heroCards)
            {
                heroCardInfos.Add(heroCard.GetMessageInfo());
            }

            response.HeroCardInfos = heroCardInfos;
            response.Error = ErrorCode.ERR_Success;
            reply();

            //取消设置队伍
            // long account = request.AccountId;
            // long troopId = request.TroopId;
            // long heroId = request.HeroId;
            //
            // List<HeroCard> heroCards = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Query<HeroCard>(a =>
            //         a.OwnerId.Equals(account) &&
            //         a.TroopId.Equals(troopId) && a.State == (int)StateType.Active);
            //
            // HeroCard card = heroCards.Find(a => a.Id.Equals(heroId));
            // if (card == null)
            // {
            //     response.Error = ErrorCode.ERR_NotFoundHero;
            // }
            // else
            // {
            //     heroCards.Remove(card);
            //     card.TroopId = 0;
            //     card.InTroopIndex = -1;
            //     await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(card);
            //
            //     List<HeroCardInfo> heroCardInfos = new List<HeroCardInfo>();
            //     foreach (var heroCard in heroCards)
            //     {
            //         heroCardInfos.Add(heroCard.GetMessageInfo());
            //     }
            //
            //     response.HeroCardInfos = heroCardInfos;
            //     response.Error = ErrorCode.ERR_Success;
            // }
            //
            // reply();

            await ETTask.CompletedTask;
        }
    }
}