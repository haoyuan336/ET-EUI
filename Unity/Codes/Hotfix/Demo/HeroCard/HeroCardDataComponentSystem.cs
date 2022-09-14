using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class HeroCardDataComponentAwakeSystem1: AwakeSystem<HeroCardDataComponent>
    {
        public override void Awake(HeroCardDataComponent self)
        {
            // self.HP = 0;
            self.HP = self.GetHeroTotalHP();
            self.TotalHP = self.HP;
            HeroCard parent = self.GetParent<HeroCard>();
            var configId = parent.ConfigId;
            HeroConfig config = HeroConfigCategory.Instance.Get(configId);
            Log.Debug($"config {config.HeroName}");
            Log.Debug($"total hp {self.HP}");
            self.Angry = 0;
            self.DiamondAttackAddition = 0;
        }
    }

    public static class HeroCardDataComponentSystem
    {
        public static HeroCardDataComponentInfo GetInfo(this HeroCardDataComponent self)
        {
            Skill skill = null;
#if SERVER
            skill = self.GetParent<HeroCard>().GetComponent<SkillComponent>().GetChild<Skill>(self.CurrentSkillId);
#endif
            var info = new HeroCardDataComponentInfo()
            {
                HeroId = self.GetParent<HeroCard>().Id,
                HP = self.HP,
                TotalHP = self.TotalHP,
                DiamondAttackAddition = self.DiamondAttackAddition,
                IsCritical = self.IsCritical,
                Damage = self.Damage,
                ConfigId = self.GetParent<HeroCard>().ConfigId,
                Angry = self.Angry,
                CurrentSkillId = self.CurrentSkillId,
                CurrentSkillInfo = skill?.GetMessageInfo(),
                AddAngry = self.AddAngry,
                AddHP = self.AddHP,
                SubAngry = self.SubAngry
            };

            self.AddAngry = 0;
            self.AddHP = 0;
            self.SubAngry = 0;
            
            return info;
        }

        // public static void AddAngry(this HeroCardDataComponent self, int angry)
        // {
        //     //增加的怒气值
        //     self.Angry += angry;
        //     self.AddAngry = angry;
        // }

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

        public static int GetHeroTotalHP(this HeroCardDataComponent self)
        {
            HeroCard heroCard = self.GetParent<HeroCard>();
            Log.Debug($"GetHeroTotalHP hero card ");
            var baseHP = HeroHelper.GetHeroBaseHP(heroCard.GetMessageInfo());
            var weaponHP = heroCard.GetWeaponBaseValueByType(WordBarType.HP);
            var weaponHPAddition = heroCard.GetWeaponBaseValueByType(WordBarType.HPAddition);
            var totalHp = (baseHP + weaponHP) * (1.0f + (float)weaponHPAddition / 10000);
            return (int)totalHp;
            // return 0;
        }

        public static int GetHeroBaseDefence(this HeroCardDataComponent self)
        {
            //获取英雄的基础防御力
            return HeroHelper.GetHeroBaseDefence(self.GetParent<HeroCard>().GetMessageInfo());
        }

        public static float GetHeroPlaneAttack(this HeroCardDataComponent self)
        {
            //获取英雄的面板攻击力
            float baseAttack = self.GetHeroBaseAttack(); //角色的基础攻击
            float weaponAttack = self.GetParent<HeroCard>().GetWeaponBaseValueByType(WordBarType.Attack); // 装备的基础攻击
            float weaponAttackAddition = self.GetParent<HeroCard>().GetWeaponBaseValueByType(WordBarType.AttackAddition); //装备的攻击力加成
            float planeAttack = (baseAttack + weaponAttack) * (1 + weaponAttackAddition / 10000); //面板攻击力
            return planeAttack;
        }

        public static float GetHeroPlaneDefence(this HeroCardDataComponent self)
        {
            float baseDefence = self.GetHeroBaseDefence(); //被攻击对象的基础防御力
            float weaponDefence = self.GetParent<HeroCard>().GetWeaponBaseValueByType(WordBarType.Defecnce); //被攻击对象的装备防御力
            float weaponDefecceAddition = self.GetParent<HeroCard>().GetWeaponBaseValueByType(WordBarType.DefenceAddition); //被攻击对象的装备防御力加成
            float planeDefence = (baseDefence + weaponDefence) * (1 + weaponDefecceAddition / 10000); //被攻击对象的面板防御力
            return planeDefence;
        }

        public static int GetHeroBaseAttack(this HeroCardDataComponent self)
        {
            //计算英雄的基础伤害
            return HeroHelper.GetHeroBaseAttack(self.GetParent<HeroCard>().GetMessageInfo());
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
            baseValue += baseValue * (rank) / 10; // 升阶后的成长值
            var levelValue = baseValue * (0.03f + growthCoefficient / 1000.0f) * (level + 1);
            baseValue += levelValue; //升级后的成长值
            var starValue = growthCoefficient * 100 * (star); //升星后的成长值
            return (int)Mathf.Ceil(baseValue + starValue);
        }

        public static bool IsAngryFull(this HeroCardDataComponent self)
        {
            //todo 怒气值是否已满
            var configId = self.GetParent<HeroCard>().ConfigId;
            HeroConfig heroConfig = HeroConfigCategory.Instance.Get(configId);
            return self.Angry >= heroConfig.TotalAngry && self.HP > 0;
        }

        public static Skill MakeSureAngrySkill(this HeroCardDataComponent self)
        {
            if (!self.IsAngryFull())
            {
                return null;
            }

            Skill skill = self.Parent.GetComponent<SkillComponent>().MakeSureAngrySkill();
            self.CurrentSkillId = skill.Id;
            self.Angry = 0;
            return skill;
        }

        public static Skill MakeSureSkill(this HeroCardDataComponent self, int firstCrashCount)
        {
            Skill skill = self.Parent.GetComponent<SkillComponent>().MakeSureSkill(firstCrashCount);
            //todo 确定当前技能
            self.CurrentSkillId = skill.Id;
            return skill;
        }
    }
}