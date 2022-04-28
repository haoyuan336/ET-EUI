using System.Collections.Generic;
using System.Linq;

namespace ET
{
    public class HeroCardAwakeSystem: AwakeSystem<HeroCard, int>
    {
        public override void Awake(HeroCard self, int configId)
        {
            self.InitWithConfig(HeroConfigCategory.Instance.Get(configId));

            var skillsStr = HeroConfigCategory.Instance.Get(configId).SkillIdList;
            var skillStrList = skillsStr.Split(',').ToList();
            foreach (var skillStr in skillStrList)
            {
                self.AddChild<Skill, int>(int.Parse(skillStr));
            }
        }
    }

    public class HeroCardAwakeSystem2: AwakeSystem<HeroCard, HeroCardInfo, List<Skill>>
    {
        public override void Awake(HeroCard self, HeroCardInfo a, List<Skill> b)
        {
        }
    }

    public class HeroCardAwakeSystem3: AwakeSystem<HeroCard, HeroCard>
    {
        public override void Awake(HeroCard self, HeroCard a)
        {
            self.HeroName = a.HeroName;
            self.Level = a.Level;
            self.ConfigId = a.ConfigId;
            self.InTroopIndex = a.InTroopIndex;
            self.CampIndex = a.CampIndex;
            // List<Skill> skills = a.GetChilds<Skill>();
            // foreach (var skill in skills)
            // {
            //     self.AddChild(skill);
            // }
        }
    }

