using System;
using System.Collections.Generic;

namespace ET.Demo.Friend.Handler
{
    public class C2M_GetFriendApplyListRequestHandler: AMActorLocationRpcHandler<Unit, C2M_GetFriendApplyListRequest, M2C_GetFriendApplyListResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_GetFriendApplyListRequest request, M2C_GetFriendApplyListResponse response, Action reply)
        {
            //从邮件里面取出来
            long accountId = request.AccountId;

            List<Mail> mails = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Query<Mail>(a =>
                    a.ReceiveId.Equals(accountId) && a.MailType == MailType.AddFriendRequest && a.State == (int) StateType.Active);
            //取出邮件里面用户信息
            List<AccountInfo> accountInfos = new List<AccountInfo>();
            List<MailInfo> mailInfos = new List<MailInfo>();
            foreach (var mail in mails)
            {
                List<Account> accounts = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                        .Query<Account>(a => a.Id.Equals(mail.SendId) && a.State == (int) StateType.Active);
                if (accounts.Count > 0)
                {
                    accountInfos.Add(accounts[0].GetInfo());
                    mailInfos.Add(mail.GetInfo());
                    accounts[0].Dispose();

                }
                mail.Dispose();
            }

            response.MailInfo = mailInfos;
            response.AccountInfo = accountInfos;
            response.Error = ErrorCode.ERR_Success;
            reply();
            await ETTask.CompletedTask;
        }
    }
}