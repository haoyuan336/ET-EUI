using System.Collections.Generic;

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
    }
}