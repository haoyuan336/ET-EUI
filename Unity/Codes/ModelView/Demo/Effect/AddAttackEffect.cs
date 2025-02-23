﻿using System.Collections.Generic;
using UnityEngine;

namespace ET.Effect
{
    public class AddAttackEffect: Entity, IUpdate, IAwake<List<Vector3>, Vector3, DiamondInfo>
    {
        public Dictionary<GameObject, Vector3> EffectMap = new Dictionary<GameObject, Vector3>();
        public float Time = 0;
        public Vector3 EndPos;
        public ETTask Task;
    }
}