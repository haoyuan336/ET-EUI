using System;
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

        // public static int GetWeaponDefence(this HeroCard self)
        // {
        //     List<Weapon> weapons = self.GetChilds<Weapon>();
        //     var baseValue = 0;
        //     foreach (var weapon in weapons)
        //     {
        //         baseValue += weapon.GetWeaponWordBarValueByType(WordBarType.Defecnce);
        //     }
        //
        //     return baseValue;
        // }

        public static AttackAction AngryAttack(this HeroCard self, HeroCard tatgetHeroCard)
        {
            //首先确定，玩家是否已经满怒气
            HeroCardDataComponent heroCardDataComponent = self.GetComponent<HeroCardDataComponent>();
            if (!heroCardDataComponent.IsAngryFull())
            {
                return null;
            }

            //然后找出玩家的必杀技id
            List<Skill> skills = self.GetComponent<SkillComponent>().GetChilds<Skill>();
            Skill bigSkill = skills.Find(a =>
            {
                var skillConfig = SkillConfigCategory.Instance.Get(a.ConfigId);
                if (skillConfig.SkillType == (int)SkillType.BigSkill)
                {
                    return true;
                }

                return false;
            });
            if (bigSkill == null)
            {
                return null;
            }

            heroCardDataComponent.CurrentSkillId = bigSkill.Id;
            heroCardDataComponent.Angry = 0;
            AttackAction attackAction = self.AttackTarget(tatgetHeroCard, 0);
            return attackAction;
        }

        public static AttackAction AttackTarget(this HeroCard self, HeroCard targetHeroCard, int comboAddition)
        {
            HeroConfig config = HeroConfigCategory.Instance.Get(self.ConfigId);
            Log.Debug($"name {config.HeroName}");
            HeroCardDataComponent attackCom = self.GetComponent<HeroCardDataComponent>();
            HeroCardDataComponent beAttackCom = targetHeroCard.GetComponent<HeroCardDataComponent>();
            var baseAttack = attackCom.GetHeroBaseAttack(); //角色的基础攻击
            // Log.Debug($"base attack{baseAttack}");
            var weaponAttack = self.GetWeaponBaseValueByType(WordBarType.Attack); // 装备的基础攻击
            // Log.Debug($"base weaponAttack {weaponAttack}");
            var weaponAttackAddition = self.GetWeaponBaseValueByType(WordBarType.AttackAddition); //装备的攻击力加成
            // Log.Debug($"weaponAttackAddition{weaponAttackAddition}");
            var planeAttack = (baseAttack + weaponAttack) * (1 + weaponAttackAddition / 10000); //面板攻击力

            //取出消除宝石combo加成
            // var comboAddition = self.GetComponent<HeroCardDataComponent>().DiamondAttackAddition;
            planeAttack += (int)(planeAttack * comboAddition / 100.0f);

            // Log.Debug($"planeAttack{planeAttack}");
            var domineeringValue = self.GetDomineering(); //霸气值
            // Log.Debug($"domineeringValue{domineeringValue}");

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
            beAttackCom.HP -= (int)damage;
            if (beAttackCom.HP < 0)
            {
                beAttackCom.HP = 0;
            }

            beAttackCom.Angry += 5;
            damage = oldHp - beAttackCom.HP;
            Log.Debug($"damage {damage}");
            beAttackCom.Damage = (int)damage;
            beAttackCom.IsCritical = isCritical;
            attackCom.DiamondAttackAddition = 0;
            // if (attackCom.IsAngryFull())
            // {
            //     //todo 如果施放的是大招技能，那么需要将怒气值归零
            //     attackCom.Angry = 0;
            // }

            var attackAction = new AttackAction()
            {
                AttackHeroCardDataComponentInfo = attackCom.GetInfo(), BeAttackHeroCardDataComponentInfo = beAttackCom.GetInfo()
            };
            return attackAction;
        }
#if !SERVER
        public static async ETTask PlayHeroCardAttackAnimAsync(this HeroCard self, AttackAction action)
        {
            // Log.Debug("PlayHeroCardAttackAnimAsync");
            // HeroCard attackHeroCard = self.GetChild<HeroCard>(action.AttackHeroCardInfo.HeroId);
            // self.Angry = action.AttackHeroCardDataComponentInfo.Angry;
            // self.CurrentSkillId = action.AttackHeroCardDataComponentInfo.CurrentSkillId;
            // List<HeroCard> beAttackHeroCards = new List<HeroCard>();
            // foreach (var cardInfo in action.BeAttackHeroCardInfo)
            // {
            //     beAttackHeroCards.Add(self.Parent.GetChild<HeroCard>(cardInfo.HeroId));
            // }
            // beAttackHeroCard.GetHP() = action.BeAttackHeroCardInfo[0].HP;
            // beAttackHeroCard.Angry = action.BeAttackHeroCardInfo[0].Angry;
            Log.Debug($"be attack hero id {action.BeAttackHeroCardDataComponentInfo.HeroId}");
            HeroCard beAttackHeroCard = self.Parent.GetChild<HeroCard>(action.BeAttackHeroCardDataComponentInfo.HeroId);
            await Game.EventSystem.PublishAsync(new EventType.PlayHeroCardAttackAnim()
            {
                AttackHeroCard = self,
                BeAttackHeroCard = beAttackHeroCard,
                BeAttackHeroCardDataComponentInfo = action.BeAttackHeroCardDataComponentInfo,
                AttackHeroCardDataComponentInfo = action.AttackHeroCardDataComponentInfo
            });
            await ETTask.CompletedTask;
        }
#endif

        //         public static async ETTask Call(this HeroCard self, int zone, long ownerId)
        //         {
        //             self.OwnerId = ownerId;
        //             self.CallTime = TimeHelper.ServerNow();
        //             //todo 先创建继承数据
        //             // await self.CallSkill(zone);
        // #if SERVER
        //             await DBManagerComponent.Instance.GetZoneDB(zone).Save(self);
        // #endif
        //             Log.Debug("hero call complete");
        //         }
        // public static async ETTask CallSkill(this HeroCard self, int zone)
        // {
        //     HeroConfig heroConfig = HeroConfigCategory.Instance.Get(self.ConfigId);
        //     List<string> skillStr = heroConfig.SkillIdList.Split(',').ToList();
        //     List<ETTask> tasks = new List<ETTask>();
        //     foreach (var skillId in skillStr)
        //     {
        //         // Skill skill = self.AddChild<Skill, int>(int.Parse(skillId));
        //         Skill skill = new Skill();
        //         skill.Id = IdGenerater.Instance.GenerateId();
        //         skill.ConfigId = int.Parse(skillId);
        //         tasks.Add(skill.Call(zone, self.Id));
        //     }
        //
        //     await ETTaskHelper.WaitAll(tasks);
        //     Log.Debug("all skill call complete");
        //     await ETTask.CompletedTask;
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