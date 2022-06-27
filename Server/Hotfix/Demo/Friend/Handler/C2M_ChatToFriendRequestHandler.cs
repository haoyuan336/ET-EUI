using System;
using System.Collections.Generic;

namespace ET.Demo.Friend.Handler
{
    public class C2M_ChatToFriendRequestHandler: AMActorLocationRpcHandler<Unit, C2M_ChatToFriendRequest, M2C_ChatToFriendResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_ChatToFriendRequest request, M2C_ChatToFriendResponse response, Action reply)
        {
            long accountId = request.AccountId;
            AccountInfo accountInfo = request.AccountInfo;
            string chatText = request.ChatText;
            //取出自己的信息
            List<Account> accounts = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<Account>(a => a.Id.Equals(accountId) && a.State == (int) StateType.Active);
            if (accounts.Count == 0)
            {
                response.Error = ErrorCode.ERR_NotFoundPlayer;
                reply();
                return;
            }

            var chatInfo = new ChatInfo() { AccountInfo = accounts[0].GetInfo(), ChatText = chatText };

            accounts[0].Dispose();
            response.ChatInfo = chatInfo;
            response.Error = ErrorCode.ERR_Success;
            reply();

            //找到unit
            UnitComponent unitComponent = unit.DomainScene().GetComponent<UnitComponent>();
            if (unitComponent == null)
            {
                Log.Debug("不存在unit component");
                return;
            }
            List<Unit> units = unitComponent.GetChilds<Unit>();
            Log.Debug($"当前unit服务器存在的unit有 unit {units.Count}");
            Log.Debug($"account info {accountInfo}");
            Unit targetUnit = units.Find(a => a.AccountId.Equals(accountInfo.Account));

            if (targetUnit == null)
            {
                Log.Debug("目标玩家不在线");
                return;
            }

            //在线
            M2C_ReceiveChatFromFriend message = new M2C_ReceiveChatFromFriend() { ChatInfo = chatInfo };
            MessageHelper.SendToClient(targetUnit, message);
            await ETTask.CompletedTask;
        }
    }
}