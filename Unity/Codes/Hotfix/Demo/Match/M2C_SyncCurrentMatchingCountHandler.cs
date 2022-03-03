namespace ET
{
    public class M2C_SyncCurrentMatchingCountHandler: AMHandler<M2C_SyncCurrentMatchingCount>
    {
        protected override async ETTask Run(Session session, M2C_SyncCurrentMatchingCount message)
        {
            Log.Debug("sync current matching player count " + message.Content);
            await ETTask.CompletedTask;
        }
    }
}