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
            heroCard.GetComponent<HeroCardInfoObjectComponent>().UpdateAngryView(addItemAction.HeroCardDataComponentInfo);
            await ETTask.CompletedTask;
        }
    }

    public class AddHeroAttackHandler: AEvent<EventType.UpdateHeroAttackInfo>
    {
        protected override async ETTask Run(UpdateHeroAttackInfo a)
        {
            HeroCard heroCard = a.HeroCard;
            AddItemAction addItemAction = a.AddItemAction;
            HeroCardDataComponentInfo heroCardDataComponent = addItemAction.HeroCardDataComponentInfo;

            // var addition = heroCardDataComponent.DiamondAttackAddition;

            HeroCardInfoObjectComponent heroCardInfoObjectComponent = heroCard.GetComponent<HeroCardInfoObjectComponent>();


            heroCardInfoObjectComponent.UpdateAttackAdditionView(addItemAction);
            
            await ETTask.CompletedTask;
        }
    }

    public class UpdateHeroDataInfo: AEvent<EventType.UpdateHeroAngryInfo>
    {
        protected override async ETTask Run(UpdateHeroAngryInfo a)
        {
            HeroCard heroCard = a.HeroCard;
            HeroCardDataComponentInfo heroCardDataComponentInfo = a.HeroCardDataComponentInfo;
            HeroCardInfoObjectComponent heroCardInfoObjectComponent = heroCard.GetComponent<HeroCardInfoObjectComponent>();
            heroCardInfoObjectComponent.UpdateAngryView(heroCardDataComponentInfo);
            await ETTask.CompletedTask;
        }
    }
}