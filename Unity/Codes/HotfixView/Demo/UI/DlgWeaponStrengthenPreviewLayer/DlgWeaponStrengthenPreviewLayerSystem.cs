using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Vector3 = System.Numerics.Vector3;

namespace ET
{
    public static class DlgWeaponStrengthenPreviewLayerSystem
    {
        public static void RegisterUIEvent(this DlgWeaponStrengthenPreviewLayer self)
        {
            self.View.E_BackButton.AddListener(self.BackButtonClick);
            self.View.E_OkButtonButton.interactable = false;
            self.View.E_QuickChooseButton.AddListenerAsync(self.AutoChooseWeaponItem);
            self.View.E_OkButtonButton.AddListenerAsync(self.OnOkButtonClick);
        }

        public static async ETTask OnOkButtonClick(this DlgWeaponStrengthenPreviewLayer self)
        {
            long account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            C2M_StrengthenWeaponRequest request = new C2M_StrengthenWeaponRequest()
            {
                AccountId = account, TargetWeaponInfo = self.CurrentWeaponInfo, ChooseWeaponInfos = self.AlChooseWeaponInfos
            };
            M2C_StrengthenWeaponResponse response = (M2C_StrengthenWeaponResponse) await session.Call(request);

            if (response.Error == ErrorCode.ERR_Success)
            {
                UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
                UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_BagLayer);
                baseWindow.GetComponent<DlgBagLayer>().ReferBagInfo();
                // self.AlChooseWeaponInfos.Clear();
                // self.SetAlChooseWeaponItem();
                // self.WeaponBagCommon.SetEnableWeaponInfos(null);
                self.SetTargetInfo(response.WeaponInfo);

                UIBaseWindow weaponInfoLayer = uiComponent.GetUIBaseWindow(WindowID.WindowID_WeaponInfoLayer);
                weaponInfoLayer.GetComponent<DlgWeaponInfoLayer>().SetTargetInfo(response.WeaponInfo);
            }

            await ETTask.CompletedTask;
        }

        /// <summary>
        /// 初始化五个材料位置
        /// </summary>
        /// <param name="self"></param>
        public static async ETTask InitWeaponItems(this DlgWeaponStrengthenPreviewLayer self)
        {
            if (self.ItemWeapons.Count == 0)
            {
                var prefab = await AddressableComponent.Instance.LoadAssetByPathAsync<GameObject>("ItemWeapon");
                for (int i = 0; i < 5; i++)
                {
                    var item = self.AddChildWithId<Scroll_ItemWeapon>(IdGenerater.Instance.GenerateId());
                    self.ItemWeapons.Add(item);
                    var obj = GameObject.Instantiate(prefab);
                    obj.transform.SetParent(self.View.E_WeaponItemGroupImage.transform);
                    RectTransform rectTransform = obj.GetComponent<RectTransform>();
                    rectTransform.localScale = UnityEngine.Vector2.one;
                    item.uiTransform = obj.transform;
                    self.RegisterWeaponItemClick(item);
                }
            }

            await ETTask.CompletedTask;
        }

        public static async void OnWeaponItemClick(this DlgWeaponStrengthenPreviewLayer self)
        {
            if (self.WeaponBagCommon == null)
            {
                GameObject obj =
                        await AddressableComponent.Instance.LoadGameObjectAndInstantiateByPath("Assets/Bundles/UI/Common/ESWeaponBagCommon.prefab");
                obj.transform.SetParent(self.View.uiTransform);
                RectTransform rectTransform = obj.GetComponent<RectTransform>();
                rectTransform.offsetMin = Vector2.zero;
                rectTransform.offsetMax = new Vector2(0, -260);
                rectTransform.anchorMin = Vector2.zero;
                rectTransform.anchorMax = Vector2.one;
                rectTransform.localScale = Vector2.one;
                self.WeaponBagCommon = self.AddChildWithId<ESWeaponBagCommon, Transform>(IdGenerater.Instance.GenerateId(), obj.transform);
                self.WeaponBagCommon.WeaponItemClickAction = null;
                self.WeaponBagCommon.WeaponItemClickAction = self.OnBagWeaponItemClick;
                self.WeaponBagCommon.RegisterUIEvent();
                self.WeaponBagCommon.ShowWindow();
                self.WeaponBagCommon.SetUnAbleWeaponInfo(self.CurrentWeaponInfo);
                self.WeaponBagCommon.SetAlChooseWeaponInfos(self.AlChooseWeaponInfos);

                if (self.CheckIsChooseFull())
                {
                    self.WeaponBagCommon.SetEnableWeaponInfos(self.AlChooseWeaponInfos);
                }
                // self.WeaponBagCommon.
            }
        }

