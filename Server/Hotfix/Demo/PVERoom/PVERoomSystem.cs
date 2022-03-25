using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

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
            // Log.Debug("player game read");            
            // unit.GetComponent<LoginAccountRecordComponentSystem>()
            self.Units.Add(unit);
            //取出当前玩家 玩到的关卡数
            List<Account> accounts = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Query<Account>((a) => a.Id.Equals(AccountId));
            if (accounts.Count > 0)
            {
                int levelNum = accounts[0].PVELevelNumber;
                long troopId = accounts[0].CurrentTroopId;
                Log.Debug($"level num {levelNum}");
                levelNum = levelNum == 0? 1 : levelNum;
                self.LevelConfig = LevelConfigCategory.Instance.Get(levelNum);
                List<DiamondInfo> diamondInfos = self.GetComponent<DiamondComponent>().InitDiamonds(self.LevelConfig);

                foreach (var entity in self.Units)
                {
                    MessageHelper.SendToClient(entity, new M2C_InitMapData() { DiamondInfo = diamondInfos });
                }

                self.InitHeroCards(troopId);
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
            foreach (var str in strList)
            {
                int heroId = int.Parse(str);
                // HeroCard heroCard = self.AddChildWithId<HeroCard>(heroId);
                HeroCard heroCard = new HeroCard();
                HeroConfig heroConfig = HeroConfigCategory.Instance.Get(heroId);
                heroCard.InitWithConfig(heroConfig);
                self.enemyHeroCards.Add(heroCard);
                heroCardInfo.Add(heroCard.GetMessageInfo());
            }

            //todo 取出玩家的troopid
            self.playerHeroCards = await DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Query<HeroCard>((a) => a.TroopId.Equals(troopId));

            foreach (var heroCard in self.playerHeroCards)
            {
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
            int LieIndex = message.StartX;
            int HangIndex = message.StartY;
            Log.Debug("process scroll screen message");
            // Diamond diamond = self.GetDiamond(LieIndex, HangIndex);
            // Diamond nextDiamond = self.GetDiamondWithDir(diamond, message.DirType);
            // if (diamond != null && nextDiamond != null)
            // {
            //     M2C_SyncDiamondAction m2CSyncDiamondAction = new M2C_SyncDiamondAction();
            //     m2CSyncDiamondAction.DiamondActionItems.Add(self.SwapDiamondPos(diamond, nextDiamond));
            //     bool isCrash = true;
            //     bool isCrashSuccess = false;
            //     bool isMoveDown = false;
            //     bool isFirstAddSpecial = true;
            //     // bool isSpecial = false;
            //     Queue<Diamond> specialDiamonds = new Queue<Diamond>();
            //     while (isCrash || isMoveDown || specialDiamonds.Count > 0)
            //     {
            //         isCrash = self.CheckCrash(m2CSyncDiamondAction.DiamondActionItems, diamond, nextDiamond, specialDiamonds, isFirstAddSpecial);
            //         if (isFirstAddSpecial)
            //         {
            //             isFirstAddSpecial = false;
            //         }
            //
            //         isMoveDown = self.MoveDownAllDiamond(m2CSyncDiamondAction.DiamondActionItems);
            //         if (isCrashSuccess == false && isCrash)
            //         {
            //             isCrashSuccess = true;
            //         }
            //
            //         if (specialDiamonds.Count > 0)
            //         {
            //             Diamond specialDiamond = specialDiamonds.Dequeue();
            //             self.AutoCastSpecialDiamond(m2CSyncDiamondAction.DiamondActionItems, specialDiamond);
            //         }
            //     }
            //
            //     if (isCrashSuccess)
            //     {
            //         self.ToggleTurnSeatIndex();
            //         self.syncCurrentTurnIndex();
            //     }
            //     else
            //     {
            //         //todo 交换失败。反向交换
            //         m2CSyncDiamondAction.DiamondActionItems.Add(self.SwapDiamondPos(diamond, nextDiamond));
            //     }
            //
            //     //todo 下发交换action 消息
            //     foreach (var unit in self.Units)
            //     {
            //         MessageHelper.SendToClient(unit, m2CSyncDiamondAction);
            //     }
            // }
            // else
            // {
            //     //todo 非法操作
            // }
        }
    }
}