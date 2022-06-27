using System;
using System.Collections.Generic;
using System.Drawing;

namespace ET
{
    public class C2M_GiveGiftToFriendListRequestHandler: AMActorLocationRpcHandler<Unit, C2M_GiveGiftToFriendRequest,
        M2C_GiveGiftToFriendResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_GiveGiftToFriendRequest request, M2C_GiveGiftToFriendResponse response,
        Action reply)
        {
            long accountId = request.Account;
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.GiveGiftToFriend, accountId.GetHashCode()))
            {
                long targetAccountId = request.AccountInfo.Account;

                List<Friends> friendsList = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Query<Friends>(a =>
                        a.OwnerId.Equals(accountId) && a.FriendId.Equals(targetAccountId) && a.State == (int) StateType.Active);
                if (friendsList.Count == 0)
                {
                    response.Error = ErrorCode.ERR_Not_Friend;
                    reply();
                    return;
                }

                Friends friends = friendsList[0];

                if (friends.IsGift)
                {
                    response.Error = ErrorCode.Gift_Gived;
                    reply();
                    return;
                }

                //取出自己的信息
                long timeDay = CustomHelper.GetCurrentDayTime();
                Log.Debug($"time day {timeDay}");
                Log.Debug($"time now {TimeHelper.ServerNow()}");
                List<Item> items = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                        .Query<Item>(a =>
                                a.OwnerId.Equals(accountId) && a.State == (int) StateType.Active && a.CreateTime > timeDay &&
                                a.Type == (int) ItemType.PreDayPowerGift);
                Log.Debug($"item  {items.Count}");

                Item item;
                if (items.Count == 0)
                {
                    item = new Item()
                    {
                        Id = IdGenerater.Instance.GenerateId(),
                        Count = ConstValue.PreDayFreePowerGiftCount,
                        OwnerId = accountId,
                        Type = (int) ItemType.PreDayPowerGift
                    };
                }
                else
                {
                    item = items[0];
                }

                if (item.Count <= 0)
                {
                    response.Error = ErrorCode.Gift_Not_Enough;
                    reply();
                    return;
                }

                item.Count--;
                await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(item);

                item.Dispose();

                friends.IsGift = true;
                await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(friends);
                response.FriendInfo = friends.GetFriendInfo();
                friends.Dispose();
                response.Error = ErrorCode.ERR_Success;
                reply();
            }

            await ETTask.CompletedTask;
        }
    }
}