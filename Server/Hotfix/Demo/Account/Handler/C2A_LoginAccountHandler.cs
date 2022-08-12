using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace ET
{
    public class C2A_LoginAccountHandler: AMRpcHandler<C2A_LoginAccount, A2C_LoginAccount>
    {
        protected override async ETTask Run(Session session, C2A_LoginAccount request, A2C_LoginAccount response, Action reply)
        {
            if (session.DomainScene().SceneType != SceneType.Account)
            {
                Log.Error($"请求的Scene错误:{session.DomainScene().SceneType}");
                session.Dispose();
                return;
            }

            //移除5s自动掉线的监听组件
            session.RemoveComponent<SessionAcceptTimeoutComponent>();

            //增加连击访问限制
            if (session.GetComponent<SessionLockComponent>() != null)
            {
                session.Error = ErrorCode.ERR_RequestRepeatedly;
                reply();
                session.Disconnect().Coroutine();
                return;
            }

            if (string.IsNullOrEmpty(request.AccountName) || string.IsNullOrEmpty(request.Password))
            {
                response.Error = ErrorCode.ERR_LoginInfoIsNull;
                reply();
                session.Disconnect().Coroutine();
                return;
            }

            if (Regex.IsMatch(request.AccountName.Trim(), "^(?=.*[0-9])(?=.*[a-zA-Z])(.{8,})$"))
            {
                response.Error = ErrorCode.ERR_PasswordFormateErr;
                reply();
                session.Disconnect().Coroutine();
                return;
            }


            using (session.AddComponent<SessionLockComponent>())
            {
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginAccount, request.AccountName.GetHashCode()))
                {
                    var accountInfoList = await DBManagerComponent.Instance.GetZoneDB(session.DomainZone())
                            .Query<Account>(d => d.AccountName.Equals(request.AccountName));
                    Account account;
                    if (accountInfoList != null && accountInfoList.Count > 0)
                    {
                        account = accountInfoList[0];
                        session.AddChild(account);
                        if (account.AccountType == (int) AccountType.BlackList)
                        {
                            response.Error = ErrorCode.ERR_AccountInBlackListError;
                            reply();
                            session.Disconnect().Coroutine();
                            account?.Dispose();
                            return;
                        }

                        if (!account.Password.Equals(request.Password))
                        {
                            response.Error = ErrorCode.ERR_AccountPasswordError;
                            reply();
                            session.Dispose();
                            account?.Dispose();
                            return;
                        }
                    }
                    else
                    {
                        // string str = "qwertyuiopasdfghjklzxcvbnm";
                        account = session.AddChild<Account>();
                        account.AccountName = request.AccountName;
                        account.Password = request.Password;
                        account.CreateTime = TimeHelper.ServerNow();
                        account.AccountType = (int) AccountType.General;
                        account.NickName = request.AccountName;
                        // account.NickName = $"{str.RandomChar()}{str.RandomChar()}{str.RandomChar()}";
                        //储存用户道具信息
                        // KeyValuePair<int, ItemConfig>[] itemConfigs = ItemConfigCategory.Instance.GetAll().ToArray();
                        // foreach (var itemConfig in itemConfigs)
                        // {
                        //     Item item = new Item()
                        //     {
                        //         Id = IdGenerater.Instance.GenerateId(),
                        //         ConfigId = itemConfig.Key,
                        //         Count = itemConfig.Value.DefaultValue,
                        //         OwnerId = account.Id
                        //     };
                        //
                        //     await DBManagerComponent.Instance.GetZoneDB(session.DomainZone()).Save(item);
                        //     item.Dispose();
                        // }
                    }

                    
                    // {
                        
                        
                        //todo 储存用户的基础道具信息
                    // }
                    // account.AddComponent<ItemComponent>();
                    // account.AddComponent(Herocard)

                    account.LastLogonTime = TimeHelper.ServerNow();
                    await DBManagerComponent.Instance.GetZoneDB(session.DomainZone()).Save<Account>(account);

                    StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(session.DomainZone(), "LoginCenter");
                    long loginCenterInstanceId = startSceneConfig.InstanceId;
                    L2A_LoginAccountResponse l2aLoginAccountResponse =
                            (L2A_LoginAccountResponse) await ActorMessageSenderComponent.Instance.Call(loginCenterInstanceId,
                                new A2L_LoginAccountRequest() { AccountId = account.Id });
                    if (l2aLoginAccountResponse.Error != ErrorCode.ERR_Success)
                    {
                        response.Error = l2aLoginAccountResponse.Error;
                        reply();
                        session.Disconnect().Coroutine();
                        account.Dispose();
                        return;
                    }

                    string Token = TimeHelper.ServerNow().ToString() + RandomHelper.RandomNumber(int.MinValue, int.MaxValue);
                    session.DomainScene().GetComponent<TokenComponent>().Remove(account.Id);
                    session.DomainScene().GetComponent<TokenComponent>().Add(account.Id, Token);
                    response.AccountId = account.Id;
                    response.Token = Token;

                    reply();
                    account?.Dispose();

                    //储存当前的玩家登录动作

                    //找出来今天是否登录了
                    var currentTime = CustomHelper.GetCurrentDayTime();

                    List<GameAction> gameActions = await DBManagerComponent.Instance.GetZoneDB(session.DomainZone())
                            .Query<GameAction>(a => a.OwnerId.Equals(account.Id) && a.CreateTime >= currentTime && a.State == (int) StateType.Active);

                    if (gameActions.Count == 0)
                    {
                        var gameAction = new GameAction() { Id = IdGenerater.Instance.GenerateId(), OwnerId = account.Id, ConfigId = 10001 };
                        await DBManagerComponent.Instance.GetZoneDB(session.DomainZone()).Save(gameAction);
                        gameAction.Dispose();
                    }
                }
            }

            await ETTask.CompletedTask;
        }
    }
}