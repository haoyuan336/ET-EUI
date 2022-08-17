using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

// using static ET.Account;

namespace ET
{
    public class HeroCardComponentAwakeSystem: AwakeSystem<HeroCardComponent>
    {
        public override void Awake(HeroCardComponent self)
        {
            // Log.Debug("Hero card component awake");
        }
    }

    public class HeroCardComponentUpdateSystem: UpdateSystem<HeroCardComponent>
    {
        public override void Update(HeroCardComponent self)
        {
            self.CurrentTime += 1;
            if (self.CurrentTime >= ConstValue.AutoSaveTime)
            {
                self.CurrentTime = 0;
#if SERVER
                self.SaveData();
#endif
            }
        }
    }

    public class HeroCardComponentBeforeDestroySystem: BeforeDestroySystem<HeroCardComponent>
    {
        public override void BeforeDestroy(HeroCardComponent self)
        {
            // Log.Warning("销毁钱调用");
#if SERVER
            self.SaveData();
#endif
        }
    }

    public class HeroCardComponentDestorySystem: DestroySystem<HeroCardComponent>
    {
        public override void Destroy(HeroCardComponent self)
        {
#if SERVER
#endif
        }
    }

    public static class HeroCardComponentSystem
    {
#if SERVER

        public static async ETTask<bool> SetHeroCardShowAsync(this HeroCardComponent self, long heroId)
        {
            List<HeroCard> heroCards = await self.GetAllHeroCardAsync();
            foreach (var heroCard in heroCards)
            {
                heroCard.IsShow = false;
            }
            var targetHeroCard = heroCards.Find(a => a.Id.Equals(heroId));
            if (targetHeroCard != null)
            {
                targetHeroCard.IsShow = true;
                return true;
            }
            return false;
        }

        public static async ETTask<HeroCard> GetCurrentShowHeroAsync(this HeroCardComponent self)
        {
            List<HeroCard> heroCards = await self.GetAllHeroCardAsync();
            HeroCard heroCard = heroCards.Find(a => a.IsShow);
            return heroCard;
        }

        public static async ETTask<HeroCard> GetHeroCardsWithIdAsync(this HeroCardComponent self, long heroId)
        {
            List<HeroCard> heroCards = await self.GetAllHeroCardAsync();
            HeroCard heroCard = heroCards.Find(a => a.Id.Equals(heroId));
            return heroCard;
        }

        public static async ETTask<List<HeroCard>> GetHeroCardsWithTroopIdAsync(this HeroCardComponent self, long troopId)
        {
            List<HeroCard> heroCards = await self.GetAllHeroCardAsync();

            List<HeroCard> newHeroCards = heroCards.FindAll(a => a.TroopId.Equals(troopId));
            return newHeroCards;
        }

        public static HeroCard UpdateHeroStar(this HeroCardComponent self, long heroId, long materialHeroId)
        {
            // long account = request.Account;
            // long heroId = request.HeroId;
            // long materialId = request.MaterialHeroId;
            HeroCard heroCard = self.GetChild<HeroCard>(heroId);
            // HeroConfig heroConfig = HeroConfigCategory.Instance.Get(heroCard.ConfigId);

            if (heroCard.Star >= ConstValue.MaxHeroStarCount)
            {
                return null;
            }

            HeroCard materialHeroCard = self.GetChild<HeroCard>(materialHeroId);
            if (materialHeroCard.State != (int)StateType.Active)
            {
                return null;
            }

            materialHeroCard.State = (int)StateType.Destroy;

            heroCard.Star += 1;
            return heroCard;
        }

        public static HeroCard UpdateHeroRank(this HeroCardComponent self, long heroId)
        {
            HeroCard heroCard = self.GetChild<HeroCard>(heroId);
            HeroConfig heroConfig = HeroConfigCategory.Instance.Get(heroCard.ConfigId);
            if (heroCard.Rank >= heroConfig.MaxRank)
            {
                return null;
            }

            heroCard.Rank++;
            return heroCard;
        }

        public static HeroCard UpdateHeroCardLevel(this HeroCardComponent self, long heroId)
        {
            HeroCard heroCard = self.GetChild<HeroCard>(heroId);
            if (heroCard != null)
            {
                Item item = self.Parent.GetComponent<ItemComponent>().GetChildByConfigId(ConstValue.ExpItemConfigId);
                if (item == null)
                {
                    return null;
                }

                var needExp = HeroHelper.GetNextLevelExp(heroCard.GetMessageInfo());
                if (needExp > item.Count)
                {
                    return null;
                }

                heroCard.Level++;
                item.Count -= needExp;
            }

            return heroCard;
        }

