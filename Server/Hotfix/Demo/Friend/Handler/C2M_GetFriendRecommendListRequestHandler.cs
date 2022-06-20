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
            long accountId = request.AccountId;
            List<Account> accounts = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Query<Account>(a => !a.Id.Equals(accountId));

            List<Account> recommends = new List<Account>();
            var count = 0;
            while (count < 50 && recommends.Count < 10)
            {
                var account = accounts.RandomArray();
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

            response.AccountInfos = accountInfos;
            response.Error = ErrorCode.ERR_Success;
            reply();
            await ETTask.CompletedTask;
        }
    }
}