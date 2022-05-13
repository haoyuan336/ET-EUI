using System.Collections.Generic;

namespace ET
{
    public class DlgWeaponStrengthenLayer: Entity, IAwake
    {
        public DlgWeaponStrengthenLayerViewComponent View
        {
            get => this.Parent.GetComponent<DlgWeaponStrengthenLayerViewComponent>();
        }

        public Dictionary<int, Scroll_ItemWeapon> TargetItemWeapons = new Dictionary<int, Scroll_ItemWeapon>();
        public Dictionary<int, Scroll_ItemWeapon> ItemWeapons = new Dictionary<int, Scroll_ItemWeapon>();

        public List<WeaponInfo> WeaponInfos = new List<WeaponInfo>();
        public List<WeaponInfo> TargetWeaponInfos = new List<WeaponInfo>();

        public List<WeaponInfo> AlChooseWeaponInfos = new List<WeaponInfo>();   //已经选择的武器信息
    }
}