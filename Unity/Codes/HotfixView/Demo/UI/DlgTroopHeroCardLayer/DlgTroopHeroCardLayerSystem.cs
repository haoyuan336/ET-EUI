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
                self.InitHeroCardView(itemHeroCard, heroCardInfo);
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

        public static async void InitHeroCardView(this DlgTroopHeroCardLayer self, Scroll_ItemHeroCard itemHeroCard, HeroCardInfo heroCardInfo)
        {
            var configId = heroCardInfo.ConfigId;
            var config = HeroConfigCategory.Instance.Get(configId);
            itemHeroCard.E_CountText.gameObject.SetActive(config.MaterialType == (int) HeroBagType.Materail);
            itemHeroCard.E_CountText.text = heroCardInfo.Count.ToString();

            itemHeroCard.E_ChooseCountText.gameObject.SetActive(false);
            var spriteAtlas = ConstValue.HeroCardAtlasPath;
            var headImage = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(spriteAtlas, config.HeroIconImage);
            itemHeroCard.E_HeadImage.sprite = headImage;
            itemHeroCard.E_ElementImage.gameObject.SetActive(config.MaterialType == (int) HeroBagType.Hero);
            itemHeroCard.E_LevelText.gameObject.SetActive(config.MaterialType == (int) HeroBagType.Hero);
            itemHeroCard.E_LevelText.text = $"Lv.{heroCardInfo.Level.ToString()}";

            var elementConfig = ElementConfigCategory.Instance.Get(config.HeroColor);
            var elementImageStr = elementConfig.IconImage;
            var sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(ConstValue.HeroCardAtlasPath, elementImageStr);
            itemHeroCard.E_ElementImage.sprite = sprite;
            
            HeroQualityTypeConfig heroQualityTypeConfig = HeroQualityTypeConfigCategory.Instance.Get(config.HeroQuality);
            var aualityIcon = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(spriteAtlas, heroQualityTypeConfig.Icon);
            itemHeroCard.E_QualityIconImage.gameObject.SetActive(true);
            itemHeroCard.E_QualityIconImage.sprite = aualityIcon;

            for (int i = 0; i < 5; i++)
            {
                // var star    
                var starStr = $"Star_{i}";
                Transform starObj = UIFindHelper.FindDeepChild(itemHeroCard.uiTransform.gameObject, starStr);
                if (starObj != null)
                {
                    starObj.gameObject.SetActive(i < heroCardInfo.Star);
                }
            }
        }

        public static void HideWindow(this DlgTroopHeroCardLayer self)
        {
            self.ItemCardClickAction = null;
            
        }
    }
}