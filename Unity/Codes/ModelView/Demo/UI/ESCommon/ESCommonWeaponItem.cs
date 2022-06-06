using System;
using UnityEngine;

namespace ET
{
    public class ESCommonWeaponItem: Entity, IAwake<WeaponType, Transform>
    {
        public GameObject GameObject;

        public GameObject E_Weapon;
        public GameObject E_Element;
        public GameObject E_AddText;
        public GameObject E_Level;
        public GameObject E_QualityIcon;
        public GameObject E_Choose;
        public GameObject E_WeaponType;

        public Action<WeaponType,ESCommonWeaponItem, bool> OnWeaponItemClickAction;
        public WeaponType CurrentType;
        public WeaponInfo WeaponInfo;
    }
}