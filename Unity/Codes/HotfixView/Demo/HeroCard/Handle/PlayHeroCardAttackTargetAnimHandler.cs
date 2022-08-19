using ET.EventType;
namespace ET
{
    public class PlayHeroCardAttackTargetAnimHandler: AEvent<EventType.PlayHeroCardAttackAnim>
    {
        protected override async ETTask Run(PlayHeroCardAttackAnim a)
        {
            Log.Warning("play hero card attack anim");
            
            
            HeroCardComponent heroCardComponent = a.HeroCardComponent;
            AttackAction attackAction = a.AttackAction;

            HeroCard heroCard = heroCardComponent.GetChild<HeroCard>(attackAction.AttackHeroCardDataComponentInfo.HeroId);
            

            // Log.Debug("PlayHeroCardAttackTargetAnimHandler");
            // HeroCard heroCard = a.AttackHeroCard;
            // if (heroCard == null)
            // {
            //     Log.Debug("未找到herocard");
            // }
            // // await heroCard.GetComponent<HeroCardView>().PlayAttackAnimLogic(a);
            await heroCard.GetComponent<HeroModeObjectCompoent>().PlayAttackAnimLogic(a);
            // await ETTask.CompletedTask;
        }
    }
}