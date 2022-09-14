using System.Collections.Generic;
using UnityEngine;

namespace ET
{
 
    public class HeroCardInfoObjectComponent: Entity, IAwake, IUpdate, IDestroy, IAwake<HeroCardInfo, HeroCardDataComponentInfo>
    {
        public GameObject GameObject;

        public GameObject HeroMode;

        public float HeroHeight;

        public ESHeroCardInfoUI ESHeroCardInfoUI;

        public HeroConfig HeroConfig;

        public List<MoveActionItem> MoveActionItems = new List<MoveActionItem>();

    }
}