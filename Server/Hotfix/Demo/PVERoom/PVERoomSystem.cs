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
            FightComponent fightComponent = self.GetComponent<FightComponent>();
            fightComponent.LevelConfig = LevelConfigCategory.Instance.Get(levelNum);
            fightComponent.Units.Add(unit);
            fightComponent.Units.Add(self.AddAIUnity());
            fightComponent.SetUnitSeatIndex();
            self.AsyncRoomInfo();
            self.GetComponent<FightComponent>().InitAIUnitHeroCard();
            await fightComponent.InitPlayerHeroCards(unit);
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
        // public static async ETTask InitPlayerHeroCards(this PVERoom self, Unit unit)
        // {
        //
        //     FightComponent fightComponent = self.GetComponent<FightComponent>();
        //     await fightComponent.InitPlayerHeroCards(unit);
        //     
        //     //todo sync all player info
        //     await ETTask.CompletedTask;
        // }
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