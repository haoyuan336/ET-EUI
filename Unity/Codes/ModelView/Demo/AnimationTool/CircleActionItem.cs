using UnityEngine;

namespace ET
{
    public class CircleActionItem
    {
        public float CurrentTime = 0;
        public float Time;
        public Quaternion CurrentQuat;
        public Quaternion EndQuat;
        public ETTask Task;
        public float Speed;
        public GameObject GameObject;
        public StateType StateType = StateType.Active;
    }
}