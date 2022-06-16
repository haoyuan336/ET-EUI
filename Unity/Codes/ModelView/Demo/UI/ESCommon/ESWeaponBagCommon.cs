using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public partial class ESWeaponBagCommon
    {
        public Dictionary<int, Scroll_ItemWeapon> ItemWeapons = new Dictionary<int, Scroll_ItemWeapon>();
        public List<WeaponInfo> WeaponInfos = new List<WeaponInfo>();
        public Action<WeaponInfo, Scroll_ItemWeapon> WeaponItemClickAction;
        public int BagCount = 0;
        public int WeaponCount = 0;
        public Sprite DefaultWeaponBgSprite = null;
        public WeaponType[] WeaponTypes = new WeaponType[] { WeaponType.Weapon, WeaponType.Equip, WeaponType.Ring, WeaponType.Accessory, WeaponType.Invalide };

        public WeaponInfo UnableWeaponInfo = null;
        public List<WeaponInfo> EnableWeaponInfos = null;
        public List<WeaponInfo> AlChooseWeaponInfos = null;
        
    }
}