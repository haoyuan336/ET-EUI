using System;
using System.Collections.Generic;
using System.Net;

namespace ET
{
    public class AppStart_Init: AEvent<EventType.AppStart>
    {
        protected override async ETTask Run(EventType.AppStart args)
        {
            Game.Scene.AddComponent<ConfigComponent>();
            await ConfigComponent.Instance.LoadAsync();

            StartProcessConfig processConfig = StartProcessConfigCategory.Instance.Get(Game.Options.Process);

            Game.Scene.AddComponent<TimerComponent>();
            Game.Scene.AddComponent<OpcodeTypeComponent>();
            Game.Scene.AddComponent<MessageDispatcherComponent>();
            Game.Scene.AddComponent<SessionStreamDispatcher>();
            Game.Scene.AddComponent<CoroutineLockComponent>();
            // 发送普通actor消息
            Game.Scene.AddComponent<ActorMessageSenderComponent>();
            // 发送location actor消息
            Game.Scene.AddComponent<ActorLocationSenderComponent>();
            // 访问location server的组件
            Game.Scene.AddComponent<LocationProxyComponent>();
            Game.Scene.AddComponent<ActorMessageDispatcherComponent>();
            // 数值订阅组件
            Game.Scene.AddComponent<NumericWatcherComponent>();

            Game.Scene.AddComponent<NetThreadComponent>();

            // Game.Scene.AddComponent<NavmeshComponent, Func<string, byte[]>>(RecastFileReader.Read);
            Game.Scene.AddComponent<DBManagerComponent>();

            // List<HeroCard> allHeroCard = await DBManagerComponent.Instance.GetZoneDB(1).Query<HeroCard>(a => a.State == (int)StateType.Active);
            //
            // foreach (var heroCard in allHeroCard)
            // {
            //     HeroConfig heroConfig = HeroConfigCategory.Instance.Get(heroCard.ConfigId);
            //     if (heroConfig.MaterialType == (int)HeroBagType.Materail)
            //     {
            //         heroCard.State = (int)StateType.Destroy;
            //         await DBManagerComponent.Instance.GetZoneDB(1).Save(heroCard);
            //     }
            // }

            switch (Game.Options.AppType)
            {
                case AppType.Server:
                {
                    Game.Scene.AddComponent<NetInnerComponent, IPEndPoint, int>(processConfig.InnerIPPort,
                        SessionStreamDispatcherType.SessionStreamDispatcherServerInner);
                    //
                    var processScenes = StartSceneConfigCategory.Instance.GetByProcess(Game.Options.Process);
                    foreach (StartSceneConfig startConfig in processScenes)
                    {
                        await SceneFactory.Create(Game.Scene, startConfig.Id, startConfig.InstanceId, startConfig.Zone, startConfig.Name,
                            startConfig.Type, startConfig);
                    }

                    break;
                }
                case AppType.Watcher:
                {
                    StartMachineConfig startMachineConfig = WatcherHelper.GetThisMachineConfig();
                    WatcherComponent watcherComponent = Game.Scene.AddComponent<WatcherComponent>();
                    watcherComponent.Start(Game.Options.CreateScenes);
                    Game.Scene.AddComponent<NetInnerComponent, IPEndPoint, int>(
                        NetworkHelper.ToIPEndPoint($"{startMachineConfig.InnerIP}:{startMachineConfig.WatcherPort}"),
                        SessionStreamDispatcherType.SessionStreamDispatcherServerInner);
                    break;
                }
                case AppType.GameTool:
                    break;
            }

            if (Game.Options.Console == 1)
            {
                Game.Scene.AddComponent<ConsoleComponent>();
            }
        }
    }
}