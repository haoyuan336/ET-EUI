using System;
using System.Collections.Generic;

namespace ET
{
#if SERVER
    public class BuffAwakeSystem: AwakeSystem<Buff, int>
    {
        public override void Awake(Buff self, int configId)
        {
            self.ConfigId = configId;
        }
    }

    public class BuffDestroyAwakeSystem: DestroySystem<Buff>
    {
        public override void Destroy(Buff self)
        {
        }
    }

    public static class BuffSystem
    {
        public static BuffInfo GetBuffInfo(this Buff self)
        {
            return new BuffInfo() { BuffId = self.Id, ConfigId = self.ConfigId, RoundCount = self.RoundCount, HealthShield = self.HealthShield };
        }

        public static ActionMessage ProcessRound(this Buff self, HeroCard heroCard)
        {
            ActionMessage actionMessage =
                    new ActionMessage() { PlayType = (int)ActionMessagePlayType.Async, ActionMessages = new List<ActionMessage>() };
            BuffConfig buffConfig = BuffConfigCategory.Instance.Get(self.ConfigId);
            float deductionSelfTotalHealthRate = buffConfig.DeductionSelfTotalHealthRate / 100.0f * self.OverlabCount;
            HeroCardDataComponent heroCardDataComponent = heroCard.GetComponent<HeroCardDataComponent>();

            self.RoundCount--;
            // self.AlRoundCount++;
            if (deductionSelfTotalHealthRate > 0)
            {
                var hp = heroCardDataComponent.HP * deductionSelfTotalHealthRate;
                float oldHp = heroCardDataComponent.HP;
                float endHp = oldHp - hp;
                if (endHp < 0)
                {
                    endHp = 0;
                }

                float damage = heroCardDataComponent.HP - endHp;
                heroCardDataComponent.HP = (int)endHp;
                var buffDamageAction = new BuffDamageAction()
                {
                    BuffInfo = self.GetBuffInfo(), HeroCardDataComponentInfo = heroCardDataComponent.GetInfo(), DamageCount = (int)damage
                };
                actionMessage.BuffDamageAction = buffDamageAction;
            }

            float deductionAttackAttackHealthRate = buffConfig.DeductionAttackAttackHealthRate / 100.0f * self.OverlabCount; //施法者攻击力百分比
            if (deductionAttackAttackHealthRate > 0)
            {
                float damage = self.CastAttackPower * deductionAttackAttackHealthRate;
                float oldHp = heroCardDataComponent.HP;
                float endHp = oldHp - damage;
                if (endHp < 0)
                {
                    endHp = 0;
                }

                float endDamage = heroCardDataComponent.HP - endHp;
                heroCardDataComponent.HP = (int)endHp;
                var buffDamageAction = new BuffDamageAction()
                {
                    BuffInfo = self.GetBuffInfo(), HeroCardDataComponentInfo = heroCardDataComponent.GetInfo(), DamageCount = (int)endDamage
                };
                actionMessage.BuffDamageAction = buffDamageAction;
            }
            if (self.RoundCount == -1)
            {
                self.Dispose();
            }

            return actionMessage;
        }
    }
#endif
}