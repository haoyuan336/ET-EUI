using System.Collections.Generic;
using ET.Account;

namespace ET
{
    public class M2C_SyncDiamondActionHandler: AMHandler<M2C_SyncDiamondAction>
    {
        protected override async ETTask Run(Session session, M2C_SyncDiamondAction message)
        {
            Log.Debug("收到了 处理滑动的消息");
            //todo 开始处理action
            DiamondComponent diamondComponent = session.DomainScene().GetComponent<DiamondComponent>();
            HeroCardComponent heroCardComponent = session.DomainScene().GetComponent<HeroCardComponent>();
            List<DiamondActionItem> diamondActionItems = message.DiamondActionItems;
            List<AttackActionItem> attackActionItems = message.AttackActionItems;
            GameLoseResultAction gameLoseResultAction = message.GameLoseResultAction;
            foreach (var diamondActionItem in diamondActionItems)
            {
                List<ETTask> tasks = new List<ETTask>();

                int count = 0;
                Log.Debug($"diamond actions  {diamondActionItem.DiamondActions.Count}");
                foreach (var diamondAction in diamondActionItem.DiamondActions)
                {
                    DiamondInfo diamondInfo = diamondAction.DiamondInfo;
                    Diamond diamond = diamondComponent.GetChild<Diamond>(diamondInfo.Id);

                    switch (diamondAction.ActionType)
                    {
                        case (int) DiamondActionType.Move:
                            Log.Debug($"diamond info lieinde {diamondInfo.LieIndex}, {diamondInfo.HangIndex}");
                            Log.Debug($"diamond {diamond}");
                            diamond.SetIndex(diamondInfo.LieIndex, diamondInfo.HangIndex);
                            count++;
                            Log.Debug($"count {count}");
                            tasks.Add(Game.EventSystem.PublishAsync(new EventType.UpdateDiamondIndex() { Diamond = diamond }));
                            break;
                        case (int) DiamondActionType.Destory:
                            if (diamondInfo.HeroCardInfo != null)
                            {
                                Log.Debug("play action logic");
                                // HeroCard heroCard = session.DomainScene().GetComponent<HeroCardComponent>()
                                // .GetChild<HeroCard>(diamondInfo.HeroCardInfo.HeroId);
                                HeroCard heroCard = heroCardComponent.GetChild<HeroCard>(diamondInfo.HeroCardInfo.HeroId);
                                if (diamondInfo.HeroCardInfo.DiamondAttack > heroCard.DiamondAttack)
                                {
                                    heroCard.DiamondAttack = diamondInfo.HeroCardInfo.DiamondAttack;
                                    Game.EventSystem.PublishAsync(new EventType.PlayAddAttackViewAnim()
                                            {
                                                HeroCard = heroCard, Diamond = diamond, HeroCardInfo = diamondInfo.HeroCardInfo
                                            })
                                            .Coroutine();
                                }

                                if (diamondInfo.HeroCardInfo.Angry > heroCard.Angry)
                                {
                                    heroCard.Angry = diamondInfo.HeroCardInfo.Angry;
                                    Game.EventSystem.PublishAsync(new EventType.PlayAddAngryViewAnim()
                                            {
                                                HeroCard = heroCard, Diamond = diamond, HeroCardInfo = diamondInfo.HeroCardInfo
                                            })
                                            .Coroutine();
                                }
                            }

                            // await TimerComponent.Instance.WaitFrameAsync();

                            tasks.Add(Game.EventSystem.PublishAsync(new EventType.DestoryDiamondView() { Diamond = diamond }));

                            break;
                        case (int) DiamondActionType.Create:
                            Diamond newDiamond = diamondComponent.CreateDiamoneWithMessage(diamondAction.DiamondInfo);
                            tasks.Add(Game.EventSystem.PublishAsync(new EventType.InitDiamondAndMoveDown() { Diamond = newDiamond }));
                            break;
                    }
                }

                await ETTaskHelper.WaitAll(tasks);
            }

            foreach (var attackActionItem in attackActionItems)
            {
                List<ETTask> tasks = new List<ETTask>();
                foreach (var attackAction in attackActionItem.AttackActions)
                {
                    Log.Debug("play attack action");
                    HeroCard heroCard = heroCardComponent.GetChild<HeroCard>(attackAction.AttackHeroCardInfo.HeroId);
                    await heroCard.PlayHeroCardAttackAnimAsync(attackAction);
                }

                await ETTaskHelper.WaitAll(tasks);
            }

            //
            long AccountId = session.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;

            if (gameLoseResultAction == null)
            {
                Log.Debug("一回合结束了");
                // todo 给服务器发送ready消息
                // long RoomId = session.ZoneScene().GetComponent<PlayerComponent>().RoomId;
                // M2C_PlayerReadyTurnResponse m2CPlayerReadyTurnResponse =
                //         (M2C_PlayerReadyTurnResponse) await session.Call(new C2M_PlayerReadyTurnRequest() { AccountId = AccountId, RoomId = RoomId });
                // if (m2CPlayerReadyTurnResponse.Error == ErrorCode.ERR_Success)
                // {
                //     // await Game.EventSystem.PublishAsync(new EventType.UnLockTouchLock() { ZoneScene = session.ZoneScene().CurrentScene() });
                // }

                await Game.EventSystem.PublishAsync(new EventType.UnLockTouchLock() { ZoneScene = session.ZoneScene().CurrentScene() });
            }
            else
            {
                Log.Debug($"lose id{gameLoseResultAction.LoseAccountId}");
                Log.Debug($"self id {AccountId}");
                if (!AccountId.Equals(gameLoseResultAction.LoseAccountId))
                {
                    Log.Debug("game win");
                    Game.EventSystem.Publish(new EventType.ShowGameWinUI() { ZondScene = session.ZoneScene() });
                }
                else
                {
                    Log.Debug("game lose");
                }
            }

            await ETTask.CompletedTask;
        }
    }
}