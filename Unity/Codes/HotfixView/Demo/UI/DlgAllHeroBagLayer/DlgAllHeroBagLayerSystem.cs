using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics.Eventing.Reader;
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
            self.View.E_LoopScrollListHeroLoopVerticalScrollRect.SetVisible(true, self.HeroCardInfos.Count);
        }

        public static async void InitHeroCardView(this DlgAllHeroBagLayer self, Scroll_ItemHeroCard itemHeroCard, HeroCardInfo heroCardInfo)
        {
            var configId = heroCardInfo.ConfigId;
            var config = HeroConfigCategory.Instance.Get(configId);
            itemHeroCard.E_CountText.gameObject.SetActive(config.MaterialType == 2);
            itemHeroCard.E_CountText.text = heroCardInfo.Count.ToString();
            itemHeroCard.E_ChooseCountText.gameObject.SetActive(false);
            var spriteAtlas = ConstValue.HeroCardAtlasPath;
            var headImage = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(spriteAtlas, config.HeroIconImage);
            itemHeroCard.E_HeadImage.sprite = headImage;
            itemHeroCard.E_ElementImage.gameObject.SetActive(config.MaterialType == 1);
            itemHeroCard.E_LevelText.gameObject.SetActive(config.MaterialType == 1);

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

        // public static void SetCountInfo(this DlgAllHeroBagLayer self, Scroll_ItemHeroCard itemHeroCard, HeroCardInfo heroCardInfo)
        // {
        //     var configId = heroCardInfo.ConfigId;
        //     var config = HeroConfigCategory.Instance.Get(configId);
        //     // itemHeroCard.
        //     itemHeroCard.E_CountText.gameObject.SetActive(config.MaterialType == 2);
        //     itemHeroCard.E_CountText.text = heroCardInfo.Count.ToString();
        // }

        // public static async void SetHeroHeadImage(this DlgAllHeroBagLayer self, Scroll_ItemHeroCard itemHeroCard, HeroCardInfo heroCardInfo)
        // {
        //     var configId = heroCardInfo.ConfigId;
        //     var config = HeroConfigCategory.Instance.Get(configId);
        //     var spriteAtlas = ConstValue.HeroCardAtlasPath;
        //     var headImage = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(spriteAtlas, config.HeroIconImage);
        //     itemHeroCard.E_HeadImage.sprite = headImage;
        // }

        // public static async void SetHeroElementImage(this DlgAllHeroBagLayer self, Scroll_ItemHeroCard itemHeroCard, HeroCardInfo heroCardInfo)
        // {
        //     // var c
        //     var configId = heroCardInfo.ConfigId;
        //     var config = HeroConfigCategory.Instance.Get(configId);
        //     var elementConfig = ElementConfigCategory.Instance.Get(config.HeroColor);
        //     var elementImageStr = elementConfig.IconImage;
        //     var sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(ConstValue.HeroCardAtlasPath, elementImageStr);
        //     itemHeroCard.E_ElementImage.sprite = sprite;
        // }

        // public static void SetHeroStar(this DlgAllHeroBagLayer self, Scroll_ItemHeroCard heroCard, HeroCardInfo cardInfo)
        // {
        //     for (int i = 0; i < 5; i++)
        //     {
        //         // var star    
        //         var starStr = $"Star_{i}";
        //         Transform starObj = UIFindHelper.FindDeepChild(heroCard.uiTransform.gameObject, starStr);
        //         if (starObj != null)
        //         {
        //             starObj.gameObject.SetActive(i < cardInfo.Star);
        //         }
        //     }
        // }

        public static void OnLoopListItemRefreshHandler(this DlgAllHeroBagLayer self, Transform tr, int index)
        {
            // Debug.Log("显示卡牌信息");
            Scroll_ItemHeroCard itemHeroCard = self.ItemHeroCards[index].BindTrans(tr);
            itemHeroCard.E_ChooseToggle.isOn = false;
            HeroCardInfo heroCardInfo = self.HeroCardInfos[index];
            self.InitHeroCardView(itemHeroCard, heroCardInfo);
            // self.SetHeroHeadImage(itemHeroCard, heroCardInfo);
            // self.SetHeroElementImage(itemHeroCard, heroCardInfo);
            // self.SetHeroStar(itemHeroCard, heroCardInfo);
            // self.SetCountInfo(itemHeroCard, heroCardInfo);
            // var spriteAtlas = "Assets/Res/HeroCards/HeroCardSpriteAtlas.spriteatlas";

            if (self.UnAbleHeroCardInfo != null && self.UnAbleHeroCardInfo.HeroId.Equals(heroCardInfo.HeroId))
            {
                itemHeroCard.E_ChooseToggle.interactable = false;
            }
            else
            {
                itemHeroCard.E_ChooseToggle.interactable = true;
            }

            // itemHeroCard.E_ChooseToggle.group = self.View.E_ContentToggleGroup;
            itemHeroCard.E_ChooseToggle.onValueChanged.RemoveAllListeners();
            itemHeroCard.E_ChooseToggle.onValueChanged.AddListener((value) =>
            {
                // self.OnClickHeroItem(heroCardInfo);
                // if (self.OnHeroItemClick != null)
                // {
                //     self.OnHeroItemClick(heroCardInfo);
                // }

                if (self.OnHeroItemInfoClick != null)
                {
                    self.OnHeroItemInfoClick(heroCardInfo, itemHeroCard, value);
                }
            });
            // scrollItemHeroCard.SetHeroInfo(self.HeroCardInfos[index]);
            // scrollItemHeroCard.E_ClickImage
            // scrollItemHeroCard.E_ClickButton.onClick.RemoveAllListeners();
            // scrollItemHeroCard.E_ClickButton.onClick.AddListener(() => { self.OnClickHeroItem(scrollItemHeroCard); });
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
        }

        // public static async void OnClickHeroItem(this DlgAllHeroBagLayer self, HeroCardInfo heroCardInfo)
        // {
        //     self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_HeroInfoLayerUI);
        //     self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_MainSceneBg);
        //     // Log.Debug($"click hero {heroCard.HeroCardInfo.HeroId}");
        //     await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_ShowHeroInfoLayer);
        //     // self.DomainScene().GetComponent<UIComponent>().GetChild<UIBaseWindow>(WindowID.WindowID_ShowHeroInfoLayer);
        //
        //     // UIEventComponent.Instance.GetUIEventHandler(WindowID.WindowID_ShowHeroInfoLayer).OnInitWindowCoreData(baseWindow);
        //     UIBaseWindow baseWindow = self.DomainScene().GetComponent<UIComponent>().AllWindowsDic[(int) WindowID.WindowID_ShowHeroInfoLayer];
        //     baseWindow.GetComponent<DlgShowHeroInfoLayer>().SetHeroInfo(heroCardInfo);
        // }
        public static void ShowWindow(this DlgAllHeroBagLayer self, Entity contextData = null)
        {
            self.FilterColor(self.CurrentChooseTypeIndex).Coroutine();
        }

        public static async ETTask FilterColor(this DlgAllHeroBagLayer self, int index)
        {
            Log.Debug("filter color");
            long AccountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            M2C_GetAllHeroCardListResponse m2CGetAllTroopInfosResponse = (M2C_GetAllHeroCardListResponse) await self.ZoneScene()
                    .GetComponent<SessionComponent>().Session.Call(new C2M_GetAllHeroCardListRequest() { Account = AccountId });
            Log.Debug($"filter index{index}");
            if (m2CGetAllTroopInfosResponse.Error == ErrorCode.ERR_Success)
            {
                Dictionary<int, List<HeroCardInfo>> map = new Dictionary<int, List<HeroCardInfo>>();
                self.HeroCardInfos = m2CGetAllTroopInfosResponse.HeroCardInfos;
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
                    list = map[index + 1];
                    self.HeroCardInfos = list;
                }

                // map.Remove(index + 1);
                self.AddUIScrollItems(ref self.ItemHeroCards, self.HeroCardInfos.Count);
                self.View.E_LoopScrollListHeroLoopVerticalScrollRect.SetVisible(true, self.HeroCardInfos.Count);
            }
        }
    }
}