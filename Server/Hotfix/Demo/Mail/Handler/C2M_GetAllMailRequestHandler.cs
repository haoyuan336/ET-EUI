using System;
using System.Collections.Generic;
using System.Net;

namespace ET.Handler
{
    public class C2M_GetAllMailRequestHandler: AMActorLocationRpcHandler<Unit, C2M_GetAllMailRequest, M2C_GetAllMailResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_GetAllMailRequest request, M2C_GetAllMailResponse response, Action reply)
        {
            long accountId = request.AccountId;

            //取出所有的邮件
            List<Mail> mails = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<Mail>(a => a.ReceiveId.Equals(accountId) && a.State == (int) StateType.Active);

            List<MailInfo> mailInfos = new List<MailInfo>();
            foreach (var mail in mails)
            {
                mailInfos.Add(mail.GetInfo());
            }

            response.Error = ErrorCode.ERR_Success;
            response.MailInfos = mailInfos;
            reply();

            await ETTask.CompletedTask;
        }
    }
}