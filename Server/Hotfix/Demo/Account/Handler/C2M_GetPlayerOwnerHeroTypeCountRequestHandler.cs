using System;
using System.Collections.Generic;
using System.Linq;

namespace ET
{
    public class C2M_GetPlayerOwnerHeroTypeCountRequestHandler: AMActorLocationRpcHandler<Unit, C2M_GetPlayerOwnHeroTypeCountRequest,
        M2C_GetPlayerOwnHeroTypeCountResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_GetPlayerOwnHeroTypeCountRequest request, M2C_GetPlayerOwnHeroTypeCountResponse response,
        Action reply)
        {
            //首先取出来这个玩家的所有英雄卡拍

            HeroCardComponent heroCardComponent = unit.GetComponent<HeroCardComponent>();
            List<HeroCard> heroCards = await heroCardComponent.GetAllHeroCardAsync();
            // long accountId = request.AccountId;
            // List<HeroCard> allCards = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    // .Query<HeroCard>(a => a.OwnerId.Equals(accountId) && a.State == (int) StateType.Active);
            // List<HeroCard> heroCards = allCards.FindAll((a) =>
            // {
                // HeroConfig config = HeroConfigCategory.Instance.Get(a.ConfigId);
                // return config.MaterialType == (int) HeroBagType.Hero;
            // });
            Dictionary<int, HeroCard> heroCardMap = new Dictionary<int, HeroCard>();
            foreach (var heroCard in heroCards)
            {
                if (!heroCardMap.Keys.Contains(heroCard.ConfigId))
                {
                    heroCardMap.Add(heroCard.ConfigId, heroCard);
                }
            }

            response.Count = heroCardMap.Count;
            response.Error = ErrorCode.ERR_Success;
            reply();
            await ETTask.CompletedTask;
        }
    }
}