using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using ET.Account;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace ET
{
    public static class DlgHeroInfoLayerUISystem
    {
        public static void RegisterUIEvent(this DlgHeroInfoLayerUI self)
        {
            self.InitAllToggleEvent();
            self.View.E_LoopScrollListHeroLoopVerticalScrollRect.AddItemRefreshListener((Transform tr, int index) =>
            {
                self.OnLoopListItemRefreshHandler(tr, index);
            });
        }

        public static async void OnLoopListItemRefreshHandler(this DlgHeroInfoLayerUI self, Transform tr, int index)
        {
            Debug.Log("显示卡牌信息");
            Scroll_ItemHeroCard itemHeroCard = self.ItemHeroCards[index].BindTrans(tr);
            itemHeroCard.E_ChooseToggle.isOn = false;
            HeroCardInfo heroCardInfo = self.HeroCardInfos[index];
            var configId = heroCardInfo.ConfigId;
            var config = HeroConfigCategory.Instance.Get(configId);
            var spriteAtlas = "Assets/Res/HeroCards/HeroCardSpriteAtlas.spriteatlas";
            var headImage = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(spriteAtlas, config.HeroIconImage);
            itemHeroCard.E_HeadImage.sprite = headImage;
            itemHeroCard.E_ChooseToggle.group = self.View.E_ContentToggleGroup;
            itemHeroCard.E_ChooseToggle.onValueChanged.RemoveAllListeners();
            itemHeroCard.E_ChooseToggle.onValueChanged.AddListener((value) =>
            {
                if (value)
                {
                    self.OnClickHeroItem(heroCardInfo);
                }
            });
            // scrollItemHeroCard.SetHeroInfo(self.HeroCardInfos[index]);
            // scrollItemHeroCard.E_ClickImage
            // scrollItemHeroCard.E_ClickButton.onClick.RemoveAllListeners();
            // scrollItemHeroCard.E_ClickButton.onClick.AddListener(() => { self.OnClickHeroItem(scrollItemHeroCard); });
        }

        public static void InitHeroCardInfo(this DlgHeroInfoLayerUI self)
        {
        }

        public static void InitColorToggleEvent(this DlgHeroInfoLayerUI self, GameObject go, int index)
        {
            go.GetComponent<Toggle>().onValueChanged.AddListener((value) =>
            {
                if (value)
                {
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

        public static async ETTask FilterColor(this DlgHeroInfoLayerUI self, int index)
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
                map.Remove(index + 1);
                self.HeroCardInfos = list;
                self.AddUIScrollItems(ref self.ItemHeroCards, self.HeroCardInfos.Count);
                self.View.E_LoopScrollListHeroLoopVerticalScrollRect.SetVisible(true, self.HeroCardInfos.Count);
            }
        }

        public static void InitAllToggleEvent(this DlgHeroInfoLayerUI self)
        {
            List<GameObject> list = new List<GameObject>();
            list.Add(self.View.E_RedImage.gameObject);
            list.Add(self.View.E_YellowImage.gameObject);
            list.Add(self.View.E_GreenImage.gameObject);
            list.Add(self.View.E_BlueImage.gameObject);
            list.Add(self.View.E_PurpleImage.gameObject);
            var index = 0;
            foreach (var go in list)
            {
                self.InitColorToggleEvent(go, index);
                index++;
            }
        }

        public static async ETTask RequestHerosInfo(this DlgHeroInfoLayerUI self)
        {
            long Account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Log.Debug($"account = {Account}");
            M2C_GetAllHeroCardListResponse m2CGetAllHeroCardListResponse;
            try
            {
                m2CGetAllHeroCardListResponse = (M2C_GetAllHeroCardListResponse) await self.ZoneScene().GetComponent<SessionComponent>().Session
                        .Call(new C2M_GetAllHeroCardListRequest() { Account = Account });
                if (m2CGetAllHeroCardListResponse.Error == ErrorCode.ERR_Success)
                {
                    List<HeroCardInfo> heroCardInfos = m2CGetAllHeroCardListResponse.HeroCardInfos;
                    self.HeroCardInfos.Clear();
                    foreach (var heroCardInfo in heroCardInfos)
                    {
                        self.HeroCardInfos.Add(heroCardInfo);
                    }

                    self.AddUIScrollItems(ref self.ItemHeroCards, self.HeroCardInfos.Count);
                    self.View.E_LoopScrollListHeroLoopVerticalScrollRect.SetVisible(true, self.HeroCardInfos.Count);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static async void OnClickHeroItem(this DlgHeroInfoLayerUI self, HeroCardInfo heroCardInfo)
        {
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_HeroInfoLayerUI);
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_MainSceneBg);
            // Log.Debug($"click hero {heroCard.HeroCardInfo.HeroId}");
            await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_ShowHeroInfoLayer);
            // self.DomainScene().GetComponent<UIComponent>().GetChild<UIBaseWindow>(WindowID.WindowID_ShowHeroInfoLayer);

            // UIEventComponent.Instance.GetUIEventHandler(WindowID.WindowID_ShowHeroInfoLayer).OnInitWindowCoreData(baseWindow);
            UIBaseWindow baseWindow = self.DomainScene().GetComponent<UIComponent>().AllWindowsDic[(int) WindowID.WindowID_ShowHeroInfoLayer];
            baseWindow.GetComponent<DlgShowHeroInfoLayer>().SetHeroInfo(heroCardInfo);
        }

        public static async void ShowWindow(this DlgHeroInfoLayerUI self, Entity contextData = null)
        {
            self.RequestHerosInfo().Coroutine();
            await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_MainSceneMenu);
            await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_GoldInfoUI);
        }
    }
}