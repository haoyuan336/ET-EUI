using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using ET.Account;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgAllHeroBagLayerSystem
    {
        public static void RegisterUIEvent(this DlgAllHeroBagLayer self)
        {
            self.InitAllToggleEvent();
            self.View.E_LoopScrollListHeroLoopVerticalScrollRect.AddItemRefreshListener(self.OnLoopListItemRefreshHandler);
            // self.AddUIScrollItems();
            // self.ItemHeroCards
        }

        public static void InitAllToggleEvent(this DlgAllHeroBagLayer self)
        {
            List<GameObject> list = new List<GameObject>();
            list.Add(self.View.E_RedImage.gameObject);
            list.Add(self.View.E_YellowImage.gameObject);
            list.Add(self.View.E_GreenImage.gameObject);
            list.Add(self.View.E_BlueImage.gameObject);
            list.Add(self.View.E_PurpleImage.gameObject);
            list.Add(self.View.E_AllImage.gameObject);
            var index = 0;
            foreach (var go in list)
            {
                self.InitColorToggleEvent(go, index);
                index++;
            }
        }

        public static void UnAbleHeroItemWhitHeroInfo(this DlgAllHeroBagLayer self, HeroCardInfo heroCardInfo)
        {
            self.UnAbleHeroCardInfo = heroCardInfo;
            self.View.E_LoopScrollListHeroLoopVerticalScrollRect.RefreshCells();
        }

        public static void EnableItemWhitHeroInfos(this DlgAllHeroBagLayer self, List<HeroCardInfo> heroCardInfos)
        {
            self.EnabelHeroCardInfos = heroCardInfos;
            self.View.E_LoopScrollListHeroLoopVerticalScrollRect.RefreshCells();
        }

        public static void ReferView(this DlgAllHeroBagLayer self)
        {
            self.AllChooseHeroCardInfos = null;
            self.EnabelHeroCardInfos = null;
            // self.View.E_LoopScrollListHeroLoopVerticalScrollRect.RefreshCells();
            self.FilterColor(self.CurrentChooseTypeIndex).Coroutine();
        }

        public static async void InitHeroCardView(this DlgAllHeroBagLayer self, Scroll_ItemHeroCard itemHeroCard, HeroCardInfo heroCardInfo)
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

        public static void OnLoopListItemRefreshHandler(this DlgAllHeroBagLayer self, Transform tr, int index)
        {
            Scroll_ItemHeroCard itemHeroCard = self.ItemHeroCards[index].BindTrans(tr);
            HeroCardInfo heroCardInfo = self.HeroCardInfos[index];
            self.InitHeroCardView(itemHeroCard, heroCardInfo);
            itemHeroCard.E_ChooseToggle.interactable = true;
            if (self.UnAbleHeroCardInfo != null && self.UnAbleHeroCardInfo.HeroId.Equals(heroCardInfo.HeroId))
            {
                itemHeroCard.E_ChooseToggle.interactable = false;
            }

            itemHeroCard.E_ChooseToggle.onValueChanged.RemoveAllListeners();
            itemHeroCard.E_ChooseToggle.isOn = false;
            if (self.AllChooseHeroCardInfos != null)
            {
                var findInfo = self.AllChooseHeroCardInfos.Find(a => a.HeroId.Equals(heroCardInfo.HeroId));
                if (findInfo != null)
                {
                    var config = HeroConfigCategory.Instance.Get(heroCardInfo.ConfigId);
                    if (config.MaterialType == (int) HeroBagType.Hero)
                    {
                        itemHeroCard.E_ChooseToggle.isOn = true;
                    }
                    else if (config.MaterialType == (int) HeroBagType.Materail)
                    {
                        itemHeroCard.E_ChooseCountText.gameObject.SetActive(true);
                        itemHeroCard.E_ChooseCountText.text = findInfo.Count.ToString();
                    }
                    // itemHeroCard.E_ChooseToggle.isOn = true;
                }
            }

            if (self.EnabelHeroCardInfos != null)
            {
                //找一下是否包含
                itemHeroCard.E_ChooseToggle.interactable = false;
                bool isCon = self.EnabelHeroCardInfos.Exists(a => a.HeroId.Equals(heroCardInfo.HeroId));
                itemHeroCard.E_ChooseToggle.interactable = isCon;
            }

            itemHeroCard.E_ChooseToggle.onValueChanged.AddListener((value) =>
            {
                if (self.OnHeroItemInfoClick != null)
                {
                    self.OnHeroItemInfoClick(heroCardInfo, itemHeroCard, value);
                }
            });
        }

        public static void InitColorToggleEvent(this DlgAllHeroBagLayer self, GameObject go, int index)
        {
            go.GetComponent<Toggle>().onValueChanged.AddListener((value) =>
            {
                if (value)
                {
                    self.CurrentChooseTypeIndex = index;
                    // Log.Debug(go.name);
                    self.FilterColor(index).Coroutine();
                    go.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 160);
                }
                else
                {
                    go.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 140);
                }
            });
        }

        public static void HideWindow(this DlgAllHeroBagLayer self)
        {
            self.OnHeroItemInfoClick = null;
            self.UnAbleHeroCardInfo = null;
            self.EnabelHeroCardInfos = null;
            self.AllChooseHeroCardInfos = null;
        }

        public static void ShowWindow(this DlgAllHeroBagLayer self, Entity contextData = null)
        {
            // self.FilterColor(self.CurrentChooseTypeIndex).Coroutine();
        }

        public static void SetShowHeroType(this DlgAllHeroBagLayer self, HeroBagType type)
        {
            self.BagType = type;
            self.FilterColor(self.CurrentChooseTypeIndex).Coroutine();
        }

        public static async ETTask SetShowHeroTypeAsync(this DlgAllHeroBagLayer self, HeroBagType type)
        {
            self.BagType = type;
            await self.FilterColor(self.CurrentChooseTypeIndex);
            
        }

        public static void SetAllChooseHeroCardInfos(this DlgAllHeroBagLayer self, List<HeroCardInfo> heroCardInfos)
        {
            self.AllChooseHeroCardInfos = heroCardInfos;
            self.View.E_LoopScrollListHeroLoopVerticalScrollRect.RefreshCells();
        }

        public static async ETTask FilterColor(this DlgAllHeroBagLayer self, int index)
        {
            Log.Debug("filter color");
            long AccountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            M2C_GetAllHeroCardListResponse m2CGetAllHeroCardListResponse = (M2C_GetAllHeroCardListResponse) await self.ZoneScene()
                    .GetComponent<SessionComponent>().Session
                    .Call(new C2M_GetAllHeroCardListRequest() { Account = AccountId, BagType = (int) self.BagType });
            Log.Debug($"filter index{index}");
            if (m2CGetAllHeroCardListResponse.Error == ErrorCode.ERR_Success)
            {
                Dictionary<int, List<HeroCardInfo>> map = new Dictionary<int, List<HeroCardInfo>>();
                self.HeroCardInfos = m2CGetAllHeroCardListResponse.HeroCardInfos;
                //根据当前类型，过滤一下列表
                if (self.BagType != HeroBagType.HeroAndMaterial)
                {
                    Log.Debug($"self bag type {(int) self.BagType}");
                    //过滤调与背包类型不符的 元素
                    self.HeroCardInfos.RemoveAll(a =>
                    {
                        var config = HeroConfigCategory.Instance.Get(a.ConfigId);
                        if (config.MaterialType == (int) self.BagType)
                        {
                            return false;
                        }

                        return true;
                    });
                    Log.Debug($"hero card info {self.HeroCardInfos.Count}");
                }

                if (index != 5)
                {
                    foreach (var heroCardInfo in self.HeroCardInfos)
                    {
                        if (!map.ContainsKey(heroCardInfo.HeroColor))
                        {
                            // map[heroCardInfo.HeroColor] = new List<HeroCardInfo>();
                            map.Add(heroCardInfo.HeroColor, new List<HeroCardInfo>());
                        }

                        map[heroCardInfo.HeroColor].Add(heroCardInfo);
                    }

                    List<HeroCardInfo> list = new List<HeroCardInfo>();
                    if (map.Keys.Contains(index + 1))
                    {
                        list = map[index + 1];
                        self.HeroCardInfos = list;
                    }
                    else
                    {
                        self.HeroCardInfos = new List<HeroCardInfo>();
                    }
                }

                // map.Remove(index + 1);
                Log.Debug($"add ui item {self.HeroCardInfos.Count}");
                self.AddUIScrollItems(ref self.ItemHeroCards, self.HeroCardInfos.Count);
                self.View.E_LoopScrollListHeroLoopVerticalScrollRect.SetVisible(true, self.HeroCardInfos.Count);
            }
        }
    }
}