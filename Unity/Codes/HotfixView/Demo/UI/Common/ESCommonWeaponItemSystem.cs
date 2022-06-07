using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public class ESCommonWeaponItemAwakeSystem: AwakeSystem<ESCommonWeaponItem, WeaponType, Transform>
    {
        public override  void Awake(ESCommonWeaponItem self, WeaponType a, Transform b)
        {
            self.CurrentType = a;
            // GameObject gameObject = await AddressableComponent.Instance.LoadGameObjectAndInstantiateByPath("ItemWeapon");
            self.GameObject = b.transform.gameObject;
            // gameObject.transform.SetParent(b);
            // gameObject.GetComponent<RectTransform>()
            // gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
            // string[] typeNames = { "武器", "护甲", "戒指", "饰品" };
            Dictionary<WeaponType, string> typeNames = new Dictionary<WeaponType, string>();
            typeNames.Add(WeaponType.Equip, "护甲");
            typeNames.Add(WeaponType.Accessory, "饰品");
            typeNames.Add(WeaponType.Ring, "戒指");
            typeNames.Add(WeaponType.Weapon, "武器");

            self.E_AddText = UIFindHelper.FindDeepChild(b.gameObject, "E_AddText").gameObject;
            self.E_WeaponType = UIFindHelper.FindDeepChild(b.gameObject, "E_WeaponType").gameObject;
            self.E_Level = UIFindHelper.FindDeepChild(b.gameObject, "E_Level").gameObject;
            self.E_QualityIcon = UIFindHelper.FindDeepChild(b.gameObject, "E_QualityIcon").gameObject;
            self.E_Choose = UIFindHelper.FindDeepChild(b.gameObject, "E_Choose").gameObject;
            self.E_Weapon = UIFindHelper.FindDeepChild(b.gameObject, "E_Weapon").gameObject;

            self.E_Level.SetActive(false);
            self.E_AddText.SetActive(true);
            self.E_WeaponType.SetActive(true);
            self.E_QualityIcon.SetActive(false);
            self.E_WeaponType.GetComponent<Text>().text = typeNames[a];

            if (self.WeaponInfo != null)
            {
                self.ReferInfo();
            }

            self.Awake();
        }
    }

    public static class ESCommonWeaponItemSystem
    {
        public static void Awake(this ESCommonWeaponItem self)
        {
            self.E_Choose.GetComponent<Toggle>().onValueChanged.RemoveAllListeners();
            self.E_Choose.GetComponent<Toggle>().onValueChanged.AddListener(self.OnWeaponItemClick);
        }

        public static void OnWeaponItemClick(this ESCommonWeaponItem self, bool value)
        {
            // self.E_Choose.GetComponent<Toggle>().isOn = false;
            if (self.OnWeaponItemClickAction != null)
            {
                self.OnWeaponItemClickAction(self.CurrentType, self, value);
            }
        }

        public static async void ReferInfo(this ESCommonWeaponItem self)
        {
            if (self.GameObject == null)
            {
                Log.Debug("装备还未初始化");
                return;
            }

            WeaponsConfig config = WeaponsConfigCategory.Instance.Get(self.WeaponInfo.ConfigId);
            Sprite sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(ConstValue.WeaponAtlasPath, config.IconResName);
            self.E_Weapon.GetComponent<Image>().sprite = sprite;
            self.E_WeaponType.SetActive(false);
            self.E_AddText.SetActive(false);
            self.E_Level.gameObject.SetActive(true);
            self.E_Level.GetComponent<Text>().text = $"Lv.{self.WeaponInfo.Level}";
        }

        public static async void SetWeaponInfo(this ESCommonWeaponItem self, WeaponInfo weaponInfo)
        {
            if (weaponInfo == null)
            {
                self.E_AddText.SetActive(true);
                var sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(ConstValue.CommonUIAtlasPath, "bgpic");
                self.E_Weapon.GetComponent<Image>().sprite = sprite;
                        
                self.E_Level.gameObject.SetActive(false);
                return;
            }
            Log.Debug("设置装备信息");
            self.WeaponInfo = weaponInfo;
            self.ReferInfo();
        }
    }
}