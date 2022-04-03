using System;
using System.Collections.Generic;
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
            //取出当前玩家 玩到的关卡数
            int levelNum = await self.GetCurrentPlayerLevelNum(unit, AccountId);
            self.LevelConfig = LevelConfigCategory.Instance.Get(levelNum);
            self.Units.Add(unit);
            self.Units.Add(self.AddAIUnity());
            self.SetUnitSeatIndex();
            self.AsyncRoomInfo();
            self.InitHeroCards(unit.CurrentTroopId);
            self.InitGameMap(levelNum);
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
        public static async void InitHeroCards(this PVERoom self, long troopId)
        {
            for (var i = 0; i < self.Units.Count; i++)
            {
                Unit unit = self.Units[i];
                if (unit.IsAI)
                {
                    // //todo 首先创建敌人的英雄卡
                    unit.HeroCards = self.GetHeroIdListInLevelConfig(i);
                }
                else
                {
                    unit.HeroCards = await DBManagerComponent.Instance.GetZoneDB(self.DomainZone())
                            .Query<HeroCard>((a) => a.TroopId.Equals(troopId));
                    foreach (var heroCard in unit.HeroCards)
                    {
                        heroCard.CampIndex = i;
                    }
                }
            }

            //todo sync all player info

            List<HeroCardInfo> heroCardInfos = new List<HeroCardInfo>();
            foreach (var unit in self.Units)
            {
                foreach (var heroCard in unit.HeroCards)
                {
                    heroCardInfos.Add(heroCard.GetMessageInfo());
                }
            }

            foreach (var unit in self.Units)
            {
                if (unit.IsAI)
                {
                    continue;
                }

                MessageHelper.SendToClient(unit, new M2C_CreateHeroCardInRoom() { HeroCardInfo = heroCardInfos });
            }

            await ETTask.CompletedTask;
        }

        public static List<HeroCard> GetHeroIdListInLevelConfig(this PVERoom self, int CampIndex)
        {
            List<HeroCard> heroCards = new List<HeroCard>();
            string heroIdsstr = self.LevelConfig.HeroId;
            String[] strList = heroIdsstr.Split(',').ToArray();
            // List<HeroCardInfo> heroCardInfo = new List<HeroCardInfo>();
            int index = 0;
            foreach (var str in strList)
            {
                int heroId = int.Parse(str);
                HeroCard heroCard = new HeroCard();
                HeroConfig heroConfig = HeroConfigCategory.Instance.Get(heroId);
                long id = IdGenerater.Instance.GenerateId();
                heroCard.InitWithConfig(heroConfig, id);
                heroCard.InTroopIndex = index;
                index++;
                // self.enemyHeroCards.Add(heroCard);
                heroCards.Add(heroCard);
                heroCard.CampIndex = CampIndex;
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
                    Log.Debug($"end angry {diamondInfo.HeroCardEndAngry}");
                }
                if (self.CurrentAttackHeroCard == null)
                {
                    self.CurrentAttackHeroCard = heroCard;
                }
                if (self.CurrentAttackHeroCard != null)
                {
                    if (self.CurrentAttackHeroCard.HeroColor.Equals(diamondInfo.DiamondType))
                    {
                        Log.Debug($"current attack hero card = {self.CurrentAttackHeroCard.Id}");
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

            foreach (var unit in self.Units)
            {
                if (unit.IsAI)
                {
                    continue;
                }

                MessageHelper.SendToClient(unit, m2CSyncDiamondAction);
            }
        }

        public static void ProcessAttackLogic(this PVERoom self, M2C_SyncDiamondAction m2CSyncDiamondAction)
        {
            //todo 1 先找到当前需要释放技能的玩家
            Unit unit = self.Units[self.CurrentTurnIndex];
            Unit beAttackUnit = self.GetBeAttackUnit(unit);
            AttackActionItem attackActionItem = new AttackActionItem();
            foreach (var heroCard in unit.HeroCards)
            {
                if (heroCard.Attack > 0 || heroCard.CheckAngryIsFull())
                {
                    AttackAction attackAction = new AttackAction();
                    HeroCard beAttackHeroCard = self.GetBeAttackHeroCard(heroCard, beAttackUnit);
                    heroCard.CurrentSkillId =  heroCard.ProcessCurrentSkill();
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
            while (targetUnit == null && whileCount < 20)
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
            HeroCard targetHeroCard = null;
            int whileCount = 0;
            while (targetHeroCard == null && whileCount < 20)
            {
                if (index >= unit.HeroCards.Count)
                {
                    index = 0;
                }

                if (!unit.HeroCards[index].GetIsDead())
                {
                    targetHeroCard = unit.HeroCards[index];
                }

                whileCount++;
            }

            return targetHeroCard;
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