        public static void DestroyESWeaponBagCommon(this DlgWeaponStrengthenPreviewLayer self)
        {
            GameObject.Destroy(self.WeaponBagCommon.uiTransform.gameObject);
            self.WeaponBagCommon.Dispose();
            self.WeaponBagCommon = null;
        }

        public static async void ShowAddSubPlane(this DlgWeaponStrengthenPreviewLayer self, Scroll_ItemWeapon itemWeapon, WeaponInfo weaponInfo)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_AddSubPlane);
            UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_AddSubPlane);
            DlgAddSubPlane dlgAddSubPlane = baseWindow.GetComponent<DlgAddSubPlane>();

            dlgAddSubPlane.View.E_ContentImage.transform.position = itemWeapon.E_ChooseToggle.transform.position;

            var targetInfo = self.AlChooseWeaponInfos.Find(a => a.WeaponId.Equals(weaponInfo.WeaponId));
            if (targetInfo != null)
            {
                self.UnableAddSubPlaneAddButton(targetInfo.Count != weaponInfo.Count);
                self.UnableAddSubPlaneSubButton(targetInfo.Count != 0);
            }

            if (targetInfo == null)
            {
                self.UnableAddSubPlaneSubButton(false);
            }

            dlgAddSubPlane.AddAction = () =>
            {
                var findInfo = self.AlChooseWeaponInfos.Find(a => a.WeaponId.Equals(weaponInfo.WeaponId));
                if (findInfo == null)
                {
                    findInfo = new WeaponInfo()
                    {
                        WeaponId = weaponInfo.WeaponId,
                        ConfigId = weaponInfo.ConfigId,
                        Level = weaponInfo.Level,
                        OnWeaponHeroId = weaponInfo.OnWeaponHeroId,
                        Count = 1
                    };
                    self.AlChooseWeaponInfos.Add(findInfo);
                }
                else
                {
                    if (findInfo.Count < weaponInfo.Count)
                    {
                        findInfo.Count++;
                    }
                }

                if (findInfo.Count == weaponInfo.Count)
                {
                    self.UnableAddSubPlaneAddButton(false);
                }

                self.UnableAddSubPlaneSubButton(true);
                self.WeaponBagCommon.SetAlChooseWeaponInfos(self.AlChooseWeaponInfos);
                self.SetAlChooseWeaponItem();
                var checkIsFull = self.CheckIsChooseFull();
                if (checkIsFull)
                {
                    self.WeaponBagCommon.SetEnableWeaponInfos(self.AlChooseWeaponInfos);
                }
            };
            dlgAddSubPlane.SubAction = () =>
            {
                var findInfo = self.AlChooseWeaponInfos.Find(a => a.WeaponId.Equals(weaponInfo.WeaponId));
                if (findInfo != null && findInfo.Count > 0)
                {
                    findInfo.Count--;
                    if (findInfo.Count == 0)
                    {
                        self.UnableAddSubPlaneSubButton(false);
                        self.AlChooseWeaponInfos.Remove(findInfo);
                    }

                    self.UnableAddSubPlaneAddButton(true);
                }

                self.WeaponBagCommon.SetAlChooseWeaponInfos(self.AlChooseWeaponInfos);
                self.SetAlChooseWeaponItem();
                self.WeaponBagCommon.SetEnableWeaponInfos(null);
            };
        }

        public static void UnableAddSubPlaneAddButton(this DlgWeaponStrengthenPreviewLayer self, bool value)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_AddSubPlane);
            baseWindow.GetComponent<DlgAddSubPlane>().EnableAddButton(value);
        }

        public static void UnableAddSubPlaneSubButton(this DlgWeaponStrengthenPreviewLayer self, bool value)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_AddSubPlane);
            baseWindow.GetComponent<DlgAddSubPlane>().EnabelSubButton(value);
        }

        public static void OnBagWeaponItemClick(this DlgWeaponStrengthenPreviewLayer self, WeaponInfo weaponInfo, Scroll_ItemWeapon itemWeapon,
        bool value)
        {
            WeaponsConfig config = WeaponsConfigCategory.Instance.Get(weaponInfo.ConfigId);
            if (config.MaterialType == (int) WeaponBagType.Materail)
            {
                if (value)
                {
                    itemWeapon.E_ChooseToggle.isOn = false;
                    self.ShowAddSubPlane(itemWeapon, weaponInfo);
                }
            }
            else
            {
                if (value)
                {
                    var isFull = self.CheckIsChooseFull();
                    if (!isFull)
                    {
                        // self.AlChooseWeaponInfos.Add(weaponInfo);
                        self.AlChooseWeaponInfos.Add(new WeaponInfo()
                        {
                            WeaponId = weaponInfo.WeaponId,
                            ConfigId = weaponInfo.ConfigId,
                            Count = 1,
                            Level = weaponInfo.Level,
                            OnWeaponHeroId = weaponInfo.OnWeaponHeroId
                        });
                    }

                    // self.DestroyESWeaponBagCommon();
                }
                else
                {
                    self.AlChooseWeaponInfos.RemoveAll(a => a.WeaponId.Equals(weaponInfo.WeaponId));
                }

                if (self.CheckIsChooseFull())
                {
                    self.WeaponBagCommon.SetEnableWeaponInfos(self.AlChooseWeaponInfos);
                }
                else
                {
                    self.WeaponBagCommon.SetEnableWeaponInfos(null);
                }

                self.WeaponBagCommon.SetAlChooseWeaponInfos(self.AlChooseWeaponInfos);
                self.SetAlChooseWeaponItem();
            }

            //判断选择的武器节点是武器还是材料
        }

        public static void SetAlChooseWeaponItem(this DlgWeaponStrengthenPreviewLayer self)
        {
            // Log.Debug("");
            //讲所选内容展开
            List<WeaponInfo> weaponInfos = new List<WeaponInfo>();
            Log.Debug($"self allchoose weapon infos {self.AlChooseWeaponInfos.Count}");
            foreach (var weaponInfo in self.AlChooseWeaponInfos)
            {
                var count = weaponInfo.Count;
                Log.Debug($"count {count}");
                for (int i = 0; i < count; i++)
                {
                    weaponInfos.Add(new WeaponInfo()
                    {
                        WeaponId = weaponInfo.WeaponId,
                        ConfigId = weaponInfo.ConfigId,
                        Count = 1,
                        Level = weaponInfo.Level,
                        OnWeaponHeroId = weaponInfo.OnWeaponHeroId
                    });
                }
            }

            Log.Debug($"item weapon count {self.ItemWeapons.Count}");
            Log.Debug($"weapon infos {weaponInfos.Count}");
            for (int i = 0; i < self.ItemWeapons.Count; i++)
            {
                var item = self.ItemWeapons[i];
                if (i < weaponInfos.Count)
                {
                    var info = weaponInfos[i];
                    item.InitWeaponCardView(info);
                }
                else
                {
                    item.ShowAddButtonState();
                }
            }

            self.View.E_OkButtonButton.interactable = self.AlChooseWeaponInfos.Count != 0;

            self.UpdateAddExpInfoView();
        }

        public static void UpdateAddExpInfoView(this DlgWeaponStrengthenPreviewLayer self)
        {
            var totalExp = 0;
            foreach (var weaponInfo in self.AlChooseWeaponInfos)
            {
                totalExp += WeaponHelper.GetTotalExp(weaponInfo);
                totalExp += weaponInfo.CurrentExp;
            }

            self.View.E_AddExpInfoText.text = $"+{totalExp}EXP";

            var lastExp = WeaponHelper.GetUpdateLevelLastExp(self.CurrentWeaponInfo, totalExp);
            Log.Debug($"last exp {lastExp}");

            var endLevel = WeaponHelper.GetEndLevelWithExp(self.CurrentWeaponInfo, totalExp);
            self.View.E_AddLevelText.text = $"+{endLevel - self.CurrentWeaponInfo.Level}";
        }

        public static bool CheckIsChooseFull(this DlgWeaponStrengthenPreviewLayer self)
        {
            var totalCount = 0;
            foreach (var weaponInfo in self.AlChooseWeaponInfos)
            {
                totalCount += weaponInfo.Count;
            }

            Log.Debug($"total count {totalCount}");
            return totalCount == 5;
        }

        public static void RegisterWeaponItemClick(this DlgWeaponStrengthenPreviewLayer self, Scroll_ItemWeapon itemWeapon)
        {
            itemWeapon.E_AddTextText.gameObject.SetActive(true);
            var toggle = itemWeapon.E_ChooseToggle.GetComponent<Toggle>();
            toggle.onValueChanged.RemoveAllListeners();
            toggle.onValueChanged.AddListener((value) =>
            {
                if (value)
                {
                    toggle.isOn = false;
                    self.OnWeaponItemClick();
                }
            });
        }

        public static void BackButtonClick(this DlgWeaponStrengthenPreviewLayer self)
        {
            if (self.WeaponBagCommon != null)
            {
                self.DestroyESWeaponBagCommon();
            }
            else
            {
                self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_WeaponStrengthenPreviewLayer);
            }
        }

        public static void ShowWindow(this DlgWeaponStrengthenPreviewLayer self, Entity contextData = null)
        {
        }

        public static async void SetTargetInfo(this DlgWeaponStrengthenPreviewLayer self, WeaponInfo targetInfo)
        {
            self.AlChooseWeaponInfos.Clear();
            self.CurrentWeaponInfo = targetInfo;
            self.InitTargetWeaponInfoView();
            await self.InitWeaponItems();
            await self.InitWordBarItems();
            self.SetAlChooseWeaponItem();
        }

        public static void InitTargetWeaponInfoView(this DlgWeaponStrengthenPreviewLayer self)
        {
            List<WeaponLevelExpConfig> configs = WeaponLevelExpConfigCategory.Instance.GetAll().Values.ToList();
            var endLevel = configs.Last().Id;
            self.View.E_CurrentLevelText.text = $"{self.CurrentWeaponInfo.Level}/{endLevel}";
            var needExp = WeaponHelper.GetUpdateLevelNeedExp(self.CurrentWeaponInfo);
            // self.View.E_ExpBarImage
            self.View.E_ExpInfoText.text = $"{self.CurrentWeaponInfo.CurrentExp}/{needExp}";

            self.View.E_ExpBarImage.fillAmount = (float) self.CurrentWeaponInfo.CurrentExp / needExp;
            self.View.E_AddExpInfoText.text = "+0EXP";
            self.View.E_AddLevelText.text = "+0";
        }

        public static async ETTask AutoChooseWeaponItem(this DlgWeaponStrengthenPreviewLayer self)
        {
            //首先取出玩家的所有英雄
            var accountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            var session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            C2M_GetAllWeaponsRequest request = new C2M_GetAllWeaponsRequest() { AccountId = accountId };
            M2C_GetAllWeaponsResponse response = (M2C_GetAllWeaponsResponse) await session.Call(request);
            if (response.Error == ErrorCode.ERR_Success)
            {
                List<WeaponInfo> weaponInfos = response.WeaponInfos;
                //去掉当前的英雄
                weaponInfos.RemoveAll(a => a.WeaponId.Equals(self.CurrentWeaponInfo.WeaponId));
            
                // weaponInfos.Sort((a, b) =>
                // {
                //     var configB = WeaponsConfigCategory.Instance.Get(b.ConfigId);
                //     if (configB.MaterialType == (int) WeaponBagType.Materail)
                //     {
                //         return 1;
                //     }
                //
                //     return -1;
                // });
                weaponInfos.Sort((a, b) =>
                {
                    var configA = WeaponsConfigCategory.Instance.Get(a.ConfigId);
                    var configB = WeaponsConfigCategory.Instance.Get(b.ConfigId);
                    return configA.Star - configB.Star;
                });
                weaponInfos.Sort((a, b) => { return a.Level - b.Level; });


                var count = 0;
                var index = 0;
                self.AlChooseWeaponInfos.Clear();
                while (index < 5 && count < 5)
                {
                    if (index < weaponInfos.Count)
                    {
                        var weaponInfo = weaponInfos[index];
                        var endCount = count + weaponInfo.Count;
                        var add = 1;
                        if (endCount > 5)
                        {
                            add = 5 - count;
                        }
                        else
                        {
                            add = weaponInfo.Count;
                        }

                        count = endCount;

                        self.AlChooseWeaponInfos.Add(new WeaponInfo()
                        {
                            WeaponId = weaponInfo.WeaponId,
                            Level = weaponInfo.Level,
                            ConfigId = weaponInfo.ConfigId,
                            Count = add,
                            OnWeaponHeroId = weaponInfo.OnWeaponHeroId
                        });
                    }

                    index++;
                }

                self.SetAlChooseWeaponItem();
            }
        }

        public static void HideWindow(this DlgWeaponStrengthenPreviewLayer self)
        {
            Log.Debug("hide window dlgweapon strengthen");
            self.AlChooseWeaponInfos.Clear();
            foreach (var itemWeapon in self.ItemWeapons)
            {
                GameObject.Destroy(itemWeapon.uiTransform.gameObject);
                itemWeapon.Dispose();
            }

            self.ItemWeapons.Clear();

            foreach (var wordBar in self.CommonWordBars)
            {
                GameObject.Destroy(wordBar.uiTransform.gameObject);
                wordBar.Dispose();
            }

            self.CommonWordBars.Clear();
        }

        public static async ETTask InitWordBarItems(this DlgWeaponStrengthenPreviewLayer self)
        {
            //

            if (self.CommonWordBars.Count == 0)
            {
                //根据装备的星级，
                WeaponsConfig config = WeaponsConfigCategory.Instance.Get(self.CurrentWeaponInfo.ConfigId);
                var wordCount = config.WordBarCount;
                for (int i = 0; i < wordCount + 1; i++)
                {
                    var bar = await AddressableComponent.Instance.LoadGameObjectAndInstantiateByPath(
                        "Assets/Bundles/UI/Common/ESCommonWordBar.prefab");
                    bar.transform.SetParent(self.View.E_WordBarGroupImage.transform);
                    bar.GetComponent<RectTransform>().localScale = UnityEngine.Vector3.one;
                    ESCommonWordBar wordBar = self.AddChildWithId<ESCommonWordBar, Transform>(IdGenerater.Instance.GenerateId(), bar.transform);
                    self.CommonWordBars.Add(wordBar);
                }
            }

            long account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            // 请求词条数据
            C2M_GetWeaponWordBarsRequest request = new C2M_GetWeaponWordBarsRequest()
            {
                Account = account, WeaponId = self.CurrentWeaponInfo.WeaponId
            };
            M2C_GetWeaponWordBarsResponse response = (M2C_GetWeaponWordBarsResponse) await session.Call(request);

            List<WordBarInfo> wordBarInfos = response.WordBarInfos;

            wordBarInfos.Sort((a, b) =>
            {
                if (b.IsMain)
                {
                    return 1;
                }

                return -1;
            });
            for (int i = 0; i < self.CommonWordBars.Count; i++)
            {
                if (i < wordBarInfos.Count)
                {
                    WordBarInfo info = wordBarInfos[i];
                    self.CommonWordBars[i].SetInfo(info, self.CurrentWeaponInfo);
                }
            }

            await ETTask.CompletedTask;
        }
    }
}