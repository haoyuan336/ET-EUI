using System;
using System.Collections.Generic;
using System.Linq;

namespace ET
{
    public class FightComponentAwakeSystem: AwakeSystem<FightComponent>
    {
        public override void Awake(FightComponent self)
        {
        }
    }

    public static class FightComponentSystem
    {
        public static void InitAIUnitHeroCard(this FightComponent self)
        {
            for (var i = 0; i < self.Units.Count; i++)
            {
                Unit unit = self.Units[i];
                if (unit.IsAI)
                {
                    //todo 首先创建敌人的英雄卡
                    self.CreateHeroIdListInLevelConfig(unit);
                    break;
                }
            }
        }

        public static void CreateHeroIdListInLevelConfig(this FightComponent self, Unit unit)
        {
            string heroIdsstr = self.LevelConfig.HeroId;
            string[] strList = heroIdsstr.Split(',').ToArray();
            int index = 0;
            foreach (var str in strList)
            {
                int configId = int.Parse(str);
                EnemyHeroConfig enemyHeroConfig = EnemyHeroConfigCategory.Instance.Get(configId);
                HeroCard heroCard = new HeroCard();
                Log.Debug("创建ai玩家的英雄");
                heroCard.Id = IdGenerater.Instance.GenerateId();
                unit.AddChild(heroCard);
                heroCard.ConfigId = enemyHeroConfig.ConfigId;
                heroCard.Level = enemyHeroConfig.Level;
                heroCard.Star = enemyHeroConfig.Star;
                heroCard.Rank = enemyHeroConfig.Rank;

                heroCard.InTroopIndex = index;
                index++;
                heroCard.CampIndex = unit.SeatIndex;
                HeroConfig config = HeroConfigCategory.Instance.Get(heroCard.ConfigId);
                heroCard.AddComponent<SkillComponent>();
                heroCard.AddComponent<BuffComponent>();
                heroCard.AddComponent<HeroCardDataComponent>();
                // heroCardDataComponent.Angry = config.InitAngry;
                heroCard.AddAngry(config.InitAngry);
            }
        }

        public static async ETTask InitPlayerHeroCards(this FightComponent self, Unit unit)
        {
            TroopComponent troopComponent = unit.GetComponent<TroopComponent>();
            HeroCardComponent heroCardComponent = unit.GetComponent<HeroCardComponent>();
            Troop troop = await troopComponent.GetCurrentTroopAsync();
            List<HeroCard> heroCards = await heroCardComponent.GetHeroCardsWithTroopIdAsync(troop.Id);
            // await ETTask.CompletedTask;
            List<ETTask> tasks = new List<ETTask>();
            foreach (var heroCard in heroCards)
            {
                unit.AddChild(heroCard);
                heroCard.AddComponent<SkillComponent>();
                heroCard.AddComponent<BuffComponent>();
                // heroCard.AddComponent<WeaponComponent>();
                List<Weapon> weapons = await DBManagerComponent.Instance.GetZoneDB(self.DomainZone())
                        .Query<Weapon>(a => a.OnWeaponHeroId.Equals(heroCard.Id) && a.State == (int)StateType.Active);
                foreach (var weapon in weapons)
                {
                    heroCard.AddChild(weapon);
                    //取出来，装备的词条
                    List<WordBar> wordBars = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                            .Query<WordBar>(a => a.OwnerId.Equals(weapon.Id) && a.State == (int)StateType.Active);
                    foreach (var wordBar in wordBars)
                    {
                        weapon.AddChild(wordBar);
                    }
                }

                HeroConfig heroConfig = HeroConfigCategory.Instance.Get(heroCard.ConfigId);
                Log.Debug("创建玩家的英雄实力");
                heroCard.AddComponent<HeroCardDataComponent>();
                // heroCardDataComponent.Angry = heroConfig.InitAngry;
                heroCard.AddAngry(heroConfig.InitAngry);
            }

            await ETTaskHelper.WaitAll(tasks);
        }

        public static void SetUnitSeatIndex(this FightComponent self)
        {
            int seatIndex = 0;
            foreach (var unit in self.Units)
            {
                unit.SeatIndex = seatIndex;
                seatIndex++;
            }
        }

        public static void SyncCreateHeroCardMessage(this FightComponent self)
        {
            List<HeroCardInfo> heroCardInfos = new List<HeroCardInfo>();
            List<HeroCardDataComponentInfo> heroCardDataComponentInfos = new List<HeroCardDataComponentInfo>();

            foreach (var unit in self.Units)
            {
                List<HeroCard> heroCards = unit.GetChilds<HeroCard>();
                foreach (var heroCard in heroCards)
                {
                    heroCardInfos.Add(heroCard.GetMessageInfo());
                    heroCardDataComponentInfos.Add(heroCard.GetComponent<HeroCardDataComponent>().GetInfo());
                }
            }

            foreach (var unit in self.Units)
            {
                if (unit.IsAI)
                {
                    continue;
                }

                MessageHelper.SendToClient(unit,
                    new M2C_CreateHeroCardInRoom() { HeroCardInfos = heroCardInfos, HeroCardDataComponentInfos = heroCardDataComponentInfos });
            }
        }

