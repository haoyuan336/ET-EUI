using System.Collections.Generic;

namespace ET
{
    public class M2C_PlayerChooseAttackHeroHandler: AMHandler<M2C_PlayerChooseAttackHero>
    {
        protected override async ETTask Run(Session session, M2C_PlayerChooseAttackHero message)
        {
            HeroCardComponent heroCardComponent = session.ZoneScene().CurrentScene().GetComponent<HeroCardComponent>();
            await Game.EventSystem.PublishAsync(new EventType.SetHeroCardChooseState()
            {
                HeroCardComponent =  heroCardComponent,
                IsShow = true,
                HeroId = message.HeroId
            });
            // await ETTask.CompletedTask;
        }
    }
}