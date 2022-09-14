using System.Collections.Generic;
using ET.EventType;

namespace ET
{
    public class UpdateHeroCardInfoEventHandler: AEvent<UpdateHeroInfoEvent>
    {
        protected override async ETTask Run(UpdateHeroInfoEvent a)
        {
            HeroCardComponent heroCardComponent = a.HeroCardComponent;
            HeroCardDataComponentInfo heroCardDataComponentInfo = a.HeroCardDataComponentInfo;
            HeroCard heroCard = heroCardComponent.GetChild<HeroCard>(heroCardDataComponentInfo.HeroId);
            HeroCardInfoObjectComponent objectComponent = heroCard.GetComponent<HeroCardInfoObjectComponent>();
            objectComponent.UpdateAngryView(heroCardDataComponentInfo);
            objectComponent.UpdateAttackAdditionView(heroCardDataComponentInfo);

            await ETTask.CompletedTask;
        }
    }
}