        public static void PlayChooseAttackHero(this FightComponent self, long heroId)
        {
            Log.Debug($"寻找英雄 {heroId}");
            //玩家选择了一个可以攻击的英雄，根据id找到相应的herocard
            foreach (var unit in self.Units)
            {
                HeroCard heroCard = unit.GetChild<HeroCard>(heroId);
                if (heroCard != null)
                {
                    self.CurrentBeAttackHeroCard = heroCard;
                }
            }

            foreach (var unit in self.Units)
            {
                if (unit.IsAI)
                {
                    continue;
                }

                MessageHelper.SendToClient(unit, new M2C_PlayerChooseAttackHero() { HeroId = heroId });
            }
        }

        public static List<HeroCard> GetAliveHeroCardWithColorType(this FightComponent self, Unit unit, int colorId)
        {
            var children = unit.GetChilds<HeroCard>();
            List<HeroCard> heroCards = new List<HeroCard>();
            foreach (var child in children)
            {
                HeroConfig config = HeroConfigCategory.Instance.Get(child.ConfigId);
                if (config.HeroColor.Equals(colorId) && !child.GetIsDead())
                {
                    // return child;
                    heroCards.Add(child);
                }
            }

            return heroCards;
        }

        public static void ProcessAddHeroCardAngryLogic(this FightComponent self, ActionMessage actionMessage)
        {
            //首先遍历 树形结构
            Unit unit = self.GetCurrentAttackUnit();
            List<HeroCard> heroCards = unit.GetChilds<HeroCard>();
            Stack<ActionMessage> stack = new Stack<ActionMessage>();
            stack.Push(actionMessage);
            while (stack.Count != 0)
            {
                ActionMessage action = stack.Pop();
                if (action.ActionMessages.Count != 0)
                {
                    for (var i = action.ActionMessages.Count - 1; i >= 0; i--)
                    {
                        var message = action.ActionMessages[i];
                        if (message.ActionMessages.Count != 0)
                        {
                            stack.Push(message);
                        }

                        if (message.DiamondAction != null && message.DiamondAction.ActionType == (int)DiamondActionType.Destory)
                        {
                            List<HeroCard> cards = heroCards.FindAll(a =>
                            {
                                HeroConfig heroConfig = HeroConfigCategory.Instance.Get(a.ConfigId);
                                return heroConfig.HeroColor.Equals(message.DiamondAction.DiamondInfo.DiamondType) && !a.GetIsDead();
                            });
                            List<AddItemAction> addAngryActions = new List<AddItemAction>();

                            foreach (var card in cards)
                            {
                                // card.GetComponent<HeroCardDataComponent>().Angry += self.LevelConfig.AngryCount;

                                card.AddAngry(self.LevelConfig.AngryCount);
                                AddItemAction addItemAction = new AddItemAction()
                                {
                                    HeroCardInfo = card.GetMessageInfo(),
                                    HeroCardDataComponentInfo = card.GetComponent<HeroCardDataComponent>().GetInfo()
                                };
                                addAngryActions.Add(addItemAction);
                            }

                            message.DiamondAction.AddAngryActions = addAngryActions;
                        }
                    }
                }
            }
        }

        // public static void ProcessAddHeroCardAngryLogic(this FightComponent self, List<DiamondActionItem> diamondActionItems)
        // {
        //     // var count = 0;
        //     foreach (var diamondActionItem in diamondActionItems)
        //     {
        //         Unit unit = self.GetCurrentAttackUnit();
        //         //todo 处理增加卡牌怒气值的逻辑
        //         List<DiamondAction> diamondActions = diamondActionItem.DiamondActions;
        //         //取出与宝石的颜色相同的英雄
        //         List<HeroCard> heroCards = unit.GetChilds<HeroCard>();
        //         foreach (var diamondAction in diamondActions)
        //         {
        //             List<HeroCard> heros = heroCards.FindAll((a) =>
        //             {
        //                 HeroConfig heroConfig = HeroConfigCategory.Instance.Get(a.ConfigId);
        //                 return heroConfig.HeroColor == diamondAction.DiamondInfo.DiamondType && !a.GetIsDead();
        //             });
        //             List<AddItemAction> addAngryActions = new List<AddItemAction>();
        //             foreach (var heroCard in heros)
        //             {
        //                 heroCard.GetComponent<HeroCardDataComponent>().Angry += self.LevelConfig.AngryCount;
        //                 AddItemAction addItemAction = new AddItemAction()
        //                 {
        //                     HeroCardInfo = heroCard.GetMessageInfo(),
        //                     HeroCardDataComponentInfo = heroCard.GetComponent<HeroCardDataComponent>().GetInfo()
        //                 };
        //                 addAngryActions.Add(addItemAction);
        //             }
        //
        //             diamondAction.AddAngryActions = addAngryActions;
        //         }
        //     }
        // }

