using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgHeroWeaponPreviewLayerSystem
    {
        public static void RegisterUIEvent(this DlgHeroWeaponPreviewLayer self)
        {
            self.View.E_BackButton.AddListener(self.BackButtonClick);
            // self.View.E_EquipButton.AddListenerAsync(self.ShowChooseEquipLayer);
            // self.View.E_WeaponButton.AddListenerAsync(self.ShowWeaponLayer);
            // self.View.E_RingButton.AddListenerAsync(self.ShowAllRingLayer);
            // self.View.E_AccessoryButton.AddListenerAsync(self.ShowAllAccessory);
        }
        

        public static async void OnWeaponItemClick(this DlgHeroWeaponPreviewLayer self, WeaponType type, ESCommonWeaponItem weaponItem, bool value)
        {
            if (value)
            {
                weaponItem.E_Choose.GetComponent<Toggle>().isOn = false;

                if (weaponItem.WeaponInfo == null)
                {
                    UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
                    await uiComponent.ShowWindow(WindowID.WindowID_ChooseWeaponLayer);
                    UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_ChooseWeaponLayer);
                    baseWindow.GetComponent<DlgChooseWeaponLayer>().ShowAllWeaponType(type);
                    baseWindow.GetComponent<DlgChooseWeaponLayer>().SetAlChooseWeaponInfo(weaponItem.WeaponInfo);
                    self.RegisterWeaponClickEvent();
                }
                else
                {
                    self.ShowWeaponInfoLayer(weaponItem.WeaponInfo);
                }
            }
        }

        /// <summary>
        /// 显示武器的详细信息
        /// </summary>
        /// <param name="self"></param>
        /// <param name="weaponInfo"></param>
        public static async void ShowWeaponInfoLayer(this DlgHeroWeaponPreviewLayer self, WeaponInfo weaponInfo)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_WeaponInfoLayer);
            UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_WeaponInfoLayer);
            baseWindow.GetComponent<DlgWeaponInfoLayer>().SetTargetInfo(weaponInfo);
        }

        public static async void RegisterWeaponClickEvent(this DlgHeroWeaponPreviewLayer self)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_ChooseWeaponLayer);
            baseWindow.GetComponent<DlgChooseWeaponLayer>().OnWeaponItemClickAction = null;
            baseWindow.GetComponent<DlgChooseWeaponLayer>().OnWeaponItemClickAction = self.OnChooseOneWeaponItem;
            await ETTask.CompletedTask;
        }

        public static async void OnChooseOneWeaponItem(this DlgHeroWeaponPreviewLayer self, WeaponInfo weaponInfo)
        {
            // WeaponsConfig config = WeaponsConfigCategory.Instance.Get(weaponInfo.ConfigId);
            // ESCommonWeaponItem item = self.WeaponItems.Find(a => (int) a.CurrentType == config.WeaponType);
            // if (item != null)
            // {
            List<WeaponInfo> weaponInfos = await self.SetWeaponOnHeroRequest(weaponInfo);
            // item.SetWeaponInfo(weaponInfo);
            if (weaponInfo == null)
            {
                return;
            }
            Dictionary<int, ESCommonWeaponItem> weaponItemDicts = new Dictionary<int, ESCommonWeaponItem>();
            foreach (var weaponItem in self.WeaponItems)
            {
                weaponItem.SetWeaponInfo(null);
                weaponItemDicts.Add((int) weaponItem.CurrentType, weaponItem);
            }

            foreach (var info in weaponInfos)
            {
                Log.Debug($"weapon info id {info.WeaponId}");
                WeaponsConfig weaponsConfig = WeaponsConfigCategory.Instance.Get(info.ConfigId);
                weaponItemDicts[weaponsConfig.WeaponType].SetWeaponInfo(info);
            }
            // }
        }

        public static async ETTask<List<WeaponInfo>> SetWeaponOnHeroRequest(this DlgHeroWeaponPreviewLayer self, WeaponInfo weaponInfo)
        {
            //将道具装备到英雄身上

            long accountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            C2M_UpdateOnWeaponRequest request = new C2M_UpdateOnWeaponRequest()
            {
                Account = accountId, HeroId = self.HeroCardInfo.HeroId, WeaponId = weaponInfo.WeaponId
            };

            M2C_UpdateOnWeaponResponse response = (M2C_UpdateOnWeaponResponse) await session.Call(request);

            if (response.Error == ErrorCode.ERR_Success)
            {
                Log.Debug("设置装备成功");
                List<WeaponInfo> weaponInfos = response.WeaponInfos;
                self.InitWeaponInfoView(weaponInfos);
                return weaponInfos;
            }

            return null;
        }

        public static void BackButtonClick(this DlgHeroWeaponPreviewLayer self)
        {
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_HeroWeaponPreviewLayer);
        }

        public static void ShowWindow(this DlgHeroWeaponPreviewLayer self, Entity contextData = null)
        {
        }



        public static void ReferHeroInfo(this DlgHeroWeaponPreviewLayer self)
        {
            if (self.HeroCardInfo != null)
            {
                self.SetTargetInfo(self.HeroCardInfo);
            }
        }
        public static async void SetTargetInfo(this DlgHeroWeaponPreviewLayer self, HeroCardInfo heroCardInfo)
        {
            await self.InitWeapinItems();
            self.HeroCardInfo = heroCardInfo;
            long accountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            C2M_GetOnWeaponsRequest request = new C2M_GetOnWeaponsRequest() { Account = accountId, HeroId = self.HeroCardInfo.HeroId };
            M2C_GetOnWeaponsResponse response = (M2C_GetOnWeaponsResponse) await session.Call(request);
            if (response.Error == ErrorCode.ERR_Success)
            {
                List<WeaponInfo> weaponInfos = response.WeaponInfos;

                Dictionary<int, ESCommonWeaponItem> items = new Dictionary<int, ESCommonWeaponItem>();

                foreach (var item in self.WeaponItems)
                {
                    item.SetWeaponInfo(null);
                    items.Add((int) item.CurrentType, item);
                }

                Log.Debug($"获取装备的装备成功{weaponInfos.Count}");

                foreach (var weaponInfo in weaponInfos)
                {
                    WeaponsConfig config = WeaponsConfigCategory.Instance.Get(weaponInfo.ConfigId);
                    items[config.WeaponType].SetWeaponInfo(weaponInfo);
                }
                // for

                self.InitWeaponInfoView(weaponInfos);
            }
        }

        public static async void InitWeaponInfoView(this DlgHeroWeaponPreviewLayer self, List<WeaponInfo> weaponInfos)
        {
            Log.Debug($"init weapon info view {weaponInfos.Count}");

            long account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            // List<WordBarInfo> wordBarInfos = new List<WordBarInfo>();

            Dictionary<WeaponInfo, List<WordBarInfo>> wordBarInfos = new Dictionary<WeaponInfo, List<WordBarInfo>>();

            foreach (var weaponInfo in weaponInfos)
            {
                //获取次装备的词条
                C2M_GetWeaponWordBarsRequest request = new C2M_GetWeaponWordBarsRequest() { Account = account, WeaponId = weaponInfo.WeaponId };
                M2C_GetWeaponWordBarsResponse response = (M2C_GetWeaponWordBarsResponse) await session.Call(request);
                if (response.Error == ErrorCode.ERR_Success)
                {
                    wordBarInfos[weaponInfo] = response.WordBarInfos;
                }
            }

            var hp = 0;
            var attack = 0;
            var hpAddition = 0;
            var attackAddition = 0;
            var defenceAddition = 0;
            var defence = 0;
            var criticalHit = 0;
            var CriticalHitDamage = 0;
            var Toughness = 0;
            var DamageAddition = 0;
            var DamageReduction = 0;
            foreach (var wordBardict in wordBarInfos)
            {
                foreach (var wordBar in wordBardict.Value)
                {
                    hp += WeaponHelper.GetWordBarValueByType(wordBar, wordBardict.Key, WordBarType.HP);
                    attack += WeaponHelper.GetWordBarValueByType(wordBar, wordBardict.Key, WordBarType.Attack);
                    hpAddition += WeaponHelper.GetWordBarValueByType(wordBar, wordBardict.Key, WordBarType.HPAddition);
                    attackAddition += WeaponHelper.GetWordBarValueByType(wordBar, wordBardict.Key, WordBarType.AttackAddition);
                    defenceAddition += WeaponHelper.GetWordBarValueByType(wordBar, wordBardict.Key, WordBarType.DefenceAddition);
                    defence += WeaponHelper.GetWordBarValueByType(wordBar, wordBardict.Key, WordBarType.Defecnce);
                    criticalHit += WeaponHelper.GetWordBarValueByType(wordBar, wordBardict.Key, WordBarType.CriticalHit);
                    CriticalHitDamage += WeaponHelper.GetWordBarValueByType(wordBar, wordBardict.Key, WordBarType.CriticalHitDamage);
                    Toughness += WeaponHelper.GetWordBarValueByType(wordBar, wordBardict.Key, WordBarType.Toughness);
                    DamageAddition += WeaponHelper.GetWordBarValueByType(wordBar, wordBardict.Key, WordBarType.DamageAddition);
                    DamageReduction += WeaponHelper.GetWordBarValueByType(wordBar, wordBardict.Key, WordBarType.DamageReduction);
                }
            }

            self.View.E_CurrentHPText.text = $"{hp}";
            self.View.E_CurrentAttackText.text = $"{attack}";
            self.View.E_CurrentHPAdditionText.text = $"{(float) hpAddition / 100}%";
            self.View.E_CurrentAttackAdditionText.text = $"{(float) attackAddition / 100}%";
            self.View.E_CurrentDefenceAdditionText.text = $"{(float) defenceAddition / 100}%";
            self.View.E_CurrentDefenceText.text = $"{defence}";
            self.View.E_CriticalHitText.text = $"{(float) criticalHit / 100}%";
            self.View.E_CriticalHitDamageText.text = $"{(float) CriticalHitDamage / 100}%";
            self.View.E_ToughnessText.text = $"{(float) Toughness / 100}%";
            self.View.E_DamageAdditionText.text = $"{(float) DamageAddition / 100}%";
            self.View.E_DamageReductionText.text = $"{(float) DamageReduction / 100}%";

            // var hp = 0;
            // foreach (var wordBarInfo in wordBarInfos)
            // {
            //     hp += WeaponHelper.GetWordBarHP(wordBarInfo);
            // }

            // Log.Debug($"总词条数{wordBarInfos.Count}");
        }

        public static async ETTask InitWeapinItems(this DlgHeroWeaponPreviewLayer self)
        {
            if (self.WeaponItems.Count == 0)
            {
                Transform[] lists =
                {
                    self.View.E_EquipImage.transform, self.View.E_WeaponImage.transform, self.View.E_RingImage.transform,
                    self.View.E_AccessoryImage.transform
                };
                WeaponType[] weaponTypes = { WeaponType.Equip, WeaponType.Weapon, WeaponType.Ring, WeaponType.Accessory };

                GameObject gameObject = await AddressableComponent.Instance.LoadAssetByPathAsync<GameObject>("ItemWeapon");
                // gameObject.transform.SetParent(b);
                for (var i = 0; i < lists.Length; i++)
                {
                    Transform transform = lists[i];
                    GameObject obj = GameObject.Instantiate(gameObject, transform);
                    obj.GetComponent<RectTransform>().localScale = Vector3.one;

                    ESCommonWeaponItem item = self.AddChildWithId<ESCommonWeaponItem, WeaponType, Transform>(IdGenerater.Instance.GenerateId(),
                        weaponTypes[i],
                        obj.transform);
                    self.WeaponItems.Add(item);
                    item.OnWeaponItemClickAction = self.OnWeaponItemClick;
                }
            }
        }
    }
}