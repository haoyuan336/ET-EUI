using UnityEngine;

namespace ET
{
    public class HeroCardObjectComponent: Entity, IAwake<HeroCard>, IAwake<HeroCard, HeroCardInfo>
    {
        public GameObject HeroCard;
    }
}