        public static ActionMessage MakeSureAttackHeros(this FightComponent self, ActionMessage actionMessage)
        {
            Stack<ActionMessage> stack = new Stack<ActionMessage>();
            stack.Push(actionMessage);
            DiamondInfo diamondInfo = null;
            ActionMessage message = null;
            while (stack.Count != 0 && diamondInfo == null)
            {
                ActionMessage action = stack.Pop();
                for (int i = action.ActionMessages.Count - 1; i >= 0; i--)
                {
                    var value = action.ActionMessages[i];
                    if (value.DiamondAction != null && value.DiamondAction.ActionType == (int)DiamondActionType.Destory)
                    {
                        message = action;
                        diamondInfo = value.DiamondAction.DiamondInfo;
                        break;
                    }

                    if (value.ActionMessages.Count != 0)
                    {
                        stack.Push(value);
                    }
                }
            }

            if (diamondInfo == null)
            {
                return null;
            }

            Unit unit = self.GetCurrentAttackUnit();
            List<HeroCard> heroCards = unit.GetChilds<HeroCard>();
            heroCards = heroCards.FindAll(a =>
            {
                HeroConfig heroConfig = HeroConfigCategory.Instance.Get(a.ConfigId);
                if (heroConfig.HeroColor == diamondInfo.DiamondType && !a.GetIsDead())
                {
                    return true;
                }

                return false;
            });
            ActionMessage makeSureActionMessage =
                    new ActionMessage() { PlayType = (int)ActionMessagePlayType.Sync, ActionMessages = new List<ActionMessage>() };
            foreach (var heroCard in heroCards)
            {
                MakeSureAttackHeroAction action = new MakeSureAttackHeroAction()
                {
                    HeroCardDataComponentInfo = heroCard.GetComponent<HeroCardDataComponent>().GetInfo()
                };
                makeSureActionMessage.ActionMessages.Add(new ActionMessage() { MakeSureAttackHeroAction = action });
            }

            message.ActionMessages.Add(makeSureActionMessage);
            return makeSureActionMessage;
        }

        public static void ProcessComboResult(this FightComponent self, ActionMessage actionMessage, ActionMessage makeSureAttackMessage)
        {
            if (makeSureAttackMessage == null)
            {
                return;
            }

            Unit unit = self.GetCurrentAttackUnit();
            List<HeroCard> heroCards = new List<HeroCard>();
            foreach (var action in makeSureAttackMessage.ActionMessages)
            {
                if (action.MakeSureAttackHeroAction != null)
                {
                    heroCards.Add(unit.GetChild<HeroCard>(action.MakeSureAttackHeroAction.HeroCardDataComponentInfo.HeroId));
                }
            }

            Stack<ActionMessage> stack = new Stack<ActionMessage>();
            stack.Push(actionMessage);
            while (stack.Count != 0)
            {
                ActionMessage action = stack.Pop();
                foreach (var message in action.ActionMessages)
                {
                    if (message.CombeAction != null)
                    {
                        foreach (var heroCard in heroCards)
                        {
                            heroCard.GetComponent<HeroCardDataComponent>().DiamondAttackAddition += self.LevelConfig.AttackAddition;
                        }
                    }

                    if (message.ActionMessages.Count != 0)
                    {
                        stack.Push(message);
                    }
                }
            }

            //更新英雄卡牌信息
            ActionMessage updateMessage =
                    new ActionMessage() { PlayType = (int)ActionMessagePlayType.Sync, ActionMessages = new List<ActionMessage>() };
            foreach (var heroCard in heroCards)
            {
                UpdateHeroInfoAction action = new UpdateHeroInfoAction()
                {
                    HeroCardDataComponentInfo = heroCard.GetComponent<HeroCardDataComponent>().GetInfo()
                };
                ActionMessage message = new ActionMessage() { UpdateHeroInfoAction = action };
                updateMessage.ActionMessages.Add(message);
            }

            if (updateMessage.ActionMessages.Count > 0)
            {
                actionMessage.ActionMessages.Add(updateMessage);
            }
        }

        public static void ProcessSettlementBuffRoundLogic(this FightComponent self, Unit unit, ActionMessage actionMessage)
        {
            //处理结算 buff 回合
        }

        public static void ProcessAddRoundAngry(this FightComponent self, ActionMessage actionMessage)
        {
            if (!self.CheckIsHaveCrash(actionMessage))
            {
                return;
            }

            var AddRoundAngryItem = new AddRoundAngryItem();
            ActionMessage addAngryMessage = new ActionMessage()
            {
                PlayType = (int)ActionMessagePlayType.Sync, ActionMessages = new List<ActionMessage>()
            };
            foreach (var unit in self.Units)
            {
                List<HeroCard> cards = unit.GetChilds<HeroCard>();
                foreach (var heroCard in cards)
                {
                    if (!heroCard.GetIsDead())
                    {
                        HeroConfig config = HeroConfigCategory.Instance.Get(heroCard.ConfigId);
                        // heroCard.GetComponent<HeroCardDataComponent>().Angry += config.RoundAddAngry;

                        heroCard.AddAngry(config.RoundAddAngry);
                        AddRoundAngryItem.HeroCardDataComponentInfos.Add(heroCard.GetComponent<HeroCardDataComponent>().GetInfo());

                        UpdateHeroInfoAction updateHeroInfoAction = new UpdateHeroInfoAction()
                        {
                            HeroCardDataComponentInfo = heroCard.GetComponent<HeroCardDataComponent>().GetInfo()
                        };
                        var message = new ActionMessage() { UpdateHeroInfoAction = updateHeroInfoAction };
                        addAngryMessage.ActionMessages.Add(message);
                    }
                }
            }

            actionMessage.ActionMessages.Add(addAngryMessage);
        }

