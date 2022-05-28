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

            List<ETTask> list = new List<ETTask>();
            foreach (var addItemAction in a.AddItemActions)
            {
                List<DiamondInfo> diamondInfos = addItemAction.DiamondInfos;
                HeroCard heroCard = heroCardComponent.GetChild<HeroCard>(addItemAction.HeroCardInfo.HeroId);
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
                list.Add(effect.PlayAnim());
                list.Add(heroCard.GetComponent<HeroCardInfoObjectComponent>().UpdateAngryView(addItemAction));
            }

            await ETTaskHelper.WaitAll(list);

            // heroCard.GetComponent<HeroModeObjectCompoent>().UpdateShowDataView(a.AddItemAction.HeroCardInfo);
            await ETTask.CompletedTask;
        }
    }

    public class PlayAddAttackViewAnimHandler: AEvent<EventType.PlayAddAttackViewAnim>
    {
        protected override async ETTask Run(PlayAddAttackViewAnim a)
        {
            Log.Debug("播放增加攻击力的动效");
            DiamondComponent diamondComponent = a.Scene.GetComponent<DiamondComponent>();
            HeroCardComponent heroCardComponent = a.Scene.GetComponent<HeroCardComponent>();

            List<ETTask> tasks = new List<ETTask>();

            foreach (var addItemAction in a.AddItemActions)
            {
                Log.Debug($"hero id {addItemAction.HeroCardInfo.HeroId}");
                List<DiamondInfo> diamondInfos = addItemAction.DiamondInfos;
                HeroCard heroCard = heroCardComponent.GetChild<HeroCard>(addItemAction.HeroCardInfo.HeroId);
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

                AddAngryEffect effect = heroCard.AddChild<AddAngryEffect, List<Vector3>, Vector3>(startPosList, endPos);
                tasks.Add(effect.PlayAnim());
                tasks.Add(heroCard.GetComponent<HeroCardInfoObjectComponent>().UpdateAttackAdditionView(addItemAction));
            }

            ETTaskHelper.WaitAll(tasks);
            await ETTask.CompletedTask;
        }
    }
}