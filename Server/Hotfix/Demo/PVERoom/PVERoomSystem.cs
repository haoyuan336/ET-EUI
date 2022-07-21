using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Linq;
using System.Xml;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualBasic;
using NLog.Fluent;
using NLog.LayoutRenderers;

namespace ET
{
    public class PVERoomAwakeSystem: AwakeSystem<PVERoom>
    {
        public override void Awake(PVERoom self)
        {
            Log.Debug("pve room awake");
            self.AddComponent<DiamondComponent>();
        }
    }

    public static class PVERoomSystem
    {
        // public static void 
        public static async void PlayerGameReady(this PVERoom self, Unit unit, long AccountId)
        {
            Log.Debug($"current choose troop id {unit.CurrentTroopId}");
            //todo 取出当前玩家 玩到的关卡数
            int levelNum = await self.GetCurrentPlayerLevelNum(unit, AccountId);
            self.LevelConfig = LevelConfigCategory.Instance.Get(levelNum);
            self.Units.Add(unit);
            self.Units.Add(self.AddAIUnity());
            self.SetUnitSeatIndex();
            self.AsyncRoomInfo();
            self.InitAIUnitHeroCard();
            self.InitPlayerHeroCards(unit.CurrentTroopId).Coroutine();
            self.InitGameMap(levelNum);
        }