        public static Unit CheckGameEndResult(this FightComponent self)
        {
            //todo 检查游戏胜利还是失败
            foreach (var unit in self.Units)
            {
                var isAllDead = true;
                List<HeroCard> heroCards = unit.GetChilds<HeroCard>();
                foreach (var heroCard in heroCards)
                {
                    if (!heroCard.GetIsDead())
                    {
                        isAllDead = false;
                    }
                }

                if (isAllDead)
                {
                    return unit;
                }
            }

            return null;
        }

        public static bool CheckIsHaveCrash(this FightComponent self, ActionMessage actionMessage)
        {
            Stack<ActionMessage> stack = new Stack<ActionMessage>();
            stack.Push(actionMessage);
            while (stack.Count != 0)
            {
                var message = stack.Pop();
                for (var i = message.ActionMessages.Count - 1; i >= 0; i--)
                {
                    var action = message.ActionMessages[i];
                    if (action.CombeAction != null)
                    {
                        return true;
                    }

                    if (action.ActionMessages.Count != 0)
                    {
                        stack.Push(action);
                    }
                }
            }

            return false;
        }

        public static void ProcessReBackNormalAttackLogic(this FightComponent self, ActionMessage actionMessage, Unit attackUnit,
        Unit beUnit)
        {
            //找到轮流攻击的英雄
            HeroCard attackHero = self.GetTurnAttackHero(attackUnit);
            if (attackHero == null)
            {
                return;
            }

            if (!self.GetIsCanAttack(attackHero))
            {
                return;
            }

            // Log.Warning($"attack {attackHero.}");
            Skill skill = attackHero.GetComponent<HeroCardDataComponent>().MakeSureSkill(3);
            List<HeroCard> beAttackHeroCards = self.GetBeAttackHeroCards(beUnit, attackUnit, attackHero, skill);
            AttackAction attackAction = new AttackAction();
            List<HeroCardDataComponentInfo> beHeroCardDataComponentInfos = new List<HeroCardDataComponentInfo>();
            List<HeroBuffInfo> heroBufferInfos = new List<HeroBuffInfo>();
            ActionMessage recoryMessageList =
                    new ActionMessage() { PlayType = (int)ActionMessagePlayType.Async, ActionMessages = new List<ActionMessage>() };
            foreach (var beAttackHeroCard in beAttackHeroCards)
            {
                if (beAttackHeroCard == null)
                {
                    continue;
                }

                // self.ProcessBuffLogic(beAttackHeroCard, skill);
                attackHero.ProcessAttachBuffLogic(beAttackHeroCard, skill);
                // attackHero.AttackTarget(beAttackHeroCard, 0, skill);
                attackHero.ProcessMainFightLogic(beAttackHeroCard, skill);
                List<BuffInfo> buffInfos = beAttackHeroCard.GetComponent<BuffComponent>().GetBuffInfos();
                heroBufferInfos.Add(new HeroBuffInfo() { BuffInfos = buffInfos });
                beHeroCardDataComponentInfos.Add(beAttackHeroCard.GetComponent<HeroCardDataComponent>().GetInfo());

                ActionMessage recoveryMessage = self.ProcessHeroRecoveryLogic(beAttackHeroCard);
                if (recoveryMessage != null)
                {
                    recoryMessageList.ActionMessages.Add(recoveryMessage);
                }
            }

            attackAction.AttackHeroCardDataComponentInfo = attackHero.GetComponent<HeroCardDataComponent>().GetInfo();
            attackAction.BeAttackHeroCardDataComponentInfos = beHeroCardDataComponentInfos;
            attackAction.HeroBuffInfos = heroBufferInfos;
            ActionMessage attackMessage = new ActionMessage() { AttackAction = attackAction, ActionMessages = new List<ActionMessage>() };
            attackMessage.ActionMessages.Add(recoryMessageList);
            actionMessage.ActionMessages.Add(attackMessage);
        }

        // public static void ProcessBuffLogic(this FightComponent self, HeroCard beAttackHeroCard, Skill skill)
        // {
        //     //todo 处理buff逻辑, 被攻击的卡牌
        //     //取出来技能包含的buff
        //     BuffComponent buffComponent = beAttackHeroCard.GetComponent<BuffComponent>();
        //     // todo 给收攻击的英雄增加buff
        //     buffComponent.AddBuffWithSkillConfig(skill);
        // }

        public static void ProcessReBackAttackLogic(this FightComponent self, ActionMessage actionMessage)
        {
            bool isHaveCreash = self.CheckIsHaveCrash(actionMessage);

            if (!isHaveCreash)
            {
                //没有消除宝石，那么也没有攻击逻辑
                return;
            }

            Unit unit = self.GetCurrentAttackUnit();
            Unit beUnit = self.GetBeAttackUnit(unit);
            self.ProcessAngryAttackLogic(actionMessage, beUnit, unit);
            self.ProcessReBackNormalAttackLogic(actionMessage, beUnit, unit);
            //todo 处理反击逻辑
            //产看动作队列里面是否存在消除动作
            ActionMessage message = self.ProcessBuffRoundLogic(unit);
            if (message.ActionMessages.Count > 0)
            {
                actionMessage.ActionMessages.Add(message);
            }
        }

