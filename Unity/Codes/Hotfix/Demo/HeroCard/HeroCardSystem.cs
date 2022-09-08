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

            Log.Warning($"暴击概率 {rate}");
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
            int[] damageRate = skillConfig.LevelDamages;    //等级伤害倍率
            if (skill.Level - 1 < damageRate.Length)
            {
                var rate = damageRate[skill.Level - 1] / 100.0f;
                baseDamage += baseDamage * (rate);
            }

            int buffDamageAdditionCondition = skillConfig.BuffDamageAdditionCondition;

            //todo 是否存在 buff伤害的额外条件，是否满足，满足的话，直接造成额外百分比伤害
            if (buffs != null && buffs.Exists(a => a.ConfigId.Equals(buffDamageAdditionCondition)))
            {
                int[] buffDamageAdditions = skillConfig.BuffDamageAdditions;
                var addition = buffDamageAdditions[skill.Level - 1] / 100.0f;
                baseDamage += baseDamage * addition;
            }

            return baseDamage;
        }
#if SERVER
        public static ActionMessage ProcessBuffRoundLogic(this HeroCard self)
        {
            var actionMessage = new ActionMessage() { PlayType = (int)ActionMessagePlayType.Async, ActionMessages = new List<ActionMessage>() };
            var buffComponent = self.GetComponent<BuffComponent>();
            List<Buff> buffs = buffComponent.GetChilds<Buff>();
            if (buffs == null)
            {
                return null;
            }

            foreach (var buff in buffs)
            {
                // DeductionSelfTotalHealthRate
                actionMessage.ActionMessages.Add(buff.ProcessRound(self));
            }

            return actionMessage;
        }
#endif

        //检查激活buff是否需要条件
        public static bool AttackNewBuffWithCondition(this HeroCard self, HeroCard targetHeroCard, List<Buff> buffs, Skill skill)
        {
            SkillConfig config = SkillConfigCategory.Instance.Get(skill.ConfigId);
            //检查过滤条件
            int activeBuffCondition = config.ActiveBuffCondition; //激活新buff需要的条件
            if (activeBuffCondition != 0)
            {
                //检查是否符合条件，不符合 直接返回
                Buff findBuff = buffs?.Find(a => a.ConfigId.Equals(activeBuffCondition));
                if (findBuff == null)
                {
                    return false;
                }

                //知道的buff 回合数直接归0；
                findBuff.RoundCount = 0;
                Log.Debug("找到了 条件机会，需要将原来buff剩余回合数归零");
                //条件符合，
            }

            return true;
        }

        public static void ProcessAttachBuffLogic(this HeroCard self, HeroCard targetHeroCard, Skill skill)
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

            bool isCondition = self.AttackNewBuffWithCondition(targetHeroCard, currentBuffs, skill);
            Log.Debug($"is condition {isCondition}");
            if (!isCondition)
            {
                //不符合buff条件，直接返回
                return;
            }

            HeroCardDataComponent heroCardDataComponent = self.GetComponent<HeroCardDataComponent>();
            // heroCardDataComponent.GetHeroWeaponAttack()
            for (int i = 0; i < buffConfigIds.Length; i++)
            {
                // Log.Warning($"buffconfig id {buffConfigIds[i]}");
                //根据buff 类型 检查，buff是减益还是增益

                Buff buff = buffComponent.AddBuff(buffConfigIds[i], buffRounds[skill.Level - 1], skillConfig.BuffOverCount, self);
                // Log.Warning($"skill health shield addition {skillConfig.HealthShieldAdditions}");
                int[] healthShieldAdditions = skillConfig.HealthShieldAdditions;
                if (healthShieldAdditions != null)
                {
                    float healthShieldAddition = healthShieldAdditions[skill.Level - 1] / 100.0f;
                    buff.HealthShield = (int)(heroCardDataComponent.TotalHP * healthShieldAddition);
                    // Log.Warning($"health shield {buff.HealthShield}");
                }
                // if (buff.Config.issh)
                // {

                // }

                // Log.Warning($"add buff success {buff}");
                // BuffConfig buffConfig = BuffConfigCategory.Instance.Get(buff.ConfigId);
                // int attackAttackAddition = buffConfig.DeductionAttackAttackHealthRate;
                // if (attackAttackAddition != 0)
                // {
                // }
            }
