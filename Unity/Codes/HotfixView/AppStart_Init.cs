using System.Numerics;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ET
{
    public class AppStart_Init: AEvent<EventType.AppStart>
    {
        protected override async ETTask Run(EventType.AppStart args)
        {
            // Debug.Log("add start init");
            // await AddressableInstance.LoadAssetByLabel<GameObject>("Code");

            // Addressables.LoadAssetAsync<UnityEngine.GameObject>("DlgLogin");

            Game.Scene.AddComponent<TimerComponent>();
            Game.Scene.AddComponent<CoroutineLockComponent>();
            // 加载配置\
            Game.Scene.AddComponent<AddressableComponent>();
            
            // AddressableComponent.Instance.LoadAssetByPathAsync<UnityEngine.GameObject>("DlgMainScene");

            
            Game.Scene.AddComponent<ConfigComponent>();
            await ConfigComponent.Instance.LoadAsync();

            // Game.Scene.AddComponent<ResourcesComponent>();
            // await ResourcesComponent.Instance.LoadBundleAsync("config.unity3d");
            // Game.Scene.AddComponent<ConfigComponent>();
            // ConfigComponent.Instance.Load();
            // ResourcesComponent.Instance.UnloadBundle("config.unity3d");
            //
            Game.Scene.AddComponent<OpcodeTypeComponent>();
            Game.Scene.AddComponent<MessageDispatcherComponent>();
            //
            Game.Scene.AddComponent<NetThreadComponent>();
            Game.Scene.AddComponent<SessionStreamDispatcher>();
            Game.Scene.AddComponent<ZoneSceneManagerComponent>();
            //
            Game.Scene.AddComponent<GlobalComponent>();
            //
            Game.Scene.AddComponent<AIDispatcherComponent>();
            // await AddressableComponent.Instance.Load
            // await ResourcesComponent.Instance.LoadBundleAsync("unit.unity3d");
            // //
            Scene zoneScene = await SceneFactory.CreateZoneScene(1, "Game", Game.Scene);
            // //
            await Game.EventSystem.PublishAsync(new EventType.AppStartInitFinish() { ZoneScene = zoneScene });
            await ETTask.CompletedTask;
        }
    }
}