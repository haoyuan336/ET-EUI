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
            long targetAccountId = request.AccountInfo.Account;
            long accountId = request.Account;
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.GiveGiftToFriend, accountId.GetHashCode()))
            {
                long timeDay = CustomHelper.GetCurrentDayTime();
                
                
                //todo ------------------检查是否赠送过礼物了--------------------
                //首先取出来当日赠送礼物的动作
                List<GameAction> gameActions = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                        .Query<GameAction>(a =>
                                a.OwnerId.Equals(accountId) && a.FriendId.Equals(targetAccountId) && a.CreateTime >= timeDay &&
                                a.State == (int) StateType.Active && a.ConfigId == 10004);
                if (gameActions.Count > 0)
                {
                    response.Error = ErrorCode.ERR_Gift_Gived;
                    reply();
                    return;
                }
                //todo ------------------检查是否赠送过礼物了--------------------

                List<Item> items = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                        .Query<Item>(a =>
                                a.OwnerId.Equals(accountId) && a.State == (int) StateType.Active && a.CreateTime > timeDay &&
                                a.ConfigId == 1013);
                Log.Debug($"item  {items.Count}");

                Item item;
                if (items.Count == 0)
                {
                    ItemConfig itemConfig = ItemConfigCategory.Instance.Get(1013);

                    item = new Item()
                    {
                        Id = IdGenerater.Instance.GenerateId(), Count = itemConfig.DefaultValue, OwnerId = accountId, ConfigId = 1013
                        // Type = (int) ItemType.PreDayPowerGift
                    };
                }
                else
                {
                    item = items[0];
                }

                if (item.Count <= 0)
                {
                    response.Error = ErrorCode.ERR_Gift_Not_Enough;
                    reply();
                    return;
                }

                item.Count--;
                await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(item);

                //todo --------------------储存赠送礼物的动作---------------

                var action = new GameAction()
                {
                    Id = IdGenerater.Instance.GenerateId(),
                    OwnerId = request.Account,
                    ConfigId = 10004,
                    FriendId = targetAccountId,
                    Value = 1
                };
                await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(action);
                action.Dispose();
                //todo --------------------储存赠送礼物的动作---------------

                item.Dispose();

                // response.FriendInfo = friends.GetFriendInfo();
                // friends.Dispose();
                response.Error = ErrorCode.ERR_Success;
                reply();
            }

            await ETTask.CompletedTask;
        }
    }
}