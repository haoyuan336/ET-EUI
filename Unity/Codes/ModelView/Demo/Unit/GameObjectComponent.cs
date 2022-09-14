using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.UIElements;

namespace ET
{
  
    public class GameObjectComponent: Entity, IAwake, IDestroy, IUpdate
    {
        public GameObject GameObject;
        public List<MoveActionItem> MoveActionItems = new List<MoveActionItem>();
    }
}