using System.Net;
namespace ET
{
    public static class SceneFactory
    {
        public static async ETTask<Scene> Create(Entity parent, string name, SceneType sceneType)
        {
            long instanceId = IdGenerater.Instance.GenerateInstanceId();
            return await Create(parent, instanceId, instanceId, parent.DomainZone(), name, sceneType);
        }

        public static async ETTask<Scene> Create(Entity parent, long id, long instanceId, int zone, string name, SceneType sceneType,
        StartSceneConfig startSceneConfig = null)
        {
            await ETTask.CompletedTask;
            Scene scene = EntitySceneFactory.CreateScene(id, instanceId, zone, sceneType, name, parent);

            scene.AddComponent<MailBoxComponent, MailboxType>(MailboxType.UnOrderMessageDispatcher);
            // Log.Warning($"创建scene scene{scene.SceneType}");
            switch (scene.SceneType)
            {
                case SceneType.Realm:
                    scene.AddComponent<NetKcpComponent, IPEndPoint, int>(startSceneConfig.InnerIPOutPort,
                        SessionStreamDispatcherType.SessionStreamDispatcherServerOuter);
                    scene.AddComponent<TokenComponent>();
                    break;
                case SceneType.Gate:
                    scene.AddComponent<NetKcpComponent, IPEndPoint, int>(startSceneConfig.InnerIPOutPort,
                        SessionStreamDispatcherType.SessionStreamDispatcherServerOuter);
                    scene.AddComponent<PlayerComponent>();
                    scene.AddComponent<GateSessionKeyComponent>();
                    break;
                case SceneType.Map:
                    scene.AddComponent<UnitComponent>();
                    scene.AddComponent<AOIManagerComponent>();
                    // scene.AddComponent<MatchComponent>();
                    // scene.AddComponent<PVPRoomComponent>();
                    // scene.AddComponent<DiamondComponent>();
                    // scene.AddComponent<PVERoomComponent>();
                    break;
                case SceneType.PVEGameScene:
                    scene.AddComponent<UnitComponent>();
                    scene.AddComponent<AOIManagerComponent>();
                    // scene.AddComponent<DiamondComponent>();
                    scene.AddComponent<PVERoomComponent>();
                    break;
                case SceneType.PVPGameScene:
                    scene.AddComponent<UnitComponent>();
                    // scene.AddComponent<MatchComponent>();
                    // scene.AddComponent<PV>()
                    break;
                case SceneType.MainScene:
                    scene.AddComponent<UnitComponent>();
                    scene.AddComponent<AOIManagerComponent>();
                    scene.AddComponent<MailComponent>();
                    scene.AddComponent<MatchComponent>();
                    
                    break;
                case SceneType.ChangeTempScene:
                    scene.AddComponent<UnitComponent>();
                    break;
                case SceneType.Location:
                    scene.AddComponent<LocationComponent>();
                    break;
                case SceneType.Account:
                    scene.AddComponent<NetKcpComponent, IPEndPoint, int>(startSceneConfig.InnerIPOutPort,
                        SessionStreamDispatcherType.SessionStreamDispatcherServerOuter);
                    scene.AddComponent<TokenComponent>();
                    scene.AddComponent<ServerInfoManagerComponent>();
                    break;
                case SceneType.LoginCenter:
                    scene.AddComponent<LoginInfoRecordComponent>();
                    break;
                case SceneType.MailScene:
                    break;
            }

            return scene;
        }
    }
}