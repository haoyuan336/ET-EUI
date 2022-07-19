using System.Collections.Generic;
using System.Threading.Tasks;
using ET.Effect;
using UnityEngine;

namespace ET.Demo.Effect
{
    public class AddAngryEffectAwakeSystem: AwakeSystem<AddAngryEffect, List<Vector3>, Vector3, DiamondInfo, int>
    {
        public override async void Awake(AddAngryEffect self, List<Vector3> a, Vector3 b, DiamondInfo diamondInfo, int index)
        {
            DiamondTypeConfig config = DiamondTypeConfigCategory.Instance.Get(diamondInfo.ConfigId);
            var str = config.FlyEffectAngryRes;
            Log.Warning($"增加了 add attack effect component {a.Count} ");
            GameObject prefab = await AddressableComponent.Instance.LoadAssetByPathAsync<GameObject>(str);

            // await TimerComponent.Instance.WaitAsync(index * 1000);
            foreach (var startPos in a)
            {
                GameObject obj = GameObject.Instantiate(prefab);
                self.EffectMap.Add(obj, startPos);
                obj.transform.position = startPos;
            }

            self.EndPos = b;
        }
    }

    public class AddAngryEffectAwakeSystem2: AwakeSystem<AddAngryEffect, Vector3, DiamondInfo, Vector3>
    {
        public override async void Awake(AddAngryEffect self, Vector3 startPos, DiamondInfo b, Vector3 endPos)
        {
            DiamondTypeConfig config = DiamondTypeConfigCategory.Instance.Get(b.ConfigId);
            var str = config.FlyEffectAngryRes;
            self.EffectGameObject = await AddressableComponent.Instance.LoadGameObjectAndInstantiateByPath(str);
            self.EffectGameObject.transform.position = startPos;
            self.EndPos = endPos;
            self.StartPos = startPos;
            await ETTask.CompletedTask;
        }
    }

    public class AddAngeyEffectUpdateSystem: UpdateSystem<AddAngryEffect>
    {
        public override void Update(AddAngryEffect self)
        {
            if (self.EffectGameObject)
            {
                self.Time += Time.deltaTime * ConstValue.FlyEffectFlySpeed;

                if (self.Time < Mathf.PI * 0.5f)
                {
                    var prePos = Vector3.Lerp(self.StartPos, self.EndPos, Mathf.Sin(self.Time));
                    self.EffectGameObject.transform.position = prePos;
                }
                else
                {
                    GameObject.Destroy(self.EffectGameObject);
                    self.Task.SetResult();
                    self.EffectMap.Clear();
                    self.Dispose();
                }
            }

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
                    foreach (var effect in self.EffectMap.Keys)
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

    public static class AddAngryEffectSystem
    {
        // AddAngryEffect
        public static async ETTask PlayAnim(this AddAngryEffect self)
        {
            self.Task = ETTask.Create();
            await self.Task;
        }
    }
}