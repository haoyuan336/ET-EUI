using System;
using System.Collections.Generic;

namespace ET
{
    public class C2M_GiveGiftToFriendListRequestHandler: AMActorLocationRpcHandler<Unit, C2M_GiveGiftToFriendListRequest,
        M2C_GiveGiftToFriendListResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_GiveGiftToFriendListRequest request, M2C_GiveGiftToFriendListResponse response,
        Action reply)
        {
            long accountId = request.Account;
            long targetAccountId = request.AccountInfo.Account;
            //取出自己的信息
            long timeDay = ((int) (TimeHelper.ServerNow() / TimeHelper.OneDay)) * TimeHelper.OneDay;
            Log.Debug($"time day {timeDay}");
            Log.Debug($"time now {TimeHelper.ServerNow()}");
            List<Item> items = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<Item>(a => a.OwnerId.Equals(accountId) && a.State == (int) StateType.Active && a.CreateTime > timeDay);
            Log.Debug($"item count {items.Count}");

            if (items.Count == 0)
            {
                Item item = new Item() { Count = 5, OwnerId = accountId, ItemType = (int) ItemType.Gift };
                await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(item);
            }
            //新建道具

            //首先取出来所有的好友关系
            List<Friends> friendsList = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Query<Friends>(a =>
                    a.OwnerId.Equals(accountId) && a.FriendId.Equals(targetAccountId) && a.State == (int) StateType.Active);
            if (friendsList.Count == 0)
            {
                response.Error = ErrorCode.ERR_Not_Friend;
                reply();
                return;
            }
            //然后将好友关系的赠送礼物关系设置为真

            await ETTask.CompletedTask;
        }
    }
}