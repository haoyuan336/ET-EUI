using System.Collections.Generic;

namespace ET
{
    public class WeponSystemAwake: AwakeSystem<Weapon>
    {
        public override void Awake(Weapon self)
        {
        }
    }

    public static class WeaponSystem
    {
        public static WeaponInfo GetInfo(this Weapon self)
        {
            return new WeaponInfo()
            {
                WeaponId = self.Id,
                ConfigId = self.ConfigId,
                Count = self.Count,
                Level = self.Level,
                OnWeaponHeroId = self.OnWeaponHeroId,
                CurrentExp = self.CurrentExp
            };
        }

        public static int GetWeaponWordBarValueByType(this Weapon self, WordBarType wordBarType)
        {
            //获取装备的暴击值
            List<WordBar> wordBars = self.GetChilds<WordBar>();
            var rate = 0;
            foreach (var wordBar in wordBars)
            {
                rate += WeaponHelper.GetWordBarValueByType(wordBar.GetInfo(), self.GetInfo(), wordBarType);
            }

            return rate;
        }
    }
}