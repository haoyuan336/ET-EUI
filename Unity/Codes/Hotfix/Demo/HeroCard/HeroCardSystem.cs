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
                HeroId = self.Id, HeroName = self.HeroName, ConfigId = self.ConfigId, OwnerId = self.OwnerId,TroopId = self.TroopId, InTroopIndex = self.InTroopIndex
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
        }
    }
}