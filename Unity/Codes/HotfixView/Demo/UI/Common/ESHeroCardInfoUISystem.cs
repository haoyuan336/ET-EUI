using UnityEngine.UI;

namespace ET
{
    public static class ESHeroCardInfoUISystem
    {
        public static async void SetInfo(this ESHeroCardInfoUI self, HeroCardInfo heroCardInfo, HeroCardDataComponentInfo heroCardDataComponentInfo)
        {
            self.E_CommonText.text = "";
            HeroConfig heroConfig = HeroConfigCategory.Instance.Get(heroCardInfo.ConfigId);

            ElementConfig elementConfig = ElementConfigCategory.Instance.Get(heroConfig.HeroColor);
            // heroConfig.HeroColor;
            var spriteAtlasPath = ConstValue.HeroCardAtlasPath;
            self.E_HeroElementIconImage.GetComponent<Image>().sprite = await
                    AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(spriteAtlasPath, elementConfig.IconImage);
        }
    }
}