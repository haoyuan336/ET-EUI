using System;

namespace ET
{
    public class C2R_LoginRealmHandler: AMRpcHandler<C2R_LoginRealm, R2C_LoginRealm>
    {
        protected override async ETTask Run(Session session, C2R_LoginRealm request, R2C_LoginRealm response, Action reply)
        {
            if (session.DomainScene().SceneType != SceneType.Realm)
            {
                Log.Error("request error" + ErrorCode.ERR_NetworkError);
                session?.Dispose();
                return;
            }

            if (session.GetComponent<SessionLockComponent>() != null)
            {
                Log.Error(ErrorCode.ERR_RequestRepeatedly.ToString());
                reply();
                response.Error = ErrorCode.ERR_RequestRepeatedly;
                session?.Disconnect().Coroutine();
                return;
            }

            string token = session.DomainScene().GetComponent<TokenComponent>().Get(request.AccountId);
            if (token == null || token != request.Token)
            {
                response.Error = ErrorCode.ERR_TokenError;
                reply();
                session?.Disconnect().Coroutine();
            }

            using (session.AddComponent<SessionLockComponent>())
            {
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.AccountGetRate, request.AccountId))
                {
                    StartSceneConfig startSceneConfig = RealmGateAddressHelper.GetGate(session.DomainZone(), request.AccountId);
                    //再获取门服务器上的token
                    G2R_GetGateKey g2RGetGateKey = (G2R_GetGateKey) await MessageHelper.CallActor(startSceneConfig.InstanceId,
                        new R2G_GetGateKey() { AccountId = request.AccountId });
                    if (g2RGetGateKey.Error != ErrorCode.ERR_Success)
                    {
                        response.Error = g2RGetGateKey.Error;
                        reply();
                        session?.Disconnect().Coroutine();
                    }

                    response.Error = ErrorCode.ERR_Success;
                    response.GateKey = g2RGetGateKey.Key;
                    response.GateAddress = startSceneConfig.OuterIPPort.ToString();
                    reply();
                }
            }

            await ETTask.CompletedTask;
        }
    }
}