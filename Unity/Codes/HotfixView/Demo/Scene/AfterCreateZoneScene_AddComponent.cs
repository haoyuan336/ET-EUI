namespace ET
{
    public class AfterCreateZoneScene_AddComponent: AEvent<EventType.AfterCreateZoneScene>
    {
        protected override async ETTask Run(EventType.AfterCreateZoneScene args)
        {
            Scene zoneScene = args.ZoneScene;
            zoneScene.AddComponent<UIComponent>();
            zoneScene.AddComponent<UIPathComponent>();
            zoneScene.AddComponent<UIEventComponent>();
            zoneScene.AddComponent<RedDotComponent>();
            zoneScene.AddComponent<CameraComponent>();
            zoneScene.AddComponent<AudioComponent>();
            // zoneScene.AddComponent<ResourcesLoaderComponent>();
        
            // zoneScene.AddComponent<>()
            await zoneScene.GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Login);
            // zoneScene.GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_MatchButton);
            await ETTask.CompletedTask;
        }
    }
}