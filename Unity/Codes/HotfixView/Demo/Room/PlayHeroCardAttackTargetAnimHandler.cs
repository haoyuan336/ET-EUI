using System.Runtime.InteropServices;
using ET.EventType;
using UnityEngine;

namespace ET
{
    public class PlayHeroCardAttackTargetAnimHandler: AEvent<EventType.PlayHeroCardAttackAnim>
    {
        protected override async ETTask Run(PlayHeroCardAttackAnim a)
        {
            HeroCard selfHeroCard = a.SelfHeroCard;
            HeroCard targetHeroCard = a.TargetHeroCard;

            GameObject selfGo = selfHeroCard.GetComponent<GameObjectComponent>().GameObject;
            GameObject targetGo = targetHeroCard.GetComponent<GameObjectComponent>().GameObject;
            await selfGo.GetComponent<HeroCardViewCtl>().PlayAttackAnim(targetGo);
            await ETTask.CompletedTask;
        }
    }
}