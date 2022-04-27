using System;
using System.Collections.Generic;
using System.Linq;
using ET.EventType;
using UnityEngine;

namespace ET
{
    public class CreateHeroCardEventHandler: AEvent<EventType.CreateOneHeroCardView>
    {
        protected override async ETTask Run(CreateOneHeroCardView a)
        {
            //第一步加载卡牌资源 
            HeroCard heroCard = a.HeroCard;
            heroCard.AddComponent<HeroCardObjectComponent, HeroCard>(heroCard);
            heroCard.AddComponent<HeroModeObjectCompoent, HeroCard>(heroCard);
            heroCard.AddComponent<HeroCardView>();
            await ETTask.CompletedTask;
        }
    }
}