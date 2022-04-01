using ET.EventType;
using UnityEngine;

namespace ET
{
    public class PlayHeroCardAttackTargetAnimHandler: AEvent<EventType.PlayHeroCardAttackAnim>
    {
        protected override async ETTask Run(PlayHeroCardAttackAnim a)
        {
            HeroCard attackHeroCard = a.AttackHeroCard;
            HeroCard beAttackHeroCard = a.BeAttackHeroCard;

            GameObject selfGo = attackHeroCard.GetComponent<GameObjectComponent>().GameObject;
            GameObject targetGo = beAttackHeroCard.GetComponent<GameObjectComponent>().GameObject;
            // await selfGo.GetComponent<HeroCardViewCtl>().PlayAttackLogic(targetGo);
            GameObject heroMode = selfGo.GetComponent<HeroCardViewCtl>().ChangeModeView();
            float distance = 100;
            while (distance > 0.1f)
            {
                Vector3 prePos = Vector3.Lerp(heroMode.transform.position, targetGo.transform.position, 0.01f);
                heroMode.transform.position = prePos;
                distance = Vector3.Distance(prePos, targetGo.transform.position);
                await TimerComponent.Instance.WaitFrameAsync();
            }
            
            await ETTask.CompletedTask;
        }
    }
}