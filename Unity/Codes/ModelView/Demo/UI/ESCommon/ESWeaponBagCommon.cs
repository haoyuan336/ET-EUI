using System;
using System.Collections.Generic;

namespace ET
{
    public partial class ESWeaponBagCommon
    {
        public Dictionary<int, Scroll_ItemWeapon> ItemWeapons = new Dictionary<int, Scroll_ItemWeapon>();
        public List<WeaponInfo> WeaponInfos = new List<WeaponInfo>();
        public Action<WeaponInfo> WeaponItemClickAction;
        public int BagCount = 0;
        public int WeaponCount = 0;
    }
}