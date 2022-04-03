using System;
using UnityEngine;

namespace ET
{
    public class OperaComponent: Entity, IAwake, IUpdate
    {
        public Vector2 ClickPoint;

        public int mapMask;

        public readonly C2M_PathfindingResult frameClickMap = new C2M_PathfindingResult();

        public bool isTouching = false;

        public bool TouchLock = false;
    }
}