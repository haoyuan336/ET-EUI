using System.Collections.Generic;
using System.Threading.Tasks;
using ET.Effect;
using UnityEngine;

namespace ET.Demo.Effect
{
    public class AddAngryEffectAwakeSystem: AwakeSystem<AddAngryEffect, List<Vector3>, Vector3>
    {
        public override async void Awake(AddAngryEffect self, List<Vector3> a, Vector3 b)
        {
            
            Log.Warning($"增加了 add attack effect component {a.Count} ");
            GameObject prefab = await AddressableComponent.Instance.LoadAssetByPathAsync<GameObject>("Assets/Bundles/Effect/AddAngryParticle.prefab");
            foreach (var startPos in a)
            {
                GameObject obj = GameObject.Instantiate(prefab);
                // self.effects.Add(obj);
                self.EffectMap.Add(obj, startPos);
                obj.transform.position = startPos;
            }

            self.EndPos = b;
        }
    }

    public class AddAngeyEffectUpdateSystem: UpdateSystem<AddAngryEffect>
    {
        public override void Update(AddAngryEffect self)
        {
            if (self.EffectMap.Count > 0)
            {
                self.Time += Time.deltaTime * 3;

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
                    self.EffectMap.Clear();
                    self.Dispose();
                }
            }
        }
    }

    public static class AddAngryEffectSystem
    {
        // AddAngryEffect
        public static async ETTask PlayAnim(this AddAngryEffect self)
        {
            await ETTask.CompletedTask;
        }
    }
}