using System;
using System.Collections.Generic;
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
            self.AsyncRoomInfo();
            self.InitHeroCards(unit.CurrentTroopId);
            self.InitGameMap(levelNum);
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
            int SeatIndex = 0;
            foreach (var unit in self.Units)
            {
                if (unit.IsAI)
                {
                    //todo 如果是AI玩家，那么跳过发送消息
                    continue;
                }

                MessageHelper.SendToClient(unit, new M2C_SyncRoomInfo() { MySeatIndex = SeatIndex, RoomId = self.Id, TurnIndex = 0 });
                SeatIndex++;
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
            Unit unit = self.Units[self.CurrentTurnIndex];
            DiamondInfo diamondInfo = action.DiamondInfo;
            foreach (var heroCard in unit.HeroCards)
            {
                // heroCard.
                if (heroCard.HeroColor.Equals(diamondInfo.DiamondType))
                {
                    DiamondTypeConfig config = DiamondTypeConfigCategory.Instance.Get(diamondInfo.DiamondType);
                    heroCard.AddAttackValue(float.Parse(config.AddAttack));
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

            foreach (var unit in self.Units)
            {
                if (unit.IsAI)
                {
                    continue;
                }

                MessageHelper.SendToClient(unit, m2CSyncDiamondAction);
            }
        }
    }
}