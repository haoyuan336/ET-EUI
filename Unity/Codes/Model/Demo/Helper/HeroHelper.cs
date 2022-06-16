using UnityEngine;

namespace ET
{
    public static class HeroHelper
    {
        /// <summary>
        /// 获取升级后 剩下多少经验值
        /// </summary>
        /// <param name="heroCardInfo"></param>
        /// <param name="sumExp"></param>
        /// <returns></returns>
        public static int GetHeroLevelLastExp(HeroCardInfo heroCardInfo, int sumExp)
        {
            HeroCardInfo info = new HeroCardInfo() { Level = heroCardInfo.Level };

            var exp = GetNextLevelExp(info);
            while (sumExp >= exp)
            {
                exp = HeroHelper.GetNextLevelExp(info);
                sumExp -= exp;
                info.Level++;
            }

            return sumExp;
        }

        /// <summary>
        /// 
        /// 获取英雄可以升多少级
        /// </summary>
        /// <param name="heroCardInfo"></param>
        /// <param name="sumExp"></param>
        /// <returns></returns>
        public static int GetHeroLevelInfoWithExp(HeroCardInfo heroCardInfo, int sumExp)
        {
            HeroCardInfo info = new HeroCardInfo() { Level = heroCardInfo.Level };

            var exp = HeroHelper.GetNextLevelExp(info);
            ;
            while (sumExp >= exp)
            {
                exp = HeroHelper.GetNextLevelExp(info);
                sumExp -= exp;
                info.Level++;
            }

            return info.Level;
        }

        /// <summary>
        /// 获取英雄总共的经验值
        /// </summary>
        /// <param name="heroCardInfo"></param>
        /// <returns></returns>
        public static int GetHeroAllLevelExp(HeroCardInfo heroCardInfo)
        {
            int sumExp = 0;
            var level = heroCardInfo.Level;
            for (int i = 0; i < level; i++)
            {
                HeroLevelExpConfig config = HeroLevelExpConfigCategory.Instance.Get(i + 1);
                sumExp += config.EXP;
            }

            sumExp *= heroCardInfo.Count;

            return sumExp;
        }

        /// <summary>
        /// 获取下一级需要的经验值
        /// </summary>
        /// <param name="heroCardInfo"></param>
        /// <returns></returns>
        public static int GetNextLevelExp(HeroCardInfo heroCardInfo)
        {
            //获取下一级需要的经验值
            HeroLevelExpConfig config = HeroLevelExpConfigCategory.Instance.Get(heroCardInfo.Level + 1);
            return config.EXP;
        }

        /// <summary>
        /// 获取基础值
        /// </summary>
        /// <param name="heroCardInfo"></param>
        /// <param name="growthCoefficient"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GetHeroBaseValue(HeroCardInfo heroCardInfo, int growthCoefficient, int value)
        {
            int level = heroCardInfo.Level;
            int star = heroCardInfo.Star;
            int rank = heroCardInfo.Rank;
            float baseValue = value;
            baseValue = baseValue * growthCoefficient / 2; //基础值
            baseValue = baseValue + baseValue * (rank) / 10; // 升阶后的成长值
            var levelValue = baseValue * (0.03f + growthCoefficient / 1000.0f) * (level + 1);
            baseValue = baseValue + levelValue; //升级后的成长值
            var starValue = growthCoefficient * 100 * (star); //升星后的成长值
            return (int) Mathf.Ceil(baseValue + starValue);
        }

        /// <summary>
        /// 获得基础攻击力
        /// </summary>
        /// <param name="heroCardInfo"></param>
        /// <returns></returns>
        public static int GetHeroBaseAttack(HeroCardInfo heroCardInfo)
        {
            // HeroCard heroCard = self.GetParent<HeroCard>();
            HeroConfig config = HeroConfigCategory.Instance.Get(heroCardInfo.ConfigId);
            var baseAttack = config.BaseAttack;
            var growthCoefficient = config.AttackGrowthCoefficient;
            var baseValue = GetHeroBaseValue(heroCardInfo, growthCoefficient, baseAttack);
            Log.Warning($"get hero base attack {baseValue}");
            return baseValue;
        }

        /// <summary>
        /// 获取基础防御力
        /// </summary>
        /// <param name="heroCardInfo"></param>
        /// <returns></returns>
        public static int GetHeroBaseDefence(HeroCardInfo heroCardInfo)
        {
            HeroConfig config = HeroConfigCategory.Instance.Get(heroCardInfo.ConfigId);
            var baseValue = config.BaseDefence;
            var growth = config.DefenceGrowthCoefficient;
            return GetHeroBaseValue(heroCardInfo, growth, baseValue);
        }

