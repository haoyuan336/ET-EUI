﻿using System;
using System.Collections.Generic;
using ET.Effect;
using UnityEngine;

namespace ET.Demo.Effect
{
    public class AddAttackEffectAwakeSystem: AwakeSystem<AddAttackEffect, List<Vector3>, Vector3, DiamondInfo>
    {
        public override async void Awake(AddAttackEffect self, List<Vector3> a, Vector3 b, DiamondInfo diamondInfo)
        {
            Log.Warning($"增加了 add attack effect component {a.Count} ");
            DiamondTypeConfig diamondTypeConfig = DiamondTypeConfigCategory.Instance.Get(diamondInfo.ConfigId);
            var str = diamondTypeConfig.FlyEffectAttackRes;
            GameObject prefab = await AddressableComponent.Instance.LoadAssetByPathAsync<GameObject>(str);
            foreach (var startPos in a)
            {
                GameObject obj = GameObject.Instantiate(prefab);
                // self.effects.Add(obj);
                self.EffectMap.Add(obj, startPos);
                obj.transform.position = startPos;
            }

            self.EndPos = b;

            await ETTask.CompletedTask;
        }
    }

    public class AddAttackEffectUpdate: UpdateSystem<AddAttackEffect>
    {
        public override void Update(AddAttackEffect self)
        {
            if (self.EffectMap.Count > 0)
            {
                self.Time += Time.deltaTime * ConstValue.FlyEffectFlySpeed;

                if (self.Time < Mathf.PI * 0.5f)
                {
                    foreach (var key in self.EffectMap.Keys)
                    {
                        var startPos = self.EffectMap[key];
                        var prePos = Vector3.Lerp(startPos, self.EndPos, Mathf.Sin(self.Time));

                        key.transform.position = prePos;
                    }
                }
                else
                {
                    foreach (var effect  in self.EffectMap.Keys)
                    {
                        GameObject.Destroy(effect);
                    }
                    self.Task.SetResult();
                    self.EffectMap.Clear();
                    self.Dispose();
                }
            }
        }
    }

    public static class AddAttackEffectSystem
    {
        public static async ETTask PlayAnim(this AddAttackEffect self)
        {
            self.Task = ETTask.Create();
            await self.Task;
        }
    }
}