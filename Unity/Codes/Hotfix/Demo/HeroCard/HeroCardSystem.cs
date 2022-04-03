using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace ET
{
    public class HeroCardAwakeSystem: AwakeSystem<HeroCard>
    {
        public override void Awake(HeroCard self)
        {
            for (int i = 0; i < 2; i++)
            {
                Skill skill = self.AddChild<Skill>();
                if (i == 0)
                {
                    skill.SkillType = SkillType.BigSkill;
                }
                else
                {
                    skill.SkillType = SkillType.NormalSkill;
                }
            }
        }
    }

    public class HeroCardUpdateSystem: UpdateSystem<HeroCard>
    {
        public override void Update(HeroCard self)
        {
        }
    }

    public static class HeroCardSystem
    {
        public static HeroCardInfo GetMessageInfo(this HeroCard self)
        {
            HeroCardInfo heroCardInfo = new HeroCardInfo()
            {
                HeroId = self.Id,
                HeroName = self.HeroName,
                ConfigId = self.ConfigId,
                OwnerId = self.OwnerId,
                TroopId = self.TroopId,
                InTroopIndex = self.InTroopIndex,
                CampIndex = self.CampIndex,
                HeroColor = self.HeroColor,
                CasrSkillId = self.CurrentSkillId,
                HP = self.HP
            };

            return heroCardInfo;
        }

        public static void SetMessageInfo(this HeroCard self, HeroCardInfo message)
        {
            self.Id = message.HeroId;
            self.HeroName = message.HeroName;
            self.OwnerId = message.OwnerId;
            self.ConfigId = message.ConfigId;
            self.InTroopIndex = message.InTroopIndex;
            self.TroopId = message.TroopId;
            self.CampIndex = message.CampIndex;
            self.HeroColor = message.HeroColor;
            self.CurrentSkillId = message.CasrSkillId;

            self.HP = HeroConfigCategory.Instance.Get(self.ConfigId).HeroHP;
        }

        public static void InitWithConfig(this HeroCard self, HeroConfig heroConfig, long id)
        {
            self.Id = id;
            self.Attack = heroConfig.Attack;
            self.Defence = heroConfig.Defence;
            self.HP = heroConfig.HeroHP;
            self.HeroName = heroConfig.HeroName;
            self.ConfigId = heroConfig.Id;
            self.HeroColor = heroConfig.HeroColor;

            // self.Id = heroConfig.Id;
        }

        //todo 增加攻击值
        public static float AddAttackValue(this HeroCard self, float baseValue)
        {
            Log.Debug($"add attack value {self.Id}");
            HeroConfig heroConfig = HeroConfigCategory.Instance.Get(self.ConfigId);
            var value = float.Parse(heroConfig.AttackRate) * baseValue;
            self.Attack += value;
            // #if !SERVER
            //             Game.EventSystem.Publish(new EventType.UpdateAttackView() { HeroCard = self });
            // #endif
            return value;
        }

        //todo 增加怒气值
        public static float AddAngryValue(this HeroCard self, float baseValue)
        {
            HeroConfig heroConfig = HeroConfigCategory.Instance.Get(self.ConfigId);
            var value = float.Parse(heroConfig.AngryRate) * baseValue;
            self.Angry += value;
            if (self.Angry > heroConfig.TotalAngry)
            {
                self.Angry = heroConfig.TotalAngry;
            }

            // #if !SERVER
            //             Game.EventSystem.Publish(new EventType.UpdateAngryView() { HeroCard = self });
            // #endif
            return value;
        }

        public static void EnterAttackState(this HeroCard self)
        {
            Log.Debug("enter attack state");
#if !SERVER
            Game.EventSystem.Publish(new EventType.EnterAttackStateView() { HeroCard = self });

#endif
        }

        public static async ETTask AttackTargetAsync(this HeroCard self, HeroCard target)
        {
            // // #if !SERVER
            //             await Game.EventSystem.PublishAsync(new EventType.PlayHeroCardAttackAnim() { Att = self, TargetHeroCard = target });
            // // #endif

            await ETTask.CompletedTask;
        }

        public static bool CheckAngryIsFull(this HeroCard self)
        {
            return self.Angry >= HeroConfigCategory.Instance.Get(self.ConfigId).TotalAngry;
        }

        public static bool GetIsDead(this HeroCard self)
        {
            return self.HP <= 0;
        }

        public static void BeAttack(this HeroCard self, HeroCard attackHeroCard)
        {
            float damage = attackHeroCard.Attack - self.Defence;
            self.HP -= damage;
            if (self.HP < 0)
            {
                self.HP = 0;
            }
        }

        // public static HeroCardInfo
        //todo 处理当前应该使用哪个技能 并返回技能id
        public static long ProcessCurrentSkill(this HeroCard self)
        {
            List<Skill> skills = self.GetChilds<Skill>();

            if (skills == null)
            {
                return 0;
            }

            foreach (var skill in skills)
            {
                if (self.CheckAngryIsFull())
                {
                    if (skill.SkillType == SkillType.BigSkill)
                    {
                        return skill.Id;
                    }
                }
            }

            foreach (var skill in skills)
            {
                if (skill.SkillType == SkillType.NormalSkill)
                {
                    return skill.Id;
                }
            }

            return 0;
        }
    }
}