        public static HeroCard LockHeroCard(this HeroCardComponent self, long heroId, bool isLock)
        {
            HeroCard heroCard = self.GetChild<HeroCard>(heroId);
            if (heroCard != null)
            {
                heroCard.IsLock = isLock;
            }

            return heroCard;
        }

        // [Obsolete("Obsolete")]
        public static void SaveData(this HeroCardComponent self)
        {
            List<HeroCard> heroCards = self.GetChilds<HeroCard>();
            if (heroCards != null)
            {
                foreach (var heroCard in heroCards)
                {
                    DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Save(heroCard).Coroutine();
                }
            }
        }

        public static async ETTask<List<HeroCard>> GetAllHeroCardAsync(this HeroCardComponent self)
        {
            List<HeroCard> heroCards = self.GetChilds<HeroCard>();
            if (heroCards == null || heroCards.Count == 0)
            {
                long accountId = self.GetParent<Unit>().AccountId;
                //todo  从数据库里面取出来数据 
                heroCards = await DBManagerComponent.Instance.GetZoneDB(self.DomainZone())
                        .Query<HeroCard>(a => a.OwnerId == accountId && a.State == (int)StateType.Active);
                foreach (var heroCard in heroCards)
                {
                    self.AddChild(heroCard);
                }
            }

            List<HeroCard> newHeroCards = new List<HeroCard>();
            foreach (var heroCard in heroCards)
            {
                if (heroCard.State == (int)StateType.Active)
                {
                    newHeroCards.Add(heroCard);
                }
            }

            return newHeroCards;
        }

        public static HeroCard CallOneHero(this HeroCardComponent self)
        {
            //todo 召唤一张卡牌

            List<HeroConfig> heroConfigs = HeroConfigCategory.Instance.GetAll().Values.ToList();
            // heroConfigs.RemoveAll(a => a.MaterialType == (int)HeroBagType.Materail);
            int randomIndex = RandomHelper.RandomNumber(0, heroConfigs.Count);
            //
            long accountId = self.GetParent<Unit>().AccountId;
            HeroCard heroCard = new HeroCard();
            heroCard.Id = IdGenerater.Instance.GenerateId();
            heroCard.ConfigId = heroConfigs[randomIndex].Id;
            heroCard.OwnerId = accountId;
            self.AddChild(heroCard);
            return heroCard;
            // // HeroCard heroCard = unit.AddChild<HeroCard, int>(key);
            // await heroCard.Call(unit.DomainZone(), request.Account);
            // response.HeroCardInfo = heroCard.GetMessageInfo();
        }
#endif

#if !SERVER
        public static void InitHeroCard(this HeroCardComponent self, M2C_CreateHeroCardInRoom message)
        {
            List<HeroCardInfo> heroCardInfos = message.HeroCardInfos;
            List<HeroCardDataComponentInfo> heroCardDataComponentInfos = message.HeroCardDataComponentInfos;
            Log.Debug($"hero card infos {heroCardInfos.Count}");
            for (int i = 0; i < heroCardInfos.Count; i++)
            {
                HeroCard heroCard = new HeroCard();
                heroCard.SetMessageInfo(heroCardInfos[i]);
                self.AddChild(heroCard);
                Game.EventSystem.Publish(new EventType.CreateOneHeroCardView()
                {
                    HeroCard = heroCard, HeroCardInfo = heroCardInfos[i], HeroCardDataComponentInfo = heroCardDataComponentInfos[i]
                });
            }
            //todo 同步给显示层
        }

        public static void SyncHeroCardTurnData(this HeroCardComponent self, M2C_SyncHeroCardTurnData m2CSyncHeroCardTurnData)
        {
            foreach (var heroCardInfo in m2CSyncHeroCardTurnData.HeroCardInfos)
            {
                var heroCard = self.GetChild<HeroCard>(heroCardInfo.HeroId);
                // heroCard.Attack = heroCard.Attack;
                // heroCard.Attack = heroCardInfo.Attack;
                // heroCard.DiamondAttack = heroCardInfo.DiamondAttack;
                Game.EventSystem.Publish(new EventType.UpdateAttackView() { HeroCard = heroCard });
            }
        }
#endif
    }
}