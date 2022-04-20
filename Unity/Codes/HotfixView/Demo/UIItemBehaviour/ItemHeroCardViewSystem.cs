using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

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
            self.E_TextText.text = info.ConfigId.ToString();
            DiamondTypeConfig diamondTypeConfig = DiamondTypeConfigCategory.Instance.Get(info.HeroColor);
            self.E_ClickImage.color = ColorTool.HexToColor(diamondTypeConfig.ColorValue);

        }

      
    }
}