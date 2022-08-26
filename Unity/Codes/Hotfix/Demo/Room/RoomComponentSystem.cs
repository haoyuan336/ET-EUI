using System;
using System.Collections.Generic;
using ET.EventType;

namespace ET
{
    public class RoomComponentAwakeSystem: AwakeSystem<RoomComponent>
    {
        public override void Awake(RoomComponent self)
        {
            self.Awake();
        }
    }

    public static class RoomComponentSystem
    {
        public static void Awake(this RoomComponent self)
        {
        }

        public static async ETTask ProcessHeroAttackAction(this RoomComponent self)
        {
            await ETTask.CompletedTask;
        }

        public static async ETTask ProcessDiamondAction(this RoomComponent self, DiamondAction diamondAction)
        {
            if (diamondAction == null)
            {
                return;
            }
            DiamondComponent diamondComponent = self.DomainScene().GetComponent<DiamondComponent>();
            DiamondInfo diamondInfo = diamondAction.DiamondInfo;
            Diamond diamond = diamondComponent.GetChild<Diamond>(diamondInfo.Id);
            if (diamondAction.ActionType != (int)DiamondActionType.Create && diamond == null)
            {
                return;
            }

            switch (diamondAction.ActionType)
            {
                case (int)DiamondActionType.MoveDown:
                    diamond.SetIndex(diamondInfo.LieIndex, diamondInfo.HangIndex);
                    await Game.EventSystem.PublishAsync(
                        new UpdateDiamondIndex() { Diamond = diamond, DiamondActionType = DiamondActionType.MoveDown });
                    break;
                case (int)DiamondActionType.Move:
                    diamond.SetIndex(diamondInfo.LieIndex, diamondInfo.HangIndex);
                    await Game.EventSystem.PublishAsync(new UpdateDiamondIndex() { Diamond = diamond, DiamondActionType = DiamondActionType.Move });
                    break;
                case (int)DiamondActionType.Destory:
                case (int)DiamondActionType.SpecialDestry:
                    await Game.EventSystem.PublishAsync(new DestoryDiamondView()
                    {
                        Diamond = diamond, DiamondAction = diamondAction, Scene = self.ZoneScene().CurrentScene()
                    });
                    break;
                case (int)DiamondActionType.Create:
                    await diamondComponent.CreateDiamoneWithMessage(diamondAction.DiamondInfo);
                    break;
            }

            await ETTask.CompletedTask;
        }

        public static async ETTask ProcessActionMessageEvent(this RoomComponent self, ActionMessage actionMessage)
        {
            await self.ProcessDiamondAction(actionMessage.DiamondAction);
            await self.ProcessActionMessage(actionMessage);
            await ETTask.CompletedTask;
        }

        public static async ETTask ProcessActionMessage(this RoomComponent self, ActionMessage actionMessage)
        {
            int playType = actionMessage.PlayType;
            List<ActionMessage> actionMessages = actionMessage.ActionMessages;
            List<ETTask> tasks = new List<ETTask>();
            foreach (var action in actionMessages)
            {
                if (playType == (int)ActionMessagePlayType.Sync)
                {
                    tasks.Add(self.ProcessActionMessageEvent(action));
                }
                else if (playType == (int)ActionMessagePlayType.Async)
                {
                    await self.ProcessActionMessageEvent(action);
                }
            }

            await ETTaskHelper.WaitAll(tasks);
        }
    }
}