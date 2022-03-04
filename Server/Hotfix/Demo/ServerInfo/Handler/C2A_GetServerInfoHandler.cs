using System;

namespace ET
{
    public class C2A_GetServerInfoHandler: AMRpcHandler<C2A_GetServerInfo, A2C_GetServerInfo>
    {
        protected override async ETTask Run(Session session, C2A_GetServerInfo request, A2C_GetServerInfo response, Action reply)
        {
            if (session.DomainScene().SceneType != SceneType.Account)
            {
                Log.Error($"请求的scene错误，当前的scene是{session.DomainScene().SceneType}");
                session?.Dispose();
                return;
            }

            string token = session.DomainScene().GetComponent<TokenComponent>().Get(request.AccountId);
            // Log.Error($"token{token}");
            // Log.Error($"player token{request.Token}");
            if (token == null || token != request.Token)
            {
                response.Error = ErrorCode.ERR_TokenError;
                Log.Error(ErrorCode.ERR_TokenError.ToString());
                reply();
                session.Disconnect().Coroutine();
                return;
            }

            foreach (var serverInfo in session.DomainScene().GetComponent<ServerInfoManagerComponent>().ServerInfos)
            {
                response.ServerInfoList.Add(serverInfo.ToMessage());
            }

            reply();
            session?.Disconnect().Coroutine();

            await ETTask.CompletedTask;
        }
    }
}