using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ET
{
    public static class ESCommonWordBarSystem
    {
        public static async void SetInfo(this ESCommonWordBar self, WordBarInfo wordBarInfo)
        {
            Log.Debug($"is main {wordBarInfo.IsMain}");
            self.E_BgImage.gameObject.SetActive(wordBarInfo.IsMain);
            self.E_LockTextText.gameObject.SetActive(false);
            WeaponWordBarsConfig config = WeaponWordBarsConfigCategory.Instance.Get(wordBarInfo.ConfigId);
            self.E_WordBarTypeTextText.text = config.Name;

            //根据值获取当前的品阶

            switch (config.NumberType)
            {
                case (int) NumberType.Number:
                    self.E_WordBarValueTextText.text = wordBarInfo.Value.ToString();

                    break;
                case (int) NumberType.Percent:
                    self.E_WordBarValueTextText.text = $"{(float) wordBarInfo.Value / 10}%";

                    break;
            }

            List<WeaponWordBarsConfig> configs = WeaponWordBarsConfigCategory.Instance.GetAll().Values.ToList()
                    .FindAll(a => a.Star.Equals(config.Star) && a.WordBarType == config.WordBarType);
            configs.Sort((a, b) => { return a.Id - b.Id; });
            var qualityConfig = configs.Find(a => { return wordBarInfo.Value >= a.MinValue && wordBarInfo.Value <= a.MaxValue; });
            if (qualityConfig == null)
            {
                qualityConfig = configs.Last();
            }

            Log.Debug($"quality config {qualityConfig.Id}");

            var sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(ConstValue.WeaponAtlasPath, qualityConfig.QualityIcon);
            self.E_WordBarQualityIconImage.sprite = sprite;
        }
    }
}