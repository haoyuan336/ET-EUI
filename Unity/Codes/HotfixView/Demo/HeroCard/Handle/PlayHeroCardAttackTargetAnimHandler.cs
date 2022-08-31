using System.Collections.Generic;
using ET.EventType;

namespace ET
{
    public class PlayHeroCardAttackTargetAnimHandler: AEvent<EventType.PlayHeroCardAttackAnim>
    {
        protected override async ETTask Run(PlayHeroCardAttackAnim a)
        {
            HeroCardComponent heroCardComponent = a.HeroCardComponent;
            AttackAction attackAction = a.AttackAction;
            HeroCard heroCard = heroCardComponent.GetChild<HeroCard>(attackAction.AttackHeroCardDataComponentInfo.HeroId);
            await heroCard.GetComponent<HeroModeObjectCompoent>().PlayAttackAnimLogic(heroCardComponent, attackAction);
            //todo 更新英雄的buff信息
        }
    }
}