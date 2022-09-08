using System;
using System.Collections.Generic;
using System.Linq;

namespace ET
{
    public static class SkillComponentSystem
    {
        public static Skill MakeSureSkill(this SkillComponent self, int count)
        {
            SkillType skillType = SkillType.Attack;
            switch (count)
            {
                case 3:
                    skillType = SkillType.Attack;
                    break;
                case 4:
                    skillType = SkillType.Skill1;
                    break;
                case 5:
                    skillType = SkillType.Skill2;
                    break;
            }
            Log.Debug($"make sure skill {count}");

            List<Skill> skills = self.GetChilds<Skill>();
            Skill skill = skills.Find(a=>a.Config.SkillType == (int)skillType);
            Log.Debug($"skill config id {skill.ConfigId}");
            if (skill == null)
            {
                Log.Debug("未找到对应技能");
                skill = skills[skills.Count - 1];
            }

            // return new Skill() { ConfigId = 1000025, Level = 1 };

            // skill.ConfigId = 1000045;
            return skill;
        }

        public static Skill MakeSureAngrySkill(this SkillComponent self)
        {
            List<Skill> skills = self.GetChilds<Skill>();
            Skill skill = skills.Find(a =>
            {
                SkillConfig config = SkillConfigCategory.Instance.Get(a.ConfigId);
                if (config.SkillType == (int)SkillType.BigSkill)
                {
                    return true;
                }

                return false;
            });
            if (skill == null)
            {
                skill = skills[skills.Count - 1];
            }

            return skill;
        }
    }

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