using System.Collections.Generic;
using ET.EventType;

namespace ET
{
    public class UpdateBuffInfoStateHandler: AEvent<EventType.UpdateHeroBuffInfo>
    {
        protected override async ETTask Run(UpdateHeroBuffInfo a)
        {
            HeroCardComponent heroCardComponent = a.HeroCardComponent;
            UpdateHeroBuffInfoItem infoItem = a.UpdateHeroBuffInfoItem;
            List<HeroBuffInfo> heroBuffInfos = infoItem.HeroBuffInfos;

            foreach (var heroBufferInfo in heroBuffInfos)
            {
                List<BuffInfo> buffInfos = heroBufferInfo.BuffInfos;
                Log.Debug($"udpate buffer indo {buffInfos.Count}");
                long heroId = heroBufferInfo.HeroId;
                HeroCard heroCard = heroCardComponent.GetChild<HeroCard>(heroId);
                heroCard.GetComponent<HeroModeObjectCompoent>().ShowBuffEffect(buffInfos);
                heroCard.GetComponent<HeroCardInfoObjectComponent>().ShowBuffViewInfo(buffInfos);
            }

            await ETTask.CompletedTask;
        }
    }
}