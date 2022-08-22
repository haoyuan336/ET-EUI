using System;
using System.Collections.Generic;

namespace ET
{
#if SERVER
    public class BuffComponentAwakeSystem: AwakeSystem<BuffComponent>
    {
        public override void Awake(BuffComponent self)
        {
        }
    }

    public static class BuffComponentSystem
    {
        public static List<Buff> AddBuffWithSkillConfig(this BuffComponent self, SkillConfig skillConfig)
        {
            //找到buff
            int[] buffConfigIds = skillConfig.BuffConfigIds;
            int[] buffRounds = skillConfig.BuffRoundCounts;
            List<Buff> buffs = new List<Buff>();
            for (int i = 0; i < buffConfigIds.Length; i++)
            {
                buffs.Add(self.AddBuff(buffConfigIds[i], buffRounds[i]));
            }

            int[] deBuffConfigIds = skillConfig.DeBuffConfigIds;
            int[] deBuffRounds = skillConfig.DeBuffRoundCounts;
            for (int i = 0; i < deBuffConfigIds.Length; i++)
            {
                buffs.Add(self.AddBuff(deBuffConfigIds[i], deBuffRounds[i]));
            }

            return buffs;
        }

        public static Buff AddBuff(this BuffComponent self, int configId, int count)
        {
            BuffConfig addBuffConfig = BuffConfigCategory.Instance.Get(configId);
            //取出原有buff ，检查是否相克
            List<Buff> buffs = self.GetChilds<Buff>();
            Buff targetBuff = buffs?.Find(a => a.ConfigId.Equals(configId));
            if (targetBuff != null)
            {
                if (targetBuff.OverlabCount < addBuffConfig.MaxOverlabCount)
                {
                    targetBuff.OverlabCount++;
                }
            }
            else
            {
                targetBuff = self.AddChild<Buff, int>(configId);
            }

            targetBuff.RoundCount = count; //回合数另算
            return targetBuff;
        }

        public static void ProcessRoundLogic(this BuffComponent self)
        {
            List<Buff> buffs = self.GetChilds<Buff>();
            if (buffs != null)
            {
                foreach (var buff in buffs)
                {
                    buff.ProcessRound();
                }
            }
        }

        public static void ActiveBuff(this BuffComponent self, BuffConfig buffConfig)
        {
            int activeBuffIds = buffConfig.RoundOverActiveBuff;
            //激活新buff
            if (activeBuffIds != 0)
            {
                self.AddBuff(activeBuffIds, 1);
            }
        }
    }
#endif
}