using UnityEngine;

namespace ET
{
    public class HeroModeObjectCompoent: Entity, IAwake<HeroCard>, IUpdate, IDestroy
    {
        public GameObject HeroMode;
        public Vector3 HeroModeInitPos = Vector3.zero;
        public bool IsTouching = false;

        public GameObject ChooseMark;
        // public bool IsClickSelf = false;
    }
}