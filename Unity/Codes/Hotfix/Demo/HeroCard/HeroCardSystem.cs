using System.Collections.Generic;
using System.Linq;

namespace ET
{
    public class HeroCardAwakeSystem: AwakeSystem<HeroCard, int>
    {
        public override void Awake(HeroCard self, int configId)
        {
            // self.InitWithConfig(HeroConfigCategory.Instance.Get(configId));
            // self.Attack = heroConfig.BaseAttack;
            // self.Defence = heroConfig.BaseDefence;
            // self.HP = heroConfig.HeroHP;
            // self.HeroName = heroConfig.HeroName;
            // self.ConfigId = heroConfig.Id;
            // self.HeroColor = heroConfig.HeroColor;
            self.ConfigId = configId;
            self.HP = HeroConfigCategory.Instance.Get(configId).HeroHP;
            self.HeroName = HeroConfigCategory.Instance.Get(configId).HeroName;
            self.HeroColor = HeroConfigCategory.Instance.Get(configId).HeroColor;
            var skillsStr = HeroConfigCategory.Instance.Get(configId).SkillIdList;
            var skillStrList = skillsStr.Split(',').ToList();
            foreach (var skillStr in skillStrList)
            {
                self.AddChild<Skill, int>(int.Parse(skillStr));
            }
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
            self.Level = b.Level;
            self.HP = b.HP;
            self.HeroColor = b.HeroColor;

            List<SkillInfo> skillInfos = b.SkillInfos;
            foreach (var skillInfo in skillInfos)
            {
                Log.Debug($"init skill child {skillInfo.SkillId}");
                self.AddChildWithId<Skill, SkillInfo>(skillInfo.SkillId, skillInfo);
            }
#if !SERVER
            Game.EventSystem.Publish(new EventType.CreateOneHeroCardView() { HeroCard = self, HeroCardInfo = b });
#endif
        }
    }

    public class HeroCardAwakeSystem2: AwakeSystem<HeroCard, EnemyHeroConfig>
    {
        public override void Awake(HeroCard self, EnemyHeroConfig a)
        {
            self.Level = a.Level == 0? 1 : a.Level;
            self.ConfigId = a.ConfigId;
            self.HeroName = a.HeroName;
            self.HP = HeroConfigCategory.Instance.Get(self.ConfigId).HeroHP + HeroUpdateLevelConfigCategory.Instance.Get(self.Level).BaseHP;
            // self.HP = 
            // self.HP = 

            self.HeroColor = HeroConfigCategory.Instance.Get(self.ConfigId).HeroColor;
            var skillConfigStr = HeroConfigCategory.Instance.Get(self.ConfigId).SkillIdList;
            var skillConfigStrList = skillConfigStr.Split(',');
            foreach (var skill in skillConfigStrList)
            {
                self.AddChild<Skill, int>(int.Parse(skill));
            }
        }
    }

    public class HeroCardAwakeSystem3: AwakeSystem<HeroCard, HeroCard, List<Skill>>
    {
        public override void Awake(HeroCard self, HeroCard a, List<Skill> b)
        {
            self.Level = a.Level == 0? 1 : a.Level;
            self.ConfigId = a.ConfigId;
            self.HeroName = a.HeroName;
            self.CampIndex = a.CampIndex;
            self.InTroopIndex = a.InTroopIndex;
            self.HeroColor = a.HeroColor;
            self.HP = HeroConfigCategory.Instance.Get(self.ConfigId).HeroHP + HeroUpdateLevelConfigCategory.Instance.Get(self.Level).BaseHP;
            foreach (var skill in b)
            {
                self.AddChild(skill);
            }
        }
    }

    // public class HeroCardAwakeSystem3: AwakeSystem<HeroCard, HeroCard>
    // {
    //     public override void Awake(HeroCard self, HeroCard a)
    //     {
    //         self.HeroName = a.HeroName;
    //         self.Level = a.Level;
    //         self.ConfigId = a.ConfigId;
    //         self.InTroopIndex = a.InTroopIndex;
    //         self.CampIndex = a.CampIndex;
    //         // List<Skill> skills = a.GetChilds<Skill>();
    //         // foreach (var skill in skills)
    //         // {
    //         //     self.AddChild(skill);
    //         // }
    //     }
    // }

    // public class HeroCardUpdateSystem: UpdateSystem<HeroCard>
    // {
    //     public override void Update(HeroCard self)
    //     {
    //     }
    // }

