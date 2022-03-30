using System.Collections.Generic;
using System.Runtime.InteropServices;

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
                            tasks.Add(Game.EventSystem.PublishAsync(new EventType.DestoryDiamondView()
                            {
                                Diamond = diamond
                            }));
                            
                            
                            session.DomainScene().GetComponent<HeroCardComponent>().AddHeroValueByDiamondDestroy(diamond).Coroutine();
                            // diamond.Dispose();
                            break;
                        case (int)DiamondActionType.Create:
                            Diamond newDiamond = diamondComponent.CreateDiamoneWithMessage(diamondAction.DiamondInfo);
                            tasks.Add(Game.EventSystem.PublishAsync(new EventType.InitDiamondAndMoveDown(){Diamond = newDiamond}));
                            break;
                    }
                }
                await ETTaskHelper.WaitAll(tasks);
                

            }
            
            await session.DomainScene().GetComponent<HeroCardComponent>().PlayHeroCardAttackAnimAsync();
            
            Game.EventSystem.Publish(new EventType.UnLockTouchLock(){ZoneScene = session.ZoneScene().CurrentScene()});

            await ETTask.CompletedTask;
        }
    }
}