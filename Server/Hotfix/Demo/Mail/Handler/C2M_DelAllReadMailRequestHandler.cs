using System;
using System.Collections.Generic;
using System.Linq;

namespace ET.Handler
{
    public class C2M_DelAllReadMailRequestHandler: AMActorLocationRpcHandler<Unit, C2M_DelAllReadMailRequest, M2C_DelAllReadMailResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_DelAllReadMailRequest request, M2C_DelAllReadMailResponse response, Action reply)
        {
            long accountId = request.AccountId;
            //取出邮件
            List<Mail> allMails = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<Mail>(a => a.ReceiveId.Equals(accountId) && a.State == (int) StateType.Active);
            if (allMails.Count == 0)
            {
                response.Error = ErrorCode.ERR_Not_Found_Mail;
                reply();
                return;
            }

            //删掉已读邮件
            // List<Mail> readMails = allMails.FindAll(a => a.IsRead);
            List<MailInfo> mailInfos = new List<MailInfo>();
            foreach (var mail in allMails)
            {
                if (mail.IsRead)
                {
                    mail.State = (int) StateType.Destroy;
                    await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(mail);
                }
                else
                {
                    mailInfos.Add(mail.GetInfo());
                }

                mail.Dispose();
            }
            response.MailInfos = mailInfos;
            reply();
        }
    }
}