        public static void PlayChooseAttackHero(this PVERoom self, long heroId)
        {
            Log.Debug($"寻找英雄 {heroId}");
            //玩家选择了一个可以攻击的英雄，根据id找到相应的herocard
            foreach (var unit in self.Units)
            {
                // List<HeroCard> heroCards = unit.GetChilds<HeroCard>();
                // foreach (var herocard in heroCards)
                // {
                // Log.Warning($"hero card {herocard.Id}");
                // }
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

        public static void InitAIUnitHeroCard(this PVERoom self)
        {
            for (var i = 0; i < self.Units.Count; i++)
            {
                Unit unit = self.Units[i];
                if (unit.IsAI)
                {
                    //todo 首先创建敌人的英雄卡
                    self.CreateHeroIdListInLevelConfig(unit);
                }
            }
        }

        public static void SetUnitSeatIndex(this PVERoom self)
        {
            int seatIndex = 0;
            foreach (var unit in self.Units)
            {
                unit.SeatIndex = seatIndex;
                seatIndex++;
            }
        }

        public static async ETTask<int> GetCurrentPlayerLevelNum(this PVERoom self, Unit unit, long AccountId)
        {
            Account account = await self.GetCurrentAccountInfo(unit, AccountId);
            int levelNum = account.PVELevelNumber == 0? 1 : account.PVELevelNumber;
            return levelNum;
        }

        public static Unit AddAIUnity(this PVERoom self)
        {
            Log.Debug("创建一个电脑玩家");
            Unit unit = self.DomainScene().GetComponent<UnitComponent>().AddChild<Unit, int>(1002);
            unit.AccountId = unit.Id;
            Log.Debug($"create unit is ai {unit.IsAI}");
            return unit;
        }

        public static void AsyncRoomInfo(this PVERoom self)
        {
            foreach (var unit in self.Units)
            {
                if (unit.IsAI)
                {
                    //todo 如果是AI玩家，那么跳过发送消息
                    continue;
                }

                MessageHelper.SendToClient(unit,
                    new M2C_SyncRoomInfo() { MySeatIndex = unit.SeatIndex, RoomId = self.Id, TurnIndex = 0, SeatCount = 2 });
            }
        }

        public static async ETTask<Account> GetCurrentAccountInfo(this PVERoom self, Unit unit, long AccountId)
        {
            // unit.GetComponent<>()
            Log.Debug($"unit account id ={AccountId}");
            List<Account> accounts = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Query<Account>((a) => a.Id.Equals(AccountId));
            if (accounts.Count > 0)
            {
                return accounts[0];
            }

            return null;
        }

        public static void InitGameMap(this PVERoom self, int levelNum)
        {
            List<DiamondInfo> diamondInfos = self.GetComponent<DiamondComponent>().InitDiamonds(levelNum);
            foreach (var entity in self.Units)
            {
                if (entity.IsAI)
                {
                    continue;
                }

                MessageHelper.SendToClient(entity, new M2C_InitMapData() { DiamondInfo = diamondInfos });
            }
        }

        //todo 初始化英雄卡
        public static async ETTask InitPlayerHeroCards(this PVERoom self, long troopId)
        {
            List<ETTask> tasks = new List<ETTask>();
            for (var i = 0; i < self.Units.Count; i++)
            {
                Unit unit = self.Units[i];
                if (unit.IsAI)
                {
                    continue;
                }

                List<HeroCard> heroCards = await DBManagerComponent.Instance.GetZoneDB(self.DomainZone())
                        .Query<HeroCard>((a) => a.TroopId.Equals(troopId) && a.State == (int) HeroCardState.Active);

                foreach (var heroCard in heroCards)
                {
                    unit.AddChild(heroCard);
                    List<Skill> skills = await DBManagerComponent.Instance.GetZoneDB(self.DomainZone())
                            .Query<Skill>(a => a.OwnerId.Equals(heroCard.Id));
                    // Log.Warning($"InitPlayerHeroCards skills {skills.Count}");
                    foreach (var skill in skills)
                    {
                        heroCard.AddChild(skill);
                    }

                    List<Weapon> weapons = await DBManagerComponent.Instance.GetZoneDB(self.DomainZone())
                            .Query<Weapon>(a => a.OnWeaponHeroId.Equals(heroCard.Id) && a.State == (int) StateType.Active);
                    foreach (var weapon in weapons)
                    {
                        heroCard.AddChild(weapon);
                        //取出来，装备的词条
                        List<WordBar> wordBars = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                                .Query<WordBar>(a => a.OwnerId.Equals(weapon.Id) && a.State == (int) StateType.Active);
                        foreach (var wordBar in wordBars)
                        {
                            weapon.AddChild(wordBar);
                        }
                    }

                    Log.Debug("创建玩家的英雄实力");
                    heroCard.AddComponent<HeroCardDataComponent>();
                }
            }

            await ETTaskHelper.WaitAll(tasks);
            //todo sync all player info

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

            await ETTask.CompletedTask;
        }

        public static void CreateHeroIdListInLevelConfig(this PVERoom self, Unit unit)
        {
            // List<long> heroCards = new List<long>();
            // List<HeroCard> heroCards = new List<HeroCard>();
            string heroIdsstr = self.LevelConfig.HeroId;
            string[] strList = heroIdsstr.Split(',').ToArray();
            // List<HeroCardInfo> heroCardInfo = new List<HeroCardInfo>();
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

                var skillStrs = config.SkillIdList.Split(',');
                foreach (var skillStr in skillStrs)
                {
                    SkillConfig skillConfig = SkillConfigCategory.Instance.Get(int.Parse(skillStr));
                    Skill skill = new Skill();
                    skill.Id = IdGenerater.Instance.GenerateId();
                    skill.ConfigId = skillConfig.Id;
                    skill.OwnerId = heroCard.Id;
                    heroCard.AddChild(skill);
                }

                heroCard.AddComponent<HeroCardDataComponent>();
                // heroCards.Add(heroCard.Id);
            }
        }

        public static List<HeroCard> GetAliveHeroCardWithColorType(this PVERoom self, Unit unit, int colorId)
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

        //增加英雄钻石伤害加成
        public static void AddHeroCardsAttackAddition(this PVERoom self, M2C_SyncDiamondAction m2CSyncDiamondAction)
        {
            //todo 通过宝石的消除，增加英雄卡牌的攻击力以及怒气值

            Unit unit = self.GetCurrentAttackUnit();
            List<HeroCard> attackHeroList = new List<HeroCard>();
            CrashCommonInfo crashCommonInfo = null;
            for (int i = 0; i < m2CSyncDiamondAction.DiamondActionItems.Count; i++)
            {
                DiamondActionItem item = m2CSyncDiamondAction.DiamondActionItems[i];
                if (item.DiamondActions.Count == 0)
                {
                    //如果没有宝石动作那么返回
                    continue;
                }

                Log.Debug($"action type {item.DiamondActions[0].ActionType}");
                if (item.DiamondActions[0].ActionType != (int) DiamondActionType.Destory)
                {
                    //todo 如果消除的宝石类型不对，那么也返回
                    continue;
                }

                if (crashCommonInfo == null)
                {
                    crashCommonInfo = new CrashCommonInfo()
                    {
                        CommonCount = 0,
                        FirstCrashCount = item.DiamondActions.Count,
                        FirstCrashColor = item.DiamondActions[0].DiamondInfo.DiamondType
                    };
                    self.ProcessMakeSureAttackHero(crashCommonInfo, item);
                }
                else
                {
                    crashCommonInfo.CommonCount++;
                }

                self.ProcessAddHeroCardAttackAdditionLogic(crashCommonInfo, item);
                self.ProcessAddHeroCardAngryLogic(item);
            }
        }

        public static void ProcessMakeSureAttackHero(this PVERoom self, CrashCommonInfo crashCommonInfo, DiamondActionItem item)
        {
            Unit unit = self.GetCurrentAttackUnit();

            List<HeroCard> heroCards = unit.GetChilds<HeroCard>();
            heroCards = heroCards.FindAll(a =>
            {
                HeroConfig heroConfig = HeroConfigCategory.Instance.Get(a.ConfigId);
                Log.Debug($"herocolor {heroConfig.HeroColor}");
                Log.Debug($"first crash color {crashCommonInfo.FirstCrashColor}");
                if (heroConfig.HeroColor == crashCommonInfo.FirstCrashColor)
                {
                    return true;
                }

                return false;
            });
            List<MakeSureAttackHeroAction> actions = new List<MakeSureAttackHeroAction>();
            foreach (var heroCard in heroCards)
            {
                heroCard.GetComponent<HeroCardDataComponent>().MakeSureSkill(crashCommonInfo.FirstCrashCount);
                MakeSureAttackHeroAction action = new MakeSureAttackHeroAction()
                {
                    HeroCardDataComponentInfo = heroCard.GetComponent<HeroCardDataComponent>().GetInfo()
                };
                actions.Add(action);
            }

            item.MakeSureAttackHeroActions = actions;
        }
        // public static void Get

        public static void ProcessAddHeroCardAngryLogic(this PVERoom self, DiamondActionItem item)
        {
            Unit unit = self.GetCurrentAttackUnit();
            //todo 处理增加卡牌怒气值的逻辑
            List<DiamondAction> diamondActions = item.DiamondActions;
            //取出与宝石的颜色相同的英雄
            List<HeroCard> heroCards = unit.GetChilds<HeroCard>();
            heroCards = heroCards.FindAll(a =>
            {
                HeroConfig heroConfig = HeroConfigCategory.Instance.Get(a.ConfigId);
                if (heroConfig.HeroColor == diamondActions[0].DiamondInfo.DiamondType)
                {
                    Log.Debug($"hero config hero color{heroConfig.HeroColor}");
                    Log.Debug($"diamondtyoe {diamondActions[0].DiamondInfo.DiamondType}");
                    return true;
                }

                return false;
            });
            Log.Debug($"add angry hero card count{heroCards.Count}");
            //找到了
            List<DiamondInfo> diamondInfos = new List<DiamondInfo>();
            foreach (var diamondaction in diamondActions)
            {
                diamondInfos.Add(diamondaction.DiamondInfo);

                List<AddItemAction> addAngryActions = new List<AddItemAction>();
                foreach (var heroCard in heroCards)
                {
                    heroCard.GetComponent<HeroCardDataComponent>().Angry += diamondActions.Count;
                    AddItemAction addItemAction = new AddItemAction()
                    {
                        HeroCardInfo = heroCard.GetMessageInfo(),
                        HeroCardDataComponentInfo = heroCard.GetComponent<HeroCardDataComponent>().GetInfo()
                    };
                    addAngryActions.Add(addItemAction);
                }

                diamondaction.AddAngryActions = addAngryActions;
            }

            // List<AddItemAction> addItemActions = new List<AddItemAction>();
            // foreach (var heroCard in heroCards)
            // {
            //     AddItemAction addItemAction = new AddItemAction();
            //     // addItemAction.DiamondInfos = diamondInfos;
            //     heroCard.GetComponent<HeroCardDataComponent>().Angry += diamondActions.Count;
            //     addItemAction.HeroCardInfo = heroCard.GetMessageInfo();
            //     addItemAction.HeroCardDataComponentInfo = heroCard.GetComponent<HeroCardDataComponent>().GetInfo();
            //     // item.AddAngryItemAction = addItemAction;
            //     addItemActions.Add(addItemAction);
            // }
            // item.AddAngryItemActions = addItemActions;
        }

        public static void ProcessAddHeroCardAttackAdditionLogic(this PVERoom self, CrashCommonInfo crashCommonInfo, DiamondActionItem item)
        {
            //todo 处理增加英雄攻击力加成的逻辑
            Unit unit = self.GetCurrentAttackUnit();
            //找到需要发动攻击的英雄列表
            List<HeroCard> attackHeroList = unit.GetChilds<HeroCard>();
            attackHeroList = attackHeroList.FindAll(a =>
            {
                HeroConfig heroConfig = HeroConfigCategory.Instance.Get(a.ConfigId);
                // Log.Debug("heroconfif hero color");
                if (heroConfig.HeroColor == crashCommonInfo.FirstCrashColor)
                {
                    return true;
                }

                return false;
            });
            Log.Debug($"攻击英雄列表{attackHeroList.Count}");
            if (crashCommonInfo.CommonCount != 0)
            {
                LevelConfig levelConfig = self.LevelConfig;
                string[] baseAddiotions = levelConfig.StartAttackAddition.Split(',');
                var commonType = crashCommonInfo.FirstCrashCount - 3;
                if (commonType < 0)
                {
                    commonType = 0;
                }

                if (commonType > 2)
                {
                    commonType = 2;
                }

                int baseAddition = int.Parse(baseAddiotions[commonType]);
                List<AddItemAction> addItemActions = new List<AddItemAction>();
                foreach (var heroCard in attackHeroList)
                {
                    heroCard.GetComponent<HeroCardDataComponent>().DiamondAttackAddition =
                            baseAddition + levelConfig.AttackAddition * crashCommonInfo.CommonCount;

                    AddItemAction attackAction = new AddItemAction();
                    List<DiamondInfo> infos = new List<DiamondInfo>();
                    foreach (var diamondAction in item.DiamondActions)
                    {
                        if (diamondAction.DiamondInfo.DiamondType == crashCommonInfo.FirstCrashColor)
                        {
                            infos.Add(diamondAction.DiamondInfo);
                        }
                    }

                    attackAction.DiamondInfos = infos;
                    attackAction.HeroCardInfo = heroCard.GetMessageInfo();
                    attackAction.HeroCardDataComponentInfo = heroCard.GetComponent<HeroCardDataComponent>().GetInfo();
                    attackAction.CrashCommonInfo = new CrashCommonInfo() { CommonCount = crashCommonInfo.CommonCount };
                    addItemActions.Add(attackAction);
                }

                Log.Debug($"add item actions {addItemActions.Count}");

                item.AddAttackItemActions = addItemActions;
            }
        }

        public static void PlayerScrollScreen(this PVERoom self, C2M_PlayerScrollScreen message)
        {
            Log.Debug("process scroll screen message");
            M2C_SyncDiamondAction m2CSyncDiamondAction = self.GetComponent<DiamondComponent>().ScrollDiamond(message);
            self.AddHeroCardsAttackAddition(m2CSyncDiamondAction); //增加英雄卡牌的攻击值
            self.ProcessAttackLogic(m2CSyncDiamondAction); //处理攻击逻辑
            self.CurrentBeAttackHeroCard = null;
            self.ProcessReBackAttackLogic(m2CSyncDiamondAction);
            // self.CurrentAttackHeroCard = null;

            Unit loseUnit = self.CheckGameEndResult();

            if (loseUnit != null)
            {
                m2CSyncDiamondAction.GameLoseResultAction = new GameLoseResultAction() { LoseAccountId = loseUnit.AccountId };


                foreach (var unit in self.Units)
                {
                    //todo -----------储存游戏结果---------------
                    var action = new GameAction()
                    {
                        Id = IdGenerater.Instance.GenerateId(),
                        OwnerId = unit.AccountId,
                        ConfigId = 10002,
                        Win = !loseUnit.AccountId.Equals(unit.AccountId)
                    };
                    DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(action).Coroutine();
                    action.Dispose();
                }
              
            }

            foreach (var unit in self.Units)
            {
                if (unit.IsAI)
                {
                    continue;
                }

                MessageHelper.SendToClient(unit, m2CSyncDiamondAction);

              
            }
            //储存游戏胜利action
        }

        public static Unit CheckGameEndResult(this PVERoom self)
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

        public static void ProcessReBackAttackLogic(this PVERoom self, M2C_SyncDiamondAction m2CSyncDiamondAction)
        {
            Log.Debug($"attack action items {m2CSyncDiamondAction.AttackActionItems[0].AttackActions.Count}");
            //todo 处理反击逻辑
            //产看动作队列里面是否存在消除动作
            bool isHaveCreash = false;
            foreach (var diamondActionItem in m2CSyncDiamondAction.DiamondActionItems)
            {
                var diamondActions = diamondActionItem.DiamondActions;
                foreach (var diamondAction in diamondActions)
                {
                    if (diamondAction.ActionType == (int) DiamondActionType.Destory)
                    {
                        isHaveCreash = true;
                        break;
                    }
                }
            }

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
            var attackAction = attackHero.AttackTarget(beAttackHero);
            attackActionItem.AttackActions.Add(attackAction);
            m2CSyncDiamondAction.AttackActionItems.Add(attackActionItem);
        }

        public static HeroCard GetTurnAttackHero(this PVERoom self, Unit unit)
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

        public static void ProcessAttackLogic(this PVERoom self, M2C_SyncDiamondAction m2CSyncDiamondAction)
        {
            //todo 1 先找到当前需要释放技能的玩家
            Unit unit = self.GetCurrentAttackUnit();
            Unit beUnit = self.GetBeAttackUnit(unit);

            Log.Debug("处理攻击逻辑");
            AttackActionItem attackActionItem = new AttackActionItem();
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

                // List<AddItemAction> angryActions = diamondActionItem.AddAngryItemActions;
                // foreach (var action in angryActions)
                // {
                //     HeroCard heroCard = unit.GetChild<HeroCard>(action.HeroCardInfo.HeroId);
                //     if (heroCard.GetComponent<HeroCardDataComponent>().IsAngryFull())
                //     {
                //         // attackHeroMap.Add(heroCard);
                //         if (!heroCards.Contains(heroCard))
                //         {
                //             heroCards.Add(heroCard);
                //         }
                //     }
                // }
            }

            List<HeroCard> unitHeroCards = unit.GetChilds<HeroCard>();
            foreach (var heroCard in unitHeroCards)
            {
                if (heroCard.GetComponent<HeroCardDataComponent>().IsAngryFull())
                {
                    if (!heroCards.Contains(heroCard))
                    {
                        heroCards.Add(heroCard);
                    }
                }
            }

            Log.Debug($"找到需要发动攻击的英雄{heroCards.Count}");

            foreach (var heroCard in heroCards)
            {
                heroCard.GetComponent<HeroCardDataComponent>().MakeSureAngrySkill();
            }

            Log.Debug("确定攻击技能");
            // heroCards = heroCards.FindAll(a => a.CurrentSkillId != 0);
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
                    beAttackHeroCard = self.CurrentBeAttackHeroCard;
                }

                if (beAttackHeroCard != null)
                {
                    var attackAction = heroCard.AttackTarget(beAttackHeroCard);
                    attackActionItem.AttackActions.Add(attackAction);
                }
            }

            Log.Debug($"找到被攻击对象，并且进行攻击 {attackActionItem.AttackActions.Count}");
            // attackActionItem.AttackActions.Add(attackAction);
            m2CSyncDiamondAction.AttackActionItems.Add(attackActionItem);
        }

        public static Unit GetBeAttackUnit(this PVERoom self, Unit unit)
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

        public static HeroCard GetBeAttackHeroCard(this PVERoom self, HeroCard heroCard, Unit unit)
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

        public static Unit GetCurrentAttackUnit(this PVERoom self)
        {
            Unit attackUnit = null;
            foreach (var unit in self.Units)
            {
                if (unit.SeatIndex == self.CurrentTurnIndex)
                {
                    attackUnit = unit;
                    break;
                }
            }

            return attackUnit;
        }

        public static void PlayerRequestExitGame(this PVERoom self, long AccoundId)
        {
            //玩家发来了，强制退出房间的消息
        }
    }
}