    public static class HeroCardSystem
    {
        public static async ETTask PlayHeroCardAttackAnimAsync(this HeroCard self, AttackAction action)
        {
            // Log.Debug("PlayHeroCardAttackAnimAsync");
            // HeroCard attackHeroCard = self.GetChild<HeroCard>(action.AttackHeroCardInfo.HeroId);
            self.Angry = action.AttackHeroCardInfo.Angry;
            self.CurrentSkillId = action.AttackHeroCardInfo.CastSkillId;
            HeroCard beAttackHeroCard = self.Parent.GetChild<HeroCard>(action.BeAttackHeroCardInfo[0].HeroId);
            // beAttackHeroCard.GetHP() = action.BeAttackHeroCardInfo[0].HP;
            // beAttackHeroCard.Angry = action.BeAttackHeroCardInfo[0].Angry;
#if !SERVER
            await Game.EventSystem.PublishAsync(new EventType.PlayHeroCardAttackAnim()
            {
                AttackHeroCard = self,
                BeAttackHeroCard = beAttackHeroCard,
                BeAttackHeroCardInfo = action.BeAttackHeroCardInfo[0],
                AttackHeroCardInfo = action.AttackHeroCardInfo
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
                Attack = self.GetAttack(),
                DiamondAttack = self.GetDiamondAttack(),
                Angry = self.GetAngry(),
                HP = self.HP,
                Defence = self.GetDefence(),
                Level = self.Level == 0? 1 : self.Level,
                SkillInfos = skillInfos,
                TotalHP = self.GetTotalHP(),
                Star = self.Star,
                Count = self.Count
                // Star = self.Angry
            };

            return heroCardInfo;
        }

        public static float GetTotalHP(this HeroCard self)
        {
            // HeroConfig heroConfig = HeroConfigCategory.Instance.Get(self.ConfigId);
            if (self.Level == 0)
            {
                self.Level = 1;
            }
            var TotalHP = HeroConfigCategory.Instance.Get(self.ConfigId).HeroHP + HeroUpdateLevelConfigCategory.Instance.Get(self.Level).BaseHP;
            return TotalHP;
        }

        public static float GetDiamondAttack(this HeroCard self)
        {
            return self.DiamondAttack;
        }

        public static float GetAngry(this HeroCard self)
        {
            return self.Angry;
        }

        // public static float GetHP(this HeroCard self)
        // {
        //     int level = self.Level == 0? 1 : self.Level;
        //     float baseHp = HeroConfigCategory.Instance.Get(self.ConfigId).HeroHP;
        //     float levelHp = HeroUpdateLevelConfigCategory.Instance.Get(level).BaseHP;
        //     return baseHp + levelHp;
        // }

        public static float GetAttack(this HeroCard self)
        {
            float baseAttack = HeroConfigCategory.Instance.Get(self.ConfigId).BaseAttack;
            float levelAttack = self.GetLevelAttack();
            return levelAttack + baseAttack;
        }

        public static float GetDefence(this HeroCard self)
        {
            float baseDefence = HeroConfigCategory.Instance.Get(self.ConfigId).BaseDefence;
            int level = self.Level == 0? 1 : self.Level;
            float levelDefence = HeroUpdateLevelConfigCategory.Instance.Get(level).BaseDefence;
            return baseDefence + levelDefence;
        }

        public static float GetLevelAttack(this HeroCard self)
        {
            //todo 获取等级攻击力
            // return HeroUpdateLevelConfig
            int level = self.Level == 0? 1 : self.Level;
            HeroUpdateLevelConfig info = HeroUpdateLevelConfigCategory.Instance.Get(level);
            return info.BaseAttack;
        }
        // public static void SetMessageInfo(this HeroCard self, HeroCardInfo message)
        // {
        //     self.Id = message.HeroId;
        //     self.HeroName = message.HeroName;
        //     self.OwnerId = message.OwnerId;
        //     self.ConfigId = message.ConfigId;
        //     self.InTroopIndex = message.InTroopIndex;
        //     self.TroopId = message.TroopId;
        //     self.CampIndex = message.CampIndex;
        //     self.HeroColor = message.HeroColor;
        //     self.CurrentSkillId = message.CastSkillId;
        //     self.Attack = message.Attack;
        //     self.DiamondAttack = message.DiamondAttack;
        //     self.Angry = message.Angry;
        //     self.HP = message.HP;
        //     self.Defence = message.Defence;
        //     self.Level = message.Level == 0? 1 : message.Level;
        //     Log.Debug($"set message info {self.Level}");
        // }

        // public static void InitWithConfig(this HeroCard self, HeroConfig heroConfig)
        // {
        //     self.Attack = heroConfig.BaseAttack;
        //     self.Defence = heroConfig.BaseDefence;
        //     self.HP = heroConfig.HeroHP;
        //     self.HeroName = heroConfig.HeroName;
        //     self.ConfigId = heroConfig.Id;
        //     self.HeroColor = heroConfig.HeroColor;
        // }

        //todo 增加攻击值
        public static void AddDiamondAttackValue(this HeroCard self, DiamondInfo diamondInfo)
        {
            Log.Debug($"add attack value {self.Id}");
            DiamondTypeConfig config = DiamondTypeConfigCategory.Instance.Get(diamondInfo.ConfigId);
            HeroConfig heroConfig = HeroConfigCategory.Instance.Get(self.ConfigId);
            var value = float.Parse(heroConfig.AddAttackRate) * float.Parse(config.AddAttack);
            self.DiamondAttack += value;
        }

        //todo 增加怒气值
        public static void AddDiamondAngryValue(this HeroCard self, DiamondInfo diamondInfo)
        {
            DiamondTypeConfig config = DiamondTypeConfigCategory.Instance.Get(diamondInfo.ConfigId);
            HeroConfig heroConfig = HeroConfigCategory.Instance.Get(self.ConfigId);
            var value = float.Parse(heroConfig.AddAngryRate) * float.Parse(config.AddAngry);
            self.Angry += value;
            if (self.Angry > heroConfig.TotalAngry)
            {
                self.Angry = heroConfig.TotalAngry;
            }
        }

        //         public static void EnterAttackState(this HeroCard self)
        //         {
        //             Log.Debug("enter attack state");
        // #if !SERVER
        //             Game.EventSystem.Publish(new EventType.EnterAttackStateView() { HeroCard = self });
        //
        // #endif
        //         }

        // public static async ETTask AttackTargetAsync(this HeroCard self, HeroCard target)
        // {
        //     // // #if !SERVER
        //     //             await Game.EventSystem.PublishAsync(new EventType.PlayHeroCardAttackAnim() { Att = self, TargetHeroCard = target });
        //     // // #endif
        //
        //     await ETTask.CompletedTask;
        // }

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

        public static float GetAddAngryWhitBeAttackSkill(this HeroCard self, HeroCard heroCard, float damage)
        {
            //todo 获得怒气值 用被攻击的技能
            // 那就按被哪个技能攻击获得多少怒气先吧 普攻10% 1技能 2技能20% 大招40%
            var rates = new[] { 0.1f, 0.2f, 0.2f, 0.4f };
            Skill skill = heroCard.GetChild<Skill>(heroCard.CurrentSkillId);
            // Log.Error($"attack skill id {heroCard.CurrentSkillId}");
            SkillConfig skillConfig = SkillConfigCategory.Instance.Get(skill.ConfigId);
            var skillType = skillConfig.SkillType;
            var rate = rates[skillType - 1];
            return damage * rate;
        }

        public static void BeAttack(this HeroCard self, HeroCard attackHeroCard)
        {
            float attack = attackHeroCard.GetAttack();
            float damage = attack - self.GetDefence();
            if (damage < 0)
            {
                damage = 0;
            }

            HeroConfig heroConfig = HeroConfigCategory.Instance.Get(self.ConfigId);
            // var addAngry = float.Parse(heroConfig.AddAngryRate) * Mathf.Abs(damage);
            var addAngry = self.GetAddAngryWhitBeAttackSkill(attackHeroCard, damage);
            self.Angry += addAngry;
            if (self.Angry > heroConfig.TotalAngry)
            {
                self.Angry = heroConfig.TotalAngry;
            }

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
        // public static void InitTurnGame(this HeroCard self)
        // {
        //     // self.Attack = 0;
        //     self.DiamondAttack = 0;
        // }

        public static void MakeHeroCardAngrySkill(this HeroCard self)
        {
            List<Skill> skills = self.GetChilds<Skill>();
            //todo 确定英雄是否进行怒气值技能
            if (self.TotalAngry == 0)
            {
                HeroConfig config = HeroConfigCategory.Instance.Get(self.ConfigId);
                self.TotalAngry = config.TotalAngry;
            }

            //todo 如果怒气值满了，那么施放的技能为大招            
            if (self.Angry >= self.TotalAngry)
            {
                foreach (var target in skills)
                {
                    SkillConfig skillConfig = SkillConfigCategory.Instance.Get(target.ConfigId);
                    if (skillConfig.SkillType == 4)
                    {
                        self.CurrentSkillId = target.Id;
                        return;
                    }
                }
            }
        }

        public static void MakeHeroCardSkill(this HeroCard self, int count)
        {
            Log.Debug($"make hero card skill  count {count}");
            List<Skill> skills = self.GetChilds<Skill>();
            // self.Angry
            count -= 3;
            if (count < 0)
            {
                count = 0;
            }

            if (count > 3)
            {
                count = 3;
            }

            //todo 根据消除宝石的数量，决定将要施放的技能id
            skills.Sort((a, b) =>
            {
                var configA = SkillConfigCategory.Instance.Get(a.ConfigId);
                var configB = SkillConfigCategory.Instance.Get(b.ConfigId);
                return configA.SkillType - configB.SkillType;
            });
            for (int i = 0; i < skills.Count; i++)
            {
                Log.Debug($"skill type{skills[i].ConfigId}");
            }

            Log.Debug($"skill index{count}");
            Log.Debug($"skill count {skills.Count}");
            Skill skill = skills[count];
            self.CurrentSkillId = skill.Id;
        }

        public static void InitSkill(this HeroCard self)
        {
            self.CurrentSkillId = 0;
        }

        public static void CastSkill(this HeroCard self)
        {
            //todo 如果当前施放的技能属于必杀技，那么怒气值需要归0

            if (self.CurrentSkillId == 0)
            {
                return;
            }

            Skill skill = self.GetChild<Skill>(self.CurrentSkillId);
            SkillConfig config = SkillConfigCategory.Instance.Get(skill.ConfigId);
            if (config.SkillType == 4)
            {
                self.Angry = 0;
            }
            // self.CurrentSkillId = 0;
            // if (self.CurrentSkillId == 0)
            // {
            //     return;
            // }

            // Skill skill = self.GetChild<Skill>(self.CurrentSkillId);
            //玩家施放技能
            // heroCard.ProcessCurrentSkill();
            // self.CurrentSkillId = self.ProcessCurrentSkill();
            // Skill skill = self.GetChild<Skill>(self.CurrentSkillId);
            // SkillConfig skillConfig = SkillConfigCategory.Instance.Get(skill.ConfigId);
            // if (skillConfig.SkillType == (int) SkillType.BigSkill)
            // {
            //如果当前施放的技能是大招， 那么需要将怒气值清零
            // self.Angry = 0;
            // }
        }

        //todo 获取英雄名称
        public static string GetHeroName(this HeroCard self)
        {
            HeroConfig config = HeroConfigCategory.Instance.Get(self.ConfigId);
            return config.HeroName;
        }

        //todo 获取当前英雄的颜色
        public static DiamondTypeConfig GetHeroCardColor(this HeroCard self)
        {
            HeroConfig config = HeroConfigCategory.Instance.Get(self.ConfigId);
            int colorType = config.HeroColor;
            var diamondConfig = DiamondTypeConfigCategory.Instance.GetAll().Values.ToList().Find(a => { return a.ColorId.Equals(colorType); });
            // string colorValue = diamondConfig.ColorValue;
            // return ColorTool.HexToColor(colorValue);
            // return colorValue;
            return diamondConfig;
        }

        // public static HeroCardInfo
        //todo 处理当前应该使用哪个技能 并返回技能id
        // public static long ProcessCurrentSkill(this HeroCard self)
        // {
        // List<Skill> skills = self.GetChilds<Skill>();
        //
        // if (skills == null)
        // {
        //     Log.Warning("not skills ");
        //     return 0;
        // }
        //
        // foreach (var skill in skills)
        // {
        //     if (self.CheckAngryIsFull())
        //     {
        //         if (SkillConfigCategory.Instance.Get(skill.ConfigId).SkillType == (int) SkillType.BigSkill)
        //         {
        //             self.Angry = 0;
        //             return skill.Id;
        //         }
        //     }
        // }
        //
        // foreach (var skill in skills)
        // {
        // if (SkillConfigCategory.Instance.Get(skill.ConfigId).SkillType == (int) SkillType.NormalSkill)
        // {
        //     return skill.Id;
        // }
        // }

        // return 0;
        // }
    }
}