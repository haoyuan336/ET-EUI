using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using ET.Account;
using UnityEngine;
using UnityEngine.AddressableAssets;
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
            self.View.E_BackButton.AddListener(() =>
            {
                self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_AllHeroBagLayer);
            });
        }

        public static void InitAllToggleEvent(this DlgAllHeroBagLayer self)
        {
            // HeroElementType[] indexs =
            // {
            //     HeroElementType.Fire, HeroElementType.Dark, HeroElementType.Water, HeroElementType.Wind, HeroElementType.Light
            // };
            List<GameObject> list = new List<GameObject>();
            list.Add(self.View.E_FireToggle.gameObject);
            list.Add(self.View.E_DarkToggle.gameObject);
            list.Add(self.View.E_WaterToggle.gameObject);
            list.Add(self.View.E_WindToggle.gameObject);
            list.Add(self.View.E_LightToggle.gameObject);
            list.Add(self.View.E_AllToggle.gameObject);
            var index = 0;
            foreach (var go in list)
            {
                self.InitColorToggleEvent(go, index);
                index++;
            }
        }

        /// <summary>
        /// 设置可以选择的相同星数的英雄卡牌
        /// </summary>
        /// <param name="self"></param>
        /// <param name="heroCardInfo"></param>
        public static void SetEnableSameStarCountHeroInfo(this DlgAllHeroBagLayer self, HeroCardInfo heroCardInfo)
        {
            self.EnabelSameStarCountHeroCardInfo = heroCardInfo;
            self.View.E_LoopScrollListHeroLoopVerticalScrollRect.RefillCells();
        }

        public static void UnAbleHeroItemWhitHeroInfo(this DlgAllHeroBagLayer self, HeroCardInfo heroCardInfo)
        {
            self.UnAbleHeroCardInfo = heroCardInfo;
            self.View.E_LoopScrollListHeroLoopVerticalScrollRect.RefreshCells();
        }

        public static void SetUnableElementHeroCardInfo(this DlgAllHeroBagLayer self, List<HeroCardInfo> heroCardInfos)
        {
            self.UnableElementHeroCardInfos = heroCardInfos;
            self.View.E_LoopScrollListHeroLoopVerticalScrollRect.RefreshCells();
        }

        public static void SetUnabelNameHeroCardInfo(this DlgAllHeroBagLayer self, List<HeroCardInfo> heroCardInfos)
        {
            self.UnableNameHeroCardInfos = heroCardInfos;
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

        public static void OnLoopListItemRefreshHandler(this DlgAllHeroBagLayer self, Transform tr, int index)
        {
            Scroll_ItemHeroCard itemHeroCard = self.ItemHeroCards[index].BindTrans(tr);
            itemHeroCard.E_ChooseToggle.interactable = true;
            if (index >= self.HeroCardInfos.Count)
            {
                itemHeroCard.E_LevelText.gameObject.SetActive(false);
                itemHeroCard.E_ElementImage.gameObject.SetActive(false);
                itemHeroCard.E_QualityIconImage.gameObject.SetActive(false);
                itemHeroCard.E_CountText.gameObject.SetActive(false);
                itemHeroCard.E_ChooseToggle.gameObject.SetActive(false);
                // var commonAtlas = ConstValue.CommonUIAtlasPath;
                // var defaultSprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(commonAtlas, "bgpic");
                // itemHeroCard.E_HeadImage.sprite = defaultSprite;
                for (int i = 0; i < 5; i++)
                {
                    // var star    
                    var starStr = $"Star_{i}";
                    Transform starObj = UIFindHelper.FindDeepChild(itemHeroCard.uiTransform.gameObject, starStr);
                    if (starObj != null)
                    {
                        starObj.gameObject.SetActive(false);
                    }
                }

                itemHeroCard.E_HeadImage.sprite = self.DefaultHeadSprite;
            }
            else
            {
                itemHeroCard.E_ChooseToggle.gameObject.SetActive(true);
                HeroCardInfo heroCardInfo = self.HeroCardInfos[index];
                var heroConfig = HeroConfigCategory.Instance.Get(heroCardInfo.ConfigId);
                itemHeroCard.InitHeroCard(heroCardInfo);

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
                        if (heroConfig.MaterialType == (int) HeroBagType.Hero)
                        {
                            itemHeroCard.E_ChooseToggle.isOn = true;
                        }
                        else if (heroConfig.MaterialType == (int) HeroBagType.Materail)
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

                if (self.UnableElementHeroCardInfos != null)
                {
                    if (self.UnableElementHeroCardInfos.Exists(a =>
                        {
                            HeroConfig config = HeroConfigCategory.Instance.Get(a.ConfigId);
                            if (config.HeroColor.Equals(heroConfig.HeroColor) && !heroCardInfo.HeroId.Equals(a.HeroId))
                            {
                                return true;
                            }

                            return false;
                        }))
                    {
                        itemHeroCard.E_ChooseToggle.interactable = false;
                    }
                }

                if (self.EnabelSameStarCountHeroCardInfo != null)
                {
                    HeroConfig config = HeroConfigCategory.Instance.Get(heroCardInfo.ConfigId);
                    HeroConfig sameConfig = HeroConfigCategory.Instance.Get(self.EnabelSameStarCountHeroCardInfo.ConfigId);
                    if (heroCardInfo.Star != self.EnabelSameStarCountHeroCardInfo.Star ||
                        config.HeroName != sameConfig.HeroName)
                    {
                        itemHeroCard.E_ChooseToggle.interactable = false;
                    }
                    // Log.Debug($"self name {self.EnabelSameStarCountHeroCardInfo.HeroName}");
                    // // Log.Debug($"hero name {heroCardInfo.HeroName}");
                    // if (heroCardInfo.Star != self.EnabelSameStarCountHeroCardInfo.Star)
                    // {
                    //     itemHeroCard.E_ChooseToggle.interactable = false;
                    // }
                }

                if (self.UnableNameHeroCardInfos != null)
                {
                    if (self.UnableNameHeroCardInfos.Exists(a =>
                        {
                            HeroConfig config = HeroConfigCategory.Instance.Get(a.ConfigId);
                            if (config.HeroName.Equals(heroConfig.HeroName) && !heroCardInfo.HeroId.Equals(a.HeroId))
                            {
                                return true;
                            }

                            return false;
                        }))
                    {
                        itemHeroCard.E_ChooseToggle.interactable = false;
                    }
                }

            

                itemHeroCard.E_ChooseToggle.onValueChanged.AddListener((value) =>
                {
                    if (self.OnHeroItemInfoClick != null)
                    {
                        self.OnHeroItemInfoClick(heroCardInfo, itemHeroCard, value);
                    }
                });
            }
        }

        public static void InitColorToggleEvent(this DlgAllHeroBagLayer self, GameObject go, int index)
        {
            go.GetComponent<Toggle>().onValueChanged.AddListener((value) =>
            {
                if (value)
                {
                    self.CurrentChooseTypeIndex = index;

                    Log.Debug($"self current choose type index {self.CurrentChooseTypeIndex}");
                    // Log.Debug(go.name);
                    self.FilterColor(index).Coroutine();
                    // go.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 160);
                }
                else
                {
                    // go.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 140);
                }
            });
        }

        public static void HideWindow(this DlgAllHeroBagLayer self)
        {
            self.OnHeroItemInfoClick = null;
            self.UnAbleHeroCardInfo = null;
            self.EnabelHeroCardInfos = null;
            self.AllChooseHeroCardInfos = null;
            // self.UnableElementType = HeroElementType.Invalide;
            self.UnableElementHeroCardInfos = null;
            self.EnabelSameStarCountHeroCardInfo = null;

            self.UnableNameHeroCardInfos = null;

            // public HeroCardInfo UnAbleHeroCardInfo = null; //单独禁用的信息
            // public List<HeroCardInfo> UnableElementHeroCardInfos = null; //需要禁用的相同元素的英雄列表
            // public List<HeroCardInfo> UnableNameHeroCardInfos = null; //需要禁用的相同名称的英雄列表
            // public List<HeroCardInfo> EnabelHeroCardInfos = null; //不禁用的英雄信息列表
            // public List<HeroCardInfo> AllChooseHeroCardInfos = null; //当前已经选择的英雄列表
            // public HeroCardInfo EnabelSameStarCountHeroCardInfo = null; //可用的相同星数目的卡牌信息
        }

        public static  void ShowWindow(this DlgAllHeroBagLayer self, Entity contextData = null)
        {
            // self.FilterColor(self.CurrentChooseTypeIndex).Coroutine();
            // var spritePath = ConstValue.HeroCardAtlasPath;
            // var bgImage = ConstValue.CommonUIAtlasPath;
            // self.DefaultHeadSprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(spritePath, bgImage);
            //请求背包数据
        }

        public static async ETTask InitBagCount(this DlgAllHeroBagLayer self)
        {
            var commonAtlas = ConstValue.CommonUIAtlasPath;
            // var spritePath = ConstValue.HeroCardAtlasPath;
            var bgImage = ConstValue.FrameBgPath;
            self.DefaultHeadSprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(commonAtlas, bgImage);
            //初始化背包个数
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            long account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            C2M_GetAllItemRequest request = new C2M_GetAllItemRequest() { AccountId = account };
            M2C_GetAllItemResponse response = (M2C_GetAllItemResponse) await session.Call(request);
            // Log.Debug("获取道具数据成功");
            if (response.Error == ErrorCode.ERR_Success)
            {
                List<ItemInfo> itemInfos = response.ItemInfos;
                //todo 1006 为英雄背包格子扩展包
                itemInfos = itemInfos.FindAll(a => a.ConfigId.Equals(1004) || a.ConfigId.Equals(1006));
                // Log.Debug($"item info {itemInfos.Count}");
                if (itemInfos.Count > 0)
                {
                    var bagCount = 0;
                    foreach (var itemInfo in itemInfos)
                    {
                        // Log.Debug($"item infos {itemInfo.Count} + {itemInfo.ConfigId}");
                        ItemConfig config = ItemConfigCategory.Instance.Get(itemInfo.ConfigId);
                        bagCount += config.DefaultValue;
                    }

                    self.AddUIScrollItems(ref self.ItemHeroCards, bagCount);
                    self.View.E_LoopScrollListHeroLoopVerticalScrollRect.SetVisible(true, bagCount);
                    // self.View.E_BagCountText.text = bagCount.ToString();
                    self.BagCount = bagCount;
                    self.View.E_BagCountText.text = $"{self.HeroCount}/{self.BagCount}";
                }
            }

            await ETTask.CompletedTask;
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
        // public static void SetUnable

        public static async ETTask FilterColor(this DlgAllHeroBagLayer self, int index)
        {
            await self.InitBagCount();
            // Log.Debug("filter color");
            long AccountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            M2C_GetAllHeroCardListResponse m2CGetAllHeroCardListResponse = (M2C_GetAllHeroCardListResponse) await self.ZoneScene()
                    .GetComponent<SessionComponent>().Session
                    .Call(new C2M_GetAllHeroCardListRequest() { Account = AccountId, BagType = (int) self.BagType });
            Log.Debug($"filter index{index}");
            if (m2CGetAllHeroCardListResponse.Error == ErrorCode.ERR_Success)
            {
                Dictionary<int, List<HeroCardInfo>> map = new Dictionary<int, List<HeroCardInfo>>();
                self.HeroCardInfos = m2CGetAllHeroCardListResponse.HeroCardInfos;
                self.HeroCount = self.HeroCardInfos.Count;
                self.View.E_BagCountText.text = $"{self.HeroCount}/{self.BagCount}";

                //根据当前类型，过滤一下列表
                if (self.BagType != HeroBagType.HeroAndMaterial)
                {
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
                }

                if (index != 5)
                {
                    HeroElementType[] indexs =
                    {
                        HeroElementType.Fire, HeroElementType.Dark, HeroElementType.Water, HeroElementType.Wind, HeroElementType.Light
                    };
                    HeroElementType type = indexs[index];
                    self.HeroCardInfos = self.HeroCardInfos.FindAll(a =>
                    {
                        HeroConfig config = HeroConfigCategory.Instance.Get(a.ConfigId);
                        if (config.HeroColor == (int) type)
                        {
                            return true;
                        }

                        return false;
                    });
                }

                self.View.E_LoopScrollListHeroLoopVerticalScrollRect.RefreshCells();
            }
        }
    }
}