    public class HeroCardAwakeSystem1: AwakeSystem<HeroCard, HeroCardInfo>
    {
        public override void Awake(HeroCard self, HeroCardInfo b)
        {
            Log.Debug($"hero card awake{b.SkillInfos.Count}");

            self.ConfigId = b.ConfigId;
            self.CampIndex = b.CampIndex;
            self.InTroopIndex = b.InTroopIndex;
            self.Attack = b.Attack;
            self.Angry = b.Angry;
            self.HP = b.HP;
            self.DiamondAttack = b.DiamondAttack;

            List<SkillInfo> skillInfos = b.SkillInfos;
            foreach (var skillInfo in skillInfos)
            {
                Log.Debug($"init skill child {skillInfo.SkillId}");
                self.AddChildWithId<Skill, SkillInfo>(skillInfo.SkillId, skillInfo);
            }
#if !SERVER
            Game.EventSystem.Publish(new EventType.CreateOneHeroCardView() { HeroCard = self });
#endif
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
        public static async ETTask PlayHeroCardAttackAnimAsync(this HeroCard self, AttackAction action)
        {
            Log.Debug("PlayHeroCardAttackAnimAsync");
            // HeroCard attackHeroCard = self.GetChild<HeroCard>(action.AttackHeroCardInfo.HeroId);
            self.Angry = action.AttackHeroCardInfo.Angry;
            self.CurrentSkillId = action.AttackHeroCardInfo.CastSkillId;
            HeroCard beAttackHeroCard = self.Parent.GetChild<HeroCard>(action.BeAttackHeroCardInfo[0].HeroId);
            beAttackHeroCard.HP = action.BeAttackHeroCardInfo[0].HP;
            beAttackHeroCard.Angry = action.BeAttackHeroCardInfo[0].Angry;
#if !SERVER
            await Game.EventSystem.PublishAsync(new EventType.PlayHeroCardAttackAnim()
            {
                AttackHeroCard = self, BeAttackHeroCard = beAttackHeroCard
            });
#endif
            await ETTask.CompletedTask;
        }

        public static async ETTask Call(this HeroCard self, int zone, long ownerId)
        {
            self.OwnerId = ownerId;
            //todo 先创建继承数据
            await self.CallSkill(zone);
#if SERVER
            await DBManagerComponent.Instance.GetZoneDB(zone).Save(self);
#endif
            Log.Debug("hero call complete");
        }

        public static async ETTask CallSkill(this HeroCard self, int zone)
        {
            HeroConfig heroConfig = HeroConfigCategory.Instance.Get(self.ConfigId);
            List<string> skillStr = heroConfig.SkillIdList.Split(',').ToList();
            List<ETTask> tasks = new List<ETTask>();
            foreach (var skillId in skillStr)
            {
                Skill skill = self.AddChild<Skill, int>(int.Parse(skillId));
                tasks.Add(skill.Call(zone, self.Id));
            }

            await ETTaskHelper.WaitAll(tasks);
            Log.Debug("all skill call complete");
            await ETTask.CompletedTask;
        }

        public static HeroCardInfo GetMessageInfo(this HeroCard self)
        {
            List<Skill> skills = self.GetChilds<Skill>();
            List<SkillInfo> skillInfos = new List<SkillInfo>();
            if (skills != null && skills.Count != 0)
            {
                Log.Debug($"hero card info get message info {skills.Count}");
                foreach (var skill in skills)
                {
                    skillInfos.Add(skill.GetMessageInfo());
                }
            }

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
                CastSkillId = self.CurrentSkillId,
                Attack = self.Attack,
                DiamondAttack = self.DiamondAttack,
                Angry = self.Angry,
                HP = self.HP,
                Defence = self.Defence,
                Level = self.Level == 0? 1 : self.Level,
                SkillInfos = skillInfos,
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
            self.CurrentSkillId = message.CastSkillId;
            self.Attack = message.Attack;
            self.DiamondAttack = message.DiamondAttack;
            self.Angry = message.Angry;
            self.HP = message.HP;
            self.Defence = message.Defence;
            self.Level = message.Level == 0? 1 : message.Level;
            Log.Debug($"set message info {self.Level}");
        }

        public static void InitWithConfig(this HeroCard self, HeroConfig heroConfig)
        {
            self.Attack = heroConfig.BaseAttack;
            self.Defence = heroConfig.BaseDefence;
            self.HP = heroConfig.HeroHP;
            self.HeroName = heroConfig.HeroName;
            self.ConfigId = heroConfig.Id;
            self.HeroColor = heroConfig.HeroColor;
        }

        //todo 增加攻击值
        public static float AddAttackValue(this HeroCard self, float baseValue)
        {
            Log.Debug($"add attack value {self.Id}");
            HeroConfig heroConfig = HeroConfigCategory.Instance.Get(self.ConfigId);
            var value = float.Parse(heroConfig.AttackRate) * baseValue;
            // self.Attack += value;
            self.DiamondAttack += value;
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
            // Log.Debug($"self angry {self.Angry}");
            // Log.Debug($"total angry {HeroConfigCategory.Instance.Get(self.ConfigId).TotalAngry}");
            return self.Angry >= HeroConfigCategory.Instance.Get(self.ConfigId).TotalAngry;
        }

        public static bool GetIsDead(this HeroCard self)
        {
            return self.HP <= 0;
        }

        public static void BeAttack(this HeroCard self, HeroCard attackHeroCard)
        {
            float attack = attackHeroCard.Attack + attackHeroCard.DiamondAttack;
            float damage = attack - self.Defence;
            self.AddAngryValue(damage);
            self.HP -= damage;
            if (self.HP < 0)
            {
                self.HP = 0;
            }
        }

        // public static async ETTask UpdateHPView(this HeroCard self)
        // {
        //     await ETTask.CompletedTask;
        //
        // }
        public static void InitTurnGame(this HeroCard self)
        {
            // self.Attack = 0;
            self.DiamondAttack = 0;
        }

        // public static HeroCardInfo
        //todo 处理当前应该使用哪个技能 并返回技能id
        public static long ProcessCurrentSkill(this HeroCard self)
        {
            List<Skill> skills = self.GetChilds<Skill>();

            if (skills == null)
            {
                Log.Warning("not skills ");
                return 0;
            }

            foreach (var skill in skills)
            {
                if (self.CheckAngryIsFull())
                {
                    if (SkillConfigCategory.Instance.Get(skill.ConfigId).SkillType == (int) SkillType.BigSkill)
                    {
                        self.Angry = 0;
                        return skill.Id;
                    }
                }
            }

            foreach (var skill in skills)
            {
                if (SkillConfigCategory.Instance.Get(skill.ConfigId).SkillType == (int) SkillType.NormalSkill)
                {
                    return skill.Id;
                }
            }

            // foreach (var skill in skills)
            // {
            //     Log.Warning(skill.Id.ToString());
            //     Log.Warning(skill.SkillType.ToString());
            // }
            //
            // Log.Warning("not find skill");
            return 0;
        }
    }
}