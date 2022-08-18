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
                HeroCardDataComponent heroCardDataComponent = heroCard.AddComponent<HeroCardDataComponent>();
                heroCardDataComponent.Angry = config.InitAngry;
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
                HeroCardDataComponent heroCardDataComponent = heroCard.AddComponent<HeroCardDataComponent>();
                heroCardDataComponent.Angry = heroConfig.InitAngry;
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

        public static void ProcessAddHeroCardAngryLogic(this FightComponent self, List<DiamondActionItem> diamondActionItems)
        {
            var count = 0;
            foreach (var diamondActionItem in diamondActionItems)
            {
                Unit unit = self.GetCurrentAttackUnit();
                //todo 处理增加卡牌怒气值的逻辑
                List<DiamondAction> diamondActions = diamondActionItem.DiamondActions;
                //取出与宝石的颜色相同的英雄
                List<HeroCard> heroCards = unit.GetChilds<HeroCard>();
                if (diamondActionItem.CrashType == (int)CrashType.Normal)
                {
                    count++;
                }

                foreach (var diamondAction in diamondActions)
                {
                    List<HeroCard> heros = heroCards.FindAll((a) =>
                    {
                        HeroConfig heroConfig = HeroConfigCategory.Instance.Get(a.ConfigId);
                        return heroConfig.HeroColor == diamondAction.DiamondInfo.DiamondType && !a.GetIsDead();
                    });
                    List<AddItemAction> addAngryActions = new List<AddItemAction>();
                    foreach (var heroCard in heros)
                    {
                        heroCard.GetComponent<HeroCardDataComponent>().Angry += self.LevelConfig.AngryCount;
                        AddItemAction addItemAction = new AddItemAction()
                        {
                            HeroCardInfo = heroCard.GetMessageInfo(),
                            HeroCardDataComponentInfo = heroCard.GetComponent<HeroCardDataComponent>().GetInfo()
                        };
                        addAngryActions.Add(addItemAction);
                    }

                    diamondAction.AddAngryActions = addAngryActions;
                }
            }
        }

        /**
         * 确定攻击英雄
         */
        public static void MakeSureAttackHeros(this FightComponent self, M2C_SyncDiamondAction m2CSyncDiamondAction)
        {
            DiamondActionItem diamondActionItem = null;
            foreach (var item in m2CSyncDiamondAction.DiamondActionItems)
            {
                if (item.DiamondActions.Count == 0)
                {
                    continue;
                }

                if (item.CrashType != (int)CrashType.Normal)
                {
                    continue;
                }

                diamondActionItem = item;
                Log.Debug($"找到了，第一次消除的action{diamondActionItem.DiamondActions.Count}");
                break;
            }

            if (diamondActionItem != null)
            {
                Unit unit = self.GetCurrentAttackUnit();
                //取出与宝石的颜色相同的英雄
                List<HeroCard> heroCards = unit.GetChilds<HeroCard>();
                heroCards = heroCards.FindAll(a =>
                {
                    HeroConfig heroConfig = HeroConfigCategory.Instance.Get(a.ConfigId);
                    Log.Debug($"herocolor {heroConfig.HeroColor}");
                    if (heroConfig.HeroColor == diamondActionItem.DiamondActions[0].DiamondInfo.DiamondType && !a.GetIsDead())
                    {
                        return true;
                    }

                    return false;
                });
                List<MakeSureAttackHeroAction> actions = new List<MakeSureAttackHeroAction>();
                foreach (var heroCard in heroCards)
                {
                    // Log.Warning("处理攻击逻辑");
                    // heroCard.GetComponent<HeroCardDataComponent>().MakeSureSkill(diamondActionItem.DiamondActions.Count);
                    MakeSureAttackHeroAction action = new MakeSureAttackHeroAction()
                    {
                        HeroCardDataComponentInfo = heroCard.GetComponent<HeroCardDataComponent>().GetInfo()
                    };
                    actions.Add(action);
                }

                diamondActionItem.MakeSureAttackHeroActions = actions;
            }
        }

        public static void ProcessComboResult(this FightComponent self, M2C_SyncDiamondAction message)

        {
            var diamondActionItems = message.DiamondActionItems;
            Unit unit = self.GetCurrentAttackUnit();
            Log.Debug("处理攻击逻辑");
            //todo 找到需要发动攻击的英雄
            List<HeroCard> heroCards = new List<HeroCard>();
            foreach (var diamondActionItem in diamondActionItems)
            {
                List<MakeSureAttackHeroAction> makeSureAttackHeroActions = diamondActionItem.MakeSureAttackHeroActions;
                foreach (var makeSureAttackHeroAction in makeSureAttackHeroActions)
                {
                    HeroCardDataComponentInfo info = makeSureAttackHeroAction.HeroCardDataComponentInfo;
                    HeroCard heroCard = unit.GetChild<HeroCard>(info.HeroId);
                    heroCards.Add(heroCard);
                }
            }

            ComboActionItem comboActionItem = new ComboActionItem();
            var comboCount = 0;
            List<AddItemAction> addItemActions = new List<AddItemAction>();
            foreach (var diamondActionItem in diamondActionItems)
            {
                if (diamondActionItem.CrashType == (int)CrashType.Normal)
                {
                    if (comboCount != 0)
                    {
                        Log.Debug("普通消除，那么需要给相应的英雄增加");
                        foreach (var heroCard in heroCards)
                        {
                            AddItemAction action = new AddItemAction();
                            heroCard.GetComponent<HeroCardDataComponent>().DiamondAttackAddition += self.LevelConfig.AttackAddition;
                            action.HeroCardDataComponentInfo = heroCard.GetComponent<HeroCardDataComponent>().GetInfo();
                            action.CrashCommonInfo = new CrashCommonInfo() { CommonCount = comboCount };
                            addItemActions.Add(action);
                        }
                    }

                    comboCount++;
                }
            }

            comboActionItem.AddAttackActions = addItemActions;
            message.ComboActionItem = comboActionItem;
        }

        public static void ProcessAddRoundAngry(this FightComponent self, M2C_SyncDiamondAction m2CSyncDiamondAction)
        {
            if (!self.CheckIsHaveCrash(m2CSyncDiamondAction))
            {
                return;
            }

            var AddRoundAngryItem = new AddRoundAngryItem();
            foreach (var unit in self.Units)
            {
                List<HeroCard> cards = unit.GetChilds<HeroCard>();
                foreach (var heroCard in cards)
                {
                    if (!heroCard.GetIsDead())
                    {
                        HeroConfig config = HeroConfigCategory.Instance.Get(heroCard.ConfigId);
                        heroCard.GetComponent<HeroCardDataComponent>().Angry += config.RoundAddAngry;
                        AddRoundAngryItem.HeroCardDataComponentInfos.Add(heroCard.GetComponent<HeroCardDataComponent>().GetInfo());
                    }
                }

                m2CSyncDiamondAction.AddRoundAngryItem = AddRoundAngryItem;
            }
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
                    // HeroCard heroCard = unit.GetChild<HeroCard>(id);
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

        public static bool CheckIsHaveCrash(this FightComponent self, M2C_SyncDiamondAction m2CSyncDiamondAction)
        {
            foreach (var diamondActionItem in m2CSyncDiamondAction.DiamondActionItems)
            {
                var diamondActions = diamondActionItem.DiamondActions;
                foreach (var diamondAction in diamondActions)
                {
                    if (diamondAction.ActionType == (int)DiamondActionType.Destory)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static void ProcessReBackAttackLogic(this FightComponent self, M2C_SyncDiamondAction m2CSyncDiamondAction)
        {
            Log.Debug($"attack action items {m2CSyncDiamondAction.AttackActionItems[0].AttackActions.Count}");
            //todo 处理反击逻辑
            //产看动作队列里面是否存在消除动作
            bool isHaveCreash = self.CheckIsHaveCrash(m2CSyncDiamondAction);

            if (!isHaveCreash)
            {
                //没有消除宝石，那么也没有攻击逻辑
                return;
            }

            Unit attackUnit = self.GetBeAttackUnit(self.Units[self.CurrentTurnIndex]); //todo 首先找到发起攻击的玩家
            Unit beAttacUnit = self.GetBeAttackUnit(attackUnit);
            HeroCard attackHero = self.GetTurnAttackHero(attackUnit); //todo 找到发起攻击的英雄
            if (attackHero == null)
            {
                //todo 英雄都死光了，不能在发起反击了
                return;
            }

            HeroCard beAttackHero = self.GetBeAttackHeroCard(attackHero, beAttacUnit); //todo 找到被攻击的英雄
            if (beAttackHero == null)
            {
                Log.Debug("未找到被攻击英雄");
                //rodo 英雄都死光了，
                return;
            }
            else
            {
                Log.Debug($"找到被攻击英雄{attackHero.InTroopIndex},{beAttackHero.InTroopIndex}");
            }

            AttackActionItem attackActionItem = new AttackActionItem(); //todo 创建攻击actionitem
            Log.Debug("处理反攻的逻辑");
            attackHero.GetComponent<HeroCardDataComponent>().MakeSureSkill(3);

            attackHero.GetComponent<HeroCardDataComponent>().MakeSureAngrySkill();

            var attackAction = attackHero.AttackTarget(beAttackHero, 0);
            attackActionItem.AttackActions.Add(attackAction);
            m2CSyncDiamondAction.AttackActionItems.Add(attackActionItem);
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

        public static void ProcessAttackLogic(this FightComponent self, M2C_SyncDiamondAction m2CSyncDiamondAction)
        {
            //todo 1 先找到当前需要释放技能的玩家
            Unit unit = self.GetCurrentAttackUnit();
            Unit beUnit = self.GetBeAttackUnit(unit);

            self.ProcessAngryAttackLogic(m2CSyncDiamondAction, unit, beUnit);
            self.ProcessNormalAttackLogic(m2CSyncDiamondAction, unit, beUnit);
            Log.Debug("处理攻击逻辑");
        }

        public static void ProcessAngryAttackLogic(this FightComponent self, M2C_SyncDiamondAction m2CSyncDiamondAction, Unit unit, Unit beUnit)
        {
            //处理怒气值攻击的逻辑
            AttackActionItem attackActionItem = new AttackActionItem();
            List<HeroCard> heroCards = unit.GetChilds<HeroCard>();
            heroCards.RemoveAll(a => !a.GetComponent<HeroCardDataComponent>().IsAngryFull());
            heroCards.Sort((a, b) => { return a.InTroopIndex - b.InTroopIndex; });
            foreach (var heroCard in heroCards)
            {
                HeroCard beHeroCard = self.GetBeAttackHeroCard(heroCard, beUnit);
                if (self.CurrentBeAttackHeroCard != null && !self.CurrentBeAttackHeroCard.GetIsDead())
                {
                    //集火功能
                    beHeroCard = self.CurrentBeAttackHeroCard;
                }

                if (beHeroCard != null)
                {
                    var attackAction = heroCard.AngryAttack(beHeroCard);
                    attackActionItem.AttackActions.Add(attackAction);
                }
            }

            m2CSyncDiamondAction.AttackActionItems.Add(attackActionItem);
        }

        public static int GetFirstCrashCount(this FightComponent self, M2C_SyncDiamondAction m2CSyncDiamondAction)
        {
            foreach (var item in m2CSyncDiamondAction.DiamondActionItems)
            {
                if (item.DiamondActions.Count == 0)
                {
                    continue;
                }

                if (item.CrashType != (int)CrashType.Normal)
                {
                    continue;
                }

                return item.DiamondActions.Count;
            }

            return 0;
        }

        public static void ProcessNormalAttackLogic(this FightComponent self, M2C_SyncDiamondAction m2CSyncDiamondAction, Unit unit, Unit beUnit)
        {
            AttackActionItem attackActionItem = new AttackActionItem();
            //处理正常攻击的逻辑
            //todo 找到需要发动攻击的英雄
            List<HeroCard> heroCards = new List<HeroCard>();
            foreach (var diamondActionItem in m2CSyncDiamondAction.DiamondActionItems)
            {
                List<MakeSureAttackHeroAction> makeSureAttackHeroActions = diamondActionItem.MakeSureAttackHeroActions;
                foreach (var makeSureAttackHeroAction in makeSureAttackHeroActions)
                {
                    HeroCardDataComponentInfo info = makeSureAttackHeroAction.HeroCardDataComponentInfo;
                    HeroCard heroCard = unit.GetChild<HeroCard>(info.HeroId);
                    heroCards.Add(heroCard);
                }
            }

            Log.Debug("确定攻击技能");
            //todo 找到了发动攻击的英雄，开始寻找被攻击的英雄
            //todo 从左往右排序
            heroCards.Sort((a, b) => { return a.InTroopIndex - b.InTroopIndex; });
            Log.Debug("排序");
            foreach (var heroCard in heroCards)
            {
                // AttackAction attackAction = new AttackAction();
                HeroCard beAttackHeroCard = self.GetBeAttackHeroCard(heroCard, beUnit);
                if (self.CurrentBeAttackHeroCard != null && !self.CurrentBeAttackHeroCard.GetIsDead())
                {
                    //集火功能
                    beAttackHeroCard = self.CurrentBeAttackHeroCard;
                }
                heroCard.GetComponent<HeroCardDataComponent>().MakeSureSkill(self.GetFirstCrashCount(m2CSyncDiamondAction));

                if (beAttackHeroCard != null)
                {
                    var attackAction = heroCard.AttackTarget(beAttackHeroCard, heroCard.GetComponent<HeroCardDataComponent>().DiamondAttackAddition);
                    attackActionItem.AttackActions.Add(attackAction);
                }
            }

            Log.Debug($"找到被攻击对象，并且进行攻击 {attackActionItem.AttackActions.Count}");
            m2CSyncDiamondAction.AttackActionItems.Add(attackActionItem);
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
            // var targetUnit = self.Units.Find(a => a.SeatIndex != self.CurrentTurnIndex);

            return targetUnit;
        }

        public static HeroCard GetBeAttackHeroCard(this FightComponent self, HeroCard heroCard, Unit unit)
        {
            //todo 找到需要攻击的玩家之后，开始寻找被攻击的牌
            //todo 找到与自己位置一致的牌
            var index = heroCard.InTroopIndex;
            int whileCount = 0;
            while (whileCount < 10)
            {
                if (index >= unit.GetChilds<HeroCard>().Count)
                {
                    index = 0;
                }

                List<HeroCard> heroCards = unit.GetChilds<HeroCard>();
                foreach (var card in heroCards)
                {
                    // var card = unit.GetChild<HeroCard>(id);
                    if (card.InTroopIndex.Equals(index) && !card.GetIsDead())
                    {
                        return card;
                    }
                }

                index++;
                whileCount++;
            }

            Log.Debug($"not found beattack herocard {index}");
            return null;
        }

        public static Unit GetCurrentAttackUnit(this FightComponent self)
        {
            // Unit attackUnit = null;
            // foreach (var unit in self.Units)
            // {
            //     if (unit.SeatIndex == self.CurrentTurnIndex)
            //     {
            //         attackUnit = unit;
            //         break;
            //     }
            // }

            Unit unit = self.Units.Find(a => a.SeatIndex.Equals(self.CurrentTurnIndex));
            return unit;
        }

        public static void PlayerRequestExitGame(this FightComponent self, long AccoundId)
        {
            //玩家发来了，强制退出房间的消息
        }
    }
}