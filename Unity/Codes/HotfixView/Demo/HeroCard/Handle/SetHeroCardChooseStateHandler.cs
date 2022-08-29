using ET.EventType;

namespace ET
{
    public class SetHeroCardChooseStateHandler: AEvent<EventType.SetHeroCardChooseState>
    {
        protected override async ETTask Run(SetHeroCardChooseState a)
        {
            if (!a.Show)
            {
                foreach (var card in a.AllHeroCard)
                {
                    card.GetComponent<HeroModeObjectCompoent>().ShowChooseMark(false);
                }

                return;
            }

            HeroCard heroCard = a.HeroCard;

            foreach (var card in a.AllHeroCard)
            {
                if (card.Equals(heroCard))
                {
                    continue;
                }

                card.GetComponent<HeroModeObjectCompoent>().ShowChooseMark(false);
            }

            heroCard.GetComponent<HeroModeObjectCompoent>().ShowChooseMark(a.Show);

            await ETTask.CompletedTask;
        }
    }

    public class ShowAttackMarkHandler: AEvent<EventType.ShowAttackMark>
    {
        protected override async ETTask Run(ShowAttackMark a)
        {
            // HeroCard heroCard = a.HeroCard;
            // heroCard.GetComponent<HeroModeObjectCompoent>().ShowAttackMark(a.IsShow);

            HeroCardComponent heroCardComponent = a.HeroCardComponent;
            HeroCardDataComponentInfo heroCardDataComponentInfo = a.HeroCardDataComponentInfo;
            bool isShow = a.IsShow;

            HeroCard heroCard = heroCardComponent.GetChild<HeroCard>(heroCardDataComponentInfo.HeroId);
            heroCard.GetComponent<HeroModeObjectCompoent>().ShowAttackMark(isShow);

            await ETTask.CompletedTask;
        }
    }
}