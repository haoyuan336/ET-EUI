using System.Collections.Generic;

namespace ET
{
    public class RoomAwakeSystem: AwakeSystem<PVPRoom>
    {
        public override void Awake(PVPRoom self)
        {
            self.AddComponent<DiamondComponent>();
            self.AddComponent<FightComponent>();
            self.GetComponent<FightComponent>().LevelConfig = LevelConfigCategory.Instance.Get(ConstValue.PVPLevelConfigId);
        }
    }

    public static class PvPRoomSystem
    {
        public static void PlayerGameReady(this PVPRoom self, Unit unit)
        {
            FightComponent fightComponent = self.GetComponent<FightComponent>();
            if (!fightComponent.Units.Contains(unit))
            {
                fightComponent.Units.Add(unit);
            }

            if (fightComponent.Units.Count == ConstValue.RoomPlayerCount)
            {
                self.GameStart();
            }
        }

        public static async void GameStart(this PVPRoom self)
        {
            FightComponent fightComponent = self.GetComponent<FightComponent>();
            fightComponent.SetUnitSeatIndex();
            self.AsyncRoomInfo();
            await self.InitPlayerHeroCards();
            self.SyncCreateHeroCardMessage();
            self.InitGameMap(ConstValue.PVPLevelConfigId);
        }

        public static void InitGameMap(this PVPRoom self, int levelNum)
        {
            List<DiamondInfo> diamondInfos = self.GetComponent<DiamondComponent>().InitDiamonds(levelNum);
            foreach (var entity in self.GetComponent<FightComponent>().Units)
            {
                MessageHelper.SendToClient(entity, new M2C_InitMapData() { DiamondInfo = diamondInfos });
            }
        }

        public static void SyncCreateHeroCardMessage(this PVPRoom self)
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
                MessageHelper.SendToClient(unit,
                    new M2C_CreateHeroCardInRoom() { HeroCardInfos = heroCardInfos, HeroCardDataComponentInfos = heroCardDataComponentInfos });
            }
        }

        public static void AsyncRoomInfo(this PVPRoom self)
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

        //todo 初始化英雄卡
        public static async ETTask InitPlayerHeroCards(this PVPRoom self)
        {
            FightComponent fightComponent = self.GetComponent<FightComponent>();
            foreach (var unit in fightComponent.Units)
            {
                long troopId = unit.CurrentTroopId;
                //取出英雄数据
                List<HeroCard> heroCards =
                        await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                                .Query<HeroCard>(a => a.TroopId.Equals(troopId) && a.State == (int)HeroCardState.Active);
                foreach (var heroCard in heroCards)
                {
                    // heroCard.OwnerId
                    unit.AddChild(heroCard);
                    // List<Skill> skills = await DBManagerComponent.Instance.GetZoneDB(self.DomainZone())
                    //         .Query<Skill>(a => a.OwnerId.Equals(heroCard.Id));
                    // foreach (var skill in skills)
                    // {
                    //     heroCard.AddChild(skill);
                    // }

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
                    // Log.Debug("创建玩家的英雄实力");
                    HeroCardDataComponent heroCardDataComponent = heroCard.AddComponent<HeroCardDataComponent>();
                    heroCardDataComponent.Angry = heroConfig.InitAngry;
                }
            }

            await ETTask.CompletedTask;
        }

        public static async void PlayerScrollScreen(this PVPRoom self, C2M_PlayerScrollScreen message, Unit playerUnit)
        {
            FightComponent fightComponent = self.GetComponent<FightComponent>();
            //检查当前轮流的玩家序号是否匹配
            if (playerUnit.SeatIndex != fightComponent.CurrentTurnIndex)
            {
                return;
            }

            // self.GetComponent<FightComponent>().PlayerScrollScreen(message);
            self.GetComponent<FightComponent>().DiamondActionItems = new List<DiamondActionItem>();
            M2C_SyncDiamondAction m2CSyncDiamondAction = new M2C_SyncDiamondAction();
            self.GetComponent<DiamondComponent>().ScrollDiamond(message, m2CSyncDiamondAction);
            ActionMessage makeSureMessage = fightComponent.MakeSureAttackHeros(m2CSyncDiamondAction.ActionMessage);
            fightComponent.ProcessAddHeroCardAngryLogic(m2CSyncDiamondAction.DiamondActionItems);
            fightComponent.ProcessComboResult(m2CSyncDiamondAction.ActionMessage, makeSureMessage);
            fightComponent.ProcessAttackLogic(m2CSyncDiamondAction.ActionMessage); //处理攻击逻辑
            fightComponent.ProcessAddRoundAngry(m2CSyncDiamondAction);
            fightComponent.CurrentBeAttackHeroCard = null;
            // fightComponent.ProcessReBackAttackLogic(m2CSyncDiamondAction);
            // fightComponent.ProcessAddRoundAngry(m2CSyncDiamondAction);
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

            if (fightComponent.CheckIsHaveCrash(m2CSyncDiamondAction))
            {
                fightComponent.CurrentTurnIndex++;
                if (fightComponent.CurrentTurnIndex == fightComponent.Units.Count)
                    fightComponent.CurrentTurnIndex = 0;
            }
        }
    }
}