using System.Collections.Generic;
using System.ComponentModel;
using ET.EventType;

namespace ET
{
    public class SetHeroCardChooseStateHandler: AEvent<EventType.SetHeroCardChooseState>
    {
        protected override async ETTask Run(SetHeroCardChooseState a)
        {
            HeroCardComponent heroCardComponent = a.HeroCardComponent;
            List<HeroCard> heroCards = heroCardComponent.GetChilds<HeroCard>();

            foreach (var card in heroCards)
            {
                card.GetComponent<HeroModeObjectCompoent>().ShowChooseMark(false);
            }

            if (!a.IsShow)
            {
                return;
            }

            HeroCard heroCard = heroCardComponent.GetChild<HeroCard>(a.HeroId);
            heroCard.GetComponent<HeroModeObjectCompoent>().ShowChooseMark(a.IsShow);
            await ETTask.CompletedTask;
        }
    }

    public class ShowAttackMarkHandler: AEvent<EventType.ShowAttackMark>
    {
        protected override async ETTask Run(ShowAttackMark a)
        {
            // HeroCard heroCard = a.HeroCard;
            // heroCard.GetComponent<HeroModeObjectCompoent>().ShowAttackMark(a.IsShow);

            Log.Debug($"ShowAttackMarkHandler {a.IsShow}");
            HeroCardComponent heroCardComponent = a.HeroCardComponent;
            HeroCardDataComponentInfo heroCardDataComponentInfo = a.HeroCardDataComponentInfo;
            bool isShow = a.IsShow;

            if (isShow)
            {
                HeroCard heroCard = heroCardComponent.GetChild<HeroCard>(heroCardDataComponentInfo.HeroId);
                heroCard.GetComponent<HeroModeObjectCompoent>().ShowAttackMark(isShow);
            }

            await ETTask.CompletedTask;
        }
    }
}