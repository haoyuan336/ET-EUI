using System;
using System.Collections.Generic;

namespace ET
{
    public class C2M_GetAllHeroListInfoRequestHandler: AMActorLocationRpcHandler<Unit, C2M_GetAllHeroCardListRequest, M2C_GetAllHeroCardListResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_GetAllHeroCardListRequest request, M2C_GetAllHeroCardListResponse response, Action reply)
        {
            HeroCardComponent heroCardComponent = unit.GetComponent<HeroCardComponent>();
            List<HeroCard> heroCards = await heroCardComponent.GetAllHeroCardAsync();
            // M2C_GetAllHeroCardListResponse m2CGetAllHeroCardListResponse = new M2C_GetAllHeroCardListResponse();
            // m2CGetAllHeroCardListResponse.Error = ErrorCode.ERR_Success;

            // heroCards.RemoveAll((a) =>
            // {
            //     HeroConfig config = HeroConfigCategory.Instance.Get(a.ConfigId);
            //     if (config.MaterialType == (int)HeroBagType.Materail)
            //     {
            //         return true;
            //     }
            //     return false;
            // });
            List<HeroCardInfo> heroCardInfos = new List<HeroCardInfo>();
            foreach (var heroCard in heroCards)
            {
                heroCardInfos.Add(heroCard.GetMessageInfo());
            }

            response.HeroCardInfos = heroCardInfos;
            response.Error = ErrorCode.ERR_Success;
            reply();
            await ETTask.CompletedTask;
        }
    }
}