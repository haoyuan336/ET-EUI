using UnityEngine;

namespace ET
{
    public class ScaleActionItem
    {
        public float Time;
        public float CurrentTime;
        public float CurrentScale;
        public float EndScale;
        public ETTask Task;
    }
    public class HeroCardInfoObjectComponent: Entity, IAwake, IUpdate, IDestroy, IAwake<HeroCardInfo, HeroCardDataComponentInfo>
    {
        public GameObject GameObject;

        public GameObject HeroMode;

        public float HeroHeight;

        public ESHeroCardInfoUI ESHeroCardInfoUI;

        public HeroConfig HeroConfig;
        

    }
}