        public static HeroCard GetTurnAttackHero(this FightComponent self, Unit unit)
        {
            //todo 获得当前轮流攻击的英雄
            int currentIndex = unit.CurrentTurnAttackHeroSeatIndex;
            int index = 0;
            while (index < 10)
            {
                List<HeroCard> heroCards = unit.GetChilds<HeroCard>();
                foreach (var heroCard in heroCards)
                {
                    // HeroCard card = unit.GetChild<HeroCard>(id);
                    if (heroCard.InTroopIndex.Equals(currentIndex) && !heroCard.GetIsDead())
                    {
                        unit.CurrentTurnAttackHeroSeatIndex = currentIndex + 1;
                        return heroCard;
                    }
                }

                currentIndex++;
                if (currentIndex >= unit.GetChilds<HeroCard>().Count)
                {
                    currentIndex = 0;
                }

                index++;
            }

            return null;
        }

        public static void HideAttackTargetMark(this FightComponent self, ActionMessage actionMessage)
        {
            if (self.CurrentBeAttackHeroCard == null)
            {
                return;
            }

            actionMessage.ActionMessages.Add(new ActionMessage()
            {
                HideAttackMarkAction = new HideAttackMarkAction()
                {
                    HeroCardDataComponentInfo = self.CurrentBeAttackHeroCard.GetComponent<HeroCardDataComponent>().GetInfo()
                }
            });
            self.CurrentBeAttackHeroCard = null;
        }

        public static void ProcessAttackLogic(this FightComponent self, ActionMessage actionMessage, ActionMessage makeSureAttackMessage)
        {
            if (makeSureAttackMessage == null)
            {
                return;
            }

            Unit unit = self.GetCurrentAttackUnit();
            Unit beUnit = self.GetBeAttackUnit(unit);
            //
            self.ProcessAngryAttackLogic(actionMessage, unit, beUnit);
            self.ProcessNormalAttackLogic(actionMessage, unit, beUnit, makeSureAttackMessage);
            Log.Debug("处理攻击逻辑");
            // self.ProcessBuffLogic();
            ActionMessage message = self.ProcessBuffRoundLogic(beUnit);
            if (message.ActionMessages.Count > 0)
            {
                actionMessage.ActionMessages.Add(message);
            }
        }

        public static ActionMessage ProcessBuffRoundLogic(this FightComponent self, Unit unit)
        {
            List<HeroCard> heroCards = unit.GetChilds<HeroCard>();

            ActionMessage actionMessage = new ActionMessage()
            {
                PlayType = (int)ActionMessagePlayType.Async, ActionMessages = new List<ActionMessage>()
            };
            // ActionMessage action = new ActionMessage() { PlayType = (int)ActionMessagePlayType.Async, ActionMessages = new List<ActionMessage>() };
            foreach (var heroCard in heroCards)
            {
                // heroCard.ProcessBuffLogic();
                ActionMessage buffAction = heroCard.ProcessBuffRoundLogic();
                if (buffAction != null)
                {
                    actionMessage.ActionMessages.Add(buffAction);
                }

                // BuffComponent buffComponent = heroCard.GetComponent<BuffComponent>();
                // List<Buff> buffs = buffComponent.GetChilds<Buff>();
                //
                //
                // buffComponent.ProcessRoundLogic();
                // var message = new ActionMessage()
                // {
                //     UpdateHeroBuffInfo = new UpdateHeroBuffInfo()
                //     {
                //         HeroId = heroCard.Id,
                //         BuffInfos = buffComponent.GetBuffInfos(),
                //         HeroCardDataComponentInfo = heroCard.GetComponent<HeroCardDataComponent>().GetInfo()
                //     }
                // };
                // actionMessage.ActionMessages.Add(message);
            }

            foreach (var heroCard in heroCards)
            {
                BuffComponent buffComponent = heroCard.GetComponent<BuffComponent>();
                // List<Buff> buffs = buffComponent.GetChilds<Buff>();
                // buffComponent.ProcessRoundLogic();
                var message = new ActionMessage()
                {
                    UpdateHeroBuffInfo = new UpdateHeroBuffInfo()
                    {
                        HeroId = heroCard.Id,
                        BuffInfos = buffComponent.GetBuffInfos(),
                        HeroCardDataComponentInfo = heroCard.GetComponent<HeroCardDataComponent>().GetInfo()
                    }
                };
                actionMessage.ActionMessages.Add(message);
            }

            return actionMessage;
        }

        public static bool CheckHeroIsInvisible(this FightComponent self, HeroCard heroCard)
        {
            //检查当前英雄是否隐身
            BuffComponent buffComponent = heroCard.GetComponent<BuffComponent>();
            var isInvisible = buffComponent.GetIsInvisible();
            // Log.Warning($"check hero is invisible{isInvisible}");
            return isInvisible;
        }

