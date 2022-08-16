using System.Collections.Generic;

namespace ET
{
    public class SkillComponentAwakeSystem: AwakeSystem<SkillComponent>
    {
        public override void Awake(SkillComponent self)
        {
            //创建skill
            // List<SkillConfig> skillConfigs = SkillConfigCategory.Instance.Get()
            // foreach (var VARIABLE in COLLECTION)
            // {
            // }
            HeroCard heroCard = self.GetParent<HeroCard>();
            HeroConfig heroConfig = HeroConfigCategory.Instance.Get(heroCard.ConfigId);
            int[] skillConfigIds = heroConfig.SkillIdList;
            foreach (var skillConfigId in skillConfigIds)
            {
                self.AddChild<Skill, int>(skillConfigId);
            }
        }
    }
}