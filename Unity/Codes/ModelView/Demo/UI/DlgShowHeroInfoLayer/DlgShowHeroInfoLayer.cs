using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class DlgShowHeroInfoLayer: Entity, IAwake
    {
        public DlgShowHeroInfoLayerViewComponent View
        {
            get => this.Parent.GetComponent<DlgShowHeroInfoLayerViewComponent>();
        }

        public GameObject HeroModeShow;

        public HeroCardInfo HeroCardInfo;

        public Dictionary<int, Scroll_ItemWeapon> WeaponDicts = new Dictionary<int, Scroll_ItemWeapon>();

        public List<WeaponInfo> WeaponInfos;
    }
}