        public static void ProcessAngryAttackLogic(this FightComponent self, ActionMessage actionMessage, Unit unit, Unit beUnit)
        {
            List<HeroCard> heroCards = unit.GetChilds<HeroCard>();
            heroCards.RemoveAll(a => { return !a.GetComponent<HeroCardDataComponent>().IsAngryFull() || a.GetIsDead(); });
            if (heroCards.Count == 0)
            {
                return;
            }

            heroCards.Sort((a, b) => { return a.InTroopIndex - b.InTroopIndex; });

            foreach (var heroCard in heroCards)
            {
                if (!self.GetIsCanAttack(heroCard))
                {
                    continue;
                }

                Skill skill = heroCard.GetComponent<HeroCardDataComponent>().MakeSureAngrySkill();
                List<HeroCard> beAttackHeroCards = self.GetBeAttackHeroCards(beUnit, unit, heroCard, skill);
                SkillConfig skillConfig = SkillConfigCategory.Instance.Get(skill.ConfigId);
                if (skillConfig.RangeType == (int)SkillRangeType.EnemySingle)
                {
                    BuffComponent buffComponent = heroCard.GetComponent<BuffComponent>();
                    //todo 集火功能生效 激活功能在炫目buff下无效
                    if (self.CurrentBeAttackHeroCard != null && !self.CurrentBeAttackHeroCard.GetIsDead() && !buffComponent.GetIsExistDazzling() &&
                        !self.CheckHeroIsInvisible(self.CurrentBeAttackHeroCard))
                    {
                        beAttackHeroCards.RemoveAt(0);
                        beAttackHeroCards.Add(self.CurrentBeAttackHeroCard);
                    }
                }

                //         //todo 发动攻击
                AttackAction attackAction = new AttackAction();
                List<HeroCardDataComponentInfo> beHeroCardDataComponentInfos = new List<HeroCardDataComponentInfo>();
                List<HeroBuffInfo> heroBufferInfos = new List<HeroBuffInfo>();
                ActionMessage recoveryMessage =
                        new ActionMessage() { PlayType = (int)ActionMessagePlayType.Async, ActionMessages = new List<ActionMessage>() };
                foreach (var beAttackHeroCard in beAttackHeroCards)
                {
                    if (beAttackHeroCard == null)
                    {
                        continue;
                    }

                    //
                    // self.ProcessBuffLogic(beAttackHeroCard, skill);
                    heroCard.ProcessAttachBuffLogic(beAttackHeroCard, skill);
                    heroCard.ProcessMainFightLogic(beAttackHeroCard, skill);
                    List<BuffInfo> buffInfos = beAttackHeroCard.GetComponent<BuffComponent>().GetBuffInfos();
                    heroBufferInfos.Add(new HeroBuffInfo() { HeroId = beAttackHeroCard.Id, BuffInfos = buffInfos });
                    beHeroCardDataComponentInfos.Add(beAttackHeroCard.GetComponent<HeroCardDataComponent>().GetInfo());
                    ActionMessage message = self.ProcessHeroRecoveryLogic(beAttackHeroCard);
                    if (message != null)
                    {
                        recoveryMessage.ActionMessages.Add(message);
                    }
                }

                //
                attackAction.AttackHeroCardDataComponentInfo = heroCard.GetComponent<HeroCardDataComponent>().GetInfo();
                attackAction.BeAttackHeroCardDataComponentInfos = beHeroCardDataComponentInfos;
                attackAction.HeroBuffInfos = heroBufferInfos;

                ActionMessage attackMessage = new ActionMessage()
                {
                    PlayType = (int)ActionMessagePlayType.Async, AttackAction = attackAction, ActionMessages = new List<ActionMessage>()
                };

                attackMessage.ActionMessages.Add(recoveryMessage);

                actionMessage.ActionMessages.Add(attackMessage);
            }
        }

        public static ActionMessage ProcessHeroRecoveryLogic(this FightComponent self, HeroCard beAttackHeroCard)
        {
            // HeroCardDataComponent heroCardDataComponent = beAttackHeroCard.GetComponent<Hero>()
            bool isDead = beAttackHeroCard.GetIsDead();
            if (isDead)
            {
                //查看一次英雄身上，是否具有复苏buff
                bool isRecovery = beAttackHeroCard.ProcessRecoveryLogic();
                if (isRecovery) //复活了
                {
                    ActionMessage actionMessage = new ActionMessage()
                    {
                        RecoveryAction = new RecoveryAction()
                        {
                            HeroCardDataComponentInfo = beAttackHeroCard.GetComponent<HeroCardDataComponent>().GetInfo()
                        }
                    };
                    return actionMessage;
                }
            }

            return null;
        }

        public static bool GetIsCanAttack(this FightComponent self, HeroCard heroCard)
        {
            bool isCanAttack = heroCard.GetComponent<BuffComponent>().GetIsCanAttack();
            return isCanAttack;
        }

