using ET.EventType;
using UnityEngine;
namespace ET
{
    public class UpdateHeroCardAngryViewHandler: AEvent<EventType.UpdateAngryView>
    {
        protected override async ETTask Run(UpdateAngryView a)
        {
            HeroCard heroCard = a.HeroCard;
            GameObject go = heroCard.GetComponent<GameObjectComponent>().GameObject;
            go.GetComponent<HeroCardViewCtl>().UpdateAngryView(heroCard.Angry.ToString());
            await ETTask.CompletedTask;
            
        }
    }
}