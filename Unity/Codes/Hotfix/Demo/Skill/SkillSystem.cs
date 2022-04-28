namespace ET
{
    public class SkillAwakeSystem: AwakeSystem<Skill, int>
    {
        public override void Awake(Skill self, int configId)
        {
            // SkillConfig skillConfig = SkillConfigCategory.Instance.Get(configId);
            // self.SkillName = skillConfig.SkillName;
            // self.SkillType = skillConfig.SkillType;
            // self.SkillAnimName = skillConfig.SkillAnimName;
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
            // self.SkillName = a.SkillName;
            // self.SkillType = a.SkillType;
            // public long SkillId { get; set; }
            //
            // [ProtoMember(2)]
            // public string SkillName { get; set; }
            //
            // [ProtoMember(3)]
            // public int SkillType { get; set; }
            //
            // [ProtoMember(4)]
            // public int SkillConfigId { get; set; }
            //
            // [ProtoMember(5)]
            // public string SkillAnimName { get; set; }
            //
            // [ProtoMember(6)]
            // public long OwnerId { get; set; }
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

        // public static void InitSkillWithDBData(this Skill self, Skill skill)
        // {
        //     self.SetMessageInfo(skill.GetMessageInfo());
        // }

        // public static void SetMessageInfo(this Skill self, SkillInfo skillInfo)
        // {
        //     self.ConfigId = skillInfo.SkillConfigId;
        //     self.OwnerId = skillInfo.OwnerId;
        // }

        public static SkillInfo GetMessageInfo(this Skill self)
        {
            return new SkillInfo() { SkillConfigId = self.ConfigId, OwnerId = self.OwnerId, SkillId = self.Id };
        }
    }
}