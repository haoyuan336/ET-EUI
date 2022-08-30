using System.Collections.Generic;
using ET.EventType;

namespace ET
{
    public class PlayHeroCardAttackTargetAnimHandler: AEvent<EventType.PlayHeroCardAttackAnim>
    {
        protected override async ETTask Run(PlayHeroCardAttackAnim a)
        {
            HeroCardComponent heroCardComponent = a.HeroCardComponent;
            // List<AttackActionItem> attackActionItems = a.AttackActionItems;
            // foreach (var attackActionItem in attackActionItems)
            // {
            // foreach (var attackAction in attackActionItem.AttackActions)
            // {
            AttackAction attackAction = a.AttackAction;
            HeroCard heroCard = heroCardComponent.GetChild<HeroCard>(attackAction.AttackHeroCardDataComponentInfo.HeroId);
            await heroCard.GetComponent<HeroModeObjectCompoent>().PlayAttackAnimLogic(heroCardComponent, attackAction);
            // }

            // List<HeroBuffInfo> heroBuffInfos = attackActionItem.HeroBuffInfos;
            //todo 更新英雄的buff信息
            // }
        }
    }
}