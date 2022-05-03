using System;

namespace ET
{
    public class Skill: Entity, IAwake<int>, IAwake<SkillInfo>, IAwake<Skill>
    {
        public long OwnerId;
        public int ConfigId;
        
    }
}