using System;
using System.Net.NetworkInformation;

namespace ET
{
    public class C2M_MatchRoomHandler: AMActorLocationRpcHandler<Unit, C2M_MatchRoomRequest, M2C_MatchRoomResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_MatchRoomRequest request, M2C_MatchRoomResponse response, Action reply)
        {
            foreach (var key in unit.DomainScene().Components.Keys)
            {
                Log.Debug("key = " + key);
            }
            MatchComponent matchComponent = unit.DomainScene().GetComponent<MatchComponent>();
            matchComponent.AddMatch(unit);
            Log.Debug("客户端发来了。匹配房间的消息" + matchComponent.ToString());
            response.Error = ErrorCode.ERR_Success;
            reply();
            await ETTask.CompletedTask;
        }
    }
}