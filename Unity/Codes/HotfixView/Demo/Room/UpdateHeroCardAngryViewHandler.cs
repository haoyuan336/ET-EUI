using ET.EventType;
using UnityEngine;

namespace ET
{
    public class UpdateHeroCardAngryViewHandler: AEvent<EventType.UpdateAngryView>
    {
        protected override async ETTask Run(UpdateAngryView a)
        {
            HeroCard heroCard = a.HeroCard;
            GameObject go = heroCard.GetComponent<HeroCardObjectComponent>().HeroCard;
            // go.GetComponent<HeroCardViewCtl>()
            //         .UpdateAngryView($"{heroCard.Angry.ToString()} /{HeroConfigCategory.Instance.Get(heroCard.ConfigId).TotalAngry.ToString()}");
            await ETTask.CompletedTask;
        }
    }
}