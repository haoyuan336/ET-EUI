using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class HeroModeObjectCompoent: Entity, IAwake<HeroCard>, IUpdate, IDestroy
    {
        public GameObject HeroMode;
        public Vector3 HeroModeInitPos = Vector3.zero;
        public bool IsTouching = false;

        public GameObject ChooseMark;

        public GameObject AttackMark;
        // public bool IsClickSelf = false;
        
        public List<MoveActionItem> MoveActionItems = new List<MoveActionItem>();
    }
}