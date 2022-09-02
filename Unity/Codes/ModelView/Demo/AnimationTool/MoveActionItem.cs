using UnityEngine;

namespace ET
{
    public class MoveActionItem
    {
        public float CurrentTime = 0;
        public float Time;
        public Vector3 CurrentPos;
        public Quaternion CurrentQuat;
        public Quaternion EndQuat;
        public Vector3 EndPos;
        public ETTask Task;
        public float Speed;
        public GameObject GameObject;
        public MoveActionType MoveActionType = MoveActionType.Invalide;
        public StateType StateType = StateType.Active;
    }

}