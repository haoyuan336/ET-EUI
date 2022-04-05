using System.Collections.Generic;
using System.Runtime.InteropServices;
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

            List<DiamondActionItem> diamondActionItems = message.DiamondActionItems;
            List<AttackActionItem> attackActionItems = message.AttackActionItems;

            foreach (var diamondActionItem in diamondActionItems)
            {
                List<ETTask> tasks = new List<ETTask>();
                foreach (var diamondAction in diamondActionItem.DiamondActions)
                {
                    DiamondInfo diamondInfo = diamondAction.DiamondInfo;
                    Diamond diamond = diamondComponent.GetChild<Diamond>(diamondInfo.Id);
                    switch (diamondAction.ActionType)
                    {
                        case (int) DiamondActionType.Move:
                            diamond.SetIndex(diamondInfo.LieIndex, diamondInfo.HangIndex);
                            tasks.Add(Game.EventSystem.PublishAsync(new EventType.UpdateDiamondIndex() { Diamond = diamond }));
                            break;
                        case (int) DiamondActionType.Destory:
                            tasks.Add(Game.EventSystem.PublishAsync(new EventType.DestoryDiamondView() { Diamond = diamond }));
                            if (diamondInfo.HeroCardId != 0)
                            {
                                HeroCard heroCard = session.DomainScene().GetComponent<HeroCardComponent>()
                                        .GetChild<HeroCard>(diamondInfo.HeroCardId);
                                Game.EventSystem.Publish(new EventType.PlayAddAttackAngryViewAnim()
                                {
                                    HeroCard = heroCard,
                                    Diamond = diamond,
                                    AddAttack = diamondInfo.HeroCardAddAttack,
                                    AddAngry = diamondInfo.HeroCardAddAngry,
                                    EndAngry = diamondInfo.HeroCardEndAngry,
                                    EndAttack = diamondInfo.HeroCardEndAttack
                                });
                            }

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
                    tasks.Add(session.DomainScene().GetComponent<HeroCardComponent>().PlayHeroCardAttackAnimAsync(attackAction));
                }

                await ETTaskHelper.WaitAll(tasks);
            }

            Log.Debug("一回合结束了");
            //todo 给服务器发送ready消息
            long AccountId = session.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            long RoomId = session.ZoneScene().GetComponent<PlayerComponent>().RoomId;
            M2C_PlayerReadyTurnResponse m2CPlayerReadyTurnResponse =
                    (M2C_PlayerReadyTurnResponse) await session.Call(new C2M_PlayerReadyTurnRequest() { AccountId = AccountId, RoomId = RoomId });
            if (m2CPlayerReadyTurnResponse.Error == ErrorCode.ERR_Success)
            {
                await Game.EventSystem.PublishAsync(new EventType.UnLockTouchLock() { ZoneScene = session.ZoneScene().CurrentScene() });
            }

            await ETTask.CompletedTask;
        }
    }
}