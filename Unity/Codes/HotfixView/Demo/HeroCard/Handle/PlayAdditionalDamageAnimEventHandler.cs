using ET.EventType;

namespace ET
{
    public class PlayAdditionalDamageAnimEventHandler: AEvent<EventType.PlayAdditionalDamageAnimEvent>
    {
        protected override async ETTask Run(PlayAdditionalDamageAnimEvent a)
        {
            HeroCardComponent heroCardComponent = a.HeroCardComponent;
            HeroCardDataComponentInfo heroCardDataComponentInfo = a.HeroCardDataComponentInfo;

            HeroCard heroCard = heroCardComponent.GetChild<HeroCard>(heroCardDataComponentInfo.HeroId);
            // heroCard.GetComponent<HeroModeObjectCompoent>().PlayAttackAnimLogic();

            heroCard.GetComponent<HeroCardInfoObjectComponent>().ShowDamageViewAnim(heroCardDataComponentInfo);
            heroCard.GetComponent<HeroCardInfoObjectComponent>().UpdateHPView(heroCardDataComponentInfo);
            await heroCard.GetComponent<HeroModeObjectCompoent>().PlayBeAttackAnim();
        }
    }
}