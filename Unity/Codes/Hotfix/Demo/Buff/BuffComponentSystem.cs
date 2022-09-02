using System;
using System.Collections.Generic;
using ET.EventType;

namespace ET
{
#if SERVER
    public class HeroDeadEventHandler: AEvent<EventType.HeroDead>
    {
        protected override async ETTask Run(HeroDead a)
        {
            HeroCard heroCard = a.HeroCard;
            heroCard.GetComponent<BuffComponent>().HeroDead();
            await ETTask.CompletedTask;
        }
    }

    public class BuffComponentAwakeSystem: AwakeSystem<BuffComponent>
    {
        public override void Awake(BuffComponent self)
        {
        }
    }

    public static class BuffComponentSystem
    {
        public static void HeroDead(this BuffComponent self)
        {
            self.RemoveAllChild<Buff>();
        }

        public static List<BuffInfo> GetBuffInfos(this BuffComponent self)
        {
            List<Buff> buffs = self.GetChilds<Buff>();
            if (buffs == null)
            {
                return null;
            }

            List<BuffInfo> buffInfos = new List<BuffInfo>();
            foreach (var buff in buffs)
            {
                if (buff.RoundCount >= 0)
                {
                    buffInfos.Add(buff.GetBuffInfo());
                }
            }

            return buffInfos;
        }

        // public static void AddBuffWithSkillConfig(this BuffComponent self, Skill skill)
        // {
            // SkillConfig skillConfig = SkillConfigCategory.Instance.Get(skill.ConfigId);
            // //找到buff
            // int[] buffConfigIds = skillConfig.BuffConfigIds;
            // int[] buffRounds = skillConfig.LevelBuffRoundCounts;
            //
            // if (buffConfigIds == null)
            // {
            //     return;
            // }
            //
            // List<Buff> currentBuffs = self.GetChilds<Buff>();
            // if (currentBuffs != null)
            // {
            //     currentBuffs.RemoveAll(a => a.RoundCount <= 0);
            // }
            //
            // //检查过滤条件
            // int activeBuffCondition = skillConfig.ActiveBuffCondition;
            // if (activeBuffCondition != 0)
            // {
            //     //检查是否符合条件，不符合 直接返回
            //     if (currentBuffs == null)
            //     {
            //         return;
            //     }
            //
            //     Buff findBuff = currentBuffs.Find(a => a.ConfigId.Equals(activeBuffCondition));
            //     if (findBuff == null)
            //     {
            //         return;
            //     }
            //
            //     findBuff.RoundCount = 0;
            //     //条件符合，
            // }
            //
            // for (int i = 0; i < buffConfigIds.Length; i++)
            // {
            //     self.AddBuff(buffConfigIds[i], buffRounds[i]);
            // }
        // }

        /*
         * round count 回合数
         * overCount 叠加层数
         */
        public static Buff AddBuff(this BuffComponent self, int configId, int roundCount, int overCount, long heroId)
        {
            BuffConfig addBuffConfig = BuffConfigCategory.Instance.Get(configId);
            //取出原有buff ，检查是否相克
            List<Buff> buffs = self.GetChilds<Buff>();
            buffs?.RemoveAll(a => a.RoundCount <= 0);
            Buff targetBuff = null;
            if (addBuffConfig.IsCanCover == (int)BuffIsCanCover.Can)
            {
                targetBuff = buffs?.Find(a => a.ConfigId.Equals(configId));
                if (targetBuff == null)
                {
                    targetBuff = self.AddChild<Buff, int>(configId);
                }

                targetBuff.OverlabCount = overCount; //层数
                targetBuff.AttachHeroId = heroId;
            }
            else
            {
                //todo 不可覆盖的buff，查找是否存在同一个施法者的buff
                targetBuff = buffs?.Find(a => a.AttachHeroId.Equals(heroId));
                if (targetBuff == null)
                {
                    //todo未找到了buff
                    targetBuff = self.AddChild<Buff, int>(configId);
                }

                targetBuff.AttachHeroId = heroId;
                targetBuff.OverlabCount += overCount;
                if (targetBuff.OverlabCount > addBuffConfig.MaxOverlabCount)
                {
                    targetBuff.OverlabCount = addBuffConfig.MaxOverlabCount;
                }
            }
            targetBuff.RoundCount = roundCount; //回合数另算
            return targetBuff;
        }

        public static ActionMessage ProcessRoundLogic(this BuffComponent self, HeroCard heroCard)
        {
            ActionMessage actionMessage = new ActionMessage()
            {
                PlayType = (int)ActionMessagePlayType.Async, ActionMessages = new List<ActionMessage>()
            };
            List<Buff> buffs = self.GetChilds<Buff>();
            if (buffs != null)
            {
                foreach (var buff in buffs)
                {
                    ActionMessage message = buff.ProcessRound(heroCard);
                    actionMessage.ActionMessages.Add(message);
                }
            }

            return actionMessage;
        }

        public static void ActiveBuff(this BuffComponent self, BuffConfig buffConfig,int roundCount, int overCount, long heroId)
        {
            int activeBuffIds = buffConfig.RoundFullActiveBuff;
            //激活新buff
            if (activeBuffIds != 0)
            {
                // int configId, int roundCount, int overCount, long heroId
                self.AddBuff(activeBuffIds,roundCount, overCount, heroId);
            }
        }

        public static bool GetIsCanAttack(this BuffComponent self)
        {
            List<Buff> buffs = self.GetChilds<Buff>();
            if (buffs == null)
            {
                return true;
            }

            return !buffs.Exists(a =>
            {
                BuffConfig buffConfig = BuffConfigCategory.Instance.Get(a.ConfigId);
                return buffConfig.IsCanAttack == 0;
            });
        }

        public static bool IsExistsBuff(this BuffComponent self, int configId)
        {
            List<Buff> buffs = self.GetChilds<Buff>();
            if (buffs != null)
            {
                return buffs.Exists(a => a.ConfigId.Equals(configId) && a.RoundCount > 0);
            }

            return false;
        }
    }
#endif
}