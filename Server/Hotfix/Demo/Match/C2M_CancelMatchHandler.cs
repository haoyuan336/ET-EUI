using System;

namespace ET
{
    public class C2M_CancelMatchHandler: AMActorLocationRpcHandler<Unit, C2M_CancelMatchRoomRequest, M2C_CancelMatchRoomResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_CancelMatchRoomRequest request, M2C_CancelMatchRoomResponse response, Action reply)
        {
            int errorCode = (int) await unit.DomainScene().GetComponent<MatchComponent>().CancelMatch(unit);
            response.Error = errorCode;
            reply();
            await ETTask.CompletedTask;
        }
    }
}