using System.Collections.Generic;
using ET.EventType;

namespace ET
{
    public class UpdateBuffInfoStateHandler: AEvent<EventType.UpdateHeroBuffInfoEvent>
    {
        protected override async ETTask Run(UpdateHeroBuffInfoEvent a)
        {
            HeroCardComponent heroCardComponent = a.HeroCardComponent;
            long heroId = a.HeroId;
            List<BuffInfo> buffInfos = a.BuffInfos;

            HeroCard heroCard = heroCardComponent.GetChild<HeroCard>(heroId);
            heroCard.GetComponent<HeroModeObjectCompoent>().ShowBuffEffect(buffInfos, a.HeroCardDataComponentInfo);
            heroCard.GetComponent<HeroCardInfoObjectComponent>().ShowBuffViewInfo(buffInfos, a.HeroCardDataComponentInfo);

            await ETTask.CompletedTask;
        }
    }
}