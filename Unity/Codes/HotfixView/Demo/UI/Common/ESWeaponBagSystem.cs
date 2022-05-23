using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using ET.Account;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class ESWeaponBagSystem
    {
        public static void RegisterUIEvent(this ESWeaponBagCommon self)
        {
            self.E_WeaponLoopVerticalScrollRect.AddItemRefreshListener(self.OnLoopListItemRefreshHandler);

            List<Toggle> toggleButtons = new List<Toggle>()
            {
                self.E_WeaponToggle,
                self.E_SuitToggle,
                self.E_RingToggle,
                self.E_ShiPinToggle,
                self.E_AllToggle
            };
            for (var i = 0; i < toggleButtons.Count; i++)
            {
                self.InitToggleClickEvent(toggleButtons[i], i);
            }
        }

        public static void InitToggleClickEvent(this ESWeaponBagCommon self, Toggle toggle, int index)
        {
            toggle.onValueChanged.AddListener((value) =>
            {
                if (value)
                {
                    self.FiltType(index);
                }
            });
        }

        public static async void FiltType(this ESWeaponBagCommon self, int index)
        {
            //首先请求背包格子的个数
            var count = await self.GetBagCountInfoRequest();
            //然后渲染
            self.AddUIScrollItems(ref self.ItemWeapons, count);
            self.E_WeaponLoopVerticalScrollRect.SetVisible(true, count);

            self.InitWeaponInfo(index);
            //show
        }

        public static async ETTask GetWeaponInfoRequestAsync(this ESWeaponBagCommon self)
        {
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            long account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;

            //首先获取玩家的全部道具
            C2M_GetAllWeaponsRequest request = new C2M_GetAllWeaponsRequest() { AccountId = account };
            M2C_GetAllWeaponsResponse response = (M2C_GetAllWeaponsResponse) await session.Call(request);
            if (response.Error == ErrorCode.ERR_Success)
            {
                self.WeaponInfos = response.WeaponInfos;
                self.WeaponCount = self.WeaponInfos.Count;
                self.E_BagCountText.text = $"{self.WeaponCount}/{self.BagCount}";
                Log.Debug($"weapin infos {self.WeaponInfos.Count}");
                // self.E_WeaponLoopVerticalScrollRect.RefreshCells();
            }
        }

        public static async void InitWeaponInfo(this ESWeaponBagCommon self, int index)
        {
            await self.GetWeaponInfoRequestAsync();

            WeaponType[] types = new[] { WeaponType.Weapon, WeaponType.Equip, WeaponType.Ring, WeaponType.Accessory };
            if (index < types.Length)
            {
                var type = types[index];
                self.WeaponInfos = self.WeaponInfos.FindAll(a =>
                {
                    WeaponsConfig config = WeaponsConfigCategory.Instance.Get(a.ConfigId);
                    if (config.WeaponType == (int) type)
                    {
                        return true;
                    }

                    return false;
                });
            }

            self.E_WeaponLoopVerticalScrollRect.RefreshCells();
        }

        public static async void OnLoopListItemRefreshHandler(this ESWeaponBagCommon self, Transform transform, int index)
        {
            Scroll_ItemWeapon itemWeapon = self.ItemWeapons[index].BindTrans(transform);
            itemWeapon.E_ChooseToggle.onValueChanged.RemoveAllListeners();
            itemWeapon.E_ChooseToggle.isOn = false;
            if (index < self.WeaponInfos.Count)
            {
                WeaponInfo info = self.WeaponInfos[index];
                itemWeapon.InitWeaponCardView(info);
                itemWeapon.E_ChooseToggle.gameObject.SetActive(true);
                itemWeapon.E_ChooseToggle.onValueChanged.AddListener((value) =>
                {
                    if (value)
                    {
                        itemWeapon.E_ChooseToggle.isOn = false;
                        self.WeaponItemClickAction(info);
                    }
                });
            }
            else
            {
                itemWeapon.E_ChooseCountText.gameObject.SetActive(false);
                itemWeapon.E_ChooseToggle.gameObject.SetActive(false);
                itemWeapon.E_ChooseToggle.onValueChanged.RemoveAllListeners();
                itemWeapon.E_QualityIconImage.gameObject.SetActive(false);
                itemWeapon.E_LevelText.gameObject.SetActive(false);
                var commonAtlas = ConstValue.CommonUIAtlasPath;
                var sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(commonAtlas, "bgpic");
                itemWeapon.E_WeaponImage.sprite = sprite;
            }

            // if (index < self.WeaponInfos.Count)
            // {
            //     WeaponInfo weaponInfo = self.WeaponInfos[index];
            //     var config = WeaponsConfigCategory.Instance.Get(weaponInfo.ConfigId);
            //
            //     itemWeapon.E_ChooseToggle.onValueChanged.AddListener((value) =>
            //     {
            //         if (value)
            //         {
            //             self.OnItemWeaponClick(weaponInfo);
            //         }
            //
            //         itemWeapon.E_ChooseToggle.isOn = false;
            //     });
            //     Sprite sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(ConstValue.WeaponAtlasPath, config.IconResName);
            //     itemWeapon.E_HeadImage.sprite = sprite;
            //     itemWeapon.E_LevelText.text = $"Lv.{weaponInfo.Level}";
            // }
        }

        public static void OnItemWeaponClick(this ESWeaponBagCommon self, WeaponInfo weaponInfo)
        {
            if (self.WeaponItemClickAction != null)
            {
                self.WeaponItemClickAction(weaponInfo);
            }
        }

        public static async ETTask<int> GetBagCountInfoRequest(this ESWeaponBagCommon self)
        {
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            long account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            C2M_GetAllItemRequest request = new C2M_GetAllItemRequest() { AccountId = account };
            M2C_GetAllItemResponse response = (M2C_GetAllItemResponse) await session.Call(request);
            if (response.Error == ErrorCode.ERR_Success)
            {
                //todo 1005 为基础背包值 1007 为扩展包
                List<ItemInfo> infos = response.ItemInfos.FindAll(a => a.ConfigId.Equals(1005) || a.ConfigId.Equals(1007));
                var count = 0;
                foreach (var info in infos)
                {
                    var config = ItemConfigCategory.Instance.Get(info.ConfigId);
                    count += config.DefaultValue;
                    
                }

                self.BagCount = count; 
                return count;
            }

            return 0;
        }

        public static void ShowWindow(this ESWeaponBagCommon self, Entity contextData = null)
        {
            // //todo 首先取出来，玩家拥有的所有装备
            self.FiltType(4);
        }
    }
}