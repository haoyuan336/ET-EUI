using System;
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
            
            Log.Debug("播放增加怒气值的动效");
            Vector3 startPos = a.StartPos;
            DiamondInfo diamondInfo = a.DiamondInfo;
            AddItemAction addItemAction = a.AddItemAction;
            HeroCard heroCard = a.HeroCard;
            //
            var endPos = heroCard.GetComponent<HeroModeObjectCompoent>().HeroMode.transform.position + Vector3.up;
            AddAngryEffect effect = heroCard.AddChild<AddAngryEffect, Vector3, DiamondInfo, Vector3>(startPos, diamondInfo, endPos);
            await effect.PlayAnim();
            await heroCard.GetComponent<HeroCardInfoObjectComponent>().UpdateAngryView(addItemAction.HeroCardDataComponentInfo);
            // await heroCard.GetComponent<HeroCardInfoObjectComponent>().UpdateAttackAdditionView(addItemAction);
            // AddAngryEffect effect = heroCard.AddChild<AddAngryEffect, List<Vector3>, Vector3, DiamondInfo, int>(startPosList, endPos, diamondInfos[0],0);
            //     list.Add(effect.PlayAnim());
            //     list.Add(heroCard.GetComponent<HeroCardInfoObjectComponent>().UpdateAngryView(addItemAction.HeroCardDataComponentInfo));

            // DiamondComponent diamondComponent = a.Scene.GetComponent<DiamondComponent>();
            // HeroCardComponent heroCardComponent = a.Scene.GetComponent<HeroCardComponent>();
            //
            // // var count = 0;
            // List<ETTask> list = new List<ETTask>();
            // foreach (var addItemAction in a.AddItemActions)
            // {
            //     List<DiamondInfo> diamondInfos = addItemAction.DiamondInfos;
            //     Log.Debug($"add angry view anim {addItemAction.HeroCardInfo.HeroId}");
            //     HeroCard heroCard = heroCardComponent.GetChild<HeroCard>(addItemAction.HeroCardInfo.HeroId);
            //     Vector3 endPos = heroCard.GetComponent<HeroModeObjectCompoent>().HeroMode.transform.position + Vector3.up;
            //     // List<Diamond> diamonds = new List<Diamond>();
            //     // await TimerComponent.Instance.WaitAsync(100 * count);
            //     // count++;
            //
            //     List<Vector3> startPosList = new List<Vector3>();
            //     foreach (var info in diamondInfos)
            //     {
            //         Diamond diamond = diamondComponent.GetChild<Diamond>(info.Id);
            //         // diamonds.Add(diamond);
            //         GameObject diamondObj = diamond.GetComponent<GameObjectComponent>().GameObject;
            //         startPosList.Add(diamondObj.transform.position + Vector3.up * 0.5f);
            //     }
            //     
            //     Log.Warning($"找到了 消除的几个宝石{startPosList.Count}");
            //     AddAngryEffect effect = heroCard.AddChild<AddAngryEffect, List<Vector3>, Vector3, DiamondInfo, int>(startPosList, endPos, diamondInfos[0],0);
            //     list.Add(effect.PlayAnim());
            //     list.Add(heroCard.GetComponent<HeroCardInfoObjectComponent>().UpdateAngryView(addItemAction.HeroCardDataComponentInfo));
            // }
            //
            // await ETTaskHelper.WaitAll(list);
            //
            // // heroCard.GetComponent<HeroModeObjectCompoent>().UpdateShowDataView(a.AddItemAction.HeroCardInfo);
            await ETTask.CompletedTask;
        }
    }

    // public class PlayAddAttackViewAnimHandler: AEvent<EventType.PlayAddAttackViewAnim>
    // {
    //     protected override async ETTask Run(PlayAddAttackViewAnim a)
    //     {
    //         Log.Debug("播放增加攻击力的动效");
    //         DiamondComponent diamondComponent = a.Scene.GetComponent<DiamondComponent>();
    //         HeroCardComponent heroCardComponent = a.Scene.GetComponent<HeroCardComponent>();
    //
    //         List<ETTask> tasks = new List<ETTask>();
    //         // var count = 0;
    //         foreach (var addItemAction in a.AddItemActions)
    //         {
    //             Log.Debug($"hero id {addItemAction.HeroCardInfo.HeroId}");
    //             List<DiamondInfo> diamondInfos = addItemAction.DiamondInfos;
    //             HeroCard heroCard = heroCardComponent.GetChild<HeroCard>(addItemAction.HeroCardInfo.HeroId);
    //             Vector3 endPos = heroCard.GetComponent<HeroModeObjectCompoent>().HeroMode.transform.position + Vector3.up;
    //             // List<Diamond> diamonds = new List<Diamond>();
    //             List<Vector3> startPosList = new List<Vector3>();
    //             // await TimerComponent.Instance.WaitAsync(100 * count);
    //             Log.Debug($"diomondinfo {diamondInfos.Count}");
    //             if (diamondInfos.Count == 0)
    //             {
    //                 continue;
    //             }
    //
    //             foreach (var info in diamondInfos)
    //             {
    //                 Diamond diamond = diamondComponent.GetChild<Diamond>(info.Id);
    //                 // diamonds.Add(diamond);
    //                 GameObject diamondObj = diamond.GetComponent<GameObjectComponent>().GameObject;
    //                 startPosList.Add(diamondObj.transform.position + Vector3.up * 0.5f);
    //             }
    //
    //             AddAttackEffect effect =
    //                     heroCard.AddChild<AddAttackEffect, List<Vector3>, Vector3, DiamondInfo>(startPosList, endPos, diamondInfos[0]);
    //             tasks.Add(effect.PlayAnim());
    //             tasks.Add(heroCard.GetComponent<HeroCardInfoObjectComponent>().UpdateAttackAdditionView(addItemAction));
    //             // count++;
    //         }
    //
    //         await ETTaskHelper.WaitAll(tasks);
    //         await ETTask.CompletedTask;
    //     }
    // }
}