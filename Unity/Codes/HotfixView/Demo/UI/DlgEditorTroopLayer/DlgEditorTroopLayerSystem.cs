using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;
using Toggle = UnityEngine.UI.Toggle;

namespace ET
{
    public static class DlgEditorTroopLayerSystem
    {
        public static void RegisterUIEvent(this DlgEditorTroopLayer self)
        {
            // self.View.E_EditorTroopButton.AddListenerAsync(self.ShowEditorTroopLayer);
            // self.View.ESTroopHeroCards.RegisterUIEvent();

            var childCount = self.View.E_TroopGroupContentImage.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                var go = self.View.E_TroopGroupContentImage.transform.GetChild(i);
                self.OnTroopToggleOnClick(go, i + 1);
            }
        }

        public static void OnTroopToggleOnClick(this DlgEditorTroopLayer self, Transform transform, int index)
        {
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            transform.GetComponent<Toggle>().onValueChanged.AddListener(async (value) =>
            {
                if (value)
                {
                    var response =
                            await session.Call(new C2M_PlayerChooseTroopIndexRequest() { Index = index }) as M2C_PlayerChooseTroopIndexResponse;
                    if (response.Error == ErrorCode.ERR_Success)
                    {
                        self.UpdateChooseHeroState(response.HeroCardInfo);
                        self.SetTroopHeroCardInfo(response.HeroCardInfo);
                    }
                }
            });
        }

        /// <summary>
        /// 检查体力是否足够
        /// </summary>
        /// <param name="self"></param>
        public static async ETTask<bool> CheckPowerIsEnough(this DlgEditorTroopLayer self)
        {
            long AccountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            M2C_GetGoldInfoResponse response = (M2C_GetGoldInfoResponse)await session.Call(new C2M_GetGoldInfoRequest() { AccountId = AccountId });
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

            return false;
        }

        public static async void ShowWindow(this DlgEditorTroopLayer self, Entity contextData = null)
        {
            // List<HeroCardInfo> heroCardInfos = await self.RequestCurrentTroopInfo();
            // self.SetTroopHeroCardInfo(heroCardInfos);

            int index = await self.RequestCurrentTroopIndex();
            self.ChangeTroopToggleState(index);
            Log.Debug($"index {index}");
            List<HeroCardInfo> heroCardInfos = await self.RequestCurrentTroopInfo(index);
            self.SetTroopHeroCardInfo(heroCardInfos);
        }

        public static void ChangeTroopToggleState(this DlgEditorTroopLayer self, int index)
        {
            Transform transform = self.View.E_TroopGroupContentImage.transform.GetChild(index - 1);
            transform.GetComponent<Toggle>().isOn = true;
        }

        public static async ETTask<int> RequestCurrentTroopIndex(this DlgEditorTroopLayer self)
        {
            // await ETTask.CompletedTask;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            var response = await session.Call(new C2M_GetCurrentTroopIndexRequest()) as M2C_GetCurrentTroopIndexResponse;
            if (response.Error == ErrorCode.ERR_Success)
            {
                var index = response.CurrentTroopIndex;

                Log.Debug($"index {index}");
                return index;
            }

            // return 1;
            return 1;
        }

        // public static async void GetCurrentTroopId(this DlgEditorTroopLayer self)
        // {
        //     long accountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
        //     Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
        //
        //     C2M_GetAllTroopInfosRequest request = new C2M_GetAllTroopInfosRequest() { Account = accountId };
        //     M2C_GetAllTroopInfosResponse response = (M2C_GetAllTroopInfosResponse)await session.Call(request);
        //     if (response.Error == ErrorCode.ERR_Success)
        //     {
        //         self.CurrentChooseTroopId = response.TroopInfos[0].TroopId;
        //     }
        // }

        public static async void SetTroopHeroCardInfo(this DlgEditorTroopLayer self, List<HeroCardInfo> heroCardInfos)
        {
            self.TroopHeroCardInfos = heroCardInfos;
            if (self.ItemHeroCards.Count == 0)
            {
                var itemHeroPrefab = "ItemHeroCard";
                GameObject prefab = await AddressableComponent.Instance.LoadAssetByPathAsync<GameObject>(itemHeroPrefab);
                for (int i = 0; i < 3; i++)
                {
                    GameObject obj = GameObject.Instantiate(prefab, self.View.E_TroopHeroCardItemImage.transform);
                    Scroll_ItemHeroCard heroCard =
                            self.AddChildWithId<Scroll_ItemHeroCard, Transform>(IdGenerater.Instance.GenerateId(), obj.transform);
                    self.ItemHeroCards.Add(heroCard);
                    heroCard.InTroopIndex = i;
                    heroCard.E_ChooseToggle.onValueChanged.RemoveAllListeners();
                    heroCard.E_ChooseToggle.AddListener((value) => { self.OnHeroCardItemClick(heroCard, value); });
                }
            }

            Log.Debug($"heroc card infos {heroCardInfos.Count}");

            for (int i = 0; i < self.ItemHeroCards.Count; i++)
            {
                var item = self.ItemHeroCards[i];
                var heroCardInfo = heroCardInfos.Find(a => a.InTroopIndex == i);
                item.InitHeroCard(heroCardInfo);
            }
        }

