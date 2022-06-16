using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using ET.Account;
using UnityEngine;

namespace ET
{
    public class HeroCardComponentAwakeSystem: AwakeSystem<HeroCardComponent>
    {
        public override void Awake(HeroCardComponent self)
        {
            // Log.Debug("Hero card component awake");
        }
    }

    public static class HeroCardComponentSystem
    {
        public static void InitHeroCard(this HeroCardComponent self, M2C_CreateHeroCardInRoom message)
        {
            List<HeroCardInfo> heroCardInfos = message.HeroCardInfos;
            List<HeroCardDataComponentInfo> heroCardDataComponentInfos = message.HeroCardDataComponentInfos;
            Log.Debug($"hero card infos {heroCardInfos.Count}");
            for (int i = 0; i < heroCardInfos.Count; i++)
            {
                HeroCard heroCard = new HeroCard();
                heroCard.SetMessageInfo(heroCardInfos[i]);
                self.AddChild(heroCard);
                Game.EventSystem.PublishAsync(new EventType.CreateOneHeroCardView()
                {
                    HeroCard = heroCard, HeroCardInfo = heroCardInfos[i], HeroCardDataComponentInfo = heroCardDataComponentInfos[i]
                });
            }
            //todo 同步给显示层
        }

        public static List<HeroCard> GetHeroCardsByCampIndex(this HeroCardComponent self, int campIndex)
        {
            List<HeroCard> heroCards = new List<HeroCard>();
            foreach (var heroCard in self.HeroCards)
            {
                if (heroCard.CampIndex.Equals(campIndex))
                {
                    heroCards.Add(heroCard);
                }
            }

            // C2M_GameReadyMessage
            //根据座位 号进行排序
            heroCards.Sort((a, b) => a.InTroopIndex - b.InTroopIndex);
            foreach (var heroCard in heroCards)
            {
                Log.Debug($"hero card in troop index {heroCard.InTroopIndex}");
            }

            return heroCards;
        }

        public static void SyncHeroCardTurnData(this HeroCardComponent self, M2C_SyncHeroCardTurnData m2CSyncHeroCardTurnData)
        {
            foreach (var heroCardInfo in m2CSyncHeroCardTurnData.HeroCardInfos)
            {
                var heroCard = self.GetChild<HeroCard>(heroCardInfo.HeroId);
                // heroCard.Attack = heroCard.Attack;
                // heroCard.Attack = heroCardInfo.Attack;
                // heroCard.DiamondAttack = heroCardInfo.DiamondAttack;
                Game.EventSystem.Publish(new EventType.UpdateAttackView() { HeroCard = heroCard });
            }
        }
    }
}