using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ET
{
    public class C2M_GetAllFriendsRequestHandler: AMActorLocationRpcHandler<Unit, C2M_GetAllFriendsRequest, M2C_GetAllFriendsResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_GetAllFriendsRequest request, M2C_GetAllFriendsResponse response, Action reply)
        {
            long accountId = request.AccountId;
            //首先取出来，玩家的所有好友关系
            List<Friends> friendsList = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<Friends>(a => a.OwnerId.Equals(accountId) && a.State == (int) StateType.Active);
            //取出所有的玩家根基好友关系

            Dictionary<long, Friends> friendsMap = friendsList.ToDictionary(a => a.FriendId, b => b);
            List<Account> accounts = new List<Account>();
            foreach (var friends in friendsList)
            {
                // tasks.Add(DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                //         .Query<Account>(a => a.Id.Equals(friends.FriendId) && a.State == (int) StateType.Active));
                List<Account> ac = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                        .Query<Account>(a => a.Id.Equals(friends.FriendId) && a.State == (int) StateType.Active);
                accounts = accounts.Concat(ac).ToList();
            }


            List<AccountInfo> accountInfos = new List<AccountInfo>();
            List<FriendInfo> friendInfos = new List<FriendInfo>();

            foreach (var account in accounts)
            {
                accountInfos.Add(account.GetInfo());
                friendInfos.Add(friendsMap[account.Id].GetFriendInfo());
            }

            response.AccountInfos = accountInfos;
            response.FriendInfos = friendInfos;
            response.Error = ErrorCode.ERR_Success;
            //取出来信息
            reply();
            
            
            foreach (var friend in friendsList)
            {
                friend.Dispose();
            }

            foreach (var account in accounts)
            {
                account.Dispose();
            }

            friendsMap.Clear();
            accounts.Clear();
            friendsList.Clear();

            await ETTask.CompletedTask;
        }
    }
}