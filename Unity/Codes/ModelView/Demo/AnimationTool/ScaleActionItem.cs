using UnityEngine;

namespace ET
{
    public class ScaleActionItem
    {
        public float Time;
        public float CurrentTime;
        public Vector3 CurrentScale;
        public Vector3 EndScale;
        public ETTask Task;
        public float Speed;
        public GameObject GameObject;
        public MoveActionType MoveActionType = MoveActionType.Invalide;
        public StateType StateType = StateType.Active;
    }
}