using System.Collections.Generic;
using ET.Effect;
using UnityEngine;

namespace ET.Demo.Effect
{
    public class AddAttackEffectAwakeSystem: AwakeSystem<AddAttackEffect, List<Vector3>, Vector3>
    {
        public override void Awake(AddAttackEffect self, List<Vector3> a, Vector3 b)
        {
        }
    }

    public class AddAttackEffectUpdate: UpdateSystem<AddAttackEffect>
    {
        public override void Update(AddAttackEffect self)
        {
        }
    }

    public static class AddAttackEffectSystem
    {
        public static async ETTask PlayAnim(this AddAttackEffect self)
        {
            await ETTask.CompletedTask;
        }
    }
}