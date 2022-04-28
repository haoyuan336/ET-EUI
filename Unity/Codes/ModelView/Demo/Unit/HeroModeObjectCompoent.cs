using UnityEngine;

namespace ET
{
    public class HeroModeObjectCompoent: Entity, IAwake<HeroCard>
    {
        public GameObject HeroMode;
        public Vector3 HeroModeInitPos = Vector3.zero;
    }
}