        public static int GetFirstCrashCount(this FightComponent self, ActionMessage actionMessage)
        {
            Stack<ActionMessage> stack = new Stack<ActionMessage>();
            stack.Push(actionMessage);
            CombeAction combeAction = null;
            while (stack.Count != 0 && combeAction == null)
            {
                var action = stack.Pop();

                for (int i = action.ActionMessages.Count - 1; i >= 0; i--)
                {
                    var value = action.ActionMessages[i];
                    if (value.CombeAction != null)
                    {
                        combeAction = value.CombeAction;
                        break;
                    }

                    if (value.ActionMessages.Count != 0)
                    {
                        stack.Push(value);
                    }
                }
            }

            return combeAction.CrashCount;
        }

        public static void ProcessNormalAttackLogic(this FightComponent self, ActionMessage actionMessage, Unit unit, Unit beUnit,
        ActionMessage makeSureAttackMessage)
        {
            Log.Debug("process normal attack logic");
            List<HeroCard> heroCards = new List<HeroCard>();
            Log.Debug($"ProcessNormalAttackLogic {makeSureAttackMessage}");

            foreach (var action in makeSureAttackMessage.ActionMessages)
            {
                if (action.MakeSureAttackHeroAction != null)
                {
                    heroCards.Add(unit.GetChild<HeroCard>(action.MakeSureAttackHeroAction.HeroCardDataComponentInfo.HeroId));
                }
            }

            Log.Debug($"确定攻击技能, {heroCards.Count}");
            if (heroCards.Count == 0)
            {
                return;
            }

            Log.Debug("确定攻击技能");
            //todo 找到了发动攻击的英雄，开始寻找被攻击的英雄
            //todo 从左往右排序
            heroCards.Sort((a, b) => { return a.InTroopIndex - b.InTroopIndex; });
            Log.Debug("排序");
            foreach (var heroCard in heroCards)
            {
                if (!self.GetIsCanAttack(heroCard))
                {
                    continue;
                }

                Skill skill = heroCard.GetComponent<HeroCardDataComponent>().MakeSureSkill(self.GetFirstCrashCount(actionMessage));
                SkillConfig skillConfig = SkillConfigCategory.Instance.Get(skill.ConfigId);
                // Log.Warning($"skillconfig id {skill.ConfigId}");
                List<HeroCard> beAttackHeroCards = self.GetBeAttackHeroCards(beUnit, unit, heroCard, skill);
                BuffComponent buffComponent = heroCard.GetComponent<BuffComponent>();

                if (self.CurrentBeAttackHeroCard != null && !self.CurrentBeAttackHeroCard.GetIsDead() && !buffComponent.GetIsExistDazzling() &&
                    !self.CheckHeroIsInvisible(self.CurrentBeAttackHeroCard))
                {
                    if (skillConfig.RangeType == (int)SkillRangeType.EnemySingle)
                    {
                        beAttackHeroCards[0] = self.CurrentBeAttackHeroCard;
                    }
                    //集火功能
                }

                AttackAction attackAction = new AttackAction();

                List<HeroCardDataComponentInfo> beHeroCardDataComponentInfos = new List<HeroCardDataComponentInfo>();
                List<HeroBuffInfo> heroBuffInfos = new List<HeroBuffInfo>();
                ActionMessage recoryMessage =
                        new ActionMessage() { PlayType = (int)ActionMessagePlayType.Async, ActionMessages = new List<ActionMessage>() };
                // ActionMessage updateBuffMessages =
                // new ActionMessage() { PlayType = (int)ActionMessagePlayType.Async, ActionMessages = new List<ActionMessage>() };
                foreach (var beAttackHeroCard in beAttackHeroCards)
                {
                    if (beAttackHeroCard == null)
                    {
                        continue;
                    }

                    // self.ProcessBuffLogic(beAttackHeroCard, skill);
                    // self.ProcessBuffLogic()
                    heroCard.ProcessAttachBuffLogic(beAttackHeroCard, skill);
                    heroCard.ProcessMainFightLogic(beAttackHeroCard, skill);
                    List<BuffInfo> buffInfos = beAttackHeroCard.GetComponent<BuffComponent>().GetBuffInfos();
                    heroBuffInfos.Add(new HeroBuffInfo() { HeroId = beAttackHeroCard.Id, BuffInfos = buffInfos });
                    // heroCard.AttackTarget(beAttackHeroCard, heroCard.GetComponent<HeroCardDataComponent>().DiamondAttackAddition, skill);
                    beHeroCardDataComponentInfos.Add(beAttackHeroCard.GetComponent<HeroCardDataComponent>().GetInfo());
                    // recoryMessage.ActionMessages.Add();

                    // ActionMessage updateBuffMessage = new ActionMessage()
                    // {
                    //     UpdateHeroBuffInfo = new UpdateHeroBuffInfo()
                    //     {
                    //         HeroId = beAttackHeroCard.Id,
                    //         HeroCardDataComponentInfo = beAttackHeroCard.GetComponent<HeroCardDataComponent>().GetInfo(),
                    //         BuffInfos = beAttackHeroCard.GetComponent<BuffComponent>().GetBuffInfos()
                    //     }
                    // };
                    // updateBuffMessages.ActionMessages.Add(updateBuffMessage);
                    ActionMessage message = self.ProcessHeroRecoveryLogic(beAttackHeroCard);
                    if (message != null)
                    {
                        recoryMessage.ActionMessages.Add(message);
                    }
                }

                attackAction.AttackHeroCardDataComponentInfo = heroCard.GetComponent<HeroCardDataComponent>().GetInfo();
                attackAction.BeAttackHeroCardDataComponentInfos = beHeroCardDataComponentInfos;
                attackAction.HeroBuffInfos = heroBuffInfos;
                var attackActionMessage = new ActionMessage()
                {
                    PlayType = (int)ActionMessagePlayType.Async, AttackAction = attackAction, ActionMessages = new List<ActionMessage>()
                };
                attackActionMessage.ActionMessages.Add(recoryMessage);
                // attackActionMessage.ActionMessages.Add(updateBuffMessages);
                actionMessage.ActionMessages.Add(attackActionMessage);
            }
        }

