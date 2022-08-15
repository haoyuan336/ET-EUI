using System;

namespace ET
{
    public class C2M_GetCurrentTroopIndexHandler: AMActorLocationRpcHandler<Unit, C2M_GetCurrentTroopIndexRequest, M2C_GetCurrentTroopIndexResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_GetCurrentTroopIndexRequest request, M2C_GetCurrentTroopIndexResponse response,
        Action reply)
        {
            TroopComponent troopComponent = unit.GetComponent<TroopComponent>();
            var index = await troopComponent.GetCurrentTroopIndexAsync();
            response.Error = ErrorCode.ERR_Success;
            response.CurrentTroopIndex = index;
            reply();
        }
    }
}