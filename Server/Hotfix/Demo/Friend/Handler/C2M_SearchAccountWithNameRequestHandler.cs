using System;
using System.Collections.Generic;

namespace ET.Demo.Friend.Handler
{
    public class C2M_SearchAccountWithNameRequestHandler: AMActorLocationRpcHandler<Unit, C2M_SearchAccountWithNameRequest,
        M2C_SearchAccountWithNameResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_SearchAccountWithNameRequest request, M2C_SearchAccountWithNameResponse response,
        Action reply)
        {
            var accountId = request.AccountId;
            var searchName = request.Name;

            //首先找到叫此用户名的账号
            List<Account> accounts = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<Account>(a => a.AccountName.Equals(searchName) && a.State == (int) StateType.Active);
            if (accounts.Count == 0)
            {
                response.Error = ErrorCode.ERR_NotFoundPlayer;
                return;
            }

            var account = accounts[0];

            // List<Friends> friendsList = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Query<Friends>(a =>
                    // a.OwnerId.Equals(accountId) && a.FriendId.Equals(account.Id) && a.State == (int) StateType.Active);
            
            // List<Friends> friendsList2 = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Query<Friends>()

            response.Error = ErrorCode.ERR_NotFoundPlayer;
            reply();
            await ETTask.CompletedTask;
        }
    }
}