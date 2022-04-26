using ET.EventType;
using UnityEngine;
namespace ET
{
    public class UpdateHeroCardAttackViewHandler: AEvent<EventType.UpdateAttackView>
    {
        protected override async ETTask Run(UpdateAttackView a)
        {
            HeroCard heroCard = a.HeroCard;
            GameObject go = heroCard.GetComponent<HeroCardObjectComponent>().HeroCard;
            go.GetComponent<HeroCardViewCtl>().UpdateAttackView((heroCard.Attack + heroCard.DiamondAttack).ToString());
            go.GetComponent<HeroCardViewCtl>().UpdateDiamondAttackView(heroCard.DiamondAttack.ToString());
            await ETTask.CompletedTask;
        }
    }
}