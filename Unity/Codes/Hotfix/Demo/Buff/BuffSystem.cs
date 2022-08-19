namespace ET
{
#if SERVER
    public class BuffAwakeSystem: AwakeSystem<Buff>
    {
        public override void Awake(Buff self)
        {
        }
    }

    public class BuffDestroyAwakeSystem: DestroySystem<Buff>
    {
        public override void Destroy(Buff self)
        {
        }
    }

    public static class BuffSystem
    {
        public static BuffInfo GetBuffInfo(this Buff self)
        {
            return new BuffInfo() { BuffId = self.Id, ConfigId = self.ConfigId, RoundCount = self.RoundCount };
        }
    }
#endif
}