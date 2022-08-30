using System.Collections.Generic;
using System.Linq;

namespace ET
{
    public class HeroCardDestroySystem: DestroySystem<HeroCard>
    {
        public override void Destroy(HeroCard self)
        {
#if !SERVER
            // Game.EventSystem.Publish(new EventType.DestroyHeroCard() { HeroCard = self });
#endif
        }
    }

    public class HeroCardAwakeSystem: AwakeSystem<HeroCard>
    {
        public override void Awake(HeroCard self)
        {
        }
    }

    public static class HeroCardSystem
    {
        public static HeroCard Clone(this HeroCard self)
        {
            return new HeroCard()
            {
                Id = self.Id,
                HeroName = self.HeroName,
                OwnerId = self.OwnerId,
                ConfigId = self.ConfigId,
                TroopId = self.TroopId,
                InTroopIndex = self.InTroopIndex,
                MailId = self.MailId,
                CampIndex = self.CampIndex,
                Level = self.Level,
                Star = self.Star,
                Rank = self.Rank,
                Count = self.Count,
                State = self.State,
                CurrentExp = self.CurrentExp,
                CallTime = self.CallTime,
                IsLock = self.IsLock
            };
        }

        public static int GetCriticalHit(this HeroCard self, HeroCard beAttackHeroCard)
        {
            var selfCriticalHit = self.GetWeaponBaseValueByType(WordBarType.CriticalHit);
            var targetTouchness = beAttackHeroCard.GetWeaponBaseValueByType(WordBarType.Toughness);
            var rate = selfCriticalHit - targetTouchness;
            //自身的暴击值
            //目标的韧性值
            if (rate < 0)
            {
                rate = 0;
            }

            return rate;
        }

        // public static int GetWeaponAttack(this HeroCard self)
        // {
        //     List<Weapon> weapons = self.GetChilds<Weapon>();
        //
        //     var attack = 0;
        //     foreach (var weapon in weapons)
        //     {
        //         attack += weapon.GetWeaponWordBarValueByType(WordBarType.Attack);
        //     }
        //
        //     return attack;
        // }

        // public static int GetWeaponAttackAddition(this HeroCard self)
        // {
        //     List<Weapon> weapons = self.GetChilds<Weapon>();
        //     var addition = 0;
        //     foreach (var weapon in weapons)
        //     {
        //         addition += weapon.GetWeaponWordBarValueByType(WordBarType.AttackAddition);
        //     }
        //
        //     return addition;
        // }

        public static int GetDomineering(this HeroCard self)
        {
            var baseValue = ConstValue.DomineeringBaseValue;
            float endValue = baseValue + (self.Level * 15) + (baseValue * 0.1f * self.Rank) + baseValue * self.Star;
            return (int)endValue;
        }

        // public static int GetWeaponDefenceAddition(this HeroCard self)
        // {
        //     List<Weapon> weapons = self.GetChilds<Weapon>();
        //     var baseValue = 0;
        //     foreach (var weapon in weapons)
        //     {
        //         baseValue += weapon.GetWeaponWordBarValueByType(WordBarType.DefenceAddition);
        //     }
        //
        //     //获取防御力加成
        //     return baseValue;
        // }

        public static int GetWeaponBaseValueByType(this HeroCard self, WordBarType type)
        {
            Log.Debug($"GetWeaponBaseValueByType{self} ");
            List<Weapon> weapons = self.GetChilds<Weapon>();
            if (weapons == null)
            {
                return 0;
            }

            Log.Debug($"weapon s {weapons.Count}");
            var baseValue = 0;
            foreach (var weapon in weapons)
            {
                baseValue += weapon.GetWeaponWordBarValueByType(type);
            }

            return baseValue;
        }

        // public static AttackAction AngryAttack(this HeroCard self, HeroCard tatgetHeroCard)
        // {
        //  
        //     AttackAction attackAction = self.AttackTarget(tatgetHeroCard, 0);
        //     return attackAction;
        // }
        public static float ProcessSkillDamageValue(this HeroCard self, Skill skill, float baseDamage, List<Buff> buffs)
        {
            SkillConfig skillConfig = SkillConfigCategory.Instance.Get(skill.ConfigId);
            int[] damageRate = skillConfig.LevelDamages;
            if (skill.Level - 1 < damageRate.Length)
            {
                var rate = damageRate[skill.Level - 1] / 100.0f;
                baseDamage += baseDamage * (rate);
            }

            int buffDamageAdditionCondition = skillConfig.BuffDamageAdditionCondition;

            if (buffs != null && buffs.Exists(a => a.ConfigId.Equals(buffDamageAdditionCondition)))
            {
                int[] buffDamageAdditions = skillConfig.BuffDamageAdditions;
                var addition = buffDamageAdditions[skill.Level - 1] / 100.0f;
                baseDamage += baseDamage * addition;
            }

            return baseDamage;
        }

