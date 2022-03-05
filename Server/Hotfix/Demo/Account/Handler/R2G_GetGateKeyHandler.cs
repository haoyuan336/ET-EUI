using System;

namespace ET
{
    public class R2G_GetGateKeyHandler: AMActorRpcHandler<Scene, R2G_GetGateKey, G2R_GetGateKey>
    {
        protected override async  ETTask Run(Scene unit, R2G_GetGateKey request, G2R_GetGateKey response, Action reply)
        {
            string token = TimeHelper.ServerNow().ToString() + RandomHelper.RandomNumber(int.MinValue, int.MaxValue).ToString();
            unit.DomainScene().GetComponent<GateSessionKeyComponent>().Remove(request.AccountId);
            unit.DomainScene().GetComponent<GateSessionKeyComponent>().Add(request.AccountId, token);
            response.Token = token;
            response.Error = ErrorCode.ERR_Success;
            reply();
            await ETTask.CompletedTask;
        }
    }
}