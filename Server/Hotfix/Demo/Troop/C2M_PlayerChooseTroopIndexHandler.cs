using System;

namespace ET
{
    public class C2M_PlayerChooseTroopIndexHandler: AMActorLocationRpcHandler<Unit, C2M_PlayerChooseTroopIndexRequest,
        M2C_PlayerChooseTroopIndexResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_PlayerChooseTroopIndexRequest request, M2C_PlayerChooseTroopIndexResponse response,
        Action reply)
        {
            TroopComponent troopComponent = unit.GetComponent<TroopComponent>();
            troopComponent.PlayerChooseTroopIndex(request.Index);
            response.Error = ErrorCode.ERR_Success;
            reply();
            await ETTask.CompletedTask;
        }
    }
}