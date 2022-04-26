using System.Collections.Generic;
using System.Linq;

namespace ET
{
    public class HeroCardAwakeSystem: AwakeSystem<HeroCard, int>
    {
        public override void Awake(HeroCard self, int configId)
        {
            self.InitWithConfig(HeroConfigCategory.Instance.Get(configId));
        }
    }

    public class HeroCardAwakeSystem1: AwakeSystem<HeroCard, HeroCardInfo>
    {
        public override void Awake(HeroCard self, HeroCardInfo b)
        {
            Log.Debug("hero card awake");

            self.ConfigId = b.ConfigId;
            self.CampIndex = b.CampIndex;
            self.InTroopIndex = b.InTroopIndex;

            Game.EventSystem.Publish(new EventType.CreateOneHeroCardView() { HeroCard = self });
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

        public static void InitHeroSkillWithConfig(this HeroCard self)
        {
            HeroConfig heroConfig = HeroConfigCategory.Instance.Get(self.ConfigId);
            string[] skillStrList = heroConfig.SkillIdList.Split(',').ToArray();
            foreach (var str in skillStrList)
            {
                Skill skill = self.AddChild<Skill, int>(int.Parse(str));
                skill.OwnerId = self.Id;
            }
        }

        public static async ETTask InitHeroWithDBData(this HeroCard self, HeroCard dbHeroCard)
        {
            // self = dbHeroCard;
            // Log.Debug($"self in troop index {self.InTroopIndex}");
            self.SetMessageInfo(dbHeroCard.GetMessageInfo());
            //todo  init skill info 初始化 技能信息
#if SERVER
            List<Skill> skills = await DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Query<Skill>(a => a.OwnerId.Equals(self.Id));
            foreach (var skill in skills)
            {
                Skill sk = self.AddChildWithId<Skill, int>(skill.Id, skill.ConfigId);
                sk.InitSkillWithDBData(skill);
            }
#endif

            await ETTask.CompletedTask;
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
                    if (skill.SkillType == (int) SkillType.BigSkill)
                    {
                        self.Angry = 0;
                        return skill.Id;
                    }
                }
            }

            foreach (var skill in skills)
            {
                if (skill.SkillType == (int) SkillType.NormalSkill)
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