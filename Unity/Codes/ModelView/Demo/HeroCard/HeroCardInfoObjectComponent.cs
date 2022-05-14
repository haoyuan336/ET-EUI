using UnityEngine;

namespace ET
{
    public class HeroCardInfoObjectComponent: Entity,IAwake, IUpdate, IDestroy
    {
        public GameObject GameObject;
        public GameObject HeroMode;
        public GameObject HpBarImage;
        public GameObject AttackBarImage;
        public GameObject AngryBarImage;
        public GameObject HeroElementIcon;
        public float HeroHeight;
    }
}