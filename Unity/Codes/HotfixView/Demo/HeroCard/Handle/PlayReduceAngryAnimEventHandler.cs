using ET.EventType;

namespace ET
{
    public class PlayReduceAngryAnimEventHandler: AEvent<EventType.PlayReduceAngryEvent>
    {
        protected override async ETTask Run(PlayReduceAngryEvent a)
        {
            HeroCardComponent heroCardComponent = a.HeroCardComponent;
            HeroCardDataComponentInfo heroCardDataComponentInfo = a.HeroCardDataComponentInfo;

            HeroCard heroCard = heroCardComponent.GetChild<HeroCard>(heroCardDataComponentInfo.HeroId);

            HeroCardInfoObjectComponent heroCardInfoObjectComponent = heroCard.GetComponent<HeroCardInfoObjectComponent>();


            heroCardInfoObjectComponent.PlayReduceAngryAnim(heroCardDataComponentInfo);

            await ETTask.CompletedTask;
        }
    }
}