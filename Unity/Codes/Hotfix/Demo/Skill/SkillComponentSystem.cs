using System;
using System.Collections.Generic;
using System.Linq;

namespace ET
{
    public class SkillComponentAwakeSystem: AwakeSystem<SkillComponent>
    {
        public override void Awake(SkillComponent self)
        {
            //创建skill
            HeroCard heroCard = self.GetParent<HeroCard>();
            //根据英雄等级，添加技能
            int levelNum = heroCard.Level;
            HeroLevelExpConfig heroLevelExpConfig = HeroLevelExpConfigCategory.Instance.Get(levelNum);
            HeroConfig heroConfig = HeroConfigCategory.Instance.Get(heroCard.ConfigId);
            List<int> skillConfigIds = heroConfig.SkillIdList.ToList();
            // heroLevelExpConfig.SkillLevel1
            for (int i = 1; i < 5; i++)
            {
                int value = Convert.ToInt32(heroLevelExpConfig.GetType().GetProperty($"SkillLevel{i}").GetValue(heroLevelExpConfig, null));
                // Log.Warning($"hero level exp config  {value}");
                if (value != 0)
                {
                    int configId = skillConfigIds.Find(a =>
                    {
                        SkillConfig skillConfig = SkillConfigCategory.Instance.Get(a);
                        if (skillConfig.SkillType == i)
                        {
                            return true;
                        }

                        return false;
                    });

                    if (configId != 0)
                    {
                       Skill skill = self.AddChild<Skill, int>(configId);
                       skill.Level = value;
                    }
                }
            }
        }
    }
}