namespace ET
{
    public class HeroCardAwakeSystem: AwakeSystem<HeroCard>
    {
        public override void Awake(HeroCard self)
        {
            self.BigSkill = self.AddChild<Skill>();
            self.BigSkill.SkillType = SkillType.BigSkill;

            self.NormalSkill = self.AddChild<Skill>();
            self.NormalSkill.SkillType = SkillType.BigSkill;
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
            int skillType = 0;
            if (self.CurrentCastSkill != null)
            {
                skillType = (int) self.CurrentCastSkill.SkillType;
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
                SkillType = skillType
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
#if !SERVER
            Game.EventSystem.Publish(new EventType.UpdateAngryView() { HeroCard = self });
#endif
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
        // public static HeroCardInfo
    }
}