namespace ET
{
    public class M2C_InitMapDataHandler : AMHandler<M2C_InitMapData>
    {
        protected override async ETTask Run(Session session, M2C_InitMapData message)
        {
            Log.Debug("init map data" + message.DiamondInfo);
            await ETTask.CompletedTask;
        }
    }
}