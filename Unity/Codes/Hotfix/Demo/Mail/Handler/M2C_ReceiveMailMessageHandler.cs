using System.Collections.Generic;

namespace ET.Handler
{
    public class M2C_ReceiveMailMessageHandler: AMHandler<M2C_SendMails>
    {
        protected override async ETTask Run(Session session, M2C_SendMails message)
        {
            List<MailInfo> mailInfos = message.MailInfos;

            // Log.Debug($"mail info count {mailInfos.Count}");

            Scene scene = session.ZoneScene();

            Game.EventSystem.Publish(new EventType.SetNewMails() { Scene = scene, MailInfos = mailInfos });
            

            await ETTask.CompletedTask;
        }
    }
}