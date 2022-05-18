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
            long accountId = request.AccountId;
            // int InTroopIndex = request.InTroopIndex;

            // Log.Debug($"TroopId{TroopId}");
            // Log.Debug($"InTroopIndex{InTroopIndex}");

            List<HeroCard> heroCards = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<HeroCard>(a => a.Id.Equals(HeroId) && a.OwnerId.Equals(accountId));
            if (heroCards.Count > 0)
            {
                HeroCard card = heroCards[0];
                List<HeroCard> troopHeroCards =
                        await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Query<HeroCard>((a) =>
                                a.TroopId.Equals(TroopId) && a.OwnerId.Equals(accountId) && a.State == (int) HeroCardState.Active);
                //找到空缺的位置
                var nullIndex = -1;
                if (troopHeroCards.Count == 0)
                {
                    nullIndex = 0;
                }

                // Log.Warning($"troop card count {troopHeroCards.Count}");
                for (int i = 0; i < 3; i++)
                {
                    // Log.Warning($"index {i + 1}");
                    var find = troopHeroCards.Find(a => a.InTroopIndex.Equals(i));
                    // Log.Warning($"find{find.InTroopIndex}");
                    if (find == null)
                    {
                        nullIndex = i;
                        break;
                    }
                }

                if (nullIndex >= 0)
                {
                    // Log.Warning($"找到空位置了{nullIndex}");
                    //那么将此英雄放在此位置
                    card.InTroopIndex = nullIndex;
                    card.TroopId = TroopId;
                    await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(card);
                    troopHeroCards.Add(card);

                    response.Error = ErrorCode.ERR_Success;

                    List<HeroCardInfo> heroCardInfos = new List<HeroCardInfo>();
                    foreach (var troopHeroCard in troopHeroCards)
                    {
                        heroCardInfos.Add(troopHeroCard.GetMessageInfo());
                    }

                    response.HeroCardInfos = heroCardInfos;
                }
                else
                {
                    response.Error = ErrorCode.ERR_TroopIsFull;
                }
            }
            else
            {
                response.Error = ErrorCode.ERR_NotFoundHero;
            }

            // HeroCard heroCard = null;
            // foreach (var card in heroCards)
            // {
            //     if (card.InTroopIndex == InTroopIndex)
            //     {
            //         heroCard = card;
            //         break;
            //     }
            // }
            //
            // if (heroCard != null)
            // {
            //     //todo 此队伍的此位置上已经存在卡了，所以将此卡移出队伍,并保存
            //     heroCard.TroopId = 0;
            //     await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(heroCard);
            // }
            //
            // //todo 从数据库里面取出对应的英雄卡牌，并且更新它的队伍id以及index
            // List<HeroCard> currentHeroCards =
            //         await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Query<HeroCard>(a => a.Id.Equals(HeroId));
            // if (currentHeroCards.Count > 0)
            // {
            //     HeroCard card = currentHeroCards[0];
            //     card.InTroopIndex = InTroopIndex;
            //     card.TroopId = TroopId;
            //     await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(card);
            //     response.HeroCardInfo = card.GetMessageInfo();
            //     response.Error = ErrorCode.ERR_Success;
            //     reply();
            // }
            // else
            // {
            //     response.Error = ErrorCode.ERR_NotFoundHero;
            //     reply();
            // }

            reply();
            await ETTask.CompletedTask;
        }
    }
}