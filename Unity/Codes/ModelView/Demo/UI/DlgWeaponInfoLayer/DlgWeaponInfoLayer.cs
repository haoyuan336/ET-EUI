using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class DlgWeaponInfoLayer: Entity, IAwake
    {
        public DlgWeaponInfoLayerViewComponent View
        {
            get => this.Parent.GetComponent<DlgWeaponInfoLayerViewComponent>();
        }

        public List<ESCommonWordBar> WordBarItems = new List<ESCommonWordBar>(); //字条对象
        public WeaponInfo WeaponInfo;
        public Scroll_ItemWeapon CurrentWeaponItem;
        public Scroll_ItemHeroCard CurrentHeroCardItem;

        // public 
    }
}