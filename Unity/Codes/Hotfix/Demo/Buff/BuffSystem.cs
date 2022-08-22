using System;

namespace ET
{
#if SERVER
    public class BuffAwakeSystem: AwakeSystem<Buff, int>
    {
        public override void Awake(Buff self, int configId)
        {
            self.ConfigId = configId;
            
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

        public static void ProcessRound(this  Buff self)
        {
            self.RoundCount--;

            if (self.RoundCount == 0)
            {
                BuffConfig buffConfig = BuffConfigCategory.Instance.Get(self.ConfigId);
                self.GetParent<BuffComponent>().ActiveBuff(buffConfig);
                //todo 回合结束的时候，查看检查自己是否存在
                self.Dispose();
            }
        }

        
    }
#endif
}