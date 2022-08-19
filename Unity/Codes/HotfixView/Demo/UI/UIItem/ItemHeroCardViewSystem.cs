using UnityEngine;

namespace ET
{
    public class ItemHeroCardAwakeSystem: AwakeSystem<Scroll_ItemHeroCard, Transform>
    {
        public override void Awake(Scroll_ItemHeroCard self, Transform a)
        {
            self.uiTransform = a;
        }
    }

    public static class ItemHeroCardViewSystem
    {
        public static void SetNullStateView(this Scroll_ItemHeroCard self)
        {
            self.E_LevelText.gameObject.SetActive(false);
            // self.E_QualityIconImage.gameObject.SetActive(false);
            // self.E_ElementImage.gameObject.SetActive(false);
            self.E_AddTextText.gameObject.SetActive(true);
            self.E_RankImage.gameObject.SetActive(false);
            // self.E_NameText.gameObject.SetActive(false);
            self.E_AddTextText.gameObject.SetActive(false);

            for (int i = 1; i < 6; i++)
            {
                // var star    
                var starStr = $"Star_{i}";
                var bStarStr = $"BStar_{i}";
                Transform starObj = UIFindHelper.FindDeepChild(self.uiTransform.gameObject, starStr);
                if (starObj != null)
                {
                    starObj.gameObject.SetActive(false);
                }
                Transform bStarObj = UIFindHelper.FindDeepChild(self.uiTransform.gameObject, bStarStr);
                if (bStarObj != null)
                {
                    bStarObj.gameObject.SetActive(false);
                }
            }

            // var commonPath = ConstValue.CommonUIAtlasPath;
            // var framePath = ConstValue.FrameBgPath;
            // self.E_HeadImage.sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(commonPath, framePath);
            self.E_HeadImage.sprite = null;
        }

        public static async void InitHeroCard(this Scroll_ItemHeroCard self, HeroCardInfo heroCardInfo)
        {
            self.HeroCardInfo = heroCardInfo;
            if (heroCardInfo == null)
            {
                self.SetNullStateView();
                return;
            }

            // for (int i = 0; i < 5; i++)
            // {
            //     // var star    
            //     var starStr = $"Star_{i}";
            //     Transform starObj = UIFindHelper.FindDeepChild(self.uiTransform.gameObject, starStr);
            //     if (starObj != null)
            //     {
            //         starObj.gameObject.SetActive(false);
            //     }
            // }
            self.E_RankImage.gameObject.SetActive(false);

            self.E_AddTextText.gameObject.SetActive(false);

            var configId = heroCardInfo.ConfigId;
            var config = HeroConfigCategory.Instance.Get(configId);
            // self.E_CountText.gameObject.SetActive(config.MaterialType == (int) HeroBagType.Materail);
            self.E_CountText.text = heroCardInfo.Count.ToString();
            // self.E_NameText.gameObject.SetActive(true);
            // self.E_NameText.text = config.HeroEnName;

            self.E_ChooseCountText.gameObject.SetActive(false);
            var spriteAtlas = ConstValue.HeroCardAtlasPath;

            // self.E_ElementImage.gameObject.SetActive(true);
            // self.E_LevelText.gameObject.SetActive(true);
            // self.E_ElementImage.gameObject.SetActive(config.MaterialType == (int) HeroBagType.Hero);
            // self.E_LevelText.gameObject.SetActive(config.MaterialType == (int) HeroBagType.Hero);
            self.E_LevelText.gameObject.SetActive(true);
            self.E_LevelText.text = $"{heroCardInfo.Level.ToString()}";

            // var elementConfig = ElementConfigCategory.Instance.Get(config.HeroColor);
            // var elementImageStr = elementConfig.IconImage;
            // var sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(ConstValue.HeroCardAtlasPath, elementImageStr);
            // self.E_ElementImage.sprite = sprite;

            // HeroQualityTypeConfig heroQualityTypeConfig = HeroQualityTypeConfigCategory.Instance.Get(config.HeroQuality);
            // var aualityIcon = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(spriteAtlas, heroQualityTypeConfig.Icon);
            // self.E_QualityIconImage.gameObject.SetActive(true);
            // self.E_QualityIconImage.sprite = aualityIcon;
            // self.E_QualityIconImage.gameObject.SetActive(config.MaterialType == (int) HeroBagType.Hero);
            for (int i = 1; i < 6; i++)
            {
                // var star    
                var starStr = $"Star_{i}";
                Transform starObj = UIFindHelper.FindDeepChild(self.uiTransform.gameObject, starStr);
                if (starObj != null)
                {
                    starObj.gameObject.SetActive(i <= heroCardInfo.Star);
                }
                var bStarStr = $"BStar_{i}";
                Transform bStarObj = UIFindHelper.FindDeepChild(self.uiTransform.gameObject, bStarStr);
                if (bStarObj != null)
                {
                    bStarObj.gameObject.SetActive(true);
                }
            }

            var headImage = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(spriteAtlas, config.HeroIconImage);
            Log.Debug("Head image load succcess");
            self.E_HeadImage.sprite = headImage;
        }
    }
}