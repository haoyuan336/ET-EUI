using System.Collections.Generic;
using ET.Demo.Effect;
using ET.Effect;
using ET.EventType;
using UnityEngine;

namespace ET
{
    public class PlayAddAngryViewAnimHandler: AEvent<EventType.PlayAddAngryViewAnim>
    {
        protected override async ETTask Run(PlayAddAngryViewAnim a)
        {
            // List<ETTask> tasks = new List<ETTask>();
            // HeroCard heroCard = a.HeroCard;
            // Log.Debug("播放增加怒气值的操作");
            // await heroCard.GetComponent<HeroModeObjectCompoent>().PlayAddAngryEffect(a);
            // heroCard.GetComponent<HeroCardObjectComponent>().UpdateHeroCardTextView(a.HeroCardInfo);

            //播放增加怒气值的动效
            //首先取出宝石
            DiamondComponent diamondComponent = a.Scene.GetComponent<DiamondComponent>();
            HeroCardComponent heroCardComponent = a.Scene.GetComponent<HeroCardComponent>();
            List<DiamondInfo> diamondInfos = a.AddItemAction.DiamondInfos;
            HeroCard heroCard = heroCardComponent.GetChild<HeroCard>(a.AddItemAction.HeroCardInfo.HeroId);
            Vector3 endPos = heroCard.GetComponent<HeroModeObjectCompoent>().HeroMode.transform.position;
            // List<Diamond> diamonds = new List<Diamond>();
            List<Vector3> startPosList = new List<Vector3>();
            foreach (var info in diamondInfos)
            {
                Diamond diamond = diamondComponent.GetChild<Diamond>(info.Id);
                // diamonds.Add(diamond);
                GameObject diamondObj = diamond.GetComponent<GameObjectComponent>().GameObject;
                startPosList.Add(diamondObj.transform.position);
            }

            Log.Warning($"找到了 消除的几个宝石{startPosList.Count}");

            AddAttackEffect effect = heroCard.AddChild<AddAttackEffect, List<Vector3>, Vector3>(startPosList, endPos);

            await effect.PlayAnim();
            await ETTask.CompletedTask;
        }
    }

    public class PlayAddAttackViewAnimHandler: AEvent<EventType.PlayAddAttackViewAnim>
    {
        protected async override ETTask Run(PlayAddAttackViewAnim a)
        {
            // Log.Debug("播放增加攻击力值的操作");
            // HeroCard heroCard = a.HeroCard;
            // await heroCard.GetComponent<HeroModeObjectCompoent>().PlayAddAttackEffect(a);
            // heroCard.GetComponent<HeroCardObjectComponent>().UpdateHeroCardTextView(a.HeroCardInfo);
            await ETTask.CompletedTask;
        }
    }
}