using System.Collections.Generic;
using System.Linq;
namespace ET
{
    public class PVERoomAwakeSystem: AwakeSystem<PVERoom>
    {
        public override void Awake(PVERoom self)
        {
            Log.Debug("pve room awake");
            self.AddComponent<DiamondComponent>();
            self.AddComponent<FightComponent>();
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
            self.GetComponent<FightComponent>().LevelConfig = LevelConfigCategory.Instance.Get(levelNum);
            // self.LevelConfig = LevelConfigCategory.Instance.Get(levelNum);
            self.GetComponent<FightComponent>().Units.Add(unit);
            self.GetComponent<FightComponent>().Units.Add(self.AddAIUnity());
            self.GetComponent<FightComponent>().SetUnitSeatIndex();
            self.AsyncRoomInfo();
            self.InitAIUnitHeroCard();
            await self.InitPlayerHeroCards(unit.CurrentTroopId);
            self.SyncCreateHeroCardMessage();
            self.InitGameMap(levelNum);
        }
        public static void SyncCreateHeroCardMessage(this PVERoom self)
        {
            List<HeroCardInfo> heroCardInfos = new List<HeroCardInfo>();
            List<HeroCardDataComponentInfo> heroCardDataComponentInfos = new List<HeroCardDataComponentInfo>();
            foreach (var unit in self.GetComponent<FightComponent>().Units)
            {
                List<HeroCard> heroCards = unit.GetChilds<HeroCard>();
                foreach (var heroCard in heroCards)
                {
                    heroCardInfos.Add(heroCard.GetMessageInfo());
                    heroCardDataComponentInfos.Add(heroCard.GetComponent<HeroCardDataComponent>().GetInfo());
                }
            }
            foreach (var unit in self.GetComponent<FightComponent>().Units)
            {
                if (unit.IsAI)
                {
                    continue;
                }
                MessageHelper.SendToClient(unit,
                    new M2C_CreateHeroCardInRoom() { HeroCardInfos = heroCardInfos, HeroCardDataComponentInfos = heroCardDataComponentInfos });
            }
        }

        public static void PlayChooseAttackHero(this PVERoom self, long heroId)
        {
            Log.Debug($"寻找英雄 {heroId}");
            //玩家选择了一个可以攻击的英雄，根据id找到相应的herocard
            foreach (var unit in self.GetComponent<FightComponent>().Units)
            {
                HeroCard heroCard = unit.GetChild<HeroCard>(heroId);
                if (heroCard != null)
                {
                    self.GetComponent<FightComponent>().CurrentBeAttackHeroCard = heroCard;
                }
            }

            foreach (var unit in self.GetComponent<FightComponent>().Units)
            {
                if (unit.IsAI)
                {
                    continue;
                }

                MessageHelper.SendToClient(unit, new M2C_PlayerChooseAttackHero() { HeroId = heroId });
            }
        }

        //
        public static void InitAIUnitHeroCard(this PVERoom self)
        {
            for (var i = 0; i < self.GetComponent<FightComponent>().Units.Count; i++)
            {
                Unit unit = self.GetComponent<FightComponent>().Units[i];
                if (unit.IsAI)
                {
                    //todo 首先创建敌人的英雄卡
                    self.CreateHeroIdListInLevelConfig(unit);
                }
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
            foreach (var unit in self.GetComponent<FightComponent>().Units)
            {
                if (unit.IsAI)
                {
                    //todo 如果是AI玩家，那么跳过发送消息
                    continue;
                }

                MessageHelper.SendToClient(unit,
                    new M2C_SyncRoomInfo()
                    {
                        MySeatIndex = unit.SeatIndex,
                        RoomId = self.Id,
                        TurnIndex = 0,
                        SeatCount = 2,
                        LevelNum = self.GetComponent<FightComponent>().LevelConfig.Id
                    });
            }
        }
        public static async ETTask<Account> GetCurrentAccountInfo(this PVERoom self, Unit unit, long AccountId)
        {
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
            foreach (var entity in self.GetComponent<FightComponent>().Units)
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
            for (var i = 0; i < self.GetComponent<FightComponent>().Units.Count; i++)
            {
                Unit unit = self.GetComponent<FightComponent>().Units[i];
                if (unit.IsAI)
                {
                    continue;
                }
                List<HeroCard> heroCards = await DBManagerComponent.Instance.GetZoneDB(self.DomainZone())
                        .Query<HeroCard>((a) => a.TroopId.Equals(troopId) && a.State == (int)HeroCardState.Active);
                foreach (var heroCard in heroCards)
                {
                    unit.AddChild(heroCard);
                    List<Skill> skills = await DBManagerComponent.Instance.GetZoneDB(self.DomainZone())
                            .Query<Skill>(a => a.OwnerId.Equals(heroCard.Id));
                    foreach (var skill in skills)
                    {
                        heroCard.AddChild(skill);
                    }
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
            }
            await ETTaskHelper.WaitAll(tasks);
            //todo sync all player info
            await ETTask.CompletedTask;
        }
        public static void CreateHeroIdListInLevelConfig(this PVERoom self, Unit unit)
        {
            string heroIdsstr = self.GetComponent<FightComponent>().LevelConfig.HeroId;
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
                HeroCardDataComponent heroCardDataComponent = heroCard.AddComponent<HeroCardDataComponent>();
                heroCardDataComponent.Angry = config.InitAngry;
            }
        }
        public static async void PlayerScrollScreen(this PVERoom self, C2M_PlayerScrollScreen message)
        {
            // self.GetComponent<FightComponent>().PlayerScrollScreen(message);
            FightComponent fightComponent = self.GetComponent<FightComponent>();
            self.GetComponent<FightComponent>().DiamondActionItems = new List<DiamondActionItem>();
            M2C_SyncDiamondAction m2CSyncDiamondAction = self.GetComponent<DiamondComponent>().ScrollDiamond(message);
            fightComponent.MakeSureAttackHeros(m2CSyncDiamondAction);
            fightComponent.ProcessAddHeroCardAngryLogic(m2CSyncDiamondAction.DiamondActionItems);
            fightComponent.ProcessComboResult(m2CSyncDiamondAction);
            fightComponent.ProcessAttackLogic(m2CSyncDiamondAction); //处理攻击逻辑
            fightComponent.CurrentBeAttackHeroCard = null;
            fightComponent.ProcessReBackAttackLogic(m2CSyncDiamondAction);
            fightComponent.ProcessAddRoundAngry(m2CSyncDiamondAction);
            Unit loseUnit = fightComponent.CheckGameEndResult();
            //
            if (loseUnit != null)
            {
                m2CSyncDiamondAction.GameLoseResultAction = new GameLoseResultAction() { LoseAccountId = loseUnit.AccountId };
            
                foreach (var unit in fightComponent.Units)
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
                    //玩家当前关卡数量+1
                    if (!unit.IsAI && !loseUnit.AccountId.Equals(unit.AccountId))
                    {
                        Log.Debug("储存胜利信息");
                        List<Account> accounts = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                                .Query<Account>(a => a.Id.Equals(unit.AccountId) && a.State == (int)StateType.Active);
                        if (accounts.Count != 0)
                        {
                            var levelCount = LevelConfigCategory.Instance.GetAll().Count;
                            var level = accounts[0].PVELevelNumber;
                            level += 1;
                            if (level > levelCount)
                            {
                                level = levelCount;
                            }
            
                            accounts[0].PVELevelNumber = level;
                            //储存当前关卡数
                            await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(accounts[0]);
                            accounts[0].Dispose();
                        }
                    }
                }
            }
            foreach (var unit in fightComponent.Units)
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