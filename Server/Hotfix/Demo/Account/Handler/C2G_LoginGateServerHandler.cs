using System;

namespace ET
{
    public class C2G_LoginGateServerHandler: AMRpcHandler<C2G_LoginGateRequeset, G2C_LoginGateResponse>
    {
        protected override async ETTask Run(Session session, C2G_LoginGateRequeset request, G2C_LoginGateResponse response, Action reply)
        {
            //首先获取key
            Scene scene = session.DomainScene();
            string accountId = scene.GetComponent<GateSessionKeyComponent>().Get(request.GateKey);
            if (accountId == null || accountId != request.AccountId.ToString())
            {
                response.Error = ErrorCode.ERR_TokenError;
                reply();
                session.Disconnect().Coroutine();
            }
            session.RemoveComponent<SessionAcceptTimeoutComponent>();
            //token验证通过之后，开始创建player
            Player player = scene.GetComponent<PlayerComponent>().AddChild<Player, String>(accountId);
            scene.GetComponent<PlayerComponent>().Add(player);
            // player.Account
            session.AddComponent<SessionPlayerComponent>().PlayerId = player.Id;
            session.AddComponent<MailBoxComponent, MailboxType>(MailboxType.GateSession);

            response.PlayerId = player.Id;
            reply();
            session.Error = ErrorCode.ERR_Success;

            await ETTask.CompletedTask;
        }
    }
}