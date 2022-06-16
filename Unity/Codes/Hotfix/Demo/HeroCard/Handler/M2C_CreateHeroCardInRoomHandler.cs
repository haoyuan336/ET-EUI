using System.Collections.Generic;

namespace ET
{
    public class M2C_CreateHeroCardInRoomHandler: AMHandler<M2C_CreateHeroCardInRoom>
    {
        protected override async ETTask Run(Session session, M2C_CreateHeroCardInRoom message)
        {
            // HeroCardComponent heroCardComponent = session.ZoneScene().GetComponent<HeroCardComponent>();

            HeroCardComponent heroCardComponent = session.ZoneScene().CurrentScene().AddComponent<HeroCardComponent>();
            
            heroCardComponent.InitHeroCard(message);

            

            await ETTask.CompletedTask;
        }
    }
}