using System.Collections.Generic;
using ET.EventType;
using UnityEngine;

namespace ET
{
    public class PlayAddAttackAngryViewAnimHandler: AEvent<EventType.PlayAddAttackAngryViewAnim>
    {
        protected override async ETTask Run(PlayAddAttackAngryViewAnim a)
        {
            List<ETTask> tasks = new List<ETTask>();
            HeroCard heroCard = a.HeroCard;
            tasks.Add(heroCard.GetComponent<HeroCardView>().PlayAddAttackEffect(a));
            tasks.Add(heroCard.GetComponent<HeroCardView>().PlayAddAngryEffect(a));
            await ETTaskHelper.WaitAll(tasks);
            await ETTask.CompletedTask;
        }
    }
}