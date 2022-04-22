using System.Collections.Generic;
using System.Runtime.InteropServices;
using ILRuntime.Runtime.Intepreter;
using UnityEngine;

namespace ET
{
    public class ConfigLoader: IConfigLoader
    {
        public async ETTask GetAllConfigBytes(Dictionary<string, byte[]> output)
        {
            Log.Debug("load all config bytes");
            // Dictionary<string, UnityEngine.Object> keys = ResourcesComponent.Instance.GetBundleAll("config.unity3d");
            List<UnityEngine.Object> resultList =
                    await AddressableComponent.Instance.LoadAssetsByLabelAsync<UnityEngine.Object>("Config", (handller) => { });

            Log.Debug($"result count {resultList.Count}");

            foreach (var result in resultList)
            {
                TextAsset v = result as TextAsset;
                // Log.Debug($"text asset {v.name}");
                output[v.name] = v.bytes;
            }
            // await ETTask.CompletedTask;

            // foreach (var kv in keys)
            // {
            //     TextAsset v = kv.Value as TextAsset;
            //     string key = kv.Key;
            //     output[key] = v.bytes;
            // }
        }

        public byte[] GetOneConfigBytes(string configName)
        {
            TextAsset v = ResourcesComponent.Instance.GetAsset("config.unity3d", configName) as TextAsset;
            return v.bytes;
        }
    }
}