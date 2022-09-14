using ET.EventType;

namespace ET
{
    public class PlayBuffDamageAnimEventHandler: AEvent<EventType.PlayBuffDamageAnim>
    {
        protected override async ETTask Run(PlayBuffDamageAnim a)
        {
            HeroCardComponent heroCardComponent = a.HeroCardComponent;
            HeroCardDataComponentInfo heroCardDataComponentInfo = a.HeroCardDataComponentInfo;
            BuffInfo buffInfo = a.BuffInfo;
            int damageCount = a.DamageCount;
            HeroCard heroCard = heroCardComponent.GetChild<HeroCard>(heroCardDataComponentInfo.HeroId);

            await heroCard.GetComponent<HeroModeObjectCompoent>().PlayBuffDamageAnim(heroCardDataComponentInfo, damageCount, buffInfo);

            await ETTask.CompletedTask;
        }
    }
}