        public static Unit GetBeAttackUnit(this FightComponent self, Unit unit)
        {
            int whileCount = 0;
            int index = unit.SeatIndex;
            Unit targetUnit = null;
            //todo 防止陷入似循环
            while (targetUnit == null && whileCount < 10)
            {
                index++;
                if (index >= self.Units.Count)
                {
                    index = 0;
                }

                if (unit.Id != self.Units[index].Id)
                {
                    targetUnit = self.Units[index];
                }

                whileCount++;
            }

            return targetUnit;
        }

        /**
*查找一个单体攻击的英雄
*/
        public static HeroCard GetSingleAttackHeroCard(this FightComponent self, HeroCard heroCard, Unit unit)
        {
            //查看是否具有挑衅buff
            List<Buff> buffs = heroCard.GetComponent<BuffComponent>().GetChilds<Buff>();
            if (buffs != null)
            {
                Buff buff = buffs.Find(a =>
                {
                    BuffConfig buffConfig = BuffConfigCategory.Instance.Get(a.ConfigId);
                    if (buffConfig.Provocation == (int)ProvocationType.Provocation && a.RoundCount > 0)
                    {
                        return true;
                    }

                    return false;
                });
                if (buff != null)
                {
                    return buff.AttachHeroCard;
                }
            }

            var index = heroCard.InTroopIndex;
            int whileCount = 0;
            List<HeroCard> heroCards = unit.GetChilds<HeroCard>();
            while (whileCount < 20)
            {
                if (index >= 3)
                {
                    index = 0;
                }

                foreach (var card in heroCards)
                {
                    if (card.InTroopIndex.Equals(index) && !card.GetIsDead())
                    {
                        return card;
                    }
                }

                index++;
                whileCount++;
            }

            return null;
        }

        public static List<HeroCard> GetBeAttackUnitAllLiveHeroCards(this FightComponent self, Unit beUnit)
        {
            List<HeroCard> heroCards = beUnit.GetChilds<HeroCard>();
            heroCards.RemoveAll(a => a.GetIsDead()); //删掉死掉的英雄
            return heroCards;
        }

        public static List<HeroCard> GetAllLiveHeroCard(this FightComponent self)
        {
            List<HeroCard> heroCards = new List<HeroCard>();
            foreach (var unit in self.Units)
            {
                List<HeroCard> list = unit.GetChilds<HeroCard>();
                list.RemoveAll(a => a.GetIsDead());
                heroCards = heroCards.Concat(list).ToList();
            }

            return heroCards;
        }

        public static List<HeroCard> GetBeAttackHeroCards(this FightComponent self, Unit beUnit, Unit attactUnit, HeroCard heroCard, Skill skill)
        {
            //todo 找到需要攻击的玩家之后，开始寻找被攻击的牌
            //todo 找到与自己位置一致的牌
            List<HeroCard> heroCards = new List<HeroCard>();
            //根据技能的攻击范围类型，寻找被攻击的英雄
            SkillConfig skillConfig = SkillConfigCategory.Instance.Get(skill.ConfigId);
            switch (skillConfig.RangeType)
            {
                case (int)SkillRangeType.EnemySingle:
                    Log.Debug("EnemySingle");
                    heroCards.Add(self.GetSingleAttackHeroCard(heroCard, beUnit));
                    break;
                case (int)SkillRangeType.EnemyGroup:
                    Log.Debug("EnemyGroup");

                    heroCards = self.GetBeAttackUnitAllLiveHeroCards(beUnit);
                    break;
                case (int)SkillRangeType.FriendSingle:
                    Log.Debug("FriendSingle");

                    heroCards.Add(self.GetSingleAttackHeroCard(heroCard, attactUnit));
                    break;
                case (int)SkillRangeType.FriendGroup:
                    Log.Debug("FriendGroup");

                    heroCards = self.GetBeAttackUnitAllLiveHeroCards(attactUnit);
                    break;
                case (int)SkillRangeType.SingleSelf:
                    Log.Debug("attack self");
                    heroCards.Add(heroCard);
                    break;
                case (int)SkillRangeType.All:
                    Log.Debug("All");

                    heroCards = self.GetAllLiveHeroCard();
                    break;
            }

            return heroCards;
        }

        public static Unit GetCurrentAttackUnit(this FightComponent self)
        {
            Unit unit = self.Units.Find(a => a.SeatIndex.Equals(self.CurrentTurnIndex));
            return unit;
        }
    }
}