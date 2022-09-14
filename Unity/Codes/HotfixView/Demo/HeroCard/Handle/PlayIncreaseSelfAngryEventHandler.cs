using ET.EventType;

namespace ET
{
    public class PlayIncreaseSelfAngryEventHandler: AEvent<EventType.PlayIncreaseSelfAngryEvent>
    {
        protected override async ETTask Run(PlayIncreaseSelfAngryEvent a)
        {
            HeroCardComponent heroCardComponent = a.HeroCardComponent;
            HeroCardDataComponentInfo heroCardDataComponentInfo = a.HeroCardDataComponentInfo;
            HeroCard heroCard = heroCardComponent.GetChild<HeroCard>(heroCardDataComponentInfo.HeroId);
            HeroCardInfoObjectComponent heroCardInfoObjectComponent = heroCard.GetComponent<HeroCardInfoObjectComponent>();
            heroCardInfoObjectComponent.UpdateAngryView(heroCardDataComponentInfo);
            await ETTask.CompletedTask;
        }
    }
}