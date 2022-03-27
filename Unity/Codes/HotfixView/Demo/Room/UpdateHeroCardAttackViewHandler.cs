using ET.EventType;
using UnityEngine;
namespace ET
{
    public class UpdateHeroCardAttackViewHandler: AEvent<EventType.UpdateAttackView>
    {
        protected override async ETTask Run(UpdateAttackView a)
        {
            HeroCard heroCard = a.HeroCard;
            GameObject go = heroCard.GetComponent<GameObjectComponent>().GameObject;
            go.GetComponent<HeroCardViewCtl>().UpdateAttackView(heroCard.Attack);
            await ETTask.CompletedTask;
        }
    }
}