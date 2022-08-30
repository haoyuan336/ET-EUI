using System;
using System.Collections.Generic;
using MongoDB.Driver;

namespace ET
{
    public class C2M_SetHeroToTroopHandler: AMActorLocationRpcHandler<Unit, C2M_SetHeroToTroopRequest, M2C_SetHeroToTroopResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_SetHeroToTroopRequest request, M2C_SetHeroToTroopResponse response, Action reply)
        {
            // long TroopId = request.TroopId;
            long heroId = request.HeroId;
            // long accountId = request.AccountId;
            TroopComponent troopComponent = unit.GetComponent<TroopComponent>();
            List<HeroCard> heroCards = await troopComponent.SetHeroToTroop(heroId);

            // Log.Warning($"hero card {heroCards}");

            List<HeroCardInfo> heroCardInfos = new List<HeroCardInfo>();
            foreach (var heroCard in heroCards)
            {
                heroCardInfos.Add(heroCard.GetMessageInfo());
            }

            response.HeroCardInfos = heroCardInfos;
            response.Error = ErrorCode.ERR_Success;
            reply();
            
            
            // List<HeroCard> heroCards = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
            //         .Query<HeroCard>(a => a.Id.Equals(HeroId) && a.OwnerId.Equals(accountId));
            // if (heroCards.Count > 0)
            // {
            //     HeroCard card = heroCards[0];
            //     List<HeroCard> troopHeroCards =
            //             await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Query<HeroCard>((a) =>
            //                     a.TroopId.Equals(TroopId) && a.OwnerId.Equals(accountId) && a.State == (int) HeroCardState.Active);
            //     //找到空缺的位置
            //     var nullIndex = -1;
            //     if (troopHeroCards.Count == 0)
            //     {
            //         nullIndex = 0;
            //     }
            //     troopHeroCards.RemoveAll(a => a.Id.Equals(HeroId));
            //
            //     // Log.Warning($"troop card count {troopHeroCards.Count}");
            //     for (int i = 0; i < 3; i++)
            //     {
            //         // Log.Warning($"index {i + 1}");
            //         var find = troopHeroCards.Find(a => a.InTroopIndex.Equals(i));
            //         // Log.Warning($"find{find.InTroopIndex}");
            //         if (find == null)
            //         {
            //             nullIndex = i;
            //             break;
            //         }
            //     }
            //     if (nullIndex >= 0)
            //     {
            //         // Log.Warning($"找到空位置了{nullIndex}");
            //         //那么将此英雄放在此位置
            //         card.InTroopIndex = nullIndex;
            //         card.TroopId = TroopId;
            //         await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(card);
            //         troopHeroCards.Add(card);
            //     }
            //
            //     response.Error = ErrorCode.ERR_Success;
            //     List<HeroCardInfo> heroCardInfos = new List<HeroCardInfo>();
            //     foreach (var troopHeroCard in troopHeroCards)
            //     {
            //         heroCardInfos.Add(troopHeroCard.GetMessageInfo());
            //     }
            //     response.HeroCardInfos = heroCardInfos;
            // }
            // else
            // {
            //     response.Error = ErrorCode.ERR_NotFoundHero;
            // }

            reply();
            await ETTask.CompletedTask;
        }
    }
}