#endif
        }

        public static bool ProcessRecoveryLogic(this HeroCard self)
        {
            //处理复活逻辑

            // heroCardDataComponent.HP = 

            BuffComponent buffComponent = self.GetComponent<BuffComponent>();
            List<Buff> buffs = buffComponent.GetChilds<Buff>();
            buffs?.RemoveAll(a => a.RoundCount <= 0);
            Buff buff = buffs?.Find(a => a.Config.IsRecovery == (int)RecoveryType.Recovery);
            if (buff != null)
            {
                //todo 可以复活
                HeroCardDataComponent heroCardDataComponent = self.GetComponent<HeroCardDataComponent>();
                heroCardDataComponent.HP = (int)(heroCardDataComponent.TotalHP * buff.Config.RecoveryHealthAddition / 100.0f);

                return true;
            }

            return false;
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
                case (int)SkillRangeType.SingleSelf:
                    self.CareTarget(targetHeroCard, skill);
                    break;
            }
        }

        public static void CareHealth(this HeroCard self, float careHealthCount)
        {
            HeroCardDataComponent heroCardDataComponent = self.GetComponent<HeroCardDataComponent>();

            List<Buff> buffs = self.GetComponent<BuffComponent>().GetChilds<Buff>();

            if (buffs != null)
            {
                int careHealthReduceRate = 0;
                foreach (var buff in buffs)
                {
                    if (buff.RoundCount > 0)
                    {
                        careHealthReduceRate += buff.Config.CareHealthReduceRate;
                    }
                }

                careHealthCount -= careHealthCount * careHealthReduceRate / 100.0f;
            }

            heroCardDataComponent.HP += (int)careHealthCount;
            heroCardDataComponent.AddHP = (int)careHealthCount;
        }

        public static void CareTarget(this HeroCard self, HeroCard targetHeroCard, Skill skill)
        {
            SkillConfig skillConfig = SkillConfigCategory.Instance.Get(skill.ConfigId);
            int[] careHealehs = skillConfig.CareHealths;
            float careHealehRate = careHealehs[skill.Level - 1] / 100.0f;

            // var cardHealthAddition = skillConfig.AttackCount;

            float careHealthCount = 0;

            float selfAttackAdditionHealthRate = skillConfig.SelfAttackAdditionHealths[skill.Level - 1] / 100.0f;

            if (selfAttackAdditionHealthRate > 0)
            {
                careHealthCount += targetHeroCard.GetComponent<HeroCardDataComponent>().GetHeroPlaneAttack() * selfAttackAdditionHealthRate;
            }

            if (careHealehRate > 0)
            {
                // careHealthCount += 
            }

            self.GetComponent<HeroCardDataComponent>().DiamondAttackAddition = 0;
            targetHeroCard.CareHealth(careHealthCount);
        }

        public static float GetBuffAttack(this HeroCard self, List<Buff> buffs, float planeAttack)
        {
            if (buffs == null)
            {
                return planeAttack;
            }

            int attackAddition = 0;
            foreach (var buff in buffs)
            {
                BuffConfig buffConfig = BuffConfigCategory.Instance.Get(buff.ConfigId);
                attackAddition -= buffConfig.AttackReduceRate;
                attackAddition += buffConfig.AttackMultRate;
            }

            if (attackAddition > 100)
            {
                attackAddition = 100;
            }

            float endAttackAddition = planeAttack + planeAttack * attackAddition / 100.0f;
            // return planeAttack - endAttackAddition;
            return endAttackAddition;
        }

        public static float GetBuffDefence(this HeroCard self, List<Buff> buffs, float planeDefence)
        {
            if (buffs == null)
            {
                return planeDefence;
            }

            int defenceReduceRate = 0;
            foreach (var buff in buffs)
            {
                BuffConfig buffConfig = BuffConfigCategory.Instance.Get(buff.ConfigId);
                defenceReduceRate -= buffConfig.DefenceReduceRate;
                defenceReduceRate += buffConfig.DefenceMultRate;
            }

            planeDefence += planeDefence * defenceReduceRate / 100.0f;
            return planeDefence;
        }

        public static void AttackTarget(this HeroCard self, HeroCard targetHeroCard, int comboAddition, Skill skill)
        {
            HeroCardDataComponent attackCom = self.GetComponent<HeroCardDataComponent>();
            HeroCardDataComponent beAttackCom = targetHeroCard.GetComponent<HeroCardDataComponent>();
            List<Buff> attackBuffs = self.GetComponent<BuffComponent>().GetChilds<Buff>();
            attackBuffs?.RemoveAll(a => a.RoundCount <= 0);
            BuffComponent buffComponent = targetHeroCard.GetComponent<BuffComponent>();
            List<Buff> beAttackbuffs = buffComponent.GetChilds<Buff>();
            beAttackbuffs?.RemoveAll(a => a.RoundCount <= 0);
            float planeAttack = attackCom.GetHeroPlaneAttack();
            //取出消除宝石combo加成
            planeAttack += (int)(planeAttack * comboAddition / 100.0f);
            planeAttack = self.GetBuffAttack(attackBuffs, planeAttack);
            // planeAttack = self.GetAddBuff
            var domineeringValue = self.GetDomineering(); //霸气值
            float planeDefence = beAttackCom.GetHeroPlaneDefence();
            planeDefence = self.GetBuffDefence(beAttackbuffs, planeDefence);
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
            damage += self.ProcessSkillDamageValue(skill, damage, beAttackbuffs);
            var critical = self.GetCriticalHit(targetHeroCard); //暴击概率
            var isCritical = RandomHelper.RandomNumber(0, 10000) < critical;
            if (isCritical)
            {
                var critialDamage = self.GetWeaponBaseValueByType(WordBarType.CriticalHitDamage);
                Log.Debug($"{critialDamage}");
                damage *= 1 + 0.5f + (float)critialDamage / 10000;
            }
            var oldHp = beAttackCom.HP;
            Log.Debug($"old hp {oldHp}");
            damage = self.ProcessBuffDamage(beAttackbuffs, damage);
            damage = self.HuDunBuffDamage(beAttackbuffs, damage);
            Log.Debug($"hudun damage {damage}");
            beAttackCom.HP -= (int)damage;
            if (beAttackCom.HP < 0)
            {
                beAttackCom.HP = 0;
            }
            self.ProcessAvoidDeathBuff(beAttackbuffs, beAttackCom, oldHp);
            targetHeroCard.AddAngry(HeroConfigCategory.Instance.Get(targetHeroCard.ConfigId).BeAttackAddAngry);
            damage = oldHp - beAttackCom.HP;
            beAttackCom.Damage = (int)damage;
            beAttackCom.IsCritical = isCritical;
            attackCom.DiamondAttackAddition = 0;
        }

        public static void AddAngry(this HeroCard self, int addAngry)
        {
            HeroCardDataComponent heroCardDataComponent = self.GetComponent<HeroCardDataComponent>();

            List<Buff> buffs = self.GetComponent<BuffComponent>().GetChilds<Buff>();
            if (buffs != null)
            {
                int getAngryReduceRate = 0;
                foreach (var buff in buffs)
                {
                    if (buff.RoundCount <= 0)
                    {
                        continue;
                    }

                    getAngryReduceRate += buff.Config.GetAngryReduceRate;
                }

                var subAngry = addAngry * getAngryReduceRate / 100.0f;
                addAngry -= (int)subAngry;
            }

            heroCardDataComponent.Angry += addAngry;
            heroCardDataComponent.AddAngry = addAngry;
        }

        public static void ProcessAvoidDeathBuff(this HeroCard self, List<Buff> buffs, HeroCardDataComponent heroCardDataComponent, int oldHp)
        {
            if (buffs == null)
            {
                return;
            }

            if (heroCardDataComponent.HP > 0)
            {
                return;
            }

            Buff buff = buffs.Find(a =>
            {
                BuffConfig config = BuffConfigCategory.Instance.Get(a.ConfigId);

                if (config.IsAvoidDeath == (int)AvoidDeathType.AvoidDeath && a.RoundCount > 0)
                {
                    return true;
                }

                return false;
            });
            if (buff == null)
            {
                return;
            }

            BuffConfig buffConfig = BuffConfigCategory.Instance.Get(buff.ConfigId);
            if (oldHp >= buffConfig.AvoidDeathHealth)
            {
                heroCardDataComponent.HP = 1;
            }
        }

        public static float ProcessBuffDamage(this HeroCard self, List<Buff> buffs, float damage)
        {
            //todo 处理buff伤害
            // int AllDamageMultRate = 

            if (buffs == null)
            {
                return damage;
            }

            int allDamageMultRate = 0;
            foreach (var buff in buffs)
            {
                BuffConfig buffConfig = BuffConfigCategory.Instance.Get(buff.ConfigId);
                allDamageMultRate += buffConfig.AllDamageMultRate;
                allDamageMultRate += buffConfig.DamageAddition;

                allDamageMultRate -= buffConfig.EndDamageReduceRate; //最终上好降低
                //所有的伤害倍率
            }
            // Log.Warning($"allDamageMultRate {allDamageMultRate}");

            float addDamage = damage * allDamageMultRate / 100.0f;
            damage += addDamage;

            if (buffs.Exists(a => a.Config.IsInvincible == (int)InvincibleType.Invincible))
            {
                damage = 1;
            }

            return damage;
        }

        public static float HuDunBuffDamage(this HeroCard self, List<Buff> buffs, float damage)
        {
            if (buffs == null)
            {
                return damage;
            }

            buffs.RemoveAll(a => a.RoundCount <= 0);
            float endDamage = damage;
            foreach (var buff in buffs)
            {
                if (endDamage <= buff.HealthShield)
                {
                    var endHp = buff.HealthShield - endDamage;
                    buff.HealthShield = (int)endHp;
                    return 0;
                }

                if (endDamage > buff.HealthShield)
                {
                    endDamage -= buff.HealthShield;
                    buff.HealthShield = 0;
                }
            }

            return endDamage;
        }

        // public static bool GenXingBuff(this HeroCard self, List<Buff> buffs)
        // {
        //     if (buffs != null && buffs.Exists(a => a.ConfigId.Equals(102)))
        //     {
        //         return true;
        //     }
        //
        //     return false;
        // }

        // public static float GetBuffDamageValue(this HeroCard self, List<Buff> buffs, float damage)
        // {
        //     var value = 0.0f;
        //     foreach (var buff in buffs)
        //     {
        //         BuffConfig config = BuffConfigCategory.Instance.Get(buff.ConfigId);
        //         switch (config.Id)
        //         {
        //             case 106:
        //                 value = damage * config.value;
        //                 break;
        //         }
        //     }
        //
        //     return value;
        // }

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