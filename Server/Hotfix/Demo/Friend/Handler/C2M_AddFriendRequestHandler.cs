using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic.FileIO;
using MongoDB.Driver;

namespace ET
{
    public class C2M_AddFriendRequestHandler: AMActorLocationRpcHandler<Unit, C2M_AddFriendRequest, M2C_AddFriendResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_AddFriendRequest request, M2C_AddFriendResponse response, Action reply)
        {
            long accountId = request.Account;
            AccountInfo targetInfo = request.TargetInfo;
            List<Account> accounts = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Query<Account>(a => a.Id.Equals(accountId));
            if (accounts.Count == 0)
            {
                response.Error = ErrorCode.ERR_NotFoundPlayer;
                reply();
                return;
            }

            List<Friends> friends = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<Friends>(a => a.OwnerId.Equals(accounts) && a.State == (int)StateType.Active);
            if (friends.Count >= 30)
            {
                foreach (var friend in friends)
                {
                    friend.Dispose();
                }

                friends = null;
                response.Error = ErrorCode.ERR_SelfFriendCount_IsFull;
                reply();
                return;
            }

            //检查目标的好友个数是否达到上限

            List<Friends> otherFriends = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<Friends>(a => a.OwnerId.Equals(targetInfo.Account) && a.State == (int)StateType.Active);

            // Log.Warning("other friend s");
            if (otherFriends.Count >= 30)
            {
                foreach (var friend in otherFriends)
                {
                    friend.Dispose();
                }

                otherFriends = null;
                response.Error = ErrorCode.ERR_OtherFriendCount_IsFull;
                reply();
                return;
            }

            Account account = accounts[0];
            //储存一条消息

            //获取目标用户的所有邮件, 检查是否发生过请求加好友的消息
            List<Mail> mails = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<Mail>(a =>
                            a.ReceiveId.Equals(targetInfo.Account) && a.SendId.Equals(accountId) && a.MailType == MailType.AddFriendRequest);
            Mail mail;
            if (mails.Count == 0)
            {
                mail = new Mail()
                {
                    Id = IdGenerater.Instance.GenerateId(),
                    ReceiveId = targetInfo.Account,
                    SendId = accountId,
                    MailType = MailType.AddFriendRequest,
                    MailContent = $"{account.NickName}请求添加你为好友",
                    MailTitle = "好友申请",
                    SendName = $"{account.NickName}",
                };
            }
            else
            {
                mail = mails[0];
            }

            account?.Dispose();

            //保存此邮件内容 
            await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(mail);

            UnitComponent unitComponent = unit.DomainScene().GetComponent<UnitComponent>();

            // MessageHelper
            if (unitComponent != null)
            {
                //取出来目标unit
                Log.Debug($"account  {account}");
                Log.Debug($"target id {targetInfo.Account}");
                List<Unit> units = unitComponent.GetChilds<Unit>();
                Log.Debug($"units count {units.Count}");
                foreach (var u in units)
                {
                    Log.Debug($"unit id {u.AccountId}");
                }

                Unit target = units.Find(a => a.AccountId.Equals(targetInfo.Account));
                if (target != null)
                {
                    List<MailInfo> mailInfos = new List<MailInfo>();
                    mailInfos.Add(mail.GetInfo());
                    MessageHelper.SendToClient(target, new M2C_SendMails() { MailInfos = mailInfos });
                }

                // unit.Dispose();
            }

            // MessageHelper.SendToClient();

            //然后给客户端发送消息
            response.Error = ErrorCode.ERR_Success;
            reply();
        }
    }
}