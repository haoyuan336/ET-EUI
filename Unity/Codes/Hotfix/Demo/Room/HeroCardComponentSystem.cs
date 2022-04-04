using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ET.Account;
using UnityEngine;

namespace ET
{
    public static class HeroCardComponentSystem
    {
        public static void InitHeroCard(this HeroCardComponent self, M2C_CreateHeroCardInRoom message)
        {
            List<HeroCardInfo> heroCardInfos = message.HeroCardInfos;
            List<SkillInfo> skillInfos = message.SkillInfos;
            Dictionary<int, List<HeroCard>> heroCardListMap = new Dictionary<int, List<HeroCard>>();

            foreach (var heroCardInfo in heroCardInfos)
            {
                HeroCard heroCard = self.AddChildWithId<HeroCard, int>(heroCardInfo.HeroId, heroCardInfo.ConfigId);
                heroCard.SetMessageInfo(heroCardInfo);
                self.AddHeroCardSkillByList(heroCard, skillInfos);
                // heroCard.InitHeroWithDBData(heroCard);
                if (!heroCardListMap.ContainsKey(heroCard.CampIndex))
                {
                    // heroCardListMap[heroCard.CampIndex] = new List<HeroCard>();
                    List<HeroCard> heroCards = new List<HeroCard>();

                    heroCards.Add(heroCard);
                    heroCardListMap.Add(heroCard.CampIndex, heroCards);
                }
                else
                {
                    heroCardListMap[heroCard.CampIndex].Add(heroCard);
                }

                self.HeroCards.Add(heroCard);
            }

            //todo 同步给显示层
            Game.EventSystem.Publish(new EventType.CreateHeroCardView() { HeroCardListMap = heroCardListMap });
        }

        public static void AddHeroCardSkillByList(this HeroCardComponent self, HeroCard heroCard, List<SkillInfo> skillInfos)
        {
            foreach (var skillInfo in skillInfos)
            {
                if (skillInfo.OwnerId.Equals(heroCard.Id))
                {
                    Log.Debug($"add skill id {skillInfo.SkillId}");
                    Skill skill = heroCard.AddChildWithId<Skill, int>(skillInfo.SkillId, skillInfo.SkillConfigId);
                    skill.SetMessageInfo(skillInfo);
                }
            }
        }

        public static async ETTask PlayHeroCardAttackAnimAsync(this HeroCardComponent self, AttackAction action)
        {
            Log.Debug("PlayHeroCardAttackAnimAsync");
            HeroCard attackHeroCard = self.GetChild<HeroCard>(action.AttackHeroCardInfo.HeroId);
            // attackHeroCard.SetMessageInfo(action.AttackHeroCardInfo);
            attackHeroCard.CurrentSkillId = action.AttackHeroCardInfo.CastSkillId;
            HeroCard beAttackHeroCard = self.GetChild<HeroCard>(action.BeAttackHeroCardInfo[0].HeroId);
            // beAttackHeroCard.SetMessageInfo(action.BeAttackHeroCardInfo[0]);
            beAttackHeroCard.HP = action.BeAttackHeroCardInfo[0].HP;
            
            Log.Debug($"be attack hero card hp {beAttackHeroCard.HP}");
            // beAttackHeroCard.GetComponent<Herocardb>()

            await Game.EventSystem.PublishAsync(new EventType.PlayHeroCardAttackAnim()
            {
                AttackHeroCard = attackHeroCard, BeAttackHeroCard = beAttackHeroCard
            });
            await ETTask.CompletedTask;
        }

        public static HeroCard GetAttackTargetByTroopIndex(this HeroCardComponent self, int inTroopIndex, List<HeroCard> heroCards)
        {
            bool isAllDead = true;
            foreach (var heroCard in heroCards)
            {
                if (heroCard.IsDead != 1)
                {
                    isAllDead = false;
                    break;
                }
            }

            if (isAllDead)
            {
                return null;
            }

            while (true)
            {
                if (inTroopIndex < heroCards.Count)
                {
                    HeroCard heroCard = heroCards[inTroopIndex];
                    if (heroCard.IsDead == 0)
                    {
                        return heroCard;
                    }

                    inTroopIndex++;
                }
                else
                {
                    inTroopIndex = 0;
                }
            }
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

            //根据座位 号进行排序
            heroCards.Sort((a, b) => a.InTroopIndex - b.InTroopIndex);
            foreach (var heroCard in heroCards)
            {
                Log.Debug($"hero card in troop index {heroCard.InTroopIndex}");
            }

            return heroCards;
        }
    }
}