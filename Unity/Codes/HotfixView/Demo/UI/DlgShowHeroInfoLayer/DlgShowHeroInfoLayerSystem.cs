using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgShowHeroInfoLayerSystem
    {
        public static void RegisterUIEvent(this DlgShowHeroInfoLayer self)
        {
            self.View.E_BackButton.AddListener(() =>
            {
                self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_HeroInfoLayerUI);
                self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_ShowHeroInfoLayer);

            });
        }

        public static void ShowWindow(this DlgShowHeroInfoLayer self, Entity contextData = null)
        {
            var heroCardItem = (Scroll_ItemHeroCard) contextData;
            self.View.E_ATKText.text = $"ATK:{heroCardItem.HeroCardInfo.Attack}";
            self.View.E_HPText.text = $"HP:{heroCardItem.HeroCardInfo.HP}";
            self.View.E_DEFText.text = $"DEF:{heroCardItem.HeroCardInfo.Defence}";
            self.View.E_HeroNameText.text = heroCardItem.HeroCardInfo.HeroName;
            self.View.E_LevelText.text = $"Lv:{heroCardItem.HeroCardInfo.Level}";
        }
    }
}