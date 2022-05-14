using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.CompilerServices;
using ET.Account;
using ILRuntime.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ET
{
    public static class DlgEditorTroopLayerSystem
    {
        public static void RegisterUIEvent(this DlgEditorTroopLayer self)
        {
            self.View.E_HeroBagLoopLoopVerticalScrollRect.AddItemRefreshListener(self.OnLoopItemListHeroCardEvent);
            self.View.E_LoopTroopHeroLoopHorizontalScrollRect.AddItemRefreshListener(self.OnLoopItemListTroopHeroCardEvent);
            self.View.E_BackButton.AddListenerAsync(self.BackButtonClick);
            self.View.E_StartGameButton.AddListenerAsync(self.StartGameButtonClick);
            self.InitFilterToggleClickEvent();
        }

        public static void InitFilterToggleClickEvent(this DlgEditorTroopLayer self)
        {
            var buttonContent = UIFindHelper.FindDeepChild(self.View.uiTransform.gameObject, "ColorContent");
            var childCount = buttonContent.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                var child = buttonContent.transform.GetChild(i);
                self.OnFilterToggleClick(child.gameObject, i);
            }
        }

        public static void OnFilterToggleClick(this DlgEditorTroopLayer self, GameObject obj, int index)
        {
            Toggle toggle = obj.GetComponent<Toggle>();
            toggle.onValueChanged.AddListener((value) =>
            {
                if (value)
                {
                    self.FilterColor(index);
                }
            });
        }
        public static async ETTask BackButtonClick(this DlgEditorTroopLayer self)
        {
            await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_MainScene);
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_EditorTroopLayer);
            await ETTask.CompletedTask;
        }

        public static void ShowWindow(this DlgEditorTroopLayer self, Entity contextData = null)
        {
            //todo 请求当前玩家拥有几支队伍
            // self.ShowBagHeroItems();
            self.ShowTroopHeroCard();
   
            self.FilterColor(self.CurrentChooseFilterIndex);
        }

        public static void HideWindow(this DlgEditorTroopLayer self)
        {
            Log.Debug("hide window");
            self.RemoveUIScrollItems(ref self.ItemTroops);
            self.RemoveUIScrollItems(ref self.ItemTroopHeroCards);
            self.RemoveUIScrollItems(ref self.ItemHeroCards);
            self.TroopHeroCardInfos.Clear();
            self.HeroCardInfos.Clear();
        }

        public static async void ShowBagHeroItems(this DlgEditorTroopLayer self)
        {
            M2C_GetAllHeroCardListResponse m2CGetAllHeroCardListResponse;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            long AccoundId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            m2CGetAllHeroCardListResponse =
                    (M2C_GetAllHeroCardListResponse) await session.Call(new C2M_GetAllHeroCardListRequest() { Account = AccoundId });
            if (m2CGetAllHeroCardListResponse.Error == ErrorCode.ERR_Success)
            {
                self.HeroCardInfos = m2CGetAllHeroCardListResponse.HeroCardInfos;
                // self.RemoveUIScrollItems(ref self.ItemHeroCards);
                self.AddUIScrollItems(ref self.ItemHeroCards, self.HeroCardInfos.Count);
                self.View.E_HeroBagLoopLoopVerticalScrollRect.SetVisible(true, m2CGetAllHeroCardListResponse.HeroCardInfos.Count);
            }
        }
        public static async void ShowTroopHeroCard(this DlgEditorTroopLayer self)
        {
            var session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            var account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;

            //获取当前的队伍id
            M2C_GetAllTroopInfosResponse response =
                    (M2C_GetAllTroopInfosResponse) await session.Call(new C2M_GetAllTroopInfosRequest() { Account = account });
            if (response.Error == ErrorCode.ERR_Success)
            {
                if (response.TroopInfos.Count > 0)
                {
                    var troopId = response.TroopInfos[0].TroopId;
                    self.CurrentChooseTroopId = troopId;
                    //根据队伍id 获取此队伍里面的英雄
                    M2C_GetHeroInfosWithTroopIdResponse heroinforesponse =
                            (M2C_GetHeroInfosWithTroopIdResponse) await session.Call(new C2M_GetHeroInfosWithTroopIdRequest() { TroopId = troopId });
                    self.TroopHeroCardInfos = heroinforesponse.HeroCardInfos;
                }
            }
            self.AddUIScrollItems(ref self.ItemTroopHeroCards, 3);
            self.View.E_LoopTroopHeroLoopHorizontalScrollRect.SetVisible(true, 3);
            Log.Debug($"self troop hero card info count {self.TroopHeroCardInfos.Count}");
            self.View.E_StartGameButton.SetVisible(self.TroopHeroCardInfos.Count == 3);
            
            
            
        }
        public static void OnLoopItemListHeroCardEvent(this DlgEditorTroopLayer self, Transform transform, int index)
        {
            Log.Debug($"OnLoopItemListHeroCardEvent{index}");
            Scroll_ItemHeroCard itemHeroCard = self.ItemHeroCards[index].BindTrans(transform);
            itemHeroCard.E_ChooseToggle.onValueChanged.RemoveAllListeners();
            itemHeroCard.E_ChooseToggle.isOn = false;
            self.InitHeroCardItem(itemHeroCard, self.HeroCardInfos[index]);
            HeroCardInfo heroCardInfo = self.HeroCardInfos[index];
            if (heroCardInfo.TroopId != 0)
            {
                itemHeroCard.E_ChooseToggle.isOn = true;
            }

            itemHeroCard.E_ChooseToggle.onValueChanged.AddListener((value) => { self.OnHeroCardClick(index); });
        }

        public static async void SetHeroHeadImage(this DlgEditorTroopLayer self, Scroll_ItemHeroCard itemHeroCard, HeroCardInfo heroCardInfo)
        {
            var configId = heroCardInfo.ConfigId;
            var config = HeroConfigCategory.Instance.Get(configId);
            var spriteAtlas = ConstValue.HeroCardAtlasPath;
            var headImage = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(spriteAtlas, config.HeroIconImage);
            itemHeroCard.E_HeadImage.sprite = headImage;
        }

        public static async void SetHeroElementImage(this DlgEditorTroopLayer self, Scroll_ItemHeroCard itemHeroCard, HeroCardInfo heroCardInfo)
        {
            // var c
            var configId = heroCardInfo.ConfigId;
            var config = HeroConfigCategory.Instance.Get(configId);
            var elementConfig = ElementConfigCategory.Instance.Get(config.HeroColor);
            var elementImageStr = elementConfig.IconImage;
            var sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(ConstValue.HeroCardAtlasPath, elementImageStr);
            itemHeroCard.E_ElementImage.sprite = sprite;
        }

        public static void SetHeroStar(this DlgEditorTroopLayer self, Scroll_ItemHeroCard heroCard, HeroCardInfo cardInfo)
        {
            for (int i = 0; i < 5; i++)
            {
                // var star    
                var starStr = $"Star_{i}";
                Transform starObj = UIFindHelper.FindDeepChild(heroCard.uiTransform.gameObject, starStr);
                if (starObj != null)
                {
                    starObj.gameObject.SetActive(i < cardInfo.Star);
                }
            }
        }

        public static void InitHeroCardItem(this DlgEditorTroopLayer self, Scroll_ItemHeroCard itemHeroCard, HeroCardInfo heroCardInfo)
        {
            self.SetHeroHeadImage(itemHeroCard, heroCardInfo);
            self.SetHeroElementImage(itemHeroCard, heroCardInfo);
            self.SetHeroStar(itemHeroCard, heroCardInfo);
        }

        public static async void OnHeroCardClick(this DlgEditorTroopLayer self, int index)
        {
            Log.Debug("hero card click" + index);
            //todo 请求将此英雄配置到队伍里面
            try
            {
                Log.Debug($"current choose troop  ={self.CurrentChooseTroopId}");
                Log.Debug($"current choose index = {self.CurrentChooseInTroopIndex}");
                HeroCardInfo heroCardInfo = self.HeroCardInfos[index];
                long HeroId = heroCardInfo.HeroId;
                Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
                M2C_SetHeroToTroopResponse m2CSetHeroToTroopResponse = (M2C_SetHeroToTroopResponse) await session.Call(new C2M_SetHeroToTroopRequest()
                {
                    HeroId = HeroId, TroopId = self.CurrentChooseTroopId, InTroopIndex = self.CurrentChooseInTroopIndex
                });

                if (m2CSetHeroToTroopResponse.Error == ErrorCode.ERR_Success)
                {
                    self.FilterColor(self.CurrentChooseFilterIndex);
                    // self.ShowBagHeroItems();
                    self.ShowTroopHeroCard();
                    // self.ChooseTroop(self.CurrentChooseTroopId);
                }
                else
                {
                    Log.Error($"{m2CSetHeroToTroopResponse.Error}");
                }
            }
            catch (Exception e)
            {
                Log.Error($"{e}");
            }

            // await ETTask.CompletedTask;
        }

        public static void OnLoopItemListTroopHeroCardEvent(this DlgEditorTroopLayer self, Transform transform, int index)
        {
            Log.Debug($"on loop item list {index}");

            HeroCardInfo heroCardInfo = null;
            foreach (var info in self.TroopHeroCardInfos)
            {
                Log.Debug($"in troop index{info.InTroopIndex}");
                if (info.InTroopIndex == index)
                {
                    heroCardInfo = info;
                }
            }

            Scroll_ItemHeroCard itemTroopHeroCard = self.ItemTroopHeroCards[index].BindTrans(transform);
            itemTroopHeroCard.E_ChooseToggle.onValueChanged.RemoveAllListeners();

            if (heroCardInfo != null)
            {
                itemTroopHeroCard.E_AddTextText.gameObject.SetActive(false);
                self.InitHeroCardItem(itemTroopHeroCard, heroCardInfo);
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    var starStr = $"Star_{i}";
                    var obj = UIFindHelper.FindDeepChild(itemTroopHeroCard.uiTransform.gameObject, starStr);
                    obj.GetComponent<Image>().sprite = null;
                }

                itemTroopHeroCard.E_AddTextText.gameObject.SetActive(true);
                itemTroopHeroCard.E_ElementImage.sprite = null;
                itemTroopHeroCard.E_HeadImage.sprite = null;
            }

            itemTroopHeroCard.E_ChooseToggle.group = self.View.E_Content_TroopHeroToggleGroup;
            itemTroopHeroCard.E_ChooseToggle.onValueChanged.AddListener((value) => { self.OnTroopHeroClick(value, index); });
            if (index == self.CurrentChooseInTroopIndex)
            {
                itemTroopHeroCard.E_ChooseToggle.isOn = true;
            }
        }

        public static void OnTroopHeroClick(this DlgEditorTroopLayer self, bool value, int index)
        {
            if (value)
            {
                Log.Debug($"on troop hero click{index}");
                self.CurrentChooseInTroopIndex = index;
            }
        }

        public static async ETTask StartGameButtonClick(this DlgEditorTroopLayer self)
        {
            bool isPowerEnough = await self.CheckPowerIsEnough();
            if (!isPowerEnough)
            {
                await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_PowerNotEnoughAlert);
                return;
            }

            var Account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            M2C_StartPVEGameResponse m2CStartPveGameResponse =
                    (M2C_StartPVEGameResponse) await session.Call(new C2M_StartPVEGameRequest()
                    {
                        AccoundId = Account, TroopId = self.CurrentChooseTroopId
                    });
            if (m2CStartPveGameResponse.Error == ErrorCode.ERR_Success)
            {
                self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_EditorTroopLayer);
            }

            await ETTask.CompletedTask;
        }

        /// <summary>
        /// 检查体力是否足够
        /// </summary>
        /// <param name="self"></param>
        public static async ETTask<bool> CheckPowerIsEnough(this DlgEditorTroopLayer self)
        {
            long AccountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            M2C_GetGoldInfoResponse response = (M2C_GetGoldInfoResponse) await session.Call(new C2M_GetGoldInfoRequest() { AccountId = AccountId });
            if (response.Error == ErrorCode.ERR_Success)
            {
                // if (response.PowerCount > 0)
                // {
                //     return true;
                // }
                var powerCount = 0;
                foreach (var itemCountInfo in response.ItemInfos)
                {
                    if (itemCountInfo.ConfigId == 1003)
                    {
                        powerCount = itemCountInfo.Count;
                    }
                }

                if (powerCount > 0)
                {
                    return true;
                }
            }

            // return false;
            return false;
            // await ETTask.CompletedTask;
        }
        
        public static async void FilterColor(this DlgEditorTroopLayer self, int index)
        {
            Log.Debug("filter color");
            self.CurrentChooseFilterIndex = index;
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
                self.View.E_HeroBagLoopLoopVerticalScrollRect.SetVisible(true, self.HeroCardInfos.Count);
            }
        }
    }
}