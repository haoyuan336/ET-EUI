using System.Collections;
using System.Collections.Generic;
using System;
using ET.Account;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgTroopHeroCardLayerSystem
    {
        public static void RegisterUIEvent(this DlgTroopHeroCardLayer self)
        {
            self.View.E_TroopCardContentLoopVerticalScrollRect.AddItemRefreshListener(self.OnLoopEvent);
        }
        public static void UpdateHeroCardInfo(this DlgTroopHeroCardLayer self, List<HeroCardInfo> heroCardInfos)
        {
            self.TroopHeroCardInfos = heroCardInfos;
            self.View.E_TroopCardContentLoopVerticalScrollRect.RefreshCells();
        }

        public static void ShowWindow(this DlgTroopHeroCardLayer self, Entity contextData = null)
        {
            self.AddUIScrollItems(ref self.ItemTroopHeroCards, 3);
            self.View.E_TroopCardContentLoopVerticalScrollRect.SetVisible(true, 3);
        }
        public static void OnLoopEvent(this DlgTroopHeroCardLayer self, Transform transform, int index)
        {
            Scroll_ItemHeroCard itemHeroCard = self.ItemTroopHeroCards[index].BindTrans(transform);
            HeroCardInfo heroCardInfo = self.TroopHeroCardInfos.Find(a => a.InTroopIndex.Equals(index));
            if (heroCardInfo != null)
            {
                itemHeroCard.InitHeroCard(heroCardInfo);
            }
            else
            {
                itemHeroCard.E_HeadImage.sprite = null;
                itemHeroCard.E_ElementImage.gameObject.SetActive(false);
                itemHeroCard.E_QualityIconImage.gameObject.SetActive(false);
            }

            itemHeroCard.E_ChooseToggle.onValueChanged.RemoveAllListeners();
            itemHeroCard.E_ChooseToggle.onValueChanged.AddListener((value) =>
            {
                itemHeroCard.E_ChooseToggle.isOn = false;
                // self.OnTroopHeroCardItemClickAction(itemHeroCard, heroCardInfo, value);
                if (self.ItemCardClickAction != null)
                {
                    self.ItemCardClickAction(heroCardInfo, value);
                }
            });
        }
        public static void HideWindow(this DlgTroopHeroCardLayer self)
        {
            self.ItemCardClickAction = null;
        }
    }
}