        public static void ProcessBuffLogic(this HeroCard self, HeroCard targetHeroCard, Skill skill)
        {
#if SERVER
            BuffComponent buffComponent = targetHeroCard.GetComponent<BuffComponent>();
            //     // todo 给收攻击的英雄增加buff
            // buffComponent.AddBuffWithSkillConfig(skill);

            SkillConfig skillConfig = SkillConfigCategory.Instance.Get(skill.ConfigId);
            //找到buff
            int[] buffConfigIds = skillConfig.BuffConfigIds;
            int[] buffRounds = skillConfig.LevelBuffRoundCounts;

            if (buffConfigIds == null)
            {
                return;
            }

            List<Buff> currentBuffs = buffComponent.GetChilds<Buff>();
            if (currentBuffs != null)
            {
                currentBuffs.RemoveAll(a => a.RoundCount <= 0); //删掉0回合buff
            }

            //检查过滤条件
            int activeBuffCondition = skillConfig.ActiveBuffCondition; //激活新buff需要的条件
            if (activeBuffCondition != 0)
            {
                //检查是否符合条件，不符合 直接返回
                if (currentBuffs == null)
                {
                    return;
                }

                Buff findBuff = currentBuffs.Find(a => a.ConfigId.Equals(activeBuffCondition));
                if (findBuff == null)
                {
                    return;
                }

                findBuff.RoundCount = 0;
                //条件符合，
            }

            for (int i = 0; i < buffConfigIds.Length; i++)
            {
                Buff buff = buffComponent.AddBuff(buffConfigIds[i], buffRounds[i]);
                switch (buffConfigIds[i])
                {
                    case 110:
                        //增加护盾 buff 原来英雄的血量的加成值 
                        var healthShield = skillConfig.HealthShieldAdditions[skill.Level - 1] / 100.0f;
                        buff.HealthShield = (int)healthShield;
                        break;
                }
            }
#endif
        }

        public static void ProcessMainFightLogic(this HeroCard self, HeroCard targetHeroCard, Skill skill)
        {
            SkillConfig skillConfig = SkillConfigCategory.Instance.Get(skill.ConfigId);
            switch (skillConfig.RangeType)
            {
                case (int)SkillRangeType.EnemySingle:
                case (int)SkillRangeType.EnemyGroup:
                    self.AttackTarget(targetHeroCard, self.GetComponent<HeroCardDataComponent>().DiamondAttackAddition, skill);
                    break;
                case (int)SkillRangeType.FriendSingle:
                case (int)SkillRangeType.FriendGroup:
                    self.CareTarget(targetHeroCard, skill);
                    break;
            }
        }

        public static void CareTarget(this HeroCard self, HeroCard targetHeroCard, Skill skill)
        {
            SkillConfig skillConfig = SkillConfigCategory.Instance.Get(skill.ConfigId);
            int[] careHealehs = skillConfig.CareHealths;
            float rate = careHealehs[skill.Level - 1] / 100.0f;

            float endHp = targetHeroCard.GetComponent<HeroCardDataComponent>().HP * (1 + rate);
            targetHeroCard.GetComponent<HeroCardDataComponent>().HP = (int)endHp;
        }