        /// <summary>
        /// 获取基础血量值
        /// </summary>
        /// <param name="heroCardInfo"></param>
        /// <returns></returns>
        public static int GetHeroBaseHP(HeroCardInfo heroCardInfo)
        {
            HeroConfig config = HeroConfigCategory.Instance.Get(heroCardInfo.ConfigId);
            var baseValue = config.HeroHP;
            var growth = config.HPGrowthCoefficient;
            return GetHeroBaseValue(heroCardInfo, growth, baseValue);
        }

        /// <summary>
        /// 获取下一级的攻击力
        /// </summary>
        /// <param name="heroCardInfo"></param>
        /// <returns></returns>
        public static int GetNextLevelHeroBaseAttack(HeroCardInfo heroCardInfo)
        {
            HeroConfig config = HeroConfigCategory.Instance.Get(heroCardInfo.ConfigId);
            int growValue = config.AttackGrowthCoefficient;
            HeroCardInfo info = new HeroCardInfo();
            info.Level = heroCardInfo.Level + 1;
            info.Star = heroCardInfo.Star;
            info.Rank = heroCardInfo.Rank;
            return GetHeroBaseValue(info, growValue, config.BaseAttack);
        }

        /// <summary>
        /// 获取下一星级的攻击力
        /// </summary>
        /// <param name="heroCardInfo"></param>
        /// <returns></returns>
        public static int GetNextLevelHeroBaseDefence(HeroCardInfo heroCardInfo)
        {
            HeroConfig config = HeroConfigCategory.Instance.Get(heroCardInfo.ConfigId);
            int growValue = config.DefenceGrowthCoefficient;
            HeroCardInfo info = new HeroCardInfo();
            info.Level = heroCardInfo.Level + 1;
            info.Star = heroCardInfo.Star;
            info.Rank = heroCardInfo.Rank;
            return GetHeroBaseValue(info, growValue, config.BaseDefence);
        }

        /// <summary>
        /// 获取下一级的基础生命值
        /// </summary>
        /// <param name="heroCardInfo"></param>
        /// <returns></returns>
        public static int GetNextLevelHeroHP(HeroCardInfo heroCardInfo)
        {
            HeroConfig config = HeroConfigCategory.Instance.Get(heroCardInfo.ConfigId);
            int growValue = config.HPGrowthCoefficient;
            HeroCardInfo info = new HeroCardInfo();
            info.Level = heroCardInfo.Level + 1;
            info.Star = heroCardInfo.Star;
            info.Rank = heroCardInfo.Rank;
            return GetHeroBaseValue(info, growValue, config.HeroHP);
        }

        /// <summary>
        /// 获取下一个星级的攻击力
        /// </summary>
        /// <param name="heroCardInfo"></param>
        /// <returns></returns>
        public static int GetNextStarHeroBaseAttack(HeroCardInfo heroCardInfo)
        {
            HeroConfig config = HeroConfigCategory.Instance.Get(heroCardInfo.ConfigId);
            int growValue = config.AttackGrowthCoefficient;
            HeroCardInfo info = new HeroCardInfo();
            info.Level = heroCardInfo.Level;
            info.Star = heroCardInfo.Star + 1;
            info.Rank = heroCardInfo.Rank;
            return GetHeroBaseValue(info, growValue, config.BaseAttack);
        }

        /// <summary>
        /// 获取下一星级的防御力
        /// </summary>
        /// <param name="heroCardInfo"></param>
        /// <returns></returns>
        public static int GetNextStarHeroBaseDefence(HeroCardInfo heroCardInfo)
        {
            HeroConfig config = HeroConfigCategory.Instance.Get(heroCardInfo.ConfigId);
            int growValue = config.DefenceGrowthCoefficient;
            HeroCardInfo info = new HeroCardInfo();
            info.Level = heroCardInfo.Level;
            info.Star = heroCardInfo.Star + 1;
            info.Rank = heroCardInfo.Rank;
            return GetHeroBaseValue(info, growValue, config.BaseDefence);
        }

        /// <summary>
        /// 获取下一星级的生命值 
        /// </summary>
        /// <param name="heroCardInfo"></param>
        /// <returns></returns>
        public static int GetNextStarHeroBaseHP(HeroCardInfo heroCardInfo)
        {
            HeroConfig config = HeroConfigCategory.Instance.Get(heroCardInfo.ConfigId);
            int growValue = config.HPGrowthCoefficient;
            HeroCardInfo info = new HeroCardInfo();
            info.Level = heroCardInfo.Level;
            info.Star = heroCardInfo.Star + 1;
            info.Rank = heroCardInfo.Rank;
            return GetHeroBaseValue(info, growValue, config.HeroHP);
        }
    }
}