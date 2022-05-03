using System;
using System.Collections.Generic;

namespace ET
{
    public class C2M_GetAllHeroListInfoRequestHandler: AMActorLocationRpcHandler<Unit, C2M_GetAllHeroCardListRequest, M2C_GetAllHeroCardListResponse>
    {
        // protected override async ETTask Run(Session session, C2M_GetAllHeroCardListRequest request, M2C_GetAllHeroCardListResponse response,
        // Action reply)
        // {
        //     Log.Debug("get all hero list info");
        //     M2C_GetAllHeroCardListResponse m2CGetAllHeroCardListResponse = new M2C_GetAllHeroCardListResponse();
        //     m2CGetAllHeroCardListResponse.Error = ErrorCode.ERR_Success;
        //     reply();
        //     await ETTask.CompletedTask;
        // }
        protected override async ETTask Run(Unit unit, C2M_GetAllHeroCardListRequest request, M2C_GetAllHeroCardListResponse response, Action reply)
        {
            M2C_GetAllHeroCardListResponse m2CGetAllHeroCardListResponse = new M2C_GetAllHeroCardListResponse();
            m2CGetAllHeroCardListResponse.Error = ErrorCode.ERR_Success;
            
            long AccountId = request.Account;
            var heroCards = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Query<HeroCard>((a)=>a.OwnerId.Equals(AccountId));
            List<HeroCardInfo> heroCardInfos = new List<HeroCardInfo>();
         
            foreach (var heroCard in heroCards)
            {
                heroCardInfos.Add(heroCard.GetMessageInfo());
            }
            
            response.HeroCardInfos = heroCardInfos;

            reply();
            await ETTask.CompletedTask;
        }
    }
}