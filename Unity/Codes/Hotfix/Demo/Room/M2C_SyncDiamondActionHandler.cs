using System.Collections.Generic;
using ET.Account;
using ET.EventType;

namespace ET
{
    public class M2C_SyncDiamondActionHandler: AMHandler<M2C_SyncDiamondAction>
    {
        protected override async ETTask Run(Session session, M2C_SyncDiamondAction message)
        {
            Log.Debug("收到了 处理滑动的消息");
            //todo 开始处理action
            DiamondComponent diamondComponent = session.ZoneScene().CurrentScene().GetComponent<DiamondComponent>();
            HeroCardComponent heroCardComponent = session.ZoneScene().CurrentScene().GetComponent<HeroCardComponent>();
            List<DiamondActionItem> diamondActionItems = message.DiamondActionItems;
            List<AttackActionItem> attackActionItems = message.AttackActionItems;
            GameLoseResultAction gameLoseResultAction = message.GameLoseResultAction;
            foreach (var diamondActionItem in diamondActionItems)
            {
                List<ETTask> tasks = new List<ETTask>();

                int count = 0;
                // Log.Debug($"diamond actions  {diamondActionItem.DiamondActions.Count}");

                if (diamondActionItem.AddAttackItemAction != null)
                {
                    // Log.Warning("存在等价攻击力的数据");
                    tasks.Add(Game.EventSystem.PublishAsync(new EventType.PlayAddAttackViewAnim()
                    {
                        AddItemAction = diamondActionItem.AddAttackItemAction, Scene = session.ZoneScene().CurrentScene()
                    }));
                }

                if (diamondActionItem.AddAngryItemAction != null)
                {
                    Log.Warning("存在增加怒气值的数据");
                    tasks.Add(Game.EventSystem.PublishAsync(new EventType.PlayAddAngryViewAnim()
                    {
                        AddItemAction = diamondActionItem.AddAngryItemAction, Scene = session.ZoneScene().CurrentScene()
                    }));
                }

                foreach (var diamondAction in diamondActionItem.DiamondActions)
                {
                    DiamondInfo diamondInfo = diamondAction.DiamondInfo;
                    Diamond diamond = diamondComponent.GetChild<Diamond>(diamondInfo.Id);

                    switch (diamondAction.ActionType)
                    {
                        case (int) DiamondActionType.Move:
                            // Log.Debug($"diamond info lieinde {diamondInfo.LieIndex}, {diamondInfo.HangIndex}");
                            // Log.Debug($"diamond {diamond}");
                            diamond.SetIndex(diamondInfo.LieIndex, diamondInfo.HangIndex);
                            count++;
                            // Log.Debug($"count {count}");
                            tasks.Add(Game.EventSystem.PublishAsync(new EventType.UpdateDiamondIndex() { Diamond = diamond }));
                            break;
                        case (int) DiamondActionType.Destory:
                            tasks.Add(diamondComponent.RemoveChild(diamond));
                            break;
                        case (int) DiamondActionType.Create:

                            tasks.Add(diamondComponent.CreateDiamoneWithMessage(diamondAction.DiamondInfo));

                            // ETTask task = ETTask.Create();
                            // await TimerComponent.Instance.WaitAsync(100);
                            // task.SetResult();
                            // Diamond newDiamond = diamondComponent.CreateDiamoneWithMessage(diamondAction.DiamondInfo);
                            // tasks.Add(Game.EventSystem.PublishAsync(new EventType.InitDiamondAndMoveDown() { Diamond = newDiamond }));
                            break;
                    }
                }

                await ETTaskHelper.WaitAll(tasks);
            }

            await Game.EventSystem.PublishAsync(new EventType.ChangeFightCameraLook() { Value = true });
            foreach (var attackActionItem in attackActionItems)
            {
                List<ETTask> tasks = new List<ETTask>();
                foreach (var attackAction in attackActionItem.AttackActions)
                {
                    // Log.Debug("play attack action");
                    HeroCard heroCard = heroCardComponent.GetChild<HeroCard>(attackAction.AttackHeroCardInfo.HeroId);
                    await heroCard.PlayHeroCardAttackAnimAsync(attackAction);
                }

                await ETTaskHelper.WaitAll(tasks);
            }

            
            
            
            await Game.EventSystem.PublishAsync(new EventType.ChangeFightCameraLook() { Value = false });

            List<HeroCard> heroCards = heroCardComponent.GetChilds<HeroCard>();
            Game.EventSystem.Publish(new EventType.GameAroundOver() { HeroCards = heroCards });
            //
            long AccountId = session.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;

            if (gameLoseResultAction == null)
            {
                Log.Debug("一回合结束了");
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
                    // Game.EventSystem.Publish(new EventType.Show);
                    Game.EventSystem.Publish(new EventType.ShowGameLoaseUI() { ZoneScene = session.ZoneScene() });
                }
            }

            await ETTask.CompletedTask;
        }
    }
}