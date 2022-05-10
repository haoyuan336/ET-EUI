using System.Collections.Generic;

namespace ET
{
    public class M2C_PlayerChooseAttackHeroHandler: AMHandler<M2C_PlayerChooseAttackHero>
    {
        protected override async ETTask Run(Session session, M2C_PlayerChooseAttackHero message)
        {
            var heroId = message.HeroId;
            List<HeroCard> allHeroCards = session.ZoneScene().CurrentScene().GetComponent<HeroCardComponent>().GetChilds<HeroCard>();
            HeroCard heroCard = session.ZoneScene().CurrentScene().GetComponent<HeroCardComponent>().GetChild<HeroCard>(heroId);
            await Game.EventSystem.PublishAsync(new EventType.SetHeroCardChooseState()
            {
                AllHeroCard = allHeroCards, HeroCard = heroCard, Show = true
            });
            // await ETTask.CompletedTask;
        }
    }
}