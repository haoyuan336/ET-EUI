using UnityEngine;

namespace ET
{
    public class HeroCardInfoObjectComponent: Entity,IAwake, IUpdate
    {
        public GameObject GameObject;
        public GameObject HeroMode;
        public GameObject HpBarImage;
        public GameObject AttackBarImage;
        public GameObject AngryBarImage;
        public float HeroHeight;
    }
}