using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualBasic;

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

        public static async ETTask PlayerReadyTurn(this PVERoom self)
        {
            self.CurrentAttackHeroCard = null;
            Log.Debug("准备好回合了");
            //做一些 英雄卡牌初始化的操作
            foreach (var unit in self.Units)
            {
                foreach (var heroCard in unit.HeroCards)
                {
                    //todo 做一些初始化的操作
                    heroCard.InitTurnGame(); //todo 初始化回合
                }
            }

            Log.Debug("初始化回合游戏卡牌");
            List<HeroCardInfo> heroCardInfos = new List<HeroCardInfo>(); 
            foreach (var unit in self.Units)
            {
                foreach (var heroCard  in unit.HeroCards)
                {
                    heroCardInfos.Add(heroCard.GetMessageInfo());
                }
            }
            Log.Debug("组装数据");

            foreach (var unit in self.Units)
            {
                if (unit.IsAI)
                {
                    continue;
                }
                Log.Debug("发送数据");
                MessageHelper.SendToClient(unit,new M2C_SyncHeroCardTurnData(){HeroCardInfos = heroCardInfos});
            }

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
                    unit.HeroCards = self.GetHeroIdListInLevelConfig(i, unit);
                    foreach (var heroCard in unit.HeroCards)
                    {
                        heroCard.InitHeroSkillWithConfig();
                    }
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
                    HeroCard card = unit.AddChildWithId<HeroCard, int>(heroCard.Id, heroCard.ConfigId);
                    // card.SetMessageInfo(heroCard.GetMessageInfo());
                    // card.InitSkill();
                    tasks.Add(card.InitHeroWithDBData(heroCard));
                    unit.HeroCards.Add(card);
                }
            }

            await ETTaskHelper.WaitAll(tasks);
            //todo sync all player info

            List<HeroCardInfo> heroCardInfos = new List<HeroCardInfo>();
            List<SkillInfo> skillInfos = new List<SkillInfo>();
            foreach (var unit in self.Units)
            {
                foreach (var heroCard in unit.HeroCards)
                {
                    heroCardInfos.Add(heroCard.GetMessageInfo());
                    List<Skill> skills = heroCard.GetChilds<Skill>();
                    Log.Debug($"skills {skills.Count}");
                    foreach (var skill in skills)
                    {
                        skillInfos.Add(skill.GetMessageInfo());
                    }
                }
            }

            foreach (var unit in self.Units)
            {
                if (unit.IsAI)
                {
                    continue;
                }

                MessageHelper.SendToClient(unit, new M2C_CreateHeroCardInRoom() { HeroCardInfos = heroCardInfos, SkillInfos = skillInfos });
            }

            await ETTask.CompletedTask;
        }

        public static List<HeroCard> GetHeroIdListInLevelConfig(this PVERoom self, int CampIndex, Unit unit)
        {
            List<HeroCard> heroCards = new List<HeroCard>();
            string heroIdsstr = self.LevelConfig.HeroId;
            string[] strList = heroIdsstr.Split(',').ToArray();
            // List<HeroCardInfo> heroCardInfo = new List<HeroCardInfo>();
            int index = 0;
            foreach (var str in strList)
            {
                int configId = int.Parse(str);
                long id = IdGenerater.Instance.GenerateId();
                HeroCard heroCard = unit.AddChildWithId<HeroCard, int>(id, configId);
                heroCard.InTroopIndex = index;
                index++;
                // self.enemyHeroCards.Add(heroCard);
                heroCards.Add(heroCard);
                heroCard.CampIndex = unit.SeatIndex;
            }

            return heroCards;
        }

        public static void AddHeroCardAttack(this PVERoom self, DiamondAction action)
        {
            // Unit unit = self.Units[self.CurrentTurnIndex];
            Unit unit = self.GetCurrentAttackUnit();
            if (unit == null)
            {
                return;
            }

            DiamondInfo diamondInfo = action.DiamondInfo;
            foreach (var heroCard in unit.HeroCards)
            {
                DiamondTypeConfig config = DiamondTypeConfigCategory.Instance.Get(diamondInfo.DiamondType);
                if (heroCard.HeroColor.Equals(diamondInfo.DiamondType))
                {
                    var addAngryValue = heroCard.AddAngryValue(float.Parse(config.AddAngry));
                    diamondInfo.HeroCardId = heroCard.Id;
                    diamondInfo.HeroCardAddAngry = addAngryValue;
                    diamondInfo.HeroCardEndAngry = heroCard.Angry;
                }

                if (self.CurrentAttackHeroCard == null)
                {
                    self.CurrentAttackHeroCard = heroCard;
                }

                if (self.CurrentAttackHeroCard != null)
                {
                    if (self.CurrentAttackHeroCard.HeroColor.Equals(diamondInfo.DiamondType))
                    {
                        var addValue = self.CurrentAttackHeroCard.AddAttackValue(float.Parse(config.AddAttack));
                        diamondInfo.HeroCardAddAttack = addValue;
                        diamondInfo.HeroCardEndAttack = self.CurrentAttackHeroCard.Attack;
                    }
                }
            }
        }

        public static void PlayerScrollScreen(this PVERoom self, C2M_PlayerScrollScreen message)
        {
            Log.Debug("process scroll screen message");
            M2C_SyncDiamondAction m2CSyncDiamondAction = self.GetComponent<DiamondComponent>().ScrollDiamond(message);

            foreach (var diamondActionItem in m2CSyncDiamondAction.DiamondActionItems)
            {
                foreach (var action in diamondActionItem.DiamondActions)
                {
                    if (action.ActionType == (int) DiamondActionType.Destory)
                    {
                        // Log.Debug($"增加相对应颜色的攻击力 以及怒气值{action.ActionType}");

                        self.AddHeroCardAttack(action);
                    }
                }
            }

            self.ProcessAttackLogic(m2CSyncDiamondAction);
            self.ProcessReBackAttackLogic(m2CSyncDiamondAction);
            foreach (var unit in self.Units)
            {
                if (unit.IsAI)
                {
                    continue;
                }

                MessageHelper.SendToClient(unit, m2CSyncDiamondAction);
            }
        }

        public static void ProcessReBackAttackLogic(this PVERoom self, M2C_SyncDiamondAction m2CSyncDiamondAction)
        {
            //todo 处理反击逻辑
            Unit attackUnit = self.GetBeAttackUnit(self.Units[self.CurrentTurnIndex]); //todo 首先找到发起攻击的玩家
            Unit beAttacUnit = self.GetBeAttackUnit(attackUnit);
            HeroCard attackHero = self.GetTurnAttackHero(attackUnit); //todo 找到发起攻击的英雄
            HeroCard beAttackHero = self.GetBeAttackHeroCard(attackHero, beAttacUnit); //todo 找到被攻击的英雄
            if (beAttackHero == null)
            {
                Log.Debug("未找到被攻击英雄");
            }
            else
            {
                Log.Debug($"找到被攻击英雄{attackHero.InTroopIndex},{beAttackHero.InTroopIndex}");
            }

            AttackActionItem attackActionItem = new AttackActionItem(); //todo 创建攻击actionitem
            AttackAction attackAction = new AttackAction(); //todo 创建攻击action
            attackHero.CurrentSkillId = attackHero.ProcessCurrentSkill(); //todo 被攻击对象被攻击
            beAttackHero.BeAttack(attackHero); //todo 组装消息体
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
                foreach (var card in unit.HeroCards)
                {
                    if (card.InTroopIndex.Equals(currentIndex) && !card.GetIsDead())
                    {
                        unit.CurrentTurnAttackHeroSeatIndex = currentIndex + 1;
                        return card;
                    }
                }

                currentIndex++;
                if (currentIndex >= unit.HeroCards.Count)
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
            Unit unit = self.Units[self.CurrentTurnIndex];
            Unit beAttackUnit = self.GetBeAttackUnit(unit);
            AttackActionItem attackActionItem = new AttackActionItem();
            foreach (var heroCard in unit.HeroCards)
            {
                Log.Debug($"hero in troop index  {heroCard.InTroopIndex}");
                Log.Debug($"hero attack {heroCard.Attack}");
                if (heroCard.Attack > 0 || heroCard.CheckAngryIsFull())
                {
                    Log.Debug($"attack hero card {heroCard.InTroopIndex}");
                    AttackAction attackAction = new AttackAction();
                    HeroCard beAttackHeroCard = self.GetBeAttackHeroCard(heroCard, beAttackUnit);
                    heroCard.CurrentSkillId = heroCard.ProcessCurrentSkill();
                    Log.Debug($"current skill id {heroCard.CurrentSkillId}");
                    beAttackHeroCard.BeAttack(heroCard);
                    attackAction.AttackHeroCardInfo = heroCard.GetMessageInfo();
                    attackAction.BeAttackHeroCardInfo.Add(beAttackHeroCard.GetMessageInfo());
                    attackActionItem.AttackActions.Add(attackAction);
                }
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
                if (index >= unit.HeroCards.Count)
                {
                    index = 0;
                }

                foreach (var card in unit.HeroCards)
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
    }
}