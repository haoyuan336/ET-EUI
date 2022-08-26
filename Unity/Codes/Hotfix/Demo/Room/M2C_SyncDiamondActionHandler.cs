using System.Collections.Generic;
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
            GameLoseResultAction gameLoseResultAction = message.GameLoseResultAction;
            //开始处理的时候 锁定触摸
            await Game.EventSystem.PublishAsync(new UnLockTouchLock() { ZoneScene = session.ZoneScene().CurrentScene(), IsLockTouch = true });
            bool isContainsCrash = false;
            var comboCount = 0;
            foreach (var diamondActionItem in message.DiamondActionItems)
            {
                Game.EventSystem.Publish(new ShowAttackMark()
                {
                    HeroCardComponent = heroCardComponent, IsShow = true, DiamondActionItem = diamondActionItem
                });
                var destoryIndex = 0;
                if (diamondActionItem.CrashType == (int)CrashType.Normal)
                {
                    comboCount++;
                    Game.EventSystem.Publish(new ShowComobAnim()
                    {
                        Scene = session.ZoneScene(), ComboCount = comboCount, CrashCount = diamondActionItem.DiamondActions.Count
                    });
                }
                List<ETTask> sunTaskList = new List<ETTask>();
                List<ETTask> tasks = new List<ETTask>();
                foreach (var diamondAction in diamondActionItem.DiamondActions)
                {
                    DiamondInfo diamondInfo = diamondAction.DiamondInfo;
                    Diamond diamond = diamondComponent.GetChild<Diamond>(diamondInfo.Id);

                    switch (diamondAction.ActionType)
                    {
                        case (int)DiamondActionType.MoveDown:
                            diamond.SetIndex(diamondInfo.LieIndex, diamondInfo.HangIndex);
                            tasks.Add(Game.EventSystem.PublishAsync(new UpdateDiamondIndex()
                            {
                                Diamond = diamond, DiamondActionType = DiamondActionType.MoveDown
                            }));
                            break;
                        case (int)DiamondActionType.Move:
                            diamond.SetIndex(diamondInfo.LieIndex, diamondInfo.HangIndex);
                            tasks.Add(Game.EventSystem.PublishAsync(new UpdateDiamondIndex()
                            {
                                Diamond = diamond, DiamondActionType = DiamondActionType.Move
                            }));
                            break;
                        case (int)DiamondActionType.Destory:
                        case (int)DiamondActionType.SpecialDestry:
                            sunTaskList.Add(diamondComponent.RemoveChild(diamond, destoryIndex, diamondAction, diamondActionItem));
                            isContainsCrash = true;
                            break;
                        case (int)DiamondActionType.Create:
                            tasks.Add(diamondComponent.CreateDiamoneWithMessage(diamondAction.DiamondInfo));
                            break;
                    }
                }

                await TimerComponent.Instance.WaitAsync(ConstValue.CrashItemWaitTime);
                await ETTaskHelper.WaitAll(sunTaskList);
                await ETTaskHelper.WaitAll(tasks);
            }

            Game.EventSystem.Publish(new UpdateHeroAttackInfo()
            {
                HeroCardComponent = heroCardComponent, ComboActionItem = message.ComboActionItem,
            });
            Game.EventSystem.Publish(new HideCombo() { Scene = session.ZoneScene() });
            if (isContainsCrash)
            {
                await Game.EventSystem.PublishAsync(new ChangeFightCameraLook() { ZoneScene = session.ZoneScene(), Value = true });
                Game.EventSystem.Publish(new PlayDiamondContentAnim() { Value = false });
            }
            await Game.EventSystem.PublishAsync(new PlayHeroCardAttackAnim()
            {
                HeroCardComponent = heroCardComponent, AttackActionItems = message.AttackActionItems
            });
            // await Game.EventSystem.PublishAsync(new UpdateHeroBuffInfo()
            // {
            //     UpdateHeroBuffInfoItem = message.UpdateHeroBuffInfoItem, HeroCardComponent = heroCardComponent
            // });

            if (isContainsCrash)
            {
                await Game.EventSystem.PublishAsync(new ChangeFightCameraLook() { ZoneScene = session.ZoneScene(), Value = false });
                Game.EventSystem.Publish(new PlayDiamondContentAnim() { Value = true });
            }

            List<HeroCard> heroCards = heroCardComponent.GetChilds<HeroCard>();
            Game.EventSystem.Publish(new GameAroundOver() { HeroCards = heroCards });
            if (message.AddRoundAngryItem != null)
            {
                Game.EventSystem.Publish(new UpdateHeroAngryInfo()
                {
                    HeroCardComponent = heroCardComponent, HeroCardDataComponentInfos = message.AddRoundAngryItem.HeroCardDataComponentInfos
                });
            }

            //
            long AccountId = session.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;

            if (gameLoseResultAction == null)
            {
                Log.Debug("一回合结束了");
                await Game.EventSystem.PublishAsync(new UnLockTouchLock() { ZoneScene = session.ZoneScene().CurrentScene(), IsLockTouch = false });
            }
            else
            {
                if (!AccountId.Equals(gameLoseResultAction.LoseAccountId))
                {
                    Log.Debug("game win");
                    Game.EventSystem.Publish(new ShowGameWinUI() { ZondScene = session.ZoneScene() });
                }
                else
                {
                    Log.Debug("game lose");
                    Game.EventSystem.Publish(new ShowGameLoaseUI() { ZoneScene = session.ZoneScene() });
                }
            }

            await ETTask.CompletedTask;
        }
    }
}