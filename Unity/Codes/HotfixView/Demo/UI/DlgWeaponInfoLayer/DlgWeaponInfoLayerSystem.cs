using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgWeaponInfoLayerSystem
    {
        public static void RegisterUIEvent(this DlgWeaponInfoLayer self)
        {
            self.View.E_BackButton.AddListener(() =>
            {
                self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_WeaponInfoLayer);
            });
            self.View.E_WeaponStrengthButton.AddListenerAsync(self.WeaponStrengthButtonClick);
            // self
            self.View.E_WeaponClearButton.AddListener(async () =>
            {
                UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
                await uiComponent.ShowWindow(WindowID.WindowID_WeaponClearLayer);
                UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_WeaponClearLayer);
                baseWindow.GetComponent<DlgWeaponClearLayer>().SetTargetInfo(self.WeaponInfo);
            });
        }

        public static async ETTask WeaponStrengthButtonClick(this DlgWeaponInfoLayer self)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_WeaponStrengthenPreviewLayer);
            UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_WeaponStrengthenPreviewLayer);
            baseWindow.GetComponent<DlgWeaponStrengthenPreviewLayer>().SetTargetInfo(self.WeaponInfo);
            await ETTask.CompletedTask;
        }

        public static void HideWindow(this DlgWeaponInfoLayer self)
        {
            // Log.Debug("dlg weapon info layer hide window");
            if (self.CurrentHeroCardItem != null)
            {
                GameObject.Destroy(self.CurrentHeroCardItem.uiTransform.gameObject);
                self.CurrentHeroCardItem?.Dispose();
                self.CurrentHeroCardItem = null;
            }

            foreach (var item in self.WordBarItems)
            {
                GameObject.Destroy(item.uiTransform.gameObject);
                item.Dispose();
            }

            self.WordBarItems.Clear();
        }

        public static async void SetWordBarInfos(this DlgWeaponInfoLayer self, List<WordBarInfo> wordBarInfos)
        {
            if (self.WordBarItems.Count  == 0)
            {
                WeaponsConfig config = WeaponsConfigCategory.Instance.Get(self.WeaponInfo.ConfigId);
                var wordsCount = config.WordBarCount;
                var prefab = await AddressableComponent.Instance.LoadAssetByPathAsync<GameObject>("Assets/Bundles/UI/Common/ESCommonWordBar.prefab");

                for (int i = 0; i < wordsCount + 1; i++)
                {
                    // var gameObject = AddressableComponent.
                    var gameObject = GameObject.Instantiate(prefab);
                    gameObject.transform.SetParent(self.View.E_WordBarGroupImage.transform);
                    gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
                    var esCommonWordBar = self.AddChildWithId<ESCommonWordBar, Transform>(IdGenerater.Instance.GenerateId(), gameObject.transform);
                    self.WordBarItems.Add(esCommonWordBar);
                }
            }

            for (int i = 0; i < self.WordBarItems.Count; i++)
            {
                var item = self.WordBarItems[i];
                if (i < wordBarInfos.Count)
                {
                    item.SetInfo(wordBarInfos[i], self.WeaponInfo);
                }
                else
                {
                    item.SetInfo(null, self.WeaponInfo);
                }
            }
            
            
        }

        public static void ShowWindow(this DlgWeaponInfoLayer self, Entity contextData = null)
        {
        }

        public static async void SetTargetInfo(this DlgWeaponInfoLayer self, WeaponInfo weaponInfo)
        {
            self.WeaponInfo = weaponInfo;
            await self.InitCurrentWeaponItem(weaponInfo);
            List<WordBarInfo> wordBarInfos = await self.GetWeaponWordsInfoRequest();

            
            // await self.InitWordBarItems();
            self.SetWordBarInfos(wordBarInfos);
            await self.InitOnWeaponHeroItem();
            // await self.GetWeaponWordsInfoRequest();
        }
        

        public static async ETTask InitOnWeaponHeroItem(this DlgWeaponInfoLayer self)
        {
            //首先取出来装备此装备的英雄
            var heroId = self.WeaponInfo.OnWeaponHeroId;
            //获取此英雄信息
            Log.Debug($"hero id {heroId}");
            Log.Debug($"weapon id {self.WeaponInfo.WeaponId}");
            if (heroId != 0)
            {
                if (self.CurrentHeroCardItem == null)
                {
                    self.CurrentHeroCardItem = self.AddChildWithId<Scroll_ItemHeroCard>(IdGenerater.Instance.GenerateId());
                    var obj = await AddressableComponent.Instance.LoadGameObjectAndInstantiateByPath("ItemHeroCard");
                    obj.transform.SetParent(self.View.E_CurrentHeroPosImage.transform);
                    self.CurrentHeroCardItem.BindTrans(obj.transform);
                    RectTransform rectTransform = obj.GetComponent<RectTransform>();
                    rectTransform.offsetMax = Vector2.zero;
                    rectTransform.offsetMin = Vector2.zero;
                    rectTransform.anchorMin = Vector2.zero;
                    rectTransform.anchorMax = Vector2.one;
                    rectTransform.localScale = Vector3.one;
                    var toggle = self.CurrentHeroCardItem.E_ChooseToggle.GetComponent<Toggle>();
                    toggle.onValueChanged.RemoveAllListeners();
                    toggle.onValueChanged.AddListener((value) =>
                    {
                        if (value)
                        {
                            toggle.isOn = false;
                        }
                    });
                }
                // self.CurrentHeroCardItem.InitHeroCard();

                long account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
                Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
                C2M_GetHeroCardByIdRequest request = new C2M_GetHeroCardByIdRequest() { Account = account, HeroId = heroId };
                M2C_GetHeroCardByIdResponse response = (M2C_GetHeroCardByIdResponse) await session.Call(request);

                if (response.Error == ErrorCode.ERR_Success)
                {
                    self.CurrentHeroCardItem.InitHeroCard(response.HeroCardInfo);
                }
            }
        }

        public static async ETTask InitCurrentWeaponItem(this DlgWeaponInfoLayer self, WeaponInfo weaponInfo)
        {
            if (self.CurrentWeaponItem == null)
            {
                self.CurrentWeaponItem = self.AddChildWithId<Scroll_ItemWeapon>(IdGenerater.Instance.GenerateId());
                var obj = await AddressableComponent.Instance.LoadGameObjectAndInstantiateByPath("ItemWeapon");
                obj.transform.SetParent(self.View.E_CurrentWeaponPosImage.transform);
                self.CurrentWeaponItem.BindTrans(obj.transform);
                RectTransform rectTransform = obj.GetComponent<RectTransform>();
                rectTransform.offsetMin = Vector2.zero;
                rectTransform.offsetMax = Vector2.zero;
                rectTransform.anchorMin = Vector2.zero;
                rectTransform.anchorMax = Vector2.one;
                rectTransform.localScale = Vector3.one;
            }

            self.CurrentWeaponItem.InitWeaponCardView(weaponInfo);
            // self.CurrentHeroCardItem
            // self.CurrentWeaponItem.UnAableButtonClick();
            var toggle = self.CurrentWeaponItem.E_ChooseToggle.GetComponent<Toggle>();
            toggle.onValueChanged.RemoveAllListeners();
            toggle.onValueChanged.AddListener((value) =>
            {
                if (value)
                {
                    toggle.isOn = false;
                }
            });

            WeaponsConfig config = WeaponsConfigCategory.Instance.Get(weaponInfo.ConfigId);
            self.View.E_WeaponTypeText.text = config.WeaponTypeName;
            self.View.E_DesText.text = config.WeaponDes;
            self.View.E_WeaponNameText.text = config.Name;
            // self.CurerntWeaponItem.
        }

        public static async ETTask<List<WordBarInfo>> GetWeaponWordsInfoRequest(this DlgWeaponInfoLayer self)
        {
            long account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            C2M_GetWeaponWordBarsRequest request = new C2M_GetWeaponWordBarsRequest() { Account = account, WeaponId = self.WeaponInfo.WeaponId };
            M2C_GetWeaponWordBarsResponse response = (M2C_GetWeaponWordBarsResponse) await session.Call(request);

            if (response.Error == ErrorCode.ERR_Success)
            {
                // List<WordBarInfo> wordbars = new List<WordBarInfo>();
                // wordbars.Add(self.WeaponInfo);
                // foreach (var wordBar in response.WordBarInfos)
                // {
                // wordbars.Add(wordbars);
                // }
                List<WordBarInfo> infos = response.WordBarInfos;
                Log.Debug($"获取装备词条成功{infos.Count}");
                if (infos.Count == 0)
                {
                    Log.Debug($"weapon id {self.WeaponInfo.WeaponId}");
                }

                infos.Sort((a, b) =>
                {
                    if (b.IsMain)
                    {
                        return 1;
                    }

                    return -1;
                });

                //分别设置四个词条的信息
                // for (int i = 0; i < self.WordBarItems.Count; i++)
                // {
                //     ESCommonWordBar bar = self.WordBarItems[i];
                //     if (i < infos.Count)
                //     {
                //         bar.SetInfo(infos[i], self.WeaponInfo);
                //     }
                // }

                self.View.E_WeaponClearButton.interactable = infos.Count >= 2;
                return response.WordBarInfos;
            }

            return null;
        }
    }
}