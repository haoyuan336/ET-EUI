using System.Numerics;
using System.Runtime.InteropServices;

namespace ET
{
    public class AppStart_Init: AEvent<EventType.AppStart>
    {
        protected override async ETTask Run(EventType.AppStart args)
        {
            Game.Scene.AddComponent<TimerComponent>();
            Game.Scene.AddComponent<CoroutineLockComponent>();

            // 加载配置
            Game.Scene.AddComponent<ResourcesComponent>();
            await ResourcesComponent.Instance.LoadBundleAsync("config.unity3d");
            Game.Scene.AddComponent<ConfigComponent>();
            ConfigComponent.Instance.Load();
            ResourcesComponent.Instance.UnloadBundle("config.unity3d");

            Game.Scene.AddComponent<OpcodeTypeComponent>();
            Game.Scene.AddComponent<MessageDispatcherComponent>();

            Game.Scene.AddComponent<NetThreadComponent>();
            Game.Scene.AddComponent<SessionStreamDispatcher>();
            Game.Scene.AddComponent<ZoneSceneManagerComponent>();

            Game.Scene.AddComponent<GlobalComponent>();

            Game.Scene.AddComponent<AIDispatcherComponent>();
            await ResourcesComponent.Instance.LoadBundleAsync("unit.unity3d");

            Scene zoneScene = SceneFactory.CreateZoneScene(1, "Game", Game.Scene);

            await Game.EventSystem.PublishAsync(new EventType.AppStartInitFinish() { ZoneScene = zoneScene });

            ETCancellationToken etCancellationToken = new ETCancellationToken();
            this.MoveToPosition(Vector3.Zero, etCancellationToken).Coroutine();
            etCancellationToken.Cancel();
        }

        public async ETTask MoveToPosition(Vector3 pos, ETCancellationToken cancellationToken)
        {
            Log.Debug("Move Start");
            bool ret = await TimerComponent.Instance.WaitAsync(3000, cancellationToken);
            if (ret)
            {
                Log.Debug("Move end");
            }
            else
            {
                Log.Debug("Move stop");
            }
        }
    }
}