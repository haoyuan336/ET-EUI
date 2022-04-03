using ET.EventType;
using UnityEngine;

namespace ET
{
    public class PlayHeroCardAttackTargetAnimHandler: AEvent<EventType.PlayHeroCardAttackAnim>
    {
        protected override async ETTask Run(PlayHeroCardAttackAnim a)
        {

            HeroCard heroCard = a.AttackHeroCard;
            await heroCard.GetComponent<HeroCardView>().PlayAttackAnimLogic(a);
            await ETTask.CompletedTask;
        }
    }
}