using System;

namespace ET
{
    public class A2R_GetRealmKeyHandler: AMActorRpcHandler<Scene, A2R_GetRealmKey, R2A_GetRealmKey>
    {
        protected override async ETTask Run(Scene scene, A2R_GetRealmKey request, R2A_GetRealmKey response, Action reply)
        {

            if (scene.SceneType != SceneType.Realm)
            {
                response.Error = ErrorCode.ERR_NetworkError;
                Log.Error(ErrorCode.ERR_NetworkError.ToString());
                reply();
                return;
            }
            string key = TimeHelper.ServerNow().ToString() + RandomHelper.RandomNumber(int.MinValue, int.MaxValue).ToString();
            scene.DomainScene().GetComponent<TokenComponent>().Remove(request.AccountId);
            scene.DomainScene().GetComponent<TokenComponent>().Add(request.AccountId, key);
            response.Token = key;
            response.Error = ErrorCode.ERR_Success;
            reply();
            await ETTask.CompletedTask;
        }
    }
}