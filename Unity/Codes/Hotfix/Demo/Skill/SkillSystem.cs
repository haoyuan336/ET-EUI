namespace ET
{
    public class SkillAwakeSystem: AwakeSystem<Skill, int>
    {
        public override void Awake(Skill self, int configId)
        {
            SkillConfig skillConfig = SkillConfigCategory.Instance.Get(configId);
            self.SkillName = skillConfig.SkillName;
            self.SkillType = skillConfig.SkillType;
            self.SkillAnimName = skillConfig.SkillAnimName;
            self.ConfigId = configId;
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

        public static void InitSkillWithDBData(this Skill self, Skill skill)
        {
            self.SetMessageInfo(skill.GetMessageInfo());
        }

        public static void SetMessageInfo(this Skill self, SkillInfo skillInfo)
        {
            self.ConfigId = skillInfo.SkillConfigId;
            self.SkillName = skillInfo.SkillName;
            self.SkillType = skillInfo.SkillType;
            self.SkillAnimName = skillInfo.SkillAnimName;
            self.OwnerId = skillInfo.OwnerId;
            
        }

        public static SkillInfo GetMessageInfo(this Skill self)
        {
            return new SkillInfo()
            {
                SkillConfigId = self.ConfigId, SkillName = self.SkillName, SkillType = self.SkillType, SkillAnimName = self.SkillAnimName,OwnerId = self.OwnerId,SkillId = self.Id
            };
        }
    }
}