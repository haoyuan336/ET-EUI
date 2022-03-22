using System;
using System.Collections.Generic;
using MongoDB.Driver;

namespace ET
{
    public class C2M_SetHeroToTroopHandler: AMActorLocationRpcHandler<Unit, C2M_SetHeroToTroopRequest, M2C_SetHeroToTroopResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_SetHeroToTroopRequest request, M2C_SetHeroToTroopResponse response, Action reply)
        {
            long TroopId = request.TroopId;
            long HeroId = request.HeroId;
            int InTroopIndex = request.InTroopIndex;

            Log.Debug($"TroopId{TroopId}");
            Log.Debug($"InTroopIndex{InTroopIndex}");

            List<HeroCard> heroCards =
                    await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Query<HeroCard>((a) => a.TroopId.Equals(TroopId));

            HeroCard heroCard = null;
            foreach (var card in heroCards)
            {
                if (card.InTroopIndex == InTroopIndex)
                {
                    heroCard = card;
                    break;
                }
            }

            if (heroCard != null)
            {
                //todo 此队伍的此位置上已经存在卡了，所以将此卡移出队伍,并保存
                heroCard.TroopId = 0;
                await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(heroCard);
            }

            //todo 从数据库里面取出对应的英雄卡牌，并且更新它的队伍id以及index
            List<HeroCard> currentHeroCards =
                    await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Query<HeroCard>(a => a.Id.Equals(HeroId));
            if (currentHeroCards.Count > 0)
            {
                HeroCard card = currentHeroCards[0];
                card.InTroopIndex = InTroopIndex;
                card.TroopId = TroopId;
                await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(card);
                response.HeroCardInfo = card.GetMessageInfo();
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