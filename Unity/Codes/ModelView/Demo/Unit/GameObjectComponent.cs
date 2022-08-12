using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

namespace ET
{
    public class MoveActionItem
    {
        public float CurrentTime = 0;
        public float Time;
        public Vector3 CurrentPos;
        public Vector3 EndPos;
        public ETTask Task;
        public float Speed;
        public GameObject GameObject;
        public MoveActionType MoveActionType;
    }
    public class GameObjectComponent: Entity, IAwake, IDestroy, IUpdate
    {
        public GameObject GameObject;
        public List<MoveActionItem> MoveActionItems = new List<MoveActionItem>();
        
        
    }
}