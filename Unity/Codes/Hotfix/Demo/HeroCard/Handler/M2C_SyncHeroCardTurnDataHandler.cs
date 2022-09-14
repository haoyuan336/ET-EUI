namespace ET
{
    public class M2C_SyncHeroCardTurnDataHandler: AMHandler<M2C_SyncHeroCardTurnData>
    {
        protected override async ETTask Run(Session session, M2C_SyncHeroCardTurnData message)
        {
            session.DomainScene().GetComponent<HeroCardComponent>().SyncHeroCardTurnData(message);
            Log.Debug("M2C_SyncHeroCardTurnDataHandler");
            await ETTask.CompletedTask;
        }
    }
}