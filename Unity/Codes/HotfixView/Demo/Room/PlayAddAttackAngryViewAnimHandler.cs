using System.Collections.Generic;
using ET.EventType;
using UnityEngine;

namespace ET
{
    public class PlayAddAngryViewAnimHandler: AEvent<EventType.PlayAddAngryViewAnim>
    {
        protected override async ETTask Run(PlayAddAngryViewAnim a)
        {
            // List<ETTask> tasks = new List<ETTask>();
            HeroCard heroCard = a.HeroCard;
            // Diamond diamond = a.Diamond;
            // Vector3 startPos = diamond.GetComponent<GameObjectComponent>().GameObject.transform.position;
            // tasks.Add(heroCard.GetComponent<HeroCardView>().PlayAddAngryEffect(a));
            // await ETTaskHelper.WaitAll(tasks);
            // await heroCard.GetComponent<HeroModeObjectCompoent>().PlayAddAttackEffect()

            Log.Debug("播放增加怒气值的操作");
            await heroCard.GetComponent<HeroModeObjectCompoent>().PlayAddAngryEffect(a);
            heroCard.GetComponent<HeroCardObjectComponent>().UpdateHeroCardTextView(a.HeroCardInfo);
            await ETTask.CompletedTask;
        }
    }

    public class PlayAddAttackViewAnimHandler: AEvent<EventType.PlayAddAttackViewAnim>
    {

        protected async override ETTask Run(PlayAddAttackViewAnim a)
        {
            Log.Debug("播放增加攻击力值的操作");
            HeroCard heroCard = a.HeroCard;
            await heroCard.GetComponent<HeroModeObjectCompoent>().PlayAddAttackEffect(a);
            // await heroCard.GetComponent<HeroCardView>().PlayAddAttackEffect(a);
            heroCard.GetComponent<HeroCardObjectComponent>().UpdateHeroCardTextView(a.HeroCardInfo);
            await ETTask.CompletedTask;
        }
    }
}