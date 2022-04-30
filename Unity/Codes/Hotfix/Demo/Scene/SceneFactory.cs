using ET.Account;

namespace ET
{
    public static class SceneFactory
    {
        public static async ETTask<Scene> CreateZoneScene(int zone, string name, Entity parent)
        {
            Scene zoneScene = EntitySceneFactory.CreateScene(Game.IdGenerater.GenerateInstanceId(), zone, SceneType.Zone, name, parent);
            zoneScene.AddComponent<ZoneSceneFlagComponent>();
            zoneScene.AddComponent<NetKcpComponent, int>(SessionStreamDispatcherType.SessionStreamDispatcherClientOuter);
            zoneScene.AddComponent<CurrentScenesComponent>();
            zoneScene.AddComponent<ObjectWait>();
            zoneScene.AddComponent<PlayerComponent>();
            zoneScene.AddComponent<AccountInfoComponent>();
            zoneScene.AddComponent<ServerInfosComponent>();
            // zoneScene.AddComponent<DiamondComponent>();
            // zoneScene.AddComponent<HeroCardComponent>();
            await Game.EventSystem.PublishAsync(new EventType.AfterCreateZoneScene() { ZoneScene = zoneScene });
            return zoneScene;

            // Scene zoneScene = EntitySceneFactory.CreateScene(Game.IdGenerater.GenerateInstanceId(), zone, SceneType.Zone, name, parent);
            // zoneScene.AddComponent<ZoneSceneFlagComponent>();
            // zoneScene.AddComponent<NetKcpComponent, int>(SessionStreamDispatcherType.SessionStreamDispatcherClientOuter);
            // zoneScene.AddComponent<UnitComponent>();
            // zoneScene.AddComponent<AIComponent, int>(1);
            //
            // // UI层的初始化
            // await Game.EventSystem.Publish(new EventType.AfterCreateZoneScene() {ZoneScene = zoneScene});
        }

        public static Scene CreateCurrentScene(long id, int zone, string name, CurrentScenesComponent currentScenesComponent)
        {
            Scene currentScene = EntitySceneFactory.CreateScene(id, zone, SceneType.Current, name, currentScenesComponent);
            currentScenesComponent.Scene = currentScene;

            Game.EventSystem.Publish(new EventType.AfterCreateCurrentScene() { CurrentScene = currentScene });
            return currentScene;
        }
    }
}