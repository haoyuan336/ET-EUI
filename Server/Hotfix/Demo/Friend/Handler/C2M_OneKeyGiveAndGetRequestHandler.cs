using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ET.Demo.Friend.Handler
{
    public class C2M_OneKeyGiveAndGetRequestHandler: AMActorLocationRpcHandler<Unit, C2M_OneKeyGiveAndGetRequest, M2C_OneKeyGiveAndGetResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_OneKeyGiveAndGetRequest request, M2C_OneKeyGiveAndGetResponse response, Action reply)
        {
            long accountId = request.Account;

            ItemInfo powerItemInfo = new ItemInfo();
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.GiveGiftToFriend, accountId.GetHashCode()))
            {
                //处理领取礼物逻辑

                List<Item> powerItems = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                        .Query<Item>(a => a.OwnerId.Equals(accountId) && a.State == (int) StateType.Active && a.ConfigId == 1003);
                if (powerItems.Count != 0)
                {
                    //取出来，所有的好友关系
                    List<Friends> friendsGiftList = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                            .Query<Friends>(a => a.FriendId.Equals(accountId) && a.State == (int) StateType.Active && a.IsGift);
                    var powerItem = powerItems[0];
                    //领取
                    foreach (var gift in friendsGiftList)
                    {
                        powerItem.Count++;
                        gift.IsGift = false;
                        using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.GiveGiftToFriend, gift.Id.GetHashCode()))
                        {
                            await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(gift);
                        }

                        gift.Dispose();
                    }

                    powerItemInfo = powerItem.GetInfo();
                    await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(powerItem);
                    powerItem.Dispose();
                    response.PowerItemInfo = powerItemInfo;
                }

                //取出来，玩家拥有的赠送额度
                long dayTime = CustomHelper.GetCurrentDayTime();
                List<Item> items = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                        .Query<Item>(a =>
                                a.OwnerId.Equals(accountId) && a.State == (int) StateType.Active && a.CreateTime > dayTime &&
                                a.Type == (int) ItemType.PreDayPowerGift);
                Log.Debug($"一键赠送并领取 道具数目 {items.Count}");
                Item item;
                if (items.Count == 0)
                {
                    item = new Item()
                    {
                        Id = IdGenerater.Instance.GenerateId(),
                        Type = (int) ItemType.PreDayPowerGift,
                        OwnerId = accountId,
                        Count = ConstValue.PreDayFreePowerGiftCount
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
                    item.Dispose();
                    return;
                }

                //获取好友关系  状态为激活，赠送礼物为 未赠送
                List<Friends> friendsList =
                        await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                                .Query<Friends>(a => a.OwnerId.Equals(accountId) && a.State == (int) StateType.Active && !a.IsGift);

                //根据好友关系取出来，玩家列表
                if (friendsList.Count == 0)
                {
                    response.Error = ErrorCode.ERR_NotFoundPlayer;
                    reply();
                    return;
                }

                List<Account> accounts = new List<Account>();
                foreach (var friends in friendsList)
                {
                    List<Account> accountList = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                            .Query<Account>(a => a.State == (int) StateType.Active && a.Id.Equals(friends.FriendId));
                    accounts = accounts.Concat(accountList).ToList();
                }

                if (accounts.Count == 0)
                {
                    response.Error = ErrorCode.ERR_NotFoundPlayer;
                    reply();
                    return;
                }

                accounts.Sort((a, b) => { return (int) b.LastLogonTime - (int) a.LastLogonTime; });

                //取出来，可以赠送玩家，根据自己当天的剩余礼物额度
                Dictionary<long, Friends> friendsMap = friendsList.ToDictionary(a => a.FriendId, b => b);

                var count = Mathf.Min(item.Count, accounts.Count);
                List<FriendInfo> friendInfos = new List<FriendInfo>();
                for (int i = 0; i < count; i++)
                {
                    var account = accounts[i];
                    var friends = friendsMap[account.Id];
                    friends.IsGift = true;
                    friendInfos.Add(friends.GetFriendInfo());
                    //保存
                    await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(friends);
                    friends.Dispose();
                }

                item.Count -= count;
                //保存item
                await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(item);
                item.Dispose();

                foreach (var account in accounts)
                {
                    account.Dispose();
                }

                accounts.Clear();
                friendsList.Clear();

                Log.Debug($"取出来的好友个数是  {accounts.Count}");

                //按照时间先后进行排序

                
                response.FriendInfos = friendInfos;
                response.Error = ErrorCode.ERR_Success;
                reply();
            }
        }
    }
}