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
            // Log.Debug($" create hero card {a.HeroCardInfo.Attack}");
            //第一步加载卡牌资源 
            HeroCard heroCard = a.HeroCard;
            heroCard.AddComponent<HeroCardInfoObjectComponent, int>(heroCard.ConfigId);
            heroCard.AddComponent<HeroModeObjectCompoent, HeroCard>(heroCard);
            await ETTask.CompletedTask;
        }
    }

    // public class DestroyHeroCardEventHandler: AEvent<EventType.DestroyHeroCard>
    // {
    //     protected override async ETTask Run(DestroyHeroCard a)
    //     {
    //         // Log.Debug("销毁英雄");
    //         HeroCardInfoObjectComponent component = a.HeroCard.GetComponent<HeroCardInfoObjectComponent>();
    //         component.Dispose();
    //         await ETTask.CompletedTask;
    //
    //     }
    // }
}