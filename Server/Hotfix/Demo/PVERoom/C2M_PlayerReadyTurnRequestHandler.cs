using System;

namespace ET
{
    public class C2M_PlayerReadyTurnRequestHandler: AMActorLocationRpcHandler<Unit, C2M_PlayerReadyTurnRequest, M2C_PlayerReadyTurnResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_PlayerReadyTurnRequest request, M2C_PlayerReadyTurnResponse response, Action reply)
        {
            PVERoom pveRoom = unit.DomainScene().GetComponent<PVERoomComponent>().GetChild<PVERoom>(request.RoomId);
            await pveRoom.PlayerReadyTurn();
            response.Error = ErrorCode.ERR_Success;
            reply();
            await ETTask.CompletedTask;
        }
    }
}