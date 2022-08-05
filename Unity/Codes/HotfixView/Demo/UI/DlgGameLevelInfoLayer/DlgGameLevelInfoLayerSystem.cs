using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public static class DlgGameLevelInfoLayerSystem
    {
        public static void RegisterUIEvent(this DlgGameLevelInfoLayer self)
        {
            self.View.E_EditorTroopButton.AddListenerAsync(self.ShowEditorTroopLayer);
            self.View.E_StartGameButton.AddListenerAsync(self.StartGameClickAction);
            // self.View.ESTroopHeroCards.RegisterUIEvent();
            self.View.E_BackButton.AddListener(self.OnBackButtonClick);
        }

        // public static async ETTask StartGame(this DlgGameLevelInfoLayer self)
        // {
        //     
        //     await ETTask.CompletedTask;
        // }
        /// <summary>
        /// 检查体力是否足够
        /// </summary>
        /// <param name="self"></param>
        public static async ETTask<bool> CheckPowerIsEnough(this DlgGameLevelInfoLayer self)
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

        public static async ETTask StartGameClickAction(this DlgGameLevelInfoLayer self)
        {
            
            bool isPowerEnough = await self.CheckPowerIsEnough();
            if (!isPowerEnough)
            {
                await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_PowerNotEnoughAlert);
                return;
            }

            var Account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;

            var inputText = self.View.E_LevelInputInputField.text;
            if (!string.IsNullOrEmpty(inputText))
            {
                Log.Debug($"玩家输入了关卡数{inputText}");
                if (int.Parse(inputText) >= LevelConfigCategory.Instance.GetAll().Count)
                {
                    await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_AlertLayer);

                    self.DomainScene().GetComponent<UIComponent>().GetUIBaseWindow(WindowID.WindowID_AlertLayer).GetComponent<DlgAlertLayer>()
                            .SetText("输入的关卡数超出配置的关卡数");
                    return;
                }

                var response = await session.Call(new C2M_PlayerChooseLevelNumRequest() { Account = Account, LevelNum = int.Parse(inputText) });
                if (response.Error != ErrorCode.ERR_Success)
                {
                    return;
                }
            }

            M2C_StartPVEGameResponse m2CStartPveGameResponse =
                    (M2C_StartPVEGameResponse) await session.Call(new C2M_StartPVEGameRequest()
                    {
                        AccountId = Account, TroopId = self.CurrentChooseTroopId
                    });
            if (m2CStartPveGameResponse.Error == ErrorCode.ERR_Success)
            {
                self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_GameLevelInfoLayer);
                self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_GoldInfoUI);
            }
        }

        public static async ETTask ShowEditorTroopLayer(this DlgGameLevelInfoLayer self)
        {
            //显示编辑队伍的页面
            await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_EditorTroopLayer);
            UIBaseWindow baseWindow = self.DomainScene().GetComponent<UIComponent>().GetUIBaseWindow(WindowID.WindowID_EditorTroopLayer);
            var troopLayer = baseWindow.GetComponent<DlgEditorTroopLayer>();

            troopLayer.EditorHeroCardAction = null;
            troopLayer.EditorHeroCardAction = self.SetTroopHeroCardInfo;
        }

        public static async void ShowWindow(this DlgGameLevelInfoLayer self, Entity contextData = null)
        {
            // self.ShowTroopHeroCardInfo();

            List<HeroCardInfo> heroCardInfos = await self.RequestCurrentTroopInfo();
            self.SetTroopHeroCardInfo(heroCardInfos);

            //显示游戏详情页面
            await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_GameLevelEnemyInfoLayer);
            //取出玩家的关卡信息
            var accountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            var session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            var request = new C2M_GetAccountInfoWithAccountIdRequest() { AccountId = accountId };
            var response = await session.Call(request) as M2C_GetAccountInfoWidthAccointIdResponse;
            if (response.Error == ErrorCode.ERR_Success)
            {
                var account = response.AccountInfo;
                var currentPveLevel = account.PvELevelNumber;
                self.View.E_LevelText.text = $"挑战关卡:{currentPveLevel}";
            }
        }

        public static async void GetCurrentTroopId(this DlgGameLevelInfoLayer self)
        {
            long accountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;

            C2M_GetAllTroopInfosRequest request = new C2M_GetAllTroopInfosRequest() { Account = accountId };
            M2C_GetAllTroopInfosResponse response = (M2C_GetAllTroopInfosResponse) await session.Call(request);
            if (response.Error == ErrorCode.ERR_Success)
            {
                self.CurrentChooseTroopId = response.TroopInfos[0].TroopId;
            }
        }

        public static async void SetTroopHeroCardInfo(this DlgGameLevelInfoLayer self, List<HeroCardInfo> heroCardInfos)
        {
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
                    heroCard.E_ChooseToggle.onValueChanged.AddListener((value) => { self.OnHeroCardItemClick(heroCard, value); });
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

        public static async void OnHeroCardItemClick(this DlgGameLevelInfoLayer self, Scroll_ItemHeroCard heroCard, bool value)
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

        public static async ETTask<List<HeroCardInfo>> UnSetHeroCardTroopIdRequest(this DlgGameLevelInfoLayer self, long heroId)
        {
            long accountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            C2M_UnSetHeroToTroopRequest request = new C2M_UnSetHeroToTroopRequest()
            {
                AccountId = accountId, HeroId = heroId, TroopId = self.CurrentChooseTroopId
            };
            M2C_UnSetHeroToTroopResponse response = (M2C_UnSetHeroToTroopResponse) await session.Call(request);
            if (response.Error == ErrorCode.ERR_Success)
            {
                // self.SetTroopHeroCardInfo(response.HeroCardInfos);
                return response.HeroCardInfos;
            }

            return new List<HeroCardInfo>();
        }

        public static async ETTask<List<HeroCardInfo>> UpdateHeroCardTroopId(this DlgGameLevelInfoLayer self, long heroId, long troopId)
        {
            long account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            //更新此英雄到队伍里面
            C2M_SetHeroToTroopRequest request = new C2M_SetHeroToTroopRequest()
            {
                AccountId = account, TroopId = self.CurrentChooseTroopId, HeroId = heroId
            };
            M2C_SetHeroToTroopResponse response = (M2C_SetHeroToTroopResponse) await session.Call(request);
            if (response.Error == ErrorCode.ERR_Success)
            {
                // response.HeroCardInfos
                // self.TroopHeroCardInfos = response.HeroCardInfos;
                // self.SetTroopHeroCardInfo(self.TroopHeroCardInfos);
                return response.HeroCardInfos;
            }

            return new List<HeroCardInfo>();
        }

        public static async void OnHeroItemClick(this DlgGameLevelInfoLayer self, HeroCardInfo heroCardInfo, Scroll_ItemHeroCard heroCard, bool value)
        {
            //
            if (value)
            {
                List<HeroCardInfo> heroCardInfos = await self.UpdateHeroCardTroopId(heroCardInfo.HeroId, self.CurrentChooseTroopId);
                self.SetTroopHeroCardInfo(heroCardInfos);
                self.UpdateChooseHeroState(heroCardInfos);
            }
            else
            {
                List<HeroCardInfo> heroCardInfos = await self.UnSetHeroCardTroopIdRequest(heroCardInfo.HeroId);
                self.SetTroopHeroCardInfo(heroCardInfos);
            }
        }

        public static void UpdateChooseHeroState(this DlgGameLevelInfoLayer self, List<HeroCardInfo> heroCardInfos)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_AllHeroBagLayer);
            if (baseWindow != null)
            {
                baseWindow.GetComponent<DlgAllHeroBagLayer>().SetAllChooseHeroCardInfos(heroCardInfos);
                baseWindow.GetComponent<DlgAllHeroBagLayer>().SetUnabelNameHeroCardInfo(heroCardInfos);
            }
        }

        public static async void ShowChooseHeroCardLayer(this DlgGameLevelInfoLayer self)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_AllHeroBagLayer);
            UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_AllHeroBagLayer);
            // baseWindow.GetComponent<DlgAllHeroBagLayer>().UnAbleHeroItemWhitHeroInfo(self.HeroCardInfo);
            // baseWindow.GetComponent<DlgAllHeroBagLayer>().SetAllChooseHeroCardInfos(self.AlChooseHeroCardInfo);
            // if (self.CheckIsFull())
            // {
            //     baseWindow.GetComponent<DlgAllHeroBagLayer>().EnableItemWhitHeroInfos(self.AlChooseHeroCardInfo);
            // }

            // baseWindow.GetComponent<DlgAllHeroBagLayer>().SetEnableSameStarCountHeroInfo(self.HeroCardInfo);
            RectTransform rectTransform = baseWindow.uiTransform.GetComponent<RectTransform>();
            baseWindow.GetComponent<DlgAllHeroBagLayer>().SetShowHeroType(HeroBagType.Hero);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.offsetMax = new Vector2(0, -700);
            rectTransform.offsetMin = new Vector2(0, 0);

            await uiComponent.ShowWindow(WindowID.WindowID_BackButton);
            UIBaseWindow backBaseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_BackButton);
            backBaseWindow.uiTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(166, -205);
            backBaseWindow.GetComponent<DlgBackButton>().BackButtonClickAction = () =>
            {
                uiComponent.HideWindow(WindowID.WindowID_AllHeroBagLayer);
                uiComponent.HideWindow(WindowID.WindowID_BackButton);
            };
            baseWindow.GetComponent<DlgAllHeroBagLayer>().OnHeroItemInfoClick = null;
            baseWindow.GetComponent<DlgAllHeroBagLayer>().OnHeroItemInfoClick = self.OnHeroItemClick;
        }

        public static async ETTask<List<HeroCardInfo>> RequestCurrentTroopInfo(this DlgGameLevelInfoLayer self)
        {
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            long account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            C2M_GetAllTroopInfosRequest troopInfosRequest = new C2M_GetAllTroopInfosRequest() { Account = account };
            M2C_GetAllTroopInfosResponse response = (M2C_GetAllTroopInfosResponse) await session.Call(troopInfosRequest);
            if (response.Error == ErrorCode.ERR_Success)
            {
                // Log.Debug($"response {response.TroopInfos.Count}");
                // TroopInfo troopInfo = response.TroopInfos[0];
                if (response.TroopInfos.Count > 0)
                {
                    long troopId = response.TroopInfos[0].TroopId;
                    //获取此id下的玩家列表
                    self.CurrentChooseTroopId = troopId;
                    C2M_GetHeroInfosWithTroopIdRequest c2m = new C2M_GetHeroInfosWithTroopIdRequest() { TroopId = troopId };
                    M2C_GetHeroInfosWithTroopIdResponse getHeroInfosWithTroopIdResponse =
                            (M2C_GetHeroInfosWithTroopIdResponse) await session.Call(c2m);
                    if (getHeroInfosWithTroopIdResponse.Error == ErrorCode.ERR_Success)
                    {
                        List<HeroCardInfo> heroCardInfos = getHeroInfosWithTroopIdResponse.HeroCardInfos;
                        return heroCardInfos;
                    }
                }
            }

            return null;
        }

        public static void OnBackButtonClick(this DlgGameLevelInfoLayer self)
        {
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_GameLevelInfoLayer);
            self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_MainScene).Coroutine();
        }

        public static void HideWindow(this DlgGameLevelInfoLayer self)
        {
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_BackButton);
        }
    }
}