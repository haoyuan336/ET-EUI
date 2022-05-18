using System.Collections;
using System.Collections.Generic;
using System;
using ET.Account;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgWeaponStrengthenLayerSystem
    {
        public static void HideWindow(this DlgWeaponStrengthenLayer self)
        {
            self.AlChooseWeaponInfos.Clear();
        }

        public static void RegisterUIEvent(this DlgWeaponStrengthenLayer self)
        {
            self.View.E_BackButton.onClick.AddListener(() =>
            {
                self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_WeaponStrengthenLayer);
            });
            // self.AddUIScrollItems(ref self.TargetItemWeapons, 10);
            self.View.E_TargetWeaponContentLoopVerticalScrollRect.AddItemRefreshListener(self.OnTargetLoopScrollview);
            self.View.E_BagContentLoopVerticalScrollRect.AddItemRefreshListener(self.OnWeaponsLoopScrollview);
        }

        public static async void RequestBagInfo(this DlgWeaponStrengthenLayer self)
        {
            M2C_GetAllWeaponsResponse response;
            var session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            var account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            response = (M2C_GetAllWeaponsResponse) await session.Call(new C2M_GetAllWeaponsRequest() { AccountId = account });
            if (response.Error == ErrorCode.ERR_Success)
            {
                self.WeaponInfos = response.WeaponInfos;

                self.AddUIScrollItems(ref self.ItemWeapons, self.WeaponInfos.Count);
                self.View.E_BagContentLoopVerticalScrollRect.SetVisible(true, self.WeaponInfos.Count);
            }
        }

        public static void OnTargetLoopScrollview(this DlgWeaponStrengthenLayer self, Transform tr, int index)
        {
            Scroll_ItemWeapon itemWeapon = self.TargetItemWeapons[index].BindTrans(tr);
            self.InitWeaponItem(self.TargetWeaponInfos[index], itemWeapon);
        }

        public static void OnWeaponsLoopScrollview(this DlgWeaponStrengthenLayer self, Transform tr, int index)
        {
            Scroll_ItemWeapon itemWeapon = self.ItemWeapons[index].BindTrans(tr);
            self.InitWeaponItem(self.WeaponInfos[index], itemWeapon);
            itemWeapon.E_ClickToggle.interactable = true;
            if (self.WeaponInfos[index].WeaponId.Equals(self.TargetWeaponInfos[0].WeaponId))
            {
                itemWeapon.E_ClickToggle.interactable = false;
            }

            itemWeapon.E_ClickToggle.onValueChanged.RemoveAllListeners();

            itemWeapon.E_ClickToggle.isOn = false;
            itemWeapon.E_ClickToggle.onValueChanged.AddListener((value) =>
            {
                WeaponInfo info = self.WeaponInfos[index];
                WeaponsConfig config = WeaponsConfigCategory.Instance.Get(info.ConfigId);
                if (config.MaterialType == (int)HeroBagType.Hero)
                {
                    if (value)
                    {
                        if (self.CheckIsFull())
                        {
                            itemWeapon.E_ClickToggle.isOn = false;
                        }
                        else
                        {
                            self.AlChooseWeaponInfos.Add(new WeaponInfo() { WeaponId = info.WeaponId, Count = 1, ConfigId = info.ConfigId });
                        }
                    }
                    else
                    {
                        self.AlChooseWeaponInfos.RemoveAll(a => a.WeaponId.Equals(info.WeaponId));
                    }
                }
                else
                {
                    if (value)
                    {
                        self.OnWeaponItemClick(info, itemWeapon);
                    }
                }

                var isFull = self.CheckIsFull();
                self.View.E_StrengthenButton.gameObject.SetActive(isFull);
            });
        }

        public static async void OnWeaponItemClick(this DlgWeaponStrengthenLayer self, WeaponInfo info, Scroll_ItemWeapon itemWeapon)
        {
            itemWeapon.E_ClickToggle.isOn = false;

            
            //如果是普通材料。那么需要选择个数
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_AddSubPlane, WindowID.WindowID_Invaild,
                new ShowWindowData() { contextData = itemWeapon });

            UIBaseWindow baseWindow = uiComponent.AllWindowsDic[(int) WindowID.WindowID_AddSubPlane];
            baseWindow.GetComponent<DlgAddSubPlane>().SetInfo(info);
            baseWindow.GetComponent<DlgAddSubPlane>().AddAction = () => { self.AddItemCount(info, itemWeapon); };
            baseWindow.GetComponent<DlgAddSubPlane>().SubAction = () => { self.SubItemCount(info, itemWeapon); };
            baseWindow.GetComponent<DlgAddSubPlane>().View.E_ContentImage.transform.position = itemWeapon.E_CountText.transform.position;
            self.UpdateAddSubPlaneButtonState();
        }

        public static void AddItemCount(this DlgWeaponStrengthenLayer self, WeaponInfo info, Scroll_ItemWeapon itemWeapon)
        {
            if (self.CheckIsFull())
            {
                return;
            }
            Log.Debug("加道具");
            //首先确定一下 ，已选择的数据里面包含不包含此道具
            WeaponInfo findInfo = self.AlChooseWeaponInfos.Find(a => a.WeaponId.Equals(info.WeaponId));
            if (findInfo == null)
            {
                findInfo = new WeaponInfo() { WeaponId = info.WeaponId, Count = 0 };
                self.AlChooseWeaponInfos.Add(findInfo);
            }

            findInfo.Count++;
            itemWeapon.E_CountText.gameObject.SetActive(true);
            itemWeapon.E_CountText.text = findInfo.Count.ToString();
            self.UpdateAddSubPlaneButtonState();
        }

        public static void UpdateAddSubPlaneButtonState(this DlgWeaponStrengthenLayer self)
        {
            var isFull = self.CheckIsFull();
            Log.Debug($"is full {isFull}");
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            UIBaseWindow baseWindow = uiComponent.AllWindowsDic[(int) WindowID.WindowID_AddSubPlane];
            baseWindow.GetComponent<DlgAddSubPlane>().SetFull(isFull);
            self.View.E_StrengthenButton.gameObject.SetActive(isFull);
        }

        public static void SubItemCount(this DlgWeaponStrengthenLayer self, WeaponInfo info, Scroll_ItemWeapon itemWeapon)
        {
            Log.Debug("减道具");
            var findInfo = self.AlChooseWeaponInfos.Find(a => a.WeaponId.Equals(info.WeaponId));
            if (findInfo != null && findInfo.Count > 0)
            {
                findInfo.Count--;
                itemWeapon.E_CountText.text = findInfo.Count.ToString();
                self.UpdateAddSubPlaneButtonState();
                if (findInfo.Count == 0)
                {
                    itemWeapon.E_CountText.gameObject.SetActive(false);
                    self.AlChooseWeaponInfos.Remove(findInfo);
                    self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_AddSubPlane);
                }
            }
        }

        public static async void InitWeaponItem(this DlgWeaponStrengthenLayer self, WeaponInfo info, Scroll_ItemWeapon weapon)
        {
            WeaponsConfig config = WeaponsConfigCategory.Instance.Get(info.ConfigId);
            Sprite sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(ConstValue.WeaponAtlasPath, config.IconResName);
            weapon.E_WeaponImage.sprite = sprite;
            weapon.E_LevelText.text = $"Lv:{info.Level}";
        }

        public static void ShowWindow(this DlgWeaponStrengthenLayer self, Entity contextData = null)
        {
            self.AlChooseWeaponInfos.Clear();
        }

        public static void SetInfo(this DlgWeaponStrengthenLayer self, WeaponInfo info)
        {
            self.TargetWeaponInfos = new List<WeaponInfo>() { info };
            Log.Debug($"set target weapon infos {self.TargetWeaponInfos.Count}");
            self.AddUIScrollItems(ref self.TargetItemWeapons, self.TargetWeaponInfos.Count);
            self.View.E_TargetWeaponContentLoopVerticalScrollRect.SetVisible(true, self.TargetWeaponInfos.Count);
            self.RequestBagInfo();
        }

        public static bool CheckIsFull(this DlgWeaponStrengthenLayer self)
        {
            var count = 0;
            foreach (var info in self.AlChooseWeaponInfos)
            {
                count += info.Count;
            }

            if (count == 5)
            {
                return true;
            }

            return false;
        }
    }
}