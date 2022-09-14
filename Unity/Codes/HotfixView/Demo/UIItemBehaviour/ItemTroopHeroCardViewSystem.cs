using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    [ObjectSystem]
    public class Scroll_ItemTroopHeroCardDestroySystem: DestroySystem<Scroll_ItemTroopHeroCard>
    {
        public override void Destroy(Scroll_ItemTroopHeroCard self)
        {
            self.DestroyWidget();
        }
    }

    public static class Scroll_ItemTroopHeroCardSystem
    {
        public static void SetHeroInfo(this Scroll_ItemTroopHeroCard self, HeroCardInfo info)
        {
            // self.HeroCardInfo = info;
            var configId = info.ConfigId;
            HeroConfig config = HeroConfigCategory.Instance.Get(configId);
            // self.E_TextText.text = info.HeroName.ToString();
            self.E_TextText.text = config.HeroName;
            // DiamondTypeConfig diamondTypeConfig = DiamondTypeConfigCategory.Instance.Get(info.HeroColor);
            // self.E_ClickImage.color = ColorTool.HexToColor(diamondTypeConfig.ColorValue);
            // self.E_ToggleToggle.image.color = ColorTool.HexToColor(diamondTypeConfig.ColorValue);
            List<DiamondTypeConfig> list = DiamondTypeConfigCategory.Instance.GetAll().Values.ToList();
            DiamondTypeConfig diamondTypeConfig = list.Find((a) =>
            {
                if (a.ColorId.Equals(config.HeroColor))
                {
                    return true;
                }

                return false;
            });
            self.E_ToggleToggle.image.color = ColorTool.HexToColor(diamondTypeConfig.ColorValue);

        }
    }
}