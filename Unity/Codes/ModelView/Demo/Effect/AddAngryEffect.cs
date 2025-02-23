﻿using System.Collections.Generic;
using UnityEngine;

namespace ET.Effect
{
    public class AddAngryEffect: Entity, IUpdate, IAwake<List<Vector3>, Vector3, DiamondInfo, int>,IAwake<Vector3, DiamondInfo, Vector3>
    {
        public Dictionary<GameObject, Vector3> EffectMap = new Dictionary<GameObject, Vector3>();
        public float Time = 0;
        public Vector3 StartPos;
        public Vector3 EndPos;
        public ETTask Task;
        public GameObject EffectGameObject;
        
    }
}