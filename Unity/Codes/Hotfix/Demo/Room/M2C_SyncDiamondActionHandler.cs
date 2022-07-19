using System.Collections.Generic;
using System.Threading.Tasks;
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

            bool isContainsCrash = false;

            foreach (var diamondActionItem in diamondActionItems)
            {
                List<ETTask> tasks = new List<ETTask>();

                List<MakeSureAttackHeroAction> makeSureAttackHeroActions = diamondActionItem.MakeSureAttackHeroActions;
                foreach (var makeSureAttackHeroAction in makeSureAttackHeroActions)
                {
                    Game.EventSystem.Publish(new EventType.ShowAttackMark()
                    {
                        IsShow = true,
                        HeroCard = heroCardComponent.GetChild<HeroCard>(makeSureAttackHeroAction.HeroCardDataComponentInfo.HeroId)
                    });
                }

                int count = 0;
                if (diamondActionItem.AddAttackItemActions.Count != 0)
                {
                    Log.Debug($"存在等价攻击力的数据{diamondActionItem.AddAttackItemActions.Count}");
                    tasks.Add(Game.EventSystem.PublishAsync(new EventType.PlayAddAttackViewAnim()
                    {
                        AddItemActions = diamondActionItem.AddAttackItemActions, Scene = session.ZoneScene().CurrentScene()
                    }));
                }

                if (diamondActionItem.AddAngryItemActions.Count != 0)
                {
                    // Log.Warning($"存在增加怒气值的数据{diamondActionItem.AddAngryItemActions.Count}");
                    // tasks.Add(Game.EventSystem.PublishAsync(new EventType.PlayAddAngryViewAnim()
                    // {
                    //     AddItemActions = diamondActionItem.AddAngryItemActions, Scene = session.ZoneScene().CurrentScene()
                    // }));
                }

                var destoryIndex = 0;
                foreach (var diamondAction in diamondActionItem.DiamondActions)
                {
                    DiamondInfo diamondInfo = diamondAction.DiamondInfo;
                    Diamond diamond = diamondComponent.GetChild<Diamond>(diamondInfo.Id);
                   
                    switch (diamondAction.ActionType)
                    {
                        case (int) DiamondActionType.Move:
                            diamond.SetIndex(diamondInfo.LieIndex, diamondInfo.HangIndex);
                            count++;
                            // Log.Debug($"count {count}");
                            tasks.Add(Game.EventSystem.PublishAsync(new EventType.UpdateDiamondIndex() { Diamond = diamond }));
                            break;
                        case (int) DiamondActionType.Destory:
                            tasks.Add(diamondComponent.RemoveChild(diamond, destoryIndex, diamondAction));
                            destoryIndex++;
                            isContainsCrash = true;
                            break;
                        case (int) DiamondActionType.Create:
                            tasks.Add(diamondComponent.CreateDiamoneWithMessage(diamondAction.DiamondInfo));
                            break;
                    }
                }

                await ETTaskHelper.WaitAll(tasks);
            }

            List<ETTask> animTasks = new List<ETTask>();

            if (isContainsCrash)
            {
                animTasks.Add(Game.EventSystem.PublishAsync(new EventType.ChangeFightCameraLook() { ZoneScene = session.ZoneScene(), Value = true }));
                animTasks.Add(Game.EventSystem.PublishAsync(new EventType.PlayDiamondContentAnim() { Value = false }));
            }

            await ETTaskHelper.WaitAll(animTasks);
            foreach (var attackActionItem in attackActionItems)
            {
                List<ETTask> tasks = new List<ETTask>();
                foreach (var attackAction in attackActionItem.AttackActions)
                {
                    // Log.Debug("play attack action");
                    HeroCard heroCard = heroCardComponent.GetChild<HeroCard>(attackAction.AttackHeroCardDataComponentInfo.HeroId);
                    await heroCard.PlayHeroCardAttackAnimAsync(attackAction);
                }

                await ETTaskHelper.WaitAll(tasks);
            }

            List<ETTask> animTaskList = new List<ETTask>();

            if (isContainsCrash)
            {
                animTaskList.Add(Game.EventSystem.PublishAsync(new EventType.ChangeFightCameraLook()
                {
                    ZoneScene = session.ZoneScene(), Value = false
                }));
                animTaskList.Add(Game.EventSystem.PublishAsync(new EventType.PlayDiamondContentAnim() { Value = true }));
            }

            // await Game.EventSystem.PublishAsync(new EventType.PlayDiamondContentAnim() { Value = true });
            // await Game.EventSystem.PublishAsync(new EventType.ChangeFightCameraLook() { Value = false });
            await ETTaskHelper.WaitAll(animTaskList);
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