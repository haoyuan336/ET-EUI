using ET.EventType;

namespace ET
{
    public class PlayHeroRecoveryAnimEventHandler: AEvent<EventType.PlayHeroRecoveryAnimEvent>
    {
        protected override async ETTask Run(PlayHeroRecoveryAnimEvent a)
        {
            HeroCardComponent heroCardComponent = a.HeroCardComponent;
            HeroCardDataComponentInfo heroCardDataComponentInfo = a.HeroCardDataComponentInfo;
            HeroCard heroCard = heroCardComponent.GetChild<HeroCard>(heroCardDataComponentInfo.HeroId);

            HeroModeObjectCompoent heroModeObjectCompoent = heroCard.GetComponent<HeroModeObjectCompoent>();
            HeroCardInfoObjectComponent heroCardInfoObjectComponent = heroCard.GetComponent<HeroCardInfoObjectComponent>();

            await heroModeObjectCompoent.PlayRebornAnimAsync();
            heroCardInfoObjectComponent.UpdateHPView(heroCardDataComponentInfo);
        }
    }
}