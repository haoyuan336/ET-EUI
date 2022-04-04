using System;

namespace ET
{
    public class Skill: Entity, IAwake<int>
    {
        public int SkillType;
        public string SkillName;
        public string SkillAnimName;
        public long OwnerId;
        public int ConfigId;
    }
}