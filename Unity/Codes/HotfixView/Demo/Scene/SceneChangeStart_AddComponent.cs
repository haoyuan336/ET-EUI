using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace ET
{
    public class SceneChangeStart_AddComponent: AEvent<EventType.SceneChangeStart>
    {
        protected override async ETTask Run(EventType.SceneChangeStart args)
        {
            Scene currentScene = args.ZoneScene.CurrentScene();

            // 加载场景资源
            // await ResourcesComponent.Instance.LoadBundleAsync($"{currentScene.Name}.unity3d");
            // 切换到map场景
            
            // if (currentScene.Name == "MainScene")
            // {
            //     await AddressableComponent.Instance.LoadAssetsByLabelAsync<GameObject>("UI", (result) =>
            //     {
            //         // Log.Debug("加载所有UI资源成功");
            //     });
            // }
            AsyncOperationHandle<SceneInstance> instance;
            await AddressableComponent.Instance.LoadSceneByPathAsync(currentScene.Name, out instance);
            await AddressableComponent.Instance.ActivateLoadScene(instance.Result);
            Log.Debug($"current scene name{currentScene.Name}");
          
            SceneChangeComponent sceneChangeComponent = null;
            try
            {
                sceneChangeComponent = Game.Scene.AddComponent<SceneChangeComponent>();
                {
                    // await sceneChangeComponent.ChangeSceneAsync(currentScene.Name);
                    await sceneChangeComponent.ChangeSceneAsync(instance);
                }
            }
            finally
            {
                sceneChangeComponent?.Dispose();
            }

            // Log.Warning($"scene change{currentScene.Name}");
            // GameObject obj = GameObject.Find("UnitRoot/MainCameraCM");
            // GameObject pveObj = GameObject.Find("UnitRoot/PVECameraCM");
            // switch (currentScene.Name)
            // {
            //     case "MainScene":
            //         obj.SetActive(true);
            //         pveObj.SetActive(false);
            //
            //         break;
            //     case "PVEGameScene":
            //         obj.SetActive(true);
            //         pveObj.SetActive(false);
            //         break;
            // }

            currentScene.AddComponent<OperaComponent>();
        }
    }
}