        public static void AttackTarget(this HeroCard self, HeroCard targetHeroCard, int comboAddition, Skill skill)
        {
            HeroCardDataComponent attackCom = self.GetComponent<HeroCardDataComponent>();
            HeroCardDataComponent beAttackCom = targetHeroCard.GetComponent<HeroCardDataComponent>();
            BuffComponent buffComponent = targetHeroCard.GetComponent<BuffComponent>();
            List<Buff> buffs = buffComponent.GetChilds<Buff>();
            var baseAttack = attackCom.GetHeroBaseAttack(); //角色的基础攻击
            var weaponAttack = self.GetWeaponBaseValueByType(WordBarType.Attack); // 装备的基础攻击
            var weaponAttackAddition = self.GetWeaponBaseValueByType(WordBarType.AttackAddition); //装备的攻击力加成
            var planeAttack = (baseAttack + weaponAttack) * (1 + weaponAttackAddition / 10000); //面板攻击力
            //取出消除宝石combo加成
            planeAttack += (int)(planeAttack * comboAddition / 100.0f);
            var domineeringValue = self.GetDomineering(); //霸气值
            float baseDefence = beAttackCom.GetHeroBaseDefence(); //被攻击对象的基础攻击力
            float weaponDefence = targetHeroCard.GetWeaponBaseValueByType(WordBarType.Defecnce); //被攻击对象的装备防御力
            float weaponDefecceAddition = targetHeroCard.GetWeaponBaseValueByType(WordBarType.DefenceAddition); //被攻击对象的装备防御力加成
            float planeDefence = (baseDefence + weaponDefence) * (1 + (float)weaponDefecceAddition / 10000); //被攻击对象的面板防御力
            float attackBuff = 0; //攻击buff
            float attackDeBuff = 0; //攻击debuff
            float deffenceBuff = 0; //防御buff
            float deffenceDeBuff = 0; //防御debuff
            float damage = (planeAttack + attackBuff - attackDeBuff) * domineeringValue / (planeDefence + deffenceBuff - deffenceDeBuff); //最终伤害
            Log.Debug($"dmage{damage}");
            var damageAddition = self.GetWeaponBaseValueByType(WordBarType.DamageAddition);
            var damageReduction = targetHeroCard.GetWeaponBaseValueByType(WordBarType.DamageReduction);
            damageAddition -= damageReduction;
            if (damageAddition < 0)
            {
                damageAddition = 0;
            }

            damage += damage * damageAddition / 10000;

            //技能加成

            damage += self.ProcessSkillDamageValue(skill, damage, buffs);

            var critical = self.GetCriticalHit(targetHeroCard); //暴击概率
            var isCritical = RandomHelper.RandomNumber(0, 10000) < critical;
            if (isCritical)
            {
                var critialDamage = self.GetWeaponBaseValueByType(WordBarType.CriticalHitDamage);
                Log.Debug($"{critialDamage}");
                damage *= 1 + 0.5f + (float)critialDamage / 10000;
            }

            if (buffs != null)
            {
                damage += self.GetBuffDamageValue(buffs, damage);
            }

            var oldHp = beAttackCom.HP;
            Log.Debug($"old hp {oldHp}");
            Buff buff = buffs?.Find(a => a.ConfigId == 102);
            if (buff != null && beAttackCom.HP / (float)beAttackCom.TotalHP > BuffConfigCategory.Instance.Get(buff.ConfigId).value / 100.0f)
            {
                //todo 英雄具有根性buff  死不了
                beAttackCom.HP -= (int)damage;
                if (beAttackCom.HP < 0)
                {
                    beAttackCom.HP = 1;
                }
            }
            else
            {
                beAttackCom.HP -= (int)damage;
                if (beAttackCom.HP < 0)
                {
                    beAttackCom.HP = 0;
                }
            }

            beAttackCom.Angry += 5;
            damage = oldHp - beAttackCom.HP;
            Log.Debug($"damage {damage}");
            beAttackCom.Damage = (int)damage;
            beAttackCom.IsCritical = isCritical;
            attackCom.DiamondAttackAddition = 0;
#if SERVER
            // targetHeroCard.GetComponent<BuffComponent>().ProcessRoundLogic();
#endif
        }

        public static float GetBuffDamageValue(this HeroCard self, List<Buff> buffs, float damage)
        {
            var value = 0.0f;
            foreach (var buff in buffs)
            {
                BuffConfig config = BuffConfigCategory.Instance.Get(buff.ConfigId);
                switch (config.Id)
                {
                    case 106:
                        value = damage * config.value;
                        break;
                }
            }

            return value;
        }

        public static HeroCardInfo GetMessageInfo(this HeroCard self)
        {
            HeroCardInfo heroCardInfo = new HeroCardInfo()
            {
                HeroId = self.Id,
                ConfigId = self.ConfigId,
                OwnerId = self.OwnerId,
                TroopId = self.TroopId,
                InTroopIndex = self.InTroopIndex,
                CampIndex = self.CampIndex,
                Level = self.Level == 0? 1 : self.Level,
                Star = self.Star,
                Count = self.Count,
                Rank = self.Rank,
                CurrentExp = self.CurrentExp,
                IsLock = self.IsLock
            };
            return heroCardInfo;
        }

        public static void SetMessageInfo(this HeroCard self, HeroCardInfo heroCardInfo)
        {
            self.Id = heroCardInfo.HeroId;
            self.ConfigId = heroCardInfo.ConfigId;
            self.OwnerId = heroCardInfo.OwnerId;
            self.TroopId = heroCardInfo.TroopId;
            self.InTroopIndex = heroCardInfo.InTroopIndex;
            self.CampIndex = heroCardInfo.CampIndex;
            self.Level = heroCardInfo.Level;
            self.Star = heroCardInfo.Star;
            self.Count = heroCardInfo.Count;
            self.Rank = heroCardInfo.Rank;
            self.CurrentExp = heroCardInfo.CurrentExp;
            self.IsLock = heroCardInfo.IsLock;
        }

        public static bool GetIsDead(this HeroCard self)
        {
            return self.GetComponent<HeroCardDataComponent>().HP <= 0;
            // return self.HP <= 0;
        }

        //todo 获取英雄名称
        public static string GetHeroName(this HeroCard self)
        {
            HeroConfig config = HeroConfigCategory.Instance.Get(self.ConfigId);
            return config.HeroName;
        }

        //todo 获取当前英雄的颜色
        public static DiamondTypeConfig GetHeroCardColor(this HeroCard self)
        {
            HeroConfig config = HeroConfigCategory.Instance.Get(self.ConfigId);
            int colorType = config.HeroColor;
            var diamondConfig = DiamondTypeConfigCategory.Instance.GetAll().Values.ToList().Find(a => { return a.ColorId.Equals(colorType); });
            return diamondConfig;
        }
    }
}