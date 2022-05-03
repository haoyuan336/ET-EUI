using System.Collections.Generic;
using ET.Effect;
using UnityEngine;

namespace ET.Demo.Effect
{
    public class AddAngryEffectAwakeSystem: AwakeSystem<AddAngryEffect, List<Vector3>, Vector3>
    {
        public override void Awake(AddAngryEffect self, List<Vector3> a, Vector3 b)
        {
        }
    }

    public class AddAngeyEffectUpdateSystem: UpdateSystem<AddAngryEffect>
    {
        public override void Update(AddAngryEffect self)
        {
        }
    }
}