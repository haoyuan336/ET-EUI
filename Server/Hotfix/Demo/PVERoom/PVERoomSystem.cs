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
            self.Units.Add(unit);
            //取出当前玩家 玩到的关卡数
            Account account = await self.GetCurrentAccountInfo(AccountId, unit);
            int levelNum = account.PVELevelNumber == 0? 1 : account.PVELevelNumber;
            self.LevelConfig = LevelConfigCategory.Instance.Get(levelNum);
            self.AsyncRoomInfo();
            self.InitHeroCards(account.CurrentTroopId);
            self.InitGameMap(levelNum);
        }

        public static void AsyncRoomInfo(this PVERoom self)
        {
            int SeatIndex = 0;
            foreach (var unit in self.Units)
            {
                MessageHelper.SendToClient(unit, new M2C_SyncRoomInfo() { MySeatIndex = SeatIndex, RoomId = self.Id, TurnIndex = 0 });
                SeatIndex++;
            }
        }

        public static async ETTask<Account> GetCurrentAccountInfo(this PVERoom self, long AccountId, Unit unit)
        {
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
                MessageHelper.SendToClient(entity, new M2C_InitMapData() { DiamondInfo = diamondInfos });
            }
        }

        //todo 初始化英雄卡
        public static async void InitHeroCards(this PVERoom self, long troopId)
        {
            //todo 首先创建敌人的英雄卡
            string heroIdsstr = self.LevelConfig.HeroId;
            String[] strList = heroIdsstr.Split(',').ToArray();
            self.enemyHeroCards = new List<HeroCard>();
            self.playerHeroCards = new List<HeroCard>();
            List<HeroCardInfo> heroCardInfo = new List<HeroCardInfo>();
            int index = 0;
            foreach (var str in strList)
            {
                int heroId = int.Parse(str);
                Log.Debug($"add enemy hero  {heroId}");
                // HeroCard heroCard = self.AddChildWithId<HeroCard>(heroId);
                HeroCard heroCard = new HeroCard();
                HeroConfig heroConfig = HeroConfigCategory.Instance.Get(heroId);
                long id = IdGenerater.Instance.GenerateId();
                heroCard.InitWithConfig(heroConfig, id);
                heroCard.InTroopIndex = index;
                index++;
                self.enemyHeroCards.Add(heroCard);
                heroCardInfo.Add(heroCard.GetMessageInfo());
                heroCard.CampIndex = 0;
            }

            //todo 取出玩家的troopid
            self.playerHeroCards = await DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Query<HeroCard>((a) => a.TroopId.Equals(troopId));

            foreach (var heroCard in self.playerHeroCards)
            {
                heroCard.CampIndex = 1;
                heroCardInfo.Add(heroCard.GetMessageInfo());
            }

            foreach (var unit in self.Units)
            {
                MessageHelper.SendToClient(unit, new M2C_CreateHeroCardInRoom() { HeroCardInfo = heroCardInfo });
            }

            await ETTask.CompletedTask;
        }

        public static void PlayerScrollScreen(this PVERoom self, C2M_PlayerScrollScreen message)
        {
            Log.Debug("process scroll screen message");
            M2C_SyncDiamondAction m2CSyncDiamondAction = self.GetComponent<DiamondComponent>().ScrollDiamond(message);
            foreach (var unit in self.Units)
            {
                MessageHelper.SendToClient(unit, m2CSyncDiamondAction);
            }
        }
    }
}