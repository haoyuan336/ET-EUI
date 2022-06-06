using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ET
{
    public static class ESCommonWordBarSystem
    {
        public static  void InitMainWordBarInfoView(this ESCommonWordBar self, WordBarInfo wordBarInfo, WeaponInfo weaponInfo)
        {
            if (!wordBarInfo.IsMain)
            {
                return;
            }
            
            //取出词条的配置
            WeaponWordBarsConfig config = WeaponWordBarsConfigCategory.Instance.Get(wordBarInfo.ConfigId);

            var value = 0;
            var numberType = config.NumberType;
            switch (numberType)
            {
                case (int)NumberType.Number:
                    value = WeaponHelper.GetWordBarNumberValueWithLevel(wordBarInfo, weaponInfo.Level);
                    self.E_WordBarValueTextText.text = value.ToString();

                    break;
                case (int)NumberType.Percent:
                    
                    Log.Debug($"base value {wordBarInfo.Value}");
                    value = WeaponHelper.GetWordBarPercentValueWidthLevel(wordBarInfo, weaponInfo.Level);
                    self.E_WordBarValueTextText.text = $"{(float)value/100}%";

                    break;
            }

            // 等级计算公式	固定数值公式		基础主属性+基础主属性*10%*当前装备等级+当前装备等级*成长系数（攻击成长系数为3，防御成长系数为2，生命成长系数为50）																					

        }
        public static async void SetInfo(this ESCommonWordBar self, WordBarInfo wordBarInfo, WeaponInfo weaponInfo)
        {
            Log.Debug($"is main {wordBarInfo.IsMain}");
            self.E_BgImage.gameObject.SetActive(wordBarInfo.IsMain);
            self.E_LockTextText.gameObject.SetActive(false);
            WeaponWordBarsConfig config = WeaponWordBarsConfigCategory.Instance.Get(wordBarInfo.ConfigId);
            self.E_WordBarTypeTextText.text = config.Name;
            
            self.E_WordBarQualityIconImage.gameObject.SetActive(!wordBarInfo.IsMain);

            
            
            //根据值获取当前的品阶

            switch (config.NumberType)
            {
                case (int) NumberType.Number:
                    self.E_WordBarValueTextText.text = wordBarInfo.Value.ToString();

                    break;
                case (int) NumberType.Percent:
                    self.E_WordBarValueTextText.text = $"{(float) wordBarInfo.Value / 100}%";

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
            
            self.InitMainWordBarInfoView(wordBarInfo, weaponInfo);
        }
    }
}