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
            HeroCardComponent heroCardComponent = a.HeroCardComponent;
            ComboActionItem comboActionItem = a.ComboActionItem;
            foreach (var addItemAction in comboActionItem.AddAttackActions)
            {
                HeroCard heroCard = heroCardComponent.GetChild<HeroCard>(addItemAction.HeroCardDataComponentInfo.HeroId);
                heroCard.GetComponent<HeroCardInfoObjectComponent>().UpdateAttackAdditionView(addItemAction.HeroCardDataComponentInfo);
            }
            await ETTask.CompletedTask;
        }
    }

    public class UpdateHeroDataInfo: AEvent<EventType.UpdateHeroAngryInfoEvent>
    {
        
        protected override async ETTask Run(EventType.UpdateHeroAngryInfoEvent a)
        {
            // {
            HeroCardComponent heroCardComponent = a.HeroCardComponent;
            HeroCardDataComponentInfo heroCardDataComponentInfo = a.HeroCardDataComponentInfo;

            HeroCard heroCard = heroCardComponent.GetChild<HeroCard>(heroCardDataComponentInfo.HeroId);
            HeroCardInfoObjectComponent heroCardInfoObjectComponent = heroCard.GetComponent<HeroCardInfoObjectComponent>();
            heroCardInfoObjectComponent.UpdateAngryView(heroCardDataComponentInfo);

            await ETTask.CompletedTask;
        }
    }
}