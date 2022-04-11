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
            self.E_ClickImage.color = self.HexToColor(diamondTypeConfig.ColorValue);
        }

        public static Color HexToColor(this Scroll_ItemHeroCard self, string hex)
        {
            hex = hex.Replace("0x", string.Empty);
            hex = hex.Replace("#", string.Empty);
            byte a = byte.MaxValue;
            byte r = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
            byte g = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
            byte b = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
            if (hex.Length == 8)
            {
                a = byte.Parse(hex.Substring(6, 2), NumberStyles.HexNumber);
            }

            return new Color32(r, g, b, a);
        }
    }
}