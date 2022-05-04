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
            DiamondComponent diamondComponent = a.Scene.GetComponent<DiamondComponent>();
            HeroCardComponent heroCardComponent = a.Scene.GetComponent<HeroCardComponent>();
            List<DiamondInfo> diamondInfos = a.AddItemAction.DiamondInfos;
            HeroCard heroCard = heroCardComponent.GetChild<HeroCard>(a.AddItemAction.HeroCardInfo.HeroId);
            Vector3 endPos = heroCard.GetComponent<HeroModeObjectCompoent>().HeroMode.transform.position + Vector3.up;
            // List<Diamond> diamonds = new List<Diamond>();
            List<Vector3> startPosList = new List<Vector3>();
            foreach (var info in diamondInfos)
            {
                Diamond diamond = diamondComponent.GetChild<Diamond>(info.Id);
                // diamonds.Add(diamond);
                GameObject diamondObj = diamond.GetComponent<GameObjectComponent>().GameObject;
                startPosList.Add(diamondObj.transform.position + Vector3.up * 0.5f);
            }

            Log.Warning($"找到了 消除的几个宝石{startPosList.Count}");

            AddAttackEffect effect = heroCard.AddChild<AddAttackEffect, List<Vector3>, Vector3>(startPosList, endPos);

            await effect.PlayAnim();
            
            heroCard.GetComponent<HeroModeObjectCompoent>().UpdateShowDataView(a.AddItemAction.HeroCardInfo);
            // heroCard.GetComponent<>()
            await ETTask.CompletedTask;
        }
    }

    public class PlayAddAttackViewAnimHandler: AEvent<EventType.PlayAddAttackViewAnim>
    {
        protected override async ETTask Run(PlayAddAttackViewAnim a)
        {
            DiamondComponent diamondComponent = a.Scene.GetComponent<DiamondComponent>();
            HeroCardComponent heroCardComponent = a.Scene.GetComponent<HeroCardComponent>();
            List<DiamondInfo> diamondInfos = a.AddItemAction.DiamondInfos;
            HeroCard heroCard = heroCardComponent.GetChild<HeroCard>(a.AddItemAction.HeroCardInfo.HeroId);
            Vector3 endPos = heroCard.GetComponent<HeroModeObjectCompoent>().HeroMode.transform.position + Vector3.up;
            // List<Diamond> diamonds = new List<Diamond>();
            List<Vector3> startPosList = new List<Vector3>();
            foreach (var info in diamondInfos)
            {
                Diamond diamond = diamondComponent.GetChild<Diamond>(info.Id);
                // diamonds.Add(diamond);
                GameObject diamondObj = diamond.GetComponent<GameObjectComponent>().GameObject;
                startPosList.Add(diamondObj.transform.position + Vector3.up * 0.5f);
            }

            Log.Warning($"找到了 消除的几个宝石{startPosList.Count}");

            AddAngryEffect effect = heroCard.AddChild<AddAngryEffect, List<Vector3>, Vector3>(startPosList, endPos);
            await effect.PlayAnim();
            await ETTask.CompletedTask;
        }
    }
}