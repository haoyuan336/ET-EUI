using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace ET
{
    public static class DlgShowHeroInfoLayerSystem
    {
        public static void RegisterUIEvent(this DlgShowHeroInfoLayer self)
        {
            self.View.E_BackButton.AddListener(self.BackButtonClick);
            self.View.E_UpdateStarButton.AddListener(self.OnUpStarButtonClick);
            self.View.E_UpdateLevelButton.AddListenerAsync(self.OnUpdateHeroLevelButtonClick);
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_GoldInfoUI);
            if (baseWindow != null)
            {
                baseWindow.GetComponent<DlgGoldInfoUI>().DataChangeAction += self.RegisterAccountInfoDataChangeAction;
            }

            self.View.E_LockToggle.onValueChanged.AddListener(self.OnLockToogleValueChange);
            self.View.E_ShowModelButton.AddListenerAsync(self.ShowHeroModeButtonClick);
        }

        public static async ETTask ShowHeroModeButtonClick(this DlgShowHeroInfoLayer self)
        {
            await ETTask.CompletedTask;
        }

        public static async void OnLockToogleValueChange(this DlgShowHeroInfoLayer self, bool value)
        {
            var account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            var session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            C2M_LockHeroCardRequest request = new C2M_LockHeroCardRequest() { Account = account, HeroId = self.HeroCardInfo.HeroId, Lock = value };
            var response = await session.Call(request) as M2C_LockHeroCardResponse;
            if (response.Error == ErrorCode.ERR_Success)
            {
                Log.Debug("切换英雄锁的消息发送成功");
            }
        }

        public static void OnUpdateHeroRankSuccessAction(this DlgShowHeroInfoLayer self, HeroCardInfo heroCardInfo)
        {
            self.SetHeroInfo(heroCardInfo);
        }

        /// <summary>
        /// 升级按钮点击回调
        /// </summary>
        /// <param name="self"></param>
        public static async ETTask OnUpdateHeroLevelButtonClick(this DlgShowHeroInfoLayer self)
        {
            if (HeroHelper.CheckIsMaxLevel(self.HeroCardInfo))
            {
                return;
            }

            if (HeroHelper.CheckIsNeedUpdateRank(self.HeroCardInfo))
            {
                //需要升阶了，显示升阶页面

                UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
                await uiComponent.ShowWindow(WindowID.WindowID_UpdateHeroRankLayer);
                UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_UpdateHeroRankLayer);
                if (baseWindow != null)
                {
                    baseWindow.GetComponent<DlgUpdateHeroRankLayer>().SetTargetInfo(self.HeroCardInfo);
                    baseWindow.GetComponent<DlgUpdateHeroRankLayer>().UpdateHeroRankSuccessAction = self.OnUpdateHeroRankSuccessAction;
                }

                return;
            }

            long account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            C2M_UpdateHeroLevelRequest request = new C2M_UpdateHeroLevelRequest() { Account = account, HeroId = self.HeroCardInfo.HeroId };
            var response = await session.Call(request) as M2C_UpdateHeroLevelResponse;
            if (response.Error == ErrorCode.ERR_Success)
            {
                self.SetHeroInfo(response.HeroCardInfo);

                UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
                UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_GoldInfoUI);
                if (baseWindow != null)
                {
                    baseWindow.GetComponent<DlgGoldInfoUI>().ReferGoldInfo();
                }
            }
        }

        public static void RegisterAccountInfoDataChangeAction(this DlgShowHeroInfoLayer self)
        {
            self.RequestCurrentExp();
        }

        public static async void OnWeaponButtonClick(this DlgShowHeroInfoLayer self)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_HeroWeaponPreviewLayer);
            UIBaseWindow uiBaseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_HeroWeaponPreviewLayer);
            uiBaseWindow.GetComponent<DlgHeroWeaponPreviewLayer>().SetTargetInfo(self.HeroCardInfo);
        }

        public static async void OnUpStarButtonClick(this DlgShowHeroInfoLayer self)
        {
            //升星系统
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_UpdateHeroStarLayer);
            UIBaseWindow uiBaseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_UpdateHeroStarLayer);
            uiBaseWindow.GetComponent<DlgUpdateHeroStarLayer>().SetTargetInfo(self.HeroCardInfo);
            await ETTask.CompletedTask;
        }

        public static async void OnUpRankButtonClick(this DlgShowHeroInfoLayer self)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_UpdateHeroRankLayer);
            UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_UpdateHeroRankLayer);
            baseWindow.GetComponent<DlgUpdateHeroRankLayer>().SetTargetInfo(self.HeroCardInfo);
        }

        public static async void OnComposeButtonClick(this DlgShowHeroInfoLayer self)
        {
            // 点击强化按钮，显示强化页面
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_HeroStrengthenPreviewLayer);
            UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_HeroStrengthenPreviewLayer);
            baseWindow.GetComponent<DlgHeroStrengthenPreviewLayer>().SetTargetInfo(self.HeroCardInfo);
        }

        public static async void BackButtonClick(this DlgShowHeroInfoLayer self)
        {
            await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_HeroInfoLayerUI);
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_ShowHeroInfoLayer);
        }

        public static void ReferHeroCardView(this DlgShowHeroInfoLayer self, HeroCardInfo heroCardInfo)
        {
            // C2M_GetHeroInfosWithTroopIdRequest
            self.SetHeroInfo(heroCardInfo);
        }

        public static async void SetHeroInfo(this DlgShowHeroInfoLayer self, HeroCardInfo heroCardInfo)
        {
            Log.Debug("Set hero info");
            self.HeroCardInfo = heroCardInfo;
            self.View.E_LevelText.text = $"Lv:{heroCardInfo.Level}";
            HeroConfig config = HeroConfigCategory.Instance.Get(heroCardInfo.ConfigId);
            string heroModeStr = config.HeroMode;
            self.View.E_HeroNameText.text = config.HeroName;
            if (self.HeroModeShow == null)
            {
                self.HeroModeShow = await AddressableComponent.Instance.LoadGameObjectAndInstantiateByPath(heroModeStr);
            }

            // self.View.E_RankText.text = $"{heroCardInfo.Rank}阶";
            var baseAttack = HeroHelper.GetHeroBaseAttack(heroCardInfo);
            var baseHP = HeroHelper.GetHeroBaseHP(heroCardInfo);
            var baseDefence = HeroHelper.GetHeroBaseDefence(heroCardInfo);

            self.View.E_BaseAttackText.text = $"{baseAttack}";
            self.View.E_BaseHPText.text = $"{baseHP}";
            self.View.E_BaseDefText.text = $"{baseDefence}";
            self.SethetoStar(heroCardInfo);
            self.SetElementInfo(heroCardInfo);
            self.InitOnWeapon();
            self.RequestCurrentExp();

            self.View.E_LockToggle.isOn = heroCardInfo.IsLock;
        }

        public static async void InitOnWeapon(this DlgShowHeroInfoLayer self)
        {
            //取出此英雄穿戴的所有装备
            if (self.WeaponDicts.Count == 0)
            {
                WeaponType[] weaponTypes = { WeaponType.Weapon, WeaponType.Equip, WeaponType.Ring, WeaponType.Accessory };
                GameObject prefab = await AddressableComponent.Instance.LoadAssetByPathAsync<GameObject>("ItemWeapon");
                foreach (var weaponType in weaponTypes)
                {
                    GameObject obj = GameObject.Instantiate(prefab, self.View.E_WeaponInfoImage.transform);
                    Scroll_ItemWeapon weapon = self.AddChildWithId<Scroll_ItemWeapon>(IdGenerater.Instance.GenerateId());
                    weapon.uiTransform = obj.transform;
                    weapon.ShowAddButtonState();
                    weapon.E_ChooseButton.onClick.AddListener(() => { self.OnWeaponPosClick(weaponType); });
                    self.WeaponDicts.Add((int)weaponType, weapon);
                }
            }

            long account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            //请求装备信息
            C2M_GetOnWeaponsRequest request = new C2M_GetOnWeaponsRequest() { Account = account, HeroId = self.HeroCardInfo.HeroId };
            var response = await session.Call(request) as M2C_GetOnWeaponsResponse;

            if (response.Error == ErrorCode.ERR_Success)
            {
                List<WeaponInfo> weaponInfos = response.WeaponInfos;
                self.SetOnWeaponInfos(weaponInfos);
            }

            await ETTask.CompletedTask;
        }

        public static void SetOnWeaponInfos(this DlgShowHeroInfoLayer self, List<WeaponInfo> weaponInfos)
        {
            foreach (var weapon in self.WeaponDicts.Values)
            {
                weapon.ShowAddButtonState();
            }

            self.WeaponInfos = weaponInfos;
            foreach (var weaponInfo in weaponInfos)
            {
                WeaponsConfig config = WeaponsConfigCategory.Instance.Get(weaponInfo.ConfigId);
                Scroll_ItemWeapon itemWeapon = self.WeaponDicts[config.WeaponType];
                itemWeapon.InitWeaponCardView(weaponInfo);
            }
        }

        public static void OnWeaponPosClick(this DlgShowHeroInfoLayer self, WeaponType weaponType)
        {
            Log.Debug($" click weapon pos {weaponType}");
            // await ETTask.CompletedTask;
            Scroll_ItemWeapon itemWeapon = self.WeaponDicts[(int)weaponType];
            if (itemWeapon.WeaponInfo == null)
            {
                self.ShowChooseWeaponLayer(weaponType);
            }
            else
            {
                self.ShowWeaponInfoLayer(itemWeapon.WeaponInfo);
            }
        }

        public static void RemoveWeaponInfo(this DlgShowHeroInfoLayer self, WeaponInfo weaponInfo)
        {
            self.WeaponInfos.RemoveAll(a => a.WeaponId.Equals(weaponInfo.WeaponId));
            self.SetOnWeaponInfos(self.WeaponInfos);
        }

        public static async void ShowWeaponInfoLayer(this DlgShowHeroInfoLayer self, WeaponInfo weaponInfo)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_WeaponInfoLayer);
            UIBaseWindow uiBaseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_WeaponInfoLayer);
            uiBaseWindow.GetComponent<DlgWeaponInfoLayer>().SetTargetInfo(weaponInfo);

            await ETTask.CompletedTask;
        }

        public static async void OnChooseWeaponItemClick(this DlgShowHeroInfoLayer self, WeaponInfo weaponInfo)
        {
            //装配此武器在英雄身上
            Log.Debug($"choose weapon info {weaponInfo.WeaponId}");
            long account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;

            C2M_UpdateOnWeaponRequest request =
                    new C2M_UpdateOnWeaponRequest() { Account = account, WeaponId = weaponInfo.WeaponId, HeroId = self.HeroCardInfo.HeroId };
            M2C_UpdateOnWeaponResponse response = await session.Call(request) as M2C_UpdateOnWeaponResponse;
            if (response.Error == ErrorCode.ERR_Success)
            {
                List<WeaponInfo> weaponInfos = response.WeaponInfos;
                self.SetOnWeaponInfos(weaponInfos);
            }
        }

        public static async void ShowChooseWeaponLayer(this DlgShowHeroInfoLayer self, WeaponType weaponType)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_ChooseWeaponLayer);
            UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_ChooseWeaponLayer);
            baseWindow.GetComponent<DlgChooseWeaponLayer>().ShowAllWeaponType(weaponType);
            baseWindow.GetComponent<DlgChooseWeaponLayer>().OnWeaponItemClickAction = self.OnChooseWeaponItemClick;
        }

        public static async void SetElementInfo(this DlgShowHeroInfoLayer self, HeroCardInfo heroCardInfo)
        {
            var configId = heroCardInfo.ConfigId;
            var config = HeroConfigCategory.Instance.Get(configId);
            var elementConfig = ElementConfigCategory.Instance.Get(config.HeroColor);
            var elementStr = elementConfig.IconImage;
            Sprite sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(ConstValue.HeroCardAtlasPath, elementStr);
            self.View.E_ElementImage.sprite = sprite;
        }

        public static async void SethetoStar(this DlgShowHeroInfoLayer self, HeroCardInfo heroCardInfo)
        {
            for (int i = 0; i < 5; i++)
            {
                var starStr = $"Star_{i}";
                Transform tr = UIFindHelper.FindDeepChild(self.View.uiTransform.gameObject, starStr);
                // tr.gameObject.SetActive(i < heroCardInfo.Star);
                var commonSprite = ConstValue.CommonUIAtlasPath;
                tr.GetComponent<Image>().sprite =
                        await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(commonSprite,
                            i < heroCardInfo.Star? "star" : "StarTexture");
            }
        }

        public static async void ShowWindow(this DlgShowHeroInfoLayer self, Entity contextData = null)
        {
            await ETTask.CompletedTask;
        }

        public static void HideWindow(this DlgShowHeroInfoLayer self)
        {
            // Log.Debug("hide window show hero info layer");
            GameObject.Destroy(self.HeroModeShow);
            Transform shadow = UIFindHelper.FindDeepChild(GlobalComponent.Instance.Global.gameObject, "Shadow");
            shadow.gameObject.SetActive(false);
        }

        public static void UpdateExpInfoView(this DlgShowHeroInfoLayer self, int expCount)
        {
            // self.View.E_ExpText.text = expCount.ToString();
            //
            var needExp = HeroHelper.GetNextLevelExp(self.HeroCardInfo);
            // self.View.E_ExpText.text = $"{expCount}/{needExp}";
            self.View.E_ExpText.text = needExp != 0? $"{needExp}" : "  ";

            var rate = (float)expCount / needExp;
            self.View.E_ExpBarImage.fillAmount = rate;
            self.View.E_UpdateLevelButton.interactable = expCount >= needExp;
            //判断是否可以升级
            // HeroLevelExpConfig config = HeroLevelExpConfigCategory.Instance.Get()
            var updateRank = HeroHelper.CheckIsNeedUpdateRank(self.HeroCardInfo);
            var isMaxLevel = HeroHelper.CheckIsMaxLevel(self.HeroCardInfo);
            self.View.E_UpdateText.text = isMaxLevel? "MAX" : updateRank? "升阶" : "升级";

            var maxLevel = HeroHelper.GetCurrentRankMaxLevel(self.HeroCardInfo);

            self.View.E_LevelText.text = isMaxLevel? $"Lv.{self.HeroCardInfo.Level}" : $"Lv.{self.HeroCardInfo.Level}/Lv.{maxLevel}";
            self.View.E_RankText.text = $"Rank{self.HeroCardInfo.Rank}";
        }

        public static async void RequestCurrentExp(this DlgShowHeroInfoLayer self)
        {
            long account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            C2M_GetItemInfoRequest request = new C2M_GetItemInfoRequest() { AccountId = account, ConfigId = 1008 };
            M2C_GetItemInfoResponse response = await session.Call(request) as M2C_GetItemInfoResponse;
            if (response.Error == ErrorCode.ERR_Success)
            {
                var count = response.ItemInfo.Count;
                self.UpdateExpInfoView(count);
            }
        }
    }
}