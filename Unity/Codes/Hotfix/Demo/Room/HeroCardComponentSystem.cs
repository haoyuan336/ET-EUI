using System.Collections.Generic;
using ET.Account;

namespace ET
{
    public static class HeroCardComponentSystem
    {
        public static void InitHeroCard(this HeroCardComponent self, M2C_CreateHeroCardInRoom message)
        {
            List<HeroCardInfo> heroCardInfos = message.HeroCardInfo;
            Dictionary<int, List<HeroCard>> heroCardListMap = new Dictionary<int, List<HeroCard>>();

            foreach (var heroCardInfo in heroCardInfos)
            {
                HeroCard heroCard = self.AddChildWithId<HeroCard>(heroCardInfo.HeroId);
                heroCard.SetMessageInfo(heroCardInfo);
                if (!heroCardListMap.ContainsKey(heroCard.CampIndex))
                {
                    // heroCardListMap[heroCard.CampIndex] = new List<HeroCard>();
                    List<HeroCard> heroCards = new List<HeroCard>();
                    heroCards.Add(heroCard);
                    heroCardListMap.Add(heroCard.CampIndex, heroCards);
                }
                else
                {
                    heroCardListMap[heroCard.CampIndex].Add(heroCard);
                }

                self.HeroCards.Add(heroCard);
            }

            //todo 同步给显示层
            Game.EventSystem.Publish(new EventType.CreateHeroCardView() { HeroCardListMap = heroCardListMap });
        }

        public static async ETTask AddHeroValueByDiamondDestroy(this HeroCardComponent self, Diamond diamond)
        {
            PlayerComponent playerComponent = self.DomainScene().GetComponent<PlayerComponent>();

            Log.Debug($"my seat index {playerComponent.MySeatIndex}");
            Log.Debug($"current turn index{playerComponent.CurrentTurnIndex}");
            if (playerComponent.MySeatIndex != playerComponent.CurrentTurnIndex)
            {
                Log.Debug("my seat index is diff");
                return;
            }

            AccountInfoComponent accountInfoComponent = self.DomainScene().GetComponent<AccountInfoComponent>();

            DiamondTypeConfig config = DiamondTypeConfigCategory.Instance.Get(diamond.DiamondType);
            foreach (var heroCard in self.HeroCards)
            {
                Log.Debug($"hero card ownerid {heroCard.OwnerId}");
                Log.Debug($"account id {accountInfoComponent.AccountId}");
                if (diamond.DiamondType.Equals(heroCard.HeroColor) && heroCard.OwnerId == accountInfoComponent.AccountId)
                {
                    if (self.CurrentTurnAttackList.Count == 0)
                    {
                        self.CurrentTurnAttackList.Add(heroCard);
                        heroCard.EnterAttackState();
                    }

                    await Game.EventSystem.PublishAsync(
                        new EventType.PlayAddHeroCardValueEffect() { StartDiamond = diamond, EndHeroCard = heroCard });
                    // Log.Debug("add hero value by diamond ");
                    heroCard.AddAttackValue(float.Parse(config.AddAttack));
                    heroCard.AddAngryValue(float.Parse(config.AddAngry));
                }
            }
        }

        public static async ETTask PlayHeroCardAttackAnimAsync(this HeroCardComponent self)
        {
            List<ETTask> tasks = new List<ETTask>();
            Log.Debug("play attack anim");
            //找到敌对阵营的敌人
            var seatCount = self.DomainScene().GetComponent<PlayerComponent>().SeatCount;
            // let currentCampIndex= self.CurrentTurnAttackList

            foreach (var heroCard in self.CurrentTurnAttackList)
            {
                int campIndex = heroCard.CampIndex;
                campIndex++;
                if (campIndex >= seatCount)
                {
                    campIndex = 0;
                }

                List<HeroCard> heroCards = self.GetHeroCardsByCampIndex(campIndex);
                HeroCard target = self.GetAttackTargetByTroopIndex(heroCard.InTroopIndex, heroCards);
                Log.Debug($"attack target troop index{target.InTroopIndex}");
                tasks.Add(heroCard.AttackTargetAsync(target));
            }

            await ETTask.CompletedTask;
        }

        public static HeroCard GetAttackTargetByTroopIndex(this HeroCardComponent self, int inTroopIndex, List<HeroCard> heroCards)
        {
            bool isAllDead = true;
            foreach (var heroCard in heroCards)
            {
                if (heroCard.IsDead != 1)
                {
                    isAllDead = false;
                    break;
                }
            }

            if (isAllDead)
            {
                return null;
            }

            while (true)
            {
                if (inTroopIndex < heroCards.Count)
                {
                    HeroCard heroCard = heroCards[inTroopIndex];
                    if (heroCard.IsDead == 0)
                    {
                        return heroCard;
                    }

                    inTroopIndex++;
                }
                else
                {
                    inTroopIndex = 0;
                }
            }
        }

        public static List<HeroCard> GetHeroCardsByCampIndex(this HeroCardComponent self, int campIndex)
        {
            List<HeroCard> heroCards = new List<HeroCard>();
            foreach (var heroCard in self.HeroCards)
            {
                if (heroCard.CampIndex.Equals(campIndex))
                {
                    heroCards.Add(heroCard);
                }
            }

            //根据座位 号进行排序
            heroCards.Sort((a, b) => a.InTroopIndex - b.InTroopIndex);
            foreach (var heroCard in heroCards)
            {
                Log.Debug($"hero card in troop index {heroCard.InTroopIndex}");
            }

            return heroCards;
        }
    }
}