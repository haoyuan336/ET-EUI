namespace ET
{
    public class SkillAwakeSystem: AwakeSystem<Skill, int>
    {
        public override void Awake(Skill self, int configId)
        {

            self.ConfigId = configId;
            
        }
    }

    public class SkillAwakeSystem2: AwakeSystem<Skill, Skill>
    {
        public override void Awake(Skill self, Skill a)
        {
        }
    }

    public class SkillAwakeSystem1: AwakeSystem<Skill, SkillInfo>
    {
        public override void Awake(Skill self, SkillInfo a)
        {
            self.ConfigId = a.SkillConfigId;
        
        }
    }

    public static class SkillSystem
    {
        public static async ETTask Call(this Skill self, int zone, long ownerId)
        {
            self.OwnerId = ownerId;
#if SERVER
            await DBManagerComponent.Instance.GetZoneDB(zone).Save(self);
#endif
            await ETTask.CompletedTask;
        }


        public static SkillInfo GetMessageInfo(this Skill self)
        {
            return new SkillInfo() { SkillConfigId = self.ConfigId, OwnerId = self.OwnerId, SkillId = self.Id };
        }
    }
}