namespace ET
{
    public class HeroCardAwakeSystem: AwakeSystem<HeroCard>
    {
        public override void Awake(HeroCard self)
        {
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
        public static void AddAttackValue(this HeroCard self, float baseValue)
        {
            HeroConfig heroConfig = HeroConfigCategory.Instance.Get(self.ConfigId);
            self.Attack += float.Parse(heroConfig.AttackRate) * baseValue;
#if !SERVER
            Game.EventSystem.Publish(new EventType.UpdateAttackView() { HeroCard = self });
#endif
        }

        //todo 增加怒气值
        public static void AddAngryValue(this HeroCard self, float baseValue)
        {
            HeroConfig heroConfig = HeroConfigCategory.Instance.Get(self.ConfigId);
            self.Angry += float.Parse(heroConfig.AngryRate) * baseValue;
#if !SERVER
            Game.EventSystem.Publish(new EventType.UpdateAngryView() { HeroCard = self });
#endif
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

#if !SERVER
            await Game.EventSystem.PublishAsync(new EventType.PlayHeroCardAttackAnim(){SelfHeroCard = self,TargetHeroCard = target});
#endif
            
            
            await ETTask.CompletedTask;
        }
    }
}