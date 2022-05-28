using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class HeroCardDataComponentAwakeSystem: AwakeSystem<HeroCardDataComponent>
    {
        public override void Awake(HeroCardDataComponent self)
        {
        }
    }

    public static class HeroCardDataComponentSystem
    {
        public static HeroCardDataComponentInfo GetInfo(this HeroCardDataComponent self)
        {
            return new HeroCardDataComponentInfo()
            {
                HP = self.HP,
                HeroAttack = self.HeroAttack,
                // DiamondAttack = self.DiamondAttack,
                DiamondAttackAddition = self.DiamondAttackAddition,
                WeaponAttack = self.WeaponAttack,
                SkillAttack = self.SkillAttack,
                NormalDamage = self.NormalDamage,
                CriticalDamage = self.CriticalDamage,
                ConfigId = self.GetParent<HeroCard>().ConfigId,
                Angry = self.Angry
            };
        }

        public static int GetHeroWeaponAttack(this HeroCardDataComponent self)
        {
            //获取英雄的装备伤害 
            return 0;
        }

        public static int GetWeapinCriticalRate(this HeroCardDataComponent self)
        {
            return 0;
        }

        public static int GetHeroSkillAttack(this HeroCardDataComponent self)
        {
            return 0;
        }

        public static int GetAttackBuff(this HeroCardDataComponent self)
        {
            //获取攻击buff

            return 0;
        }

        public static int GetHeroBaseHP(this HeroCardDataComponent self)
        {
            //获取英雄的基础血量
            HeroCard heroCard = self.GetParent<HeroCard>();
            HeroConfig config = HeroConfigCategory.Instance.Get(heroCard.ConfigId);
            var baseValue = config.HeroHP;
            var grow = config.HPGrowthCoefficient;
            return self.GetHeroBaseValue(grow, baseValue);
        }

        public static int GetHeroBaseDefence(this HeroCardDataComponent self)
        {
            //获取英雄的基础防御力
            HeroCard heroCard = self.GetParent<HeroCard>();
            HeroConfig config = HeroConfigCategory.Instance.Get(heroCard.ConfigId);
            var baseAttack = config.BaseDefence;
            var grow = config.DefenceGrowthCoefficient;
            return self.GetHeroBaseValue(grow, baseAttack);
        }

        public static int GetHeroBaseAttack(this HeroCardDataComponent self)
        {
            //计算英雄的基础伤害
            HeroCard heroCard = self.GetParent<HeroCard>();
            HeroConfig config = HeroConfigCategory.Instance.Get(heroCard.ConfigId);
            var baseAttack = config.BaseAttack;
            var growthCoefficient = config.AttackGrowthCoefficient;
            return self.GetHeroBaseValue(growthCoefficient, baseAttack);
        }

        public static int GetHeroBaseValue(this HeroCardDataComponent self, int growthCoefficient, int value)
        {
            //todo 获取英雄的基础属性值
            HeroCard heroCard = self.GetParent<HeroCard>();
            int level = heroCard.Level;
            int star = heroCard.Star;
            int rank = heroCard.Rank;
            // HeroConfig config = HeroConfigCategory.Instance.Get(heroCard.ConfigId);
            float baseValue = value;
            baseValue = baseValue * growthCoefficient / 2; //基础值
            baseValue = baseValue + baseValue * (rank) / 10; // 升阶后的成长值
            var levelValue = baseValue * (0.03f + growthCoefficient / 1000.0f) * (level + 1);
            baseValue = baseValue + levelValue; //升级后的成长值
            var starValue = growthCoefficient * 100 * (star); //升星后的成长值
            return (int)Mathf.Ceil(baseValue + starValue);
        }

        public static bool IsAngryFull(this HeroCardDataComponent self)
        {
            //todo 怒气值是否已满
            var configId = self.GetParent<HeroCard>().ConfigId;
            HeroConfig heroConfig = HeroConfigCategory.Instance.Get(configId);
            return self.Angry >= heroConfig.TotalAngry;
        }

        public static void MakeSureAngrySkill(this HeroCardDataComponent self)
        {
            //确定一下怒气值满的技能
            List<Skill> skills = self.Parent.GetChilds<Skill>();
            Skill skill = skills.Find(a =>
            {
                SkillConfig config = SkillConfigCategory.Instance.Get(a.ConfigId);
                if (config.SkillType == 4)
                {
                    return true;
                }

                return false;
            });
            self.CurrentSkillId = skill.Id;
        }

        public static void MakeSureSkill(this HeroCardDataComponent self, CrashCommonInfo crashCommonInfo)
        {
            //todo 确定当前技能
            var firstCrashCount = crashCommonInfo.FirstCrashCount;
            firstCrashCount -= 3;
            if (firstCrashCount < 0)
            {
                firstCrashCount = 0;
            }

            if (firstCrashCount > 3)
            {
                firstCrashCount = 3;
            }

            List<Skill> skills = self.Parent.GetChilds<Skill>();
            Skill skill = skills.Find(a =>
            {
                SkillConfig config = SkillConfigCategory.Instance.Get(a.ConfigId);
                if (config.SkillType == (firstCrashCount + 1))
                {
                    return true;
                }

                return false;
            });
            self.CurrentSkillId = skill.Id;
        }
    }
}