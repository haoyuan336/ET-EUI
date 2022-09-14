using System;
using System.Collections.Generic;
using MongoDB.Driver.Linq;

namespace ET
{
    public class C2M_ProcessFriendApplyRequestHandler: AMActorLocationRpcHandler<Unit, C2M_ProcessFriendApplyRequest, M2C_ProcessFriendApplyResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_ProcessFriendApplyRequest request, M2C_ProcessFriendApplyResponse response, Action reply)
        {
            long accountId = request.AccountId;
            AccountInfo accountInfo = request.AccountInfo;
            int ProcessType = request.ApplyProcessType;
            //首先将邮件删掉
            List<Mail> mails = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Query<Mail>(a =>
                    a.SendId.Equals(accountInfo.Account) && a.ReceiveId.Equals(accountId) && a.State == (int) StateType.Active &&
                    a.MailType == MailType.AddFriendRequest);
            Log.Debug($"取出好友申请的邮件 mails count {mails.Count}");
            if (mails.Count > 0)
            {
                mails[0].State = (int) StateType.Destroy;
                await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(mails[0]);
                mails[0].Dispose();
            }

            if (ProcessType == (int) ApplyProcessType.Accept)
            {
                //创建好友关系
                Friends friend1 = new Friends() { Id = IdGenerater.Instance.GenerateId(), OwnerId = accountId, FriendId = accountInfo.Account };
                Friends friend2 = new Friends() { Id = IdGenerater.Instance.GenerateId(), OwnerId = accountInfo.Account, FriendId = accountId };

                List<ETTask> tasks = new List<ETTask>();
                tasks.Add(DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(friend1));
                tasks.Add(DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(friend2));
                await ETTaskHelper.WaitAll(tasks);
            }

            response.Error = ErrorCode.ERR_Success;
            reply();

            //储存好友关系
        }
    }
}