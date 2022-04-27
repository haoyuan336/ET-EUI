using System.Collections.Generic;
using ET.EventType;
using UnityEngine;

namespace ET
{
    public class PlayAddAngryViewAnimHandler: AEvent<EventType.PlayAddAngryViewAnim>
    {
        protected override async ETTask Run(PlayAddAngryViewAnim a)
        {
            List<ETTask> tasks = new List<ETTask>();
            HeroCard heroCard = a.HeroCard;
            Diamond diamond = a.Diamond;
            // Vector3 startPos = diamond.GetComponent<GameObjectComponent>().GameObject.transform.position;
            tasks.Add(heroCard.GetComponent<HeroCardView>().PlayAddAngryEffect(a));
            await ETTaskHelper.WaitAll(tasks);
            await ETTask.CompletedTask;
        }
    }

    public class PlayAddAttackViewAnimHandler: AEvent<EventType.PlayAddAttackViewAnim>
    {
        protected async override ETTask Run(PlayAddAttackViewAnim a)
        {
            HeroCard heroCard = a.HeroCard;
            await heroCard.GetComponent<HeroCardView>().PlayAddAttackEffect(a);
            await ETTask.CompletedTask;
        }
    }
}