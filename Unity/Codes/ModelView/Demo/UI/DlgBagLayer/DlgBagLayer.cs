using System.Collections.Generic;

namespace ET
{
    public class DlgBagLayer: Entity, IAwake
    {
        public DlgBagLayerViewComponent View
        {
            get => this.Parent.GetComponent<DlgBagLayerViewComponent>();
        }

        public Dictionary<int, Scroll_ItemWeapon> ItemWeapons = new Dictionary<int, Scroll_ItemWeapon>();
        public List<WeaponInfo> WeaponInfos = new List<WeaponInfo>();
    }
}