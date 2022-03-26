using System.Collections.Generic;

namespace ET
{
    public static class HeroCardComponentSystem
    {
        public static void InitHeroCard(this HeroCardComponent self, M2C_CreateHeroCardInRoom message)
        {
            List<HeroCardInfo> heroCardInfos = message.HeroCardInfo;
            foreach (var heroCardInfo in heroCardInfos)
            {
                Log.Debug($"create hero card {heroCardInfo.HeroId}" );

                HeroCard heroCard = self.AddChildWithId<HeroCard>(heroCardInfo.HeroId);
                heroCard.SetMessageInfo(heroCardInfo);
                self.HeroCards.Add(heroCard);
            }

            //todo 同步给显示层
            foreach (var heroCard in self.HeroCards)
            {
                Game.EventSystem.Publish(new EventType.CreateHeroCardView() { HeroCard = heroCard });
            }
        }
    }
}