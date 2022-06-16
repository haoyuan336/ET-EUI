using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using UnityEngine;

namespace ET.Handler
{
    public class C2M_ReadMailsRequestHandler: AMActorLocationRpcHandler<Unit, C2M_ReadMailsRequest, M2C_ReadMailsResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_ReadMailsRequest request, M2C_ReadMailsResponse response, Action reply)
        {
            long account = request.AccountId;
            List<long> mailIds = request.MailIds;
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.ReadMail, account.GetHashCode()))
            {
                List<Mail> mails = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                        .Query<Mail>(a => a.ReceiveId.Equals(account) && a.State == (int) StateType.Active);

                foreach (var id in mailIds)
                {
                    Log.Warning($"mail id {id}");
                }

                foreach (var mail in mails)
                {
                    Log.Warning($"mail id {mail.Id}");
                }

                List<Mail> processMails = mails.FindAll(a => { return mailIds.Exists(b => { return b.Equals(a.Id); }); });
                List<ETTask> tasks = new List<ETTask>();
                foreach (var mail in processMails)
                {
                    mail.IsRead = true;
                    tasks.Add(DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(mail));
                }

                await ETTaskHelper.WaitAll(tasks);
                List<MailInfo> mailInfos = new List<MailInfo>();
                foreach (var mail in mails)
                {
                    mailInfos.Add(mail.GetInfo());
                    mail.Dispose();
                }

                response.Error = ErrorCode.ERR_Success;
                response.MailInfos = mailInfos;
                reply();
            }

            await ETTask.CompletedTask;
        }
    }
}