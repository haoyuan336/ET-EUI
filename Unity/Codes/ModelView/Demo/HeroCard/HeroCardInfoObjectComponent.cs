using UnityEngine;

namespace ET
{
    public class HeroCardInfoObjectComponent: Entity,IAwake<int>, IUpdate, IDestroy, IAwake<HeroCard, HeroCardInfo>
    {
        public GameObject GameObject;
        public GameObject HeroMode;
        public GameObject HpBarImage;
        public GameObject AttackBarImage;
        public GameObject AngryBarImage;
        public GameObject HeroElementIcon;
        public float HeroHeight;
        public GameObject CommonText;
        public HeroConfig HeroConfig;
    }
}