using System;
using System.Collections.Generic;
using System.Linq;

namespace ET.Friend.Handler
{
    public class C2M_GetFriendRecommendListRequestHandler: AMActorLocationRpcHandler<Unit, C2M_GetFriendRecommendListRequest,
        M2C_GetFriendRecommendListResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_GetFriendRecommendListRequest request, M2C_GetFriendRecommendListResponse response,
        Action reply)
        {
            Log.Debug("C2M_GetFriendRecommendListRequestHandler");
            long accountId = request.AccountId;
            List<Account> accounts = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<Account>(a => !a.Id.Equals(accountId) && a.State == (int) StateType.Active);
            List<Friends> friends =
                    await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                            .Query<Friends>(a => a.OwnerId.Equals(accountId) && a.State == (int) StateType.Active); //取出自己的所有好友

            List<Account> unFriendAccounts = accounts.FindAll((a) => { return !friends.Exists(b => b.FriendId.Equals(a.Id)); });
            Log.Debug($"非好友数目{unFriendAccounts.Count}");
            // List<Account> recommends = unFriendAccounts;
            List<Account> recommends = new List<Account>();
            var count = 0;
            while (count < 50 && recommends.Count < 10 && unFriendAccounts.Count > 0)
            {
                var account = unFriendAccounts.RandomArray();
                if (!recommends.Contains(account))
                {
                    recommends.Add(account);
                }
            
                count++;
            }

            List<AccountInfo> accountInfos = new List<AccountInfo>();
            foreach (var recommend in recommends)
            {
                accountInfos.Add(recommend.GetInfo());
            }

            foreach (var account in accounts)
            {
                account.Dispose();
            }

            response.AccountInfos = accountInfos;
            response.Error = ErrorCode.ERR_Success;
            reply();
            await ETTask.CompletedTask;
        }
    }
}