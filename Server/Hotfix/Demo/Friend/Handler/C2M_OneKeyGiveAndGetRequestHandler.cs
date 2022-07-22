using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver.Core.WireProtocol;
using UnityEngine;

namespace ET.Demo.Friend.Handler
{
    public class C2M_OneKeyGiveAndGetRequestHandler: AMActorLocationRpcHandler<Unit, C2M_OneKeyGiveAndGetRequest, M2C_OneKeyGiveAndGetResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_OneKeyGiveAndGetRequest request, M2C_OneKeyGiveAndGetResponse response, Action reply)
        {
            long accountId = request.Account;
            await ETTask.CompletedTask;
            var currentTime = CustomHelper.GetCurrentDayTime();

            //todo--------------------------------首先领取好友赠送的礼物---------------------------------------------------------------------------
            List<GameAction> beGiftedActions =
                    await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                            .Query<GameAction>(a =>
                                    a.FriendId.Equals(accountId) && a.State == (int) StateType.Active &&
                                    !a.IsReceived && a.ConfigId == 10004);
            //取出玩家的体力道具
            List<Item> powerItems = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<Item>(a => a.OwnerId.Equals(accountId) && a.State == (int) StateType.Active && a.ConfigId == 1003);

            if (powerItems.Count > 0)
            {
                foreach (var giftedAction in beGiftedActions)
                {
                    powerItems[0].Count += giftedAction.Value;
                    giftedAction.Value = 0;
                    giftedAction.IsReceived = true;
                    //然后储存一下
                    await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(giftedAction);
                    giftedAction.Dispose();
                }

                //然后储存体力道具
                await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(powerItems[0]);
                powerItems[0].Dispose();
            }

            //todo--------------------------------首先领取好友赠送的礼物---------------------------------------------------------------------------

            //todo ----------------------------------取出来，当天玩家剩余的赠送礼物的次数-------------------------------------------------------------
            List<Item> giftItems = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Query<Item>(a =>
                    a.OwnerId.Equals(accountId) && a.ConfigId.Equals(1013) && a.CreateTime >= currentTime && a.State == (int) StateType.Active);
            Item giftItem;
            if (giftItems.Count == 0)
            {
                var config = ItemConfigCategory.Instance.Get(1013);
                //如果道具数量是0 ，那么创建一个道具
                giftItem = new Item() { Id = IdGenerater.Instance.GenerateId(), OwnerId = accountId, ConfigId = 1013, Count = config.DefaultValue };
            }
            else
            {
                giftItem = giftItems[0];
            }

            if (giftItem.Count <= 0)
            {
                response.Error = ErrorCode.ERR_Gift_Not_Enough;
                reply();
                return;
            }

            //todo ----------------------------------取出来，当天玩家剩余的赠送礼物的次数-------------------------------------------------------------

            //取出来， 玩家的所有好友
            List<Friends> friendsList = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<Friends>(a => a.OwnerId.Equals(accountId) && a.State == (int) StateType.Active);

            Dictionary<long, Friends> friendsMap = friendsList.ToDictionary(a => a.FriendId, a => a);

            //取出今天赠送礼物的动作
            List<GameAction> giftActions = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<GameAction>(a =>
                            a.OwnerId.Equals(accountId) &&
                            a.CreateTime > currentTime &&
                            a.State == (int) StateType.Active &&
                            a.ConfigId.Equals(10004));
            foreach (var giftAction in giftActions)
            {
                friendsMap.Remove(giftAction.FriendId);
            }

            if (friendsMap.Count == 0)
            {
                response.Error = ErrorCode.ERR_No_Have_To_Gift_Friend;
                reply();
                return;
            }

            //根据id取出来玩家列表
            List<Account> friendAccounts = new List<Account>();
            foreach (var friendId in friendsMap.Keys)
            {
                var accounts = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                        .Query<Account>(a => a.Id.Equals(friendId) && a.State == (int) StateType.Active);
                friendAccounts = friendAccounts.Concat(accounts).ToList();
            }

            friendAccounts.Sort((a, b) => { return (int) b.LastLogonTime - (int) a.LastLogonTime; });

            List<long> giftAccounts = new List<long>();

            var count = Mathf.Min(giftItem.Count, friendAccounts.Count);
            for (int i = 0; i < count; i++)
            {
                var account = friendAccounts[i];
                //储存赠送动作
                GameAction gameAction = new GameAction()
                {
                    Id = IdGenerater.Instance.GenerateId(),
                    OwnerId = accountId,
                    ConfigId = 10004,
                    FriendId = account.Id,
                    Value = 1
                };
                giftAccounts.Add(account.Id);
                await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(gameAction);
                gameAction.Dispose();
            }

            giftItem.Count -= count;
            await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(giftItem);

            response.Error = ErrorCode.ERR_Success;
            response.AccountIds = giftAccounts;
            reply();
            //储存每日赠送礼物额度
            foreach (var friend in friendsList)
            {
                friend.Dispose();
            }

            giftItem.Dispose();

            foreach (var action in giftActions)
            {
                action.Dispose();
            }

            await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(giftItem);
        }
    }
}