        public static async void OnHeroCardItemClick(this DlgEditorTroopLayer self, Scroll_ItemHeroCard heroCard, bool value)
        {
            if (value)
            {
                heroCard.E_ChooseToggle.isOn = false;
                if (heroCard.HeroCardInfo == null)
                {
                    Log.Debug($"choose hero card index {heroCard.InTroopIndex}");
                    self.ShowChooseHeroCardLayer();
                }
                else
                {
                    //取消
                    HeroCardInfo heroCardInfo = heroCard.HeroCardInfo;
                    List<HeroCardInfo> heroCardInfos = await self.UnSetHeroCardTroopIdRequest(heroCardInfo.HeroId);

                    self.UpdateChooseHeroState(heroCardInfos);
                    self.SetTroopHeroCardInfo(heroCardInfos);
                }
            }
        }

        public static async ETTask<List<HeroCardInfo>> UnSetHeroCardTroopIdRequest(this DlgEditorTroopLayer self, long heroId)
        {
            // long accountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            // C2M_UnSetHeroFromTroopRequestTroopRequest request = new C2M_UnSetHeroToTroopRequest()
            // {
            // AccountId = accountId, HeroId = heroId, TroopId = self.CurrentChooseTroopId
            // };
            var response = (M2C_UnSetHeroFromTroopResponse)await session.Call(new C2M_UnSetHeroFromTroopRequest() { HeroId = heroId });
            if (response.Error == ErrorCode.ERR_Success)
            {
                // self.SetTroopHeroCardInfo(response.HeroCardInfos);
                return response.HeroCardInfos;
            }

            return new List<HeroCardInfo>();
        }

        public static async ETTask<List<HeroCardInfo>> UpdateHeroCardTroopId(this DlgEditorTroopLayer self, long heroId, long troopId)
        {
            // long account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            //更新此英雄到队伍里面
            var response = await session.Call(new C2M_SetHeroToTroopRequest() { HeroId = heroId }) as M2C_SetHeroToTroopResponse;
            if (response.Error == ErrorCode.ERR_Success)
            {
                return response.HeroCardInfos;
            }

            return new List<HeroCardInfo>();
        }

        public static async void OnHeroItemClick(this DlgEditorTroopLayer self, HeroCardInfo heroCardInfo, Scroll_ItemHeroCard heroCard, bool value)
        {
            //
            if (value)
            {
                Log.Debug($"troop hero card infos count {self.TroopHeroCardInfos.Count}");
                if (self.TroopHeroCardInfos.Count >= 3)
                {
                    heroCard.E_ChooseToggle.isOn = false;
                    return;
                }
                
                
                List<HeroCardInfo> heroCardInfos = await self.UpdateHeroCardTroopId(heroCardInfo.HeroId, self.CurrentChooseTroopId);
                self.SetTroopHeroCardInfo(heroCardInfos);
                self.UpdateChooseHeroState(heroCardInfos);
            }
            else
            {
                List<HeroCardInfo> heroCardInfos = await self.UnSetHeroCardTroopIdRequest(heroCardInfo.HeroId);
                self.SetTroopHeroCardInfo(heroCardInfos);
                self.UpdateChooseHeroState(heroCardInfos);

            }
        }

        public static void UpdateChooseHeroState(this DlgEditorTroopLayer self, List<HeroCardInfo> heroCardInfos)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_AllHeroBagLayer);
            if (baseWindow != null)
            {
                baseWindow.GetComponent<DlgAllHeroBagLayer>().SetAllChooseHeroCardInfos(heroCardInfos);
                baseWindow.GetComponent<DlgAllHeroBagLayer>().SetUnabelNameHeroCardInfo(heroCardInfos);
            }
        }

        public static async void ShowChooseHeroCardLayer(this DlgEditorTroopLayer self)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_AllHeroBagLayer);
            UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_AllHeroBagLayer);
            RectTransform rectTransform = baseWindow.uiTransform.GetComponent<RectTransform>();
            baseWindow.GetComponent<DlgAllHeroBagLayer>().SetShowHeroType(HeroBagType.Hero);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.offsetMax = new Vector2(0, -840);
            rectTransform.offsetMin = new Vector2(0, 0);

            await uiComponent.ShowWindow(WindowID.WindowID_BackButton);
            UIBaseWindow backBaseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_BackButton);
            // backBaseWindow.uiTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(166, -205);
            backBaseWindow.GetComponent<DlgBackButton>().BackButtonClickAction = () =>
            {
                uiComponent.HideWindow(WindowID.WindowID_AllHeroBagLayer);
                uiComponent.HideWindow(WindowID.WindowID_BackButton);
            };
            baseWindow.GetComponent<DlgAllHeroBagLayer>().OnHeroItemInfoClick = null;
            baseWindow.GetComponent<DlgAllHeroBagLayer>().OnHeroItemInfoClick = self.OnHeroItemClick;
        }

        public static async ETTask<List<HeroCardInfo>> RequestCurrentTroopInfo(this DlgEditorTroopLayer self, int index)
        {
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;

            var getHeroInfosWithTroopIdResponse =
                    await session.Call(new C2M_GetCurrentTroopHeroInfosRequest()) as M2C_GetCurrentTroopHeroInfosResponse;
            if (getHeroInfosWithTroopIdResponse.Error == ErrorCode.ERR_Success)
            {
                List<HeroCardInfo> heroCardInfos = getHeroInfosWithTroopIdResponse.HeroCardInfos;
                return heroCardInfos;
            }

            return null;
        }

        public static void OnBackButtonClick(this DlgEditorTroopLayer self)
        {
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_EditorTroopLayer);
            self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_MainScene).Coroutine();
        }

        public static void HideWindow(this DlgEditorTroopLayer self)
        {
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_BackButton);
        }
    }
}