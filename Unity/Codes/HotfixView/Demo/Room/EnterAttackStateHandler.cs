using ET.EventType;
using UnityEngine;
namespace ET
{
    public class EnterAttackStateHandler: AEvent<EventType.EnterAttackStateView>
    {
        protected override async ETTask Run(EnterAttackStateView a)
        {
            HeroCard heroCard = a.HeroCard;

            GameObject go = heroCard.GetComponent<GameObjectComponent>().GameObject;
            go.GetComponent<HeroCardViewCtl>().ChangeModeView();

            await ETTask.CompletedTask;
        }
    }
}