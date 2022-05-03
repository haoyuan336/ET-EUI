using System.Collections.Generic;
using System.Linq;

namespace ET
{
    [ObjectSystem]
    public class Scroll_ItemHeroCardDestroySystem: DestroySystem<Scroll_ItemHeroCard>
    {
        public override void Destroy(Scroll_ItemHeroCard self)
        {
            self.DestroyWidget();
        }
    }

    public static class Scroll_ItemHeroCardSystem
    {
        public static void SetHeroInfo(this Scroll_ItemHeroCard self, HeroCardInfo info)
        {
            self.HeroCardInfo = info;
            var configId = self.HeroCardInfo.ConfigId;
            HeroConfig config = HeroConfigCategory.Instance.Get(configId);
            // self.E_TextText.text = info.HeroName.ToString();
            if (info.HeroName.Equals("法老"))
            {
                Log.Warning($"hero color {info.HeroColor}");
            }
            self.E_TextText.text = config.HeroName;
            // DiamondTypeConfig diamondTypeConfig = DiamondTypeConfigCategory.Instance.Get(config.HeroColor);
            List<DiamondTypeConfig> list = DiamondTypeConfigCategory.Instance.GetAll().Values.ToList();
            DiamondTypeConfig diamondTypeConfig = list.Find((a) =>
            {
                if (a.ColorId.Equals(config.HeroColor))
                {
                    return true;
                }

                return false;
            });
            self.E_ClickImage.color = ColorTool.HexToColor(diamondTypeConfig.ColorValue);

        }

      
    }
}