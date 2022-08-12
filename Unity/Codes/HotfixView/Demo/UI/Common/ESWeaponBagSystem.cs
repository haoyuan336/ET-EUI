using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class ESWeaponBagSystem
    {
        public static async void RegisterUIEvent(this ESWeaponBagCommon self)
        {
            var commonAtlas = ConstValue.CommonUIAtlasPath;
            var bgImage = ConstValue.FrameBgPath;
            self.DefaultWeaponBgSprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(commonAtlas, bgImage);
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
            // WeaponType[] types = new WeaponType[] { WeaponType.Weapon, WeaponType.Equip, WeaponType.Ring, WeaponType.Accessory, WeaponType.Invalide };

            toggle.onValueChanged.AddListener((value) =>
            {
                if (value)
                {
                    if (index < self.WeaponTypes.Length)
                    {
                        self.FiltType(self.WeaponTypes[index]);
                    }
                }
            });
        }

        public static async void FiltType(this ESWeaponBagCommon self, WeaponType type)
        {
            //首先请求背包格子的个数
            var count = await self.GetBagCountInfoRequest();
            //然后渲染

            self.AddUIScrollItems(ref self.ItemWeapons, count);
            self.E_WeaponLoopVerticalScrollRect.SetVisible(true, count);
            //
            self.InitWeaponInfo(type);
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
                self.E_BagCountTextText.text = $"{self.WeaponCount}/{self.BagCount}";
                Log.Debug($"weapin infos {self.WeaponInfos.Count}");
                // self.E_WeaponLoopVerticalScrollRect.RefreshCells();
            }
        }

        public static async void InitWeaponInfo(this ESWeaponBagCommon self, WeaponType type)
        {
            await self.GetWeaponInfoRequestAsync();
            //
            // WeaponType[] types = new[] { WeaponType.Weapon, WeaponType.Equip, WeaponType.Ring, WeaponType.Accessory };
            // if (index < types.Length)
            // {
            //     var type = types[index];
            self.WeaponInfos = self.WeaponInfos.FindAll(a =>
            {
                WeaponsConfig config = WeaponsConfigCategory.Instance.Get(a.ConfigId);
                if (config.WeaponType == (int) type || type == WeaponType.Invalide)
                {
                    return true;
                }

                return false;
            });
            // }s
            Log.Debug($"weapin info {self.WeaponInfos.Count}");

            self.E_WeaponLoopVerticalScrollRect.RefreshCells();
        }

        public static void OnLoopListItemRefreshHandler(this ESWeaponBagCommon self, Transform transform, int index)
        {
            Scroll_ItemWeapon itemWeapon = self.ItemWeapons[index].BindTrans(transform);
            itemWeapon.E_ChooseButton.onClick.RemoveAllListeners();
            // itemWeapon.E_ChooseToggle.isOn = false;
            itemWeapon.E_ChooseButton.interactable = true;

            if (index < self.WeaponInfos.Count)
            {
                WeaponInfo info = self.WeaponInfos[index];
                itemWeapon.InitWeaponCardView(info);
                itemWeapon.E_ChooseButton.gameObject.SetActive(true);
                itemWeapon.E_ChooseButton.onClick.RemoveAllListeners();

                // itemWeapon.E_ChooseToggle.isOn = false;
                if (self.AlChooseWeaponInfos != null && self.AlChooseWeaponInfos.Exists(a => a.WeaponId.Equals(info.WeaponId)))
                {
                    WeaponsConfig weaponsConfig = WeaponsConfigCategory.Instance.Get(info.ConfigId);
                    if (weaponsConfig.MaterialType == (int) WeaponBagType.Weapon)
                    {
                        // itemWeapon.E_ChooseToggle.isOn = true;
                    }
                    else if (weaponsConfig.MaterialType == (int) WeaponBagType.Materail)
                    {
                        WeaponInfo weaponInfo = self.AlChooseWeaponInfos.Find(a => a.WeaponId.Equals(info.WeaponId));
                        itemWeapon.E_ChooseCountText.text = weaponInfo.Count.ToString();
                        itemWeapon.E_ChooseCountText.gameObject.SetActive(weaponInfo.Count > 0);
                    }
                }

                itemWeapon.E_ChooseButton.onClick.AddListener(() =>
                {
                    // if (value)
                    // {
                    //     itemWeapon.E_ChooseToggle.isOn = false;
                    self.WeaponItemClickAction(info, itemWeapon);
                    // }
                });

                if (self.UnableWeaponInfo != null && info.WeaponId.Equals(self.UnableWeaponInfo.WeaponId))
                {
                    itemWeapon.E_ChooseButton.interactable = false;
                }

                if (self.EnableWeaponInfos != null && !self.EnableWeaponInfos.Exists(a => a.WeaponId.Equals(info.WeaponId)))
                {
                    itemWeapon.E_ChooseButton.interactable = false;
                }
            }
            else
            {
                itemWeapon.E_CountText.text = "";
                itemWeapon.E_ChooseCountText.text = "";
                itemWeapon.E_ChooseButton.gameObject.SetActive(false);
                itemWeapon.E_ChooseButton.onClick.RemoveAllListeners();
                itemWeapon.E_QualityIconImage.gameObject.SetActive(false);
                itemWeapon.E_LevelText.gameObject.SetActive(false);
                // var commonAtlas = ConstValue.CommonUIAtlasPath;
                // var sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(commonAtlas, "bgpic");
                itemWeapon.E_WeaponImage.sprite = self.DefaultWeaponBgSprite;
            }
        }

        public static async ETTask<int> GetBagCountInfoRequest(this ESWeaponBagCommon self)
        {
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            long account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            C2M_GetAllItemRequest request = new C2M_GetAllItemRequest();
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

        public static void ShowWindowWidthLockType(this ESWeaponBagCommon self, WeaponType type)
        {
            var index = 0;
            for (int i = 0; i < self.WeaponTypes.Length; i++)
            {
                if (self.WeaponTypes[i] == type)
                {
                    index = i;
                    break;
                }
            }

            List<Toggle> toggleButtons = new List<Toggle>()
            {
                self.E_WeaponToggle,
                self.E_SuitToggle,
                self.E_RingToggle,
                self.E_ShiPinToggle,
                self.E_AllToggle
            };

            foreach (var toggle in toggleButtons)
            {
                toggle.interactable = true;
            }

            toggleButtons[index].isOn = true;
            self.FiltType(type);
            for (int i = 0; i < toggleButtons.Count; i++)
            {
                if (i != index)
                {
                    toggleButtons[i].interactable = false;
                }
            }
        }

        public static void ShowWindow(this ESWeaponBagCommon self, Entity contextData = null)
        {
            List<Toggle> toggleButtons = new List<Toggle>()
            {
                self.E_WeaponToggle,
                self.E_SuitToggle,
                self.E_RingToggle,
                self.E_ShiPinToggle,
                self.E_AllToggle
            };
            // var index = self.WeaponTypes
            foreach (var toggle in toggleButtons)
            {
                toggle.interactable = true;
            }

            // //todo 首先取出来，玩家拥有的所有装备
            self.FiltType(WeaponType.Invalide);
            self.E_AllToggle.isOn = true;
        }

        public static void SetUnAbleWeaponInfo(this ESWeaponBagCommon self, WeaponInfo weaponInfo)
        {
            self.UnableWeaponInfo = weaponInfo;
            self.E_WeaponLoopVerticalScrollRect.RefillCells();
        }

        public static void SetAlChooseWeaponInfos(this ESWeaponBagCommon self, List<WeaponInfo> weaponInfos)
        {
            self.AlChooseWeaponInfos = weaponInfos;
            self.E_WeaponLoopVerticalScrollRect.RefillCells();
        }

        public static void SetEnableWeaponInfos(this ESWeaponBagCommon self, List<WeaponInfo> weaponInfos)
        {
            self.EnableWeaponInfos = weaponInfos;
            self.E_WeaponLoopVerticalScrollRect.RefillCells();
        }
    }
}