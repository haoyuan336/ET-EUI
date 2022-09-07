using System;
using System.Collections.Generic;
// using System.;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace ET
{
    public static class GameObjectPoolHelper
    {
        private static Dictionary<string, GameObjectPool> poolDict = new Dictionary<string, GameObjectPool>();

        public static async ETTask InitPoolAsync(string poolName, int size, PoolInflationType type = PoolInflationType.DOUBLE)
        {
            ETTask task = ETTask.Create();
            if (poolDict.ContainsKey(poolName))
            {
                task.SetResult();
                return;
            }

            try
            {
                Log.Debug($"init poll async {poolName}");
                AsyncOperationHandle handle = GetGameObjectByResType(poolName);
                handle.Completed += (result) =>
                {
                    if (result.Status == AsyncOperationStatus.Succeeded)
                    {
                        GameObject gameObject = handle.Result as GameObject;
                        poolDict[poolName] = new GameObjectPool(poolName, gameObject, GameObject.Find("Global/PoolRoot"),
                            size, type);
                    }

                    task.SetResult();
                };
                // handle.WaitForCompletion();
                // Debug.Log($"load  handler {handle.Status}");
                // GameObject result = (GameObject) handle.Result;
                // poolDict[poolName] = new GameObjectPool(poolName, result, GameObject.Find("Global/PoolRoot"),
                //     size, type);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                task.SetResult();
            }

            await task.GetAwaiter();
        }

        public static void InitPool(string poolName, int size, PoolInflationType type = PoolInflationType.DOUBLE)
        {
            if (poolDict.ContainsKey(poolName))
            {
                return;
            }
            else
            {
                try
                {
                    // GameObject pb = GetGameObjectByResType(poolName);
                    // if (pb == null)
                    // {
                    //     Debug.LogError("[ResourceManager] Invalide prefab name for pooling :" + poolName);
                    //     return;
                    // }
                    Log.Debug($"init poll {poolName}");
                    AsyncOperationHandle handle = GetGameObjectByResType(poolName);
                    handle.WaitForCompletion();
                    Debug.Log($"load  handler {handle.Status}");
                    GameObject result = (GameObject)handle.Result;
                    poolDict[poolName] = new GameObjectPool(poolName, result, GameObject.Find("Global/PoolRoot"),
                        size, type);
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }

        public static async ETTask InitPoolFormGamObjectAsync(GameObject pb, int size, PoolInflationType type = PoolInflationType.DOUBLE)
        {
            string poolName = pb.name;
            if (poolDict.ContainsKey(poolName))
            {
                return;
            }
            else
            {
                try
                {
                    if (pb == null)
                    {
                        Debug.LogError("[ResourceManager] Invalide prefab name for pooling :" + poolName);
                        return;
                    }

                    poolDict[poolName] = new GameObjectPool(poolName, pb, GameObject.Find("Global/PoolRoot"), size, type);
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }

            await ETTask.CompletedTask;
        }

        /// <summary>
        /// Returns an available object from the pool 
        /// OR null in case the pool does not have any object available & can grow size is false.
        /// </summary>
        /// <OtherParam name="poolName"></OtherParam>
        /// <returns></returns>
        public static GameObject GetObjectFromPool(string poolName, bool autoActive = true, int autoCreate = 0)
        {
            GameObject result = null;

            if (!poolDict.ContainsKey(poolName) && autoCreate > 0)
            {
                InitPool(poolName, autoCreate, PoolInflationType.INCREMENT);
            }

            if (poolDict.ContainsKey(poolName))
            {
                GameObjectPool pool = poolDict[poolName];
                result = pool.NextAvailableObject(autoActive);
                //scenario when no available object is found in pool
#if UNITY_EDITOR
                if (result == null)
                {
                    Debug.LogWarning("[ResourceManager]:No object available in " + poolName);
                }
#endif
            }
#if UNITY_EDITOR
            else
            {
                Debug.LogError("[ResourceManager]:Invalid pool name specified: " + poolName);
            }
#endif
            return result;
        }

        /// <summary>
        /// Return obj to the pool
        /// </summary>
        /// <OtherParam name="go"></OtherParam>
        public static void ReturnObjectToPool(GameObject go)
        {
            PoolObject po = go.GetComponent<PoolObject>();
            if (po == null)
            {
#if UNITY_EDITOR
                Debug.LogWarning("Specified object is not a pooled instance: " + go.name);
#endif
            }
            else
            {
                GameObjectPool pool = null;
                if (poolDict.TryGetValue(po.poolName, out pool))
                {
                    pool.ReturnObjectToPool(po);
                }
#if UNITY_EDITOR
                else
                {
                    Debug.LogWarning("No pool available with name: " + po.poolName);
                }
#endif
            }
        }

        /// <summary>
        /// Return obj to the pool
        /// </summary>
        /// <OtherParam name="t"></OtherParam>
        public static void ReturnTransformToPool(Transform t)
        {
            if (t == null)
            {
#if UNITY_EDITOR
                Debug.LogError("[ResourceManager] try to return a null transform to pool!");
#endif
                return;
            }

            ReturnObjectToPool(t.gameObject);
        }

        // public static async ETTask<GameObject> GetGameObjectByResTypeAsync(string poolName)
        // {
        //     return  Addressables.LoadAssetAsync<GameObject>()
        // }

        public static AsyncOperationHandle GetGameObjectByResType(string poolName)
        {
            // GameObject pb = null;
            // Dictionary<string, UnityEngine.Object>  assetDict = AssetsBundleHelper.LoadBundle(poolName + ".unity3d");
            // pb = assetDict[poolName] as GameObject;
            // Log.Debug($"load asset asyna {poolName}");
            AsyncOperationHandle pb = Addressables.LoadAssetAsync<GameObject>(poolName);

            return pb;
        }
    }
}