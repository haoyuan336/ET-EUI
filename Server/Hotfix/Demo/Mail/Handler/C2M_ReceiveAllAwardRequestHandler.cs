using System;
using System.Collections.Generic;

namespace ET.Handler
{
    public class C2M_ReceiveAllAwardRequestHandler: AMActorLocationRpcHandler<Unit, C2M_ReceiveAllAwardRequest, M2C_ReceiveAllAwardResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_ReceiveAllAwardRequest request, M2C_ReceiveAllAwardResponse response, Action reply)
        {
            long account = request.AccountId;
            long ownerId = request.OwnerId;

            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.ReceiveAward, account.GetHashCode()))
            {
                List<Mail> mails = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Query<Mail>(a =>
                        a.Id.Equals(ownerId) && a.ReceiveId.Equals(account) && a.State == (int) StateType.Active);
                if (mails.Count == 0)
                {
                    response.Error = ErrorCode.ERR_Not_Found_Mail;
                    reply();
                    return;
                }

                if (mails[0].IsGet)
                {
                    response.Error = ErrorCode.ERR_Award_AlReceive;
                    reply();
                    return;
                }

                //取出此邮件下的所有奖励
                await unit.DomainScene().GetComponent<MailComponent>().PlayerReceiveAward(account, ownerId);
                mails[0].IsGet = true;
                await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(mails[0]);
                response.MailInfo = mails[0].GetInfo();
                mails[0].Dispose();

                response.Error = ErrorCode.ERR_Success;
                reply();
            }
            //首先检查用户是否拥有此邮件

            await ETTask.CompletedTask;
        }
    }
}