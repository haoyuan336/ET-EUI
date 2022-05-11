using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualBasic;
using NLog.Fluent;

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

        public static async ETTask PlayerReadyTurn(this PVERoom self)
        {
            // self.CurrentAttackHeroCard = null;
            // Log.Debug("准备好回合了");
            //做一些 英雄卡牌初始化的操作
            // foreach (var unit in self.Units)
            // {
            //     foreach (var id in unit.HeroCardIDs)
            //     {
            //         HeroCard heroCard = unit.GetChild<HeroCard>(id);
            //         //todo 做一些初始化的操作
            //         heroCard.InitTurnGame(); //todo 初始化回合
            //     }
            // }

            // Log.Debug("初始化回合游戏卡牌");
            // List<HeroCardInfo> heroCardInfos = new List<HeroCardInfo>();
            // foreach (var unit in self.Units)
            // {
            //     foreach (var id in unit.HeroCardIDs)
            //     {
            //         HeroCard heroCard = unit.GetChild<HeroCard>(id);
            //         heroCardInfos.Add(heroCard.GetMessageInfo());
            //     }
            // }
            //
            // Log.Debug("组装数据");

            // foreach (var unit in self.Units)
            // {
            //     if (unit.IsAI)
            //     {
            //         continue;
            //     }
            //
            //     Log.Debug("发送数据");
            //     MessageHelper.SendToClient(unit, new M2C_SyncHeroCardTurnData() { HeroCardInfos = heroCardInfos });
            // }

            await ETTask.CompletedTask;
        }

        public static void InitAIUnitHeroCard(this PVERoom self)
        {
            for (var i = 0; i < self.Units.Count; i++)
            {
                Unit unit = self.Units[i];
                if (unit.IsAI)
                {
                    // //todo 首先创建敌人的英雄卡
                    // unit.HeroCardIDs = self.CreateHeroIdListInLevelConfig(i, unit);
                    // foreach (var heroCard in unit.HeroCards)
                    // {
                    //     heroCard.InitHeroSkillWithConfig();
                    // }
                    self.CreateHeroIdListInLevelConfig(i, unit);
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
                        .Query<HeroCard>((a) => a.TroopId.Equals(troopId));

                foreach (var heroCard in heroCards)
                {
                    List<Skill> skills = await DBManagerComponent.Instance.GetZoneDB(self.DomainZone())
                            .Query<Skill>(a => a.OwnerId.Equals(heroCard.Id));
                    // unit.AddChild(heroCard);
                    // foreach (var skill in skills)
                    // {
                    //     heroCard.AddChild(skill);
                    // }
                    unit.AddChildWithId<HeroCard, HeroCard, List<Skill>>(heroCard.Id, heroCard, skills);
                    // unit.HeroCardIDs.Add(heroCard.Id);
                }
            }

            await ETTaskHelper.WaitAll(tasks);
            //todo sync all player info

            List<HeroCardInfo> heroCardInfos = new List<HeroCardInfo>();
            foreach (var unit in self.Units)
            {
                List<HeroCard> heroCards = unit.GetChilds<HeroCard>();
                foreach (var heroCard in heroCards)
                {
                    // HeroCard heroCard = unit.GetChild<HeroCard>(id);
                    heroCardInfos.Add(heroCard.GetMessageInfo());
                }
            }

            foreach (var unit in self.Units)
            {
                if (unit.IsAI)
                {
                    continue;
                }

                MessageHelper.SendToClient(unit, new M2C_CreateHeroCardInRoom() { HeroCardInfos = heroCardInfos });
            }

            await ETTask.CompletedTask;
        }

        public static List<long> CreateHeroIdListInLevelConfig(this PVERoom self, int CampIndex, Unit unit)
        {
            List<long> heroCards = new List<long>();
            // List<HeroCard> heroCards = new List<HeroCard>();
            string heroIdsstr = self.LevelConfig.HeroId;
            string[] strList = heroIdsstr.Split(',').ToArray();
            // List<HeroCardInfo> heroCardInfo = new List<HeroCardInfo>();
            int index = 0;
            foreach (var str in strList)
            {
                int configId = int.Parse(str);
                EnemyHeroConfig enemyHeroConfig = EnemyHeroConfigCategory.Instance.Get(configId);
                long id = IdGenerater.Instance.GenerateId();
                HeroCard heroCard = unit.AddChildWithId<HeroCard, EnemyHeroConfig>(id, enemyHeroConfig);
                heroCard.InTroopIndex = index;
                index++;
                // self.enemyHeroCards.Add(heroCard);
                heroCards.Add(heroCard.Id);
                heroCard.CampIndex = unit.SeatIndex;
            }

            return heroCards;
        }

        public static List<HeroCard> GetAliveHeroCardWithColorType(this PVERoom self, Unit unit, int colorId)
        {
            var children = unit.GetChilds<HeroCard>();
            List<HeroCard> heroCards = new List<HeroCard>();
            foreach (var child in children)
            {
                if (child.HeroColor.Equals(colorId) && !child.GetIsDead())
                {
                    // return child;
                    heroCards.Add(child);
                }
            }

            return heroCards;
        }

        public static void AddHeroCardAttack(this PVERoom self, M2C_SyncDiamondAction m2CSyncDiamondAction)
        {
            //todo 通过宝石的消除，增加英雄卡牌的攻击力以及怒气值

            Unit unit = self.GetCurrentAttackUnit();
            List<HeroCard> attackHeroList = new List<HeroCard>();
            for (int i = 0; i < m2CSyncDiamondAction.DiamondActionItems.Count; i++)
            {
                DiamondActionItem item = m2CSyncDiamondAction.DiamondActionItems[i];
                if (item.DiamondActions.Count == 0)
                {
                    continue;
                }

                Log.Debug($"action type {item.DiamondActions[0].ActionType}");
                if (item.DiamondActions[0].ActionType != (int) DiamondActionType.Destory)
                {
                    continue;
                }

                var configId = item.DiamondActions[0].DiamondInfo.ConfigId;
                DiamondTypeConfig config = DiamondTypeConfigCategory.Instance.Get(configId);
                var diamondType = config.ColorId;
                List<HeroCard> heroCards = self.GetAliveHeroCardWithColorType(unit, diamondType);
                //todo 找到相同颜色的英雄
                if (heroCards.Count == 0)
                {
                    continue;
                }

                if (attackHeroList.Count == 0)
                {
                    var diamondCount = item.DiamondActions.Count;
                    // Log.Warning($"相同颜色的英雄列表 {heroCards[0].GetHeroName()},diamond type {diamondCount}");

                    //找到相同颜色的英雄
                    if (diamondCount >= 3)
                    {
                        // heroCard.MakeHeroCardSkill(diamondCount);
                        // firstAttackHero = heroCard;
                        foreach (var heroCard in heroCards)
                        {
                            heroCard.MakeHeroCardSkill(diamondCount);
                            attackHeroList.Add(heroCard);
                        }
                    }
                }
                // if (i == 0)
                // {

                // }

                foreach (var heroCard in heroCards)
                {
                    if (attackHeroList.Contains(heroCard))
                    {
                        //todo 如果与当前需要攻击的英雄类型相同，那么需要继续给他增加攻击力
                        AddItemAction attackAction = new AddItemAction();
                        List<DiamondInfo> infos = new List<DiamondInfo>();
                        foreach (var diamondAction in item.DiamondActions)
                        {
                            infos.Add(diamondAction.DiamondInfo);
                            heroCard.AddDiamondAngryValue(diamondAction.DiamondInfo);
                        }

                        attackAction.DiamondInfos = infos;
                        attackAction.HeroCardInfo = heroCard.GetMessageInfo();
                        // m2CSyncDiamondAction.AddAttackItemActions.Add(attackAction);
                        item.AddAttackItemAction = attackAction;
                    }

                    //todo 相同颜色的英雄，需要增加怒气值
                    AddItemAction action = new AddItemAction();
                    List<DiamondInfo> diamondInfos = new List<DiamondInfo>();
                    foreach (var diamondAction in item.DiamondActions)
                    {
                        diamondInfos.Add(diamondAction.DiamondInfo);
                        heroCard.AddDiamondAngryValue(diamondAction.DiamondInfo);
                    }

                    heroCard.MakeHeroCardAngrySkill();
                    action.DiamondInfos = diamondInfos;
                    action.HeroCardInfo = heroCard.GetMessageInfo();
                    // m2CSyncDiamondAction.AddAngryItemActions.Add(action);
                    item.AddAngryItemAction = action;
                }
            }
        }

        public static void PlayerScrollScreen(this PVERoom self, C2M_PlayerScrollScreen message)
        {
            Log.Debug("process scroll screen message");
            M2C_SyncDiamondAction m2CSyncDiamondAction = self.GetComponent<DiamondComponent>().ScrollDiamond(message);
            self.AddHeroCardAttack(m2CSyncDiamondAction); //增加英雄卡牌的攻击值
            Log.Debug("处理攻击逻辑");
            self.ProcessAttackLogic(m2CSyncDiamondAction); //处理攻击逻辑
            Log.Debug("处理反击逻辑");
            self.CurrentBeAttackHeroCard = null;
            self.ProcessReBackAttackLogic(m2CSyncDiamondAction);

            // self.CurrentAttackHeroCard = null;

            Unit loseUnit = self.CheckGameEndResult();

            if (loseUnit != null)
            {
                m2CSyncDiamondAction.GameLoseResultAction = new GameLoseResultAction() { LoseAccountId = loseUnit.AccountId };
            }

            foreach (var unit in self.Units)
            {
                if (unit.IsAI)
                {
                    continue;
                }

                MessageHelper.SendToClient(unit, m2CSyncDiamondAction);
            }
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
            AttackAction attackAction = new AttackAction(); //todo 创建攻击action
            // attackHero.CurrentSkillId = attackHero.ProcessCurrentSkill(); //todo 被攻击对象被攻击
            // attackHero.CastSkill();
            RandomHelper.RandomNumber(3, 5);
            attackHero.MakeHeroCardSkill(3);
            attackHero.MakeHeroCardAngrySkill();
            beAttackHero.BeAttack(attackHero); //todo 组装消息体
            attackHero.CastSkill();
            attackAction.AttackHeroCardInfo = attackHero.GetMessageInfo();
            attackAction.BeAttackHeroCardInfo.Add(beAttackHero.GetMessageInfo());
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

            // Unit unit = self.Units[self.CurrentTurnIndex];
            // Unit beAttackUnit = self.GetBeAttackUnit(unit);
            AttackActionItem attackActionItem = new AttackActionItem();
            //todo 找到需要发动攻击的英雄
            List<HeroCard> heroCards = unit.GetChilds<HeroCard>();
            heroCards = heroCards.FindAll(a =>
            {
                if (a.CurrentSkillId != 0)
                {
                    return true;
                }

                return false;
            });
            //todo 找到了发动攻击的英雄，开始寻找被攻击的英雄

            foreach (var heroCard in heroCards)
            {
                AttackAction attackAction = new AttackAction();
                HeroCard beAttackHeroCard = self.GetBeAttackHeroCard(heroCard, beUnit);
                if (self.CurrentBeAttackHeroCard != null && !self.CurrentBeAttackHeroCard.GetIsDead())
                {
                    beAttackHeroCard = self.CurrentBeAttackHeroCard;
                }

                heroCard.CastSkill();
                beAttackHeroCard.BeAttack(heroCard);
                attackAction.AttackHeroCardInfo = heroCard.GetMessageInfo();
                attackAction.BeAttackHeroCardInfo.Add(beAttackHeroCard.GetMessageInfo());
                attackActionItem.AttackActions.Add(attackAction);
                heroCard.InitSkill();
                //初始化
            }

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