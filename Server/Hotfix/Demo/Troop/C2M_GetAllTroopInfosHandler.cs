using System;
using System.Collections.Generic;

namespace ET
{
    public class C2M_GetAllTroopInfosHandler: AMActorLocationRpcHandler<Unit, C2M_GetAllTroopInfosRequest, M2C_GetAllTroopInfosResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_GetAllTroopInfosRequest request, M2C_GetAllTroopInfosResponse response, Action reply)
        {
            long AccountId = request.Account;
            List<Troop> troops = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Query<Troop>((a) => a.OnwerId.Equals(AccountId));
            List<TroopInfo> troopInfos = new List<TroopInfo>();
            foreach (var troop in troops)
            {
                troopInfos.Add(troop.GetTroopMessageInfo());
            }
            
            response.Error = ErrorCode.ERR_Success;
            response.TroopInfos = troopInfos;
            
            reply();
            await ETTask.CompletedTask;
        }
       
    }
}