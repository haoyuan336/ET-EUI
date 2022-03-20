namespace ET
{
    [MessageHandler]
    public class M2C_StartSceneChangeHandler: AMHandler<M2C_StartSceneChange>
    {
        protected override async ETTask Run(Session session, M2C_StartSceneChange message)
        {
            await SceneChangeHelper.SceneChangeTo(session.ZoneScene(), message.SceneName, message.SceneInstanceId);
            Log.Debug($"Enter Map Scene success {message.SceneName}");
            // Game.EventSystem.Publish(new EventType.ShowMatchButtonUIMessage(){zoneScene = session.ZoneScene()});
        }
    }
}