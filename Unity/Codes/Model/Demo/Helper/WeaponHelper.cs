using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ET
{
    public static class WeaponHelper
    {
        public static int GetTotalExp(WeaponInfo weaponInfo)
        {
            var level = weaponInfo.Level;
            if (level == 0)
            {
                level = 1;
            }

            WeaponLevelExpConfig config = WeaponLevelExpConfigCategory.Instance.Get(level);

            return config.TotalExp * weaponInfo.Count;
        }

        public static int GetUpdateLevelNeedExp(WeaponInfo weaponInfo)
        {
            var level = weaponInfo.Level;
            if (level == 0)
            {
                level = 1;
            }

            WeaponLevelExpConfig config = WeaponLevelExpConfigCategory.Instance.Get(level + 1);
            return config.EXP;
        }

        /// <summary>
        /// 根据给的经验值，判断能升到多少级
        /// </summary>
        /// <param name="weaponsConfig"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static int GetEndLevelWithExp(WeaponInfo weaponInfo, int exp)
        {
            var totalExp = GetTotalExp(weaponInfo) + weaponInfo.CurrentExp + exp;
            List<WeaponLevelExpConfig> configs = WeaponLevelExpConfigCategory.Instance.GetAll().Values.ToList();
            int level = 1;
            foreach (var config in configs)
            {
                if (totalExp <= config.TotalExp)
                {
                    break;
                }

                level = config.Id;
            }

            return level;
        }

        /// <summary>
        /// 获取升级后 ，剩余的经验值
        /// </summary>
        /// <param name="weaponInfo"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static int GetUpdateLevelLastExp(WeaponInfo weaponInfo, int exp)
        {
            var currentTotalExp = GetTotalExp(weaponInfo) + weaponInfo.CurrentExp + exp;
            //根据传入的经验值，获得最终的等级。根据最终等级，获取总经验值 + exp，
            Log.Debug($"current total exp {currentTotalExp}");
            int endLevel = GetEndLevelWithExp(weaponInfo, exp);
            Log.Debug($"end level {endLevel}");
            int endExp = GetTotalExp(new WeaponInfo() { Level = endLevel, Count = 1 });
            Log.Debug($"end exp {endExp}");
            return currentTotalExp - endExp;
        }

        /// <summary>
        /// 获取词条的基础值 等级
        /// </summary>
        /// <param name="wordBarInfo"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public static int GetWordBarNumberValueWithLevel(WordBarInfo wordBarInfo, int level)
        {
            if (!wordBarInfo.IsMain)
            {
                return wordBarInfo.Value;
            }

            // 等级计算公式	固定数值公式		基础主属性+基础主属性*10%*当前装备等级+当前装备等级*成长系数（攻击成长系数为3，防御成长系数为2，生命成长系数为50）																					

            WeaponWordBarsConfig config = WeaponWordBarsConfigCategory.Instance.Get(wordBarInfo.ConfigId);
            var value = wordBarInfo.Value + wordBarInfo.Value * 0.1f * level + level * config.Coefficient;
            return (int) value;
        }

        //
        public static int GetWordBarPercentValueWidthLevel(WordBarInfo wordBarInfo, int level)
        {
            if (!wordBarInfo.IsMain)
            {
                return wordBarInfo.Value;
            }
            // 百分比数值公式		基础主属性+基础主属性*10%*当前装备等级			

            // WeaponWordBarsConfig config = WeaponWordBarsConfigCategory.Instance.Get(wordBarInfo.ConfigId);

            var baseValue = (float) wordBarInfo.Value;
            Log.Debug($"base value{baseValue}");
            float value = baseValue + baseValue * 0.1f * level;
            Log.Debug($"value{value}");
            Log.Debug($"level {level}");
            return (int) value;
        }

        /// <summary>
        /// 获得词条的血量
        /// </summary>
        /// <param name="weaponInfo"></param>
        public static int GetWordBarHP(WordBarInfo wordBarInfo, WeaponInfo weaponInfo)
        {
            WeaponWordBarsConfig config = WeaponWordBarsConfigCategory.Instance.Get(wordBarInfo.ConfigId);
            if (config.WordBarType == (int) WordBarType.HP)
            {
                return GetWordBarNumberValueWithLevel(wordBarInfo, weaponInfo.Level);
            }

            return 0;
            // if (config.)
            // {
            // }
        }
        // public static 

        public static int GetWordBarValueByType(WordBarInfo wordBarInfo, WeaponInfo weaponInfo, WordBarType type)
        {
            WeaponWordBarsConfig config = WeaponWordBarsConfigCategory.Instance.Get(wordBarInfo.ConfigId);
            Log.Debug($"type {(int)type}word bar type{config.WordBarType}");
            if (config.WordBarType == (int) type)
            {
                // return GetWordBarNumberValueWithLevel(wordBarInfo, weaponInfo.Level);
                switch (config.NumberType)
                {
                    case (int)NumberType.Number:
                        return GetWordBarNumberValueWithLevel(wordBarInfo, weaponInfo.Level);
                    case (int)NumberType.Percent:
                        Log.Debug("获取的数据是备份比");
                        return GetWordBarPercentValueWidthLevel(wordBarInfo, weaponInfo.Level);
                }
            }
            else
            {
                // Log.Debug("返回的是0");
            }

            return 0;
        }
    }
}