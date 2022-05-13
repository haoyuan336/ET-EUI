using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.CompilerServices;
using ET.Account;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace ET
{
    public static class DlgBagLayerSystem
    {
        public static void RegisterUIEvent(this DlgBagLayer self)
        {
            self.View.E_WeaponLoopVerticalScrollRect.AddItemRefreshListener(self.OnLoopListItemRefreshHandler);
        }

        public static async void OnLoopListItemRefreshHandler(this DlgBagLayer self, Transform transform, int index)
        {
            Scroll_ItemWeapon itemWeapon = self.ItemWeapons[index].BindTrans(transform);
            WeaponInfo weaponInfo = self.WeaponInfos[index];
            var config = WeaponsConfigCategory.Instance.Get(weaponInfo.ConfigId);

            itemWeapon.E_ClickToggle.onValueChanged.RemoveAllListeners();
            itemWeapon.E_ClickToggle.isOn = false;
            itemWeapon.E_ClickToggle.onValueChanged.AddListener((value) => { self.OnItemWeaponClick(weaponInfo); });
            Sprite sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(ConstValue.WeaponAtlasPath, config.IconResName);
            itemWeapon.E_WeaponImage.sprite = sprite;
            itemWeapon.E_LevelText.text = $"Lv.{weaponInfo.Level}";
        }

        public static async void OnItemWeaponClick(this DlgBagLayer self, WeaponInfo weaponInfo)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_WeaponStrengthenLayer);
            UIBaseWindow baseWindow = uiComponent.AllWindowsDic[(int) WindowID.WindowID_WeaponStrengthenLayer];
            DlgWeaponStrengthenLayer strengthenLayer = baseWindow.GetComponent<DlgWeaponStrengthenLayer>();
            strengthenLayer.SetInfo(weaponInfo);
            await ETTask.CompletedTask;
        }

        public static async void ShowWindow(this DlgBagLayer self, Entity contextData = null)
        {
            //todo 首先取出来，玩家拥有的所有装备
            long Account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            M2C_GetAllWeaponsResponse m2CGetAllWeaponsResponse =
                    (M2C_GetAllWeaponsResponse) await session.Call(new C2M_GetAllWeaponsRequest() { AccountId = Account });
            if (m2CGetAllWeaponsResponse.Error == ErrorCode.ERR_Success)
            {
                Log.Debug($"get all weapon success {m2CGetAllWeaponsResponse.WeaponInfos.Count}");
                self.WeaponInfos = m2CGetAllWeaponsResponse.WeaponInfos;
                self.AddUIScrollItems(ref self.ItemWeapons, self.WeaponInfos.Count);
                self.View.E_WeaponLoopVerticalScrollRect.SetVisible(true, self.WeaponInfos.Count);
            }

            await ETTask.CompletedTask;
        }
    }
}