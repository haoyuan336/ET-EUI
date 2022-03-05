using System;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;

namespace ET
{
    public class C2A_GetRealmKeyHandler: AMRpcHandler<C2A_GetRealmKey, A2C_GetRealmKey>
    {
        protected override async ETTask Run(Session session, C2A_GetRealmKey request, A2C_GetRealmKey response, Action reply)
        {
            //第一步，判断服务器来行是否准确
            if (session.DomainScene().SceneType != SceneType.Account)
            {
                Log.Error(ErrorCode.ERR_NetworkError.ToString());
                session?.Dispose();
                return;
            }

            if (session.GetComponent<SessionLockComponent>() != null)
            {
                Log.Error(ErrorCode.ERR_RequestRepeatedly.ToString());
                //重复请求
                response.Error = ErrorCode.ERR_RequestRepeatedly;
                reply();
                session.Disconnect().Coroutine();
                return;
            }

            //第二部，判断token是否准确
            string token = session.DomainScene().GetComponent<TokenComponent>().Get(request.AccountId);
            if (token == null || request.Token != token)
            {
                Log.Error(ErrorCode.ERR_TokenError.ToString());
                response.Error = ErrorCode.ERR_TokenError;
                reply();
                session.Disconnect().Coroutine();
                return;
            }

            using (session.AddComponent<SessionLockComponent>())
            {
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.AccountGetRealmKey, request.AccountId))
                {
                    R2A_GetRealmKey r2AGetRealmKey;
                    StartSceneConfig startSceneConfig = RealmGateAddressHelper.GetReal(request.ServerId);
                    r2AGetRealmKey = (R2A_GetRealmKey) await MessageHelper.CallActor(startSceneConfig.InstanceId,
                        new A2R_GetRealmKey() { AccountId = request.AccountId });
                    if (r2AGetRealmKey.Error != ErrorCode.ERR_Success)
                    {
                        response.Error = r2AGetRealmKey.Error;
                        Log.Error(r2AGetRealmKey.Error.ToString());
                        reply();
                        session?.Disconnect().Coroutine();
                        return;
                    }

                    response.Error = ErrorCode.ERR_Success;
                    response.RealmKey = r2AGetRealmKey.Token;
                    response.RealmAddress = startSceneConfig.OuterIPPort.ToString();
                    reply();
                }
            }

            await ETTask.CompletedTask;
        }
    }
}