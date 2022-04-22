using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace ET
{
    public static class AddressableInstance
    {
        public static async ETTask LoadAssetByLabel<T>(string label)
        {
            ETTask tcs = ETTask.Create(true);
            // tcs.SetResult(false);
            // await tcs;
            Debug.Log("start");
            await TimerComponent.Instance.WaitAsync(1000);
            Debug.Log("end");
            tcs.SetResult();
            // var task = ETTask.Create();
            try
            {
                // Addressables.LoadAssetAsync<TextAsset>("Code.pdb").Completed += (value) => { Debug.Log("Load asset ayncs complete"); };
                // TextAsset codedll = Addressables.LoadAssetAsync<TextAsset>("Code.dll").WaitForCompletion();

                Addressables.InitializeAsync().Completed += LoadCom;
                // var codedll = Addressables.LoadAssetAsync<TextAsset>("Code.dll");
                // codedll.Completed += (result) => { Debug.Log("Complete"); };
            }
            catch (Exception e)
            {
                Debug.Log($"err {e}");
            }

            await ETTask.CompletedTask;

            // return task;
        }

        private static void LoadCom(AsyncOperationHandle<IResourceLocator> obj)
        {
            Debug.Log($"Load success {obj.Status}");
        }

       
    }
}