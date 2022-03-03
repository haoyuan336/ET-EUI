namespace ET
{
    public static class MatchHelper
    {
        public static async ETTask Match(Scene zoneScene)
        {
            // Log.Debug("Match helper");
            Session session = zoneScene.GetComponent<SessionComponent>().Session;
            session.Send(new C2M_MatchRoomActorLocationMessage() { Content = "Match request" });
            await ETTask.CompletedTask;
        }
    }
}