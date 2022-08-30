using System.Collections.Generic;
using ET.EventType;
using UnityEngine;

// namespace ET
// {
//     public class GameAroundOverHandler: AEvent<EventType.GameAroundOver>
//     {
//         protected override async  ETTask Run(GameAroundOver a)
//         {
//             List<HeroCard> heroCards = a.HeroCards;
//             Game.EventSystem.Publish(new EventType.SetHeroCardChooseState(){AllHeroCard = heroCards,Show = false});
//             await ETTask.CompletedTask;
//         }
//     }
// }