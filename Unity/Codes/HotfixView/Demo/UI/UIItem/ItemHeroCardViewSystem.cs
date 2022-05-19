using UnityEngine;

namespace ET
{
    public static class ItemHeroCardViewSystem
    {
        public static async void InitHeroCard(this Scroll_ItemHeroCard self, HeroCardInfo heroCardInfo)
        {
            var configId = heroCardInfo.ConfigId;
            var config = HeroConfigCategory.Instance.Get(configId);
            self.E_CountText.gameObject.SetActive(config.MaterialType == (int) HeroBagType.Materail);
            self.E_CountText.text = heroCardInfo.Count.ToString();

            self.E_ChooseCountText.gameObject.SetActive(false);
            var spriteAtlas = ConstValue.HeroCardAtlasPath;
            var headImage = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(spriteAtlas, config.HeroIconImage);
            self.E_HeadImage.sprite = headImage;
            self.E_ElementImage.gameObject.SetActive(config.MaterialType == (int) HeroBagType.Hero);
            self.E_LevelText.gameObject.SetActive(config.MaterialType == (int) HeroBagType.Hero);
            self.E_LevelText.text = $"Lv.{heroCardInfo.Level.ToString()}";

            var elementConfig = ElementConfigCategory.Instance.Get(config.HeroColor);
            var elementImageStr = elementConfig.IconImage;
            var sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(ConstValue.HeroCardAtlasPath, elementImageStr);
            self.E_ElementImage.sprite = sprite;

            HeroQualityTypeConfig heroQualityTypeConfig = HeroQualityTypeConfigCategory.Instance.Get(config.HeroQuality);
            var aualityIcon = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(spriteAtlas, heroQualityTypeConfig.Icon);
            self.E_QualityIconImage.gameObject.SetActive(true);
            self.E_QualityIconImage.sprite = aualityIcon;
            
            for (int i = 0; i < 5; i++)
            {
                // var star    
                var starStr = $"Star_{i}";
                Transform starObj = UIFindHelper.FindDeepChild(self.uiTransform.gameObject, starStr);
                if (starObj != null)
                {
                    starObj.gameObject.SetActive(i < heroCardInfo.Star);
                }
            }
        }
    }
}