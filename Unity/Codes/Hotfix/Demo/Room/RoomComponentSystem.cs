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

        public static async ETTask ProcessDiamondAction(this RoomComponent self, ActionMessage actionMessage)
        {
            var diamondAction = actionMessage.DiamondAction;
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

        public static async ETTask ProcessMakeSureAttackHeroEvent(this RoomComponent self, ActionMessage actionMessage)
        {
            MakeSureAttackHeroAction makeSureAttackHeroAction = actionMessage.MakeSureAttackHeroAction;
            if (makeSureAttackHeroAction == null)
            {
                return;
            }

            HeroCardComponent heroCardComponent = self.ZoneScene().CurrentScene().GetComponent<HeroCardComponent>();
            Game.EventSystem.Publish(new ShowAttackMark()
            {
                HeroCardComponent = heroCardComponent,
                IsShow = true,
                HeroCardDataComponentInfo = makeSureAttackHeroAction.HeroCardDataComponentInfo
            });

            await ETTask.CompletedTask;
        }

        public static async ETTask ProcessComboActionMessageEvent(this RoomComponent self, ActionMessage actionMessage)
        {
            var comboAction = actionMessage.CombeAction;
            if (comboAction == null)
            {
                return;
            }

            await Game.EventSystem.PublishAsync(new EventType.ShowComobAnim()
            {
                Scene = self.ZoneScene(), ComboCount = comboAction.CombeTime, CrashCount = comboAction.CrashCount
            });

            await ETTask.CompletedTask;
        }

        public static async ETTask ProcessUpdateHeroInfoAction(this RoomComponent self, ActionMessage actionMessage)
        {
            UpdateHeroInfoAction updateHeroInfoAction = actionMessage.UpdateHeroInfoAction;
            if (updateHeroInfoAction == null)
            {
                return;
            }

            Game.EventSystem.Publish(new EventType.HideCombo() { Scene = self.ZoneScene() });

            HeroCardComponent heroCardComponent = self.ZoneScene().CurrentScene().GetComponent<HeroCardComponent>();
            await Game.EventSystem.PublishAsync(new EventType.UpdateHeroInfoEvent()
            {
                HeroCardComponent = heroCardComponent, HeroCardDataComponentInfo = updateHeroInfoAction.HeroCardDataComponentInfo
            });

            await ETTask.CompletedTask;
        }

        public static async ETTask ProcessAttackAction(this RoomComponent self, ActionMessage actionMessage)
        {
            //攻击逻辑
            AttackAction attackAction = actionMessage.AttackAction;

            if (attackAction == null)
            {
                return;
            }

            HeroCardComponent heroCardComponent = self.ZoneScene().CurrentScene().GetComponent<HeroCardComponent>();
            await Game.EventSystem.PublishAsync(new PlayHeroCardAttackAnim() { HeroCardComponent = heroCardComponent, AttackAction = attackAction });
            await ETTask.CompletedTask;
        }

        public static async ETTask ProcessGameResult(this RoomComponent self, ActionMessage actionMessage)
        {
            GameLoseResultAction action = actionMessage.GameLoseResultAction;
            if (action == null)
            {
                return;
            }

            long AccountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            //
            if (!AccountId.Equals(action.LoseAccountId))
            {
                Log.Debug("game win");
                Game.EventSystem.Publish(new ShowGameWinUI() { ZondScene = self.ZoneScene() });
            }
            else
            {
                Log.Debug("game lose");
                Game.EventSystem.Publish(new ShowGameLoaseUI() { ZoneScene = self.ZoneScene() });
            }

            await ETTask.CompletedTask;
        }

        public static async ETTask ProcessBuffStateEvent(this RoomComponent self, ActionMessage actionMessage)
        {
            UpdateHeroBuffInfo updateHeroBuffInfo = actionMessage.UpdateHeroBuffInfo;

            if (updateHeroBuffInfo == null)
            {
                return;
            }

            HeroCardComponent heroCardComponent = self.ZoneScene().CurrentScene().GetComponent<HeroCardComponent>();
            await Game.EventSystem.PublishAsync(new EventType.UpdateHeroBuffInfoEvent()
            {
                HeroId = updateHeroBuffInfo.HeroId,
                BuffInfos = updateHeroBuffInfo.BuffInfos,
                HeroCardComponent = heroCardComponent,
                HeroCardDataComponentInfo = updateHeroBuffInfo.HeroCardDataComponentInfo
            });

            await ETTask.CompletedTask;
        }

        public static async ETTask ProcessHideAttackMarkEvent(this RoomComponent self, ActionMessage actionMessage)
        {
            HideAttackMarkAction hideAttackMarkAction = actionMessage.HideAttackMarkAction;
            if (hideAttackMarkAction == null)
            {
                return;
            }

            HeroCardComponent heroCardComponent = self.ZoneScene().CurrentScene().GetComponent<HeroCardComponent>();
            await Game.EventSystem.PublishAsync(new EventType.SetHeroCardChooseState()
            {
                HeroCardComponent = heroCardComponent, HeroId = hideAttackMarkAction.HeroCardDataComponentInfo.HeroId, IsShow = false
            });
        }

        public static async ETTask ProcessAttackBeganEvent(this RoomComponent self, ActionMessage actionMessage)
        {
            AttackBeganAction action = actionMessage.AttackBeganAction;
            if (action == null)
            {
                return;
            }

            await Game.EventSystem.PublishAsync(new EventType.PlayDiamondContentAnim() { IShow = false });
        }

        public static async ETTask ProcessAttackEndEvent(this RoomComponent self, ActionMessage actionMessage)
        {
            AttackEndAction action = actionMessage.AttackEndAction;
            if (action == null)
            {
                return;
            }

            await Game.EventSystem.PublishAsync(new EventType.PlayDiamondContentAnim() { IShow = true });
        }

        public static async ETTask ProcessBuffDamageAction(this RoomComponent self, ActionMessage actionMessage)
        {
            BuffDamageAction buffDamageAction = actionMessage.BuffDamageAction;
            if (buffDamageAction == null)
            {
                return;
            }

            HeroCardComponent heroCardComponent = self.ZoneScene().CurrentScene().GetComponent<HeroCardComponent>();
            await Game.EventSystem.PublishAsync(new EventType.PlayBuffDamageAnim()
            {
                HeroCardComponent = heroCardComponent,
                HeroCardDataComponentInfo = buffDamageAction.HeroCardDataComponentInfo,
                BuffInfo = buffDamageAction.BuffInfo,
                DamageCount = buffDamageAction.DamageCount
            });

            await ETTask.CompletedTask;
        }

        public static async ETTask ProcessActionMessageEvent(this RoomComponent self, ActionMessage actionMessage)
        {
            await self.ProcessDiamondAction(actionMessage);
            await self.ProcessMakeSureAttackHeroEvent(actionMessage);
            await self.ProcessUpdateHeroInfoAction(actionMessage);
            await self.ProcessAttackAction(actionMessage);
            await self.ProcessGameResult(actionMessage);
            await self.ProcessBuffStateEvent(actionMessage);
            await self.ProcessHideAttackMarkEvent(actionMessage);
            await self.ProcessAttackBeganEvent(actionMessage);
            await self.ProcessAttackEndEvent(actionMessage);
            await self.ProcessComboActionMessageEvent(actionMessage);
            await self.ProcessBuffDamageAction(actionMessage);
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