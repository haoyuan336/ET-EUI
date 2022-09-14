using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public static class ESTroopHeroCardsSystem
    {

        public static void RegisterUIEvent(this ESTroopHeroCards self)
        {
            Log.Debug("注册公共组件的事件");
            self.E_TroopCardContentLoopVerticalScrollRect.AddItemRefreshListener(self.OnLoopEvent);
            self.AddUIScrollItems(ref self.ItemTroopHeroCards, 3);
            self.E_TroopCardContentLoopVerticalScrollRect.SetVisible(true, 3);
        }
        public static void UpdateHeroCardInfos(this ESTroopHeroCards self, List<HeroCardInfo> heroCardInfos)
        {
            self.TroopHeroCardInfos = heroCardInfos;
            self.E_TroopCardContentLoopVerticalScrollRect.RefreshCells();
        }

        public static void UpdateHeroCardInfo(this ESTroopHeroCards self, List<HeroCardInfo> heroCardInfos)
        {
            self.TroopHeroCardInfos = heroCardInfos;
            self.E_TroopCardContentLoopVerticalScrollRect.RefreshCells();
        }
        public static void OnLoopEvent(this ESTroopHeroCards self, Transform transform, int index)
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
    }
}