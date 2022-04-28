using ET.EventType;
namespace ET
{
    public class PlayHeroCardAttackTargetAnimHandler: AEvent<EventType.PlayHeroCardAttackAnim>
    {
        protected override async ETTask Run(PlayHeroCardAttackAnim a)
        {

            Log.Debug("PlayHeroCardAttackTargetAnimHandler");
            HeroCard heroCard = a.AttackHeroCard;
            // await heroCard.GetComponent<HeroCardView>().PlayAttackAnimLogic(a);

            await heroCard.GetComponent<HeroModeObjectCompoent>().PlayAttackAnimLogic(a);
            await ETTask.CompletedTask;
        }
    }
}