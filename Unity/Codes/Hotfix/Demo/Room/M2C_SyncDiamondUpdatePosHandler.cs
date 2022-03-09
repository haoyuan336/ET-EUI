namespace ET
{
    public class M2C_SyncDiamondUpdatePosHandler: AMHandler<M2C_SyncDiamondUpdatePos>
    {
        protected override async ETTask Run(Session session, M2C_SyncDiamondUpdatePos message)
        {
            DiamondComponent diamondComponent = session.DomainScene().GetComponent<DiamondComponent>();
            foreach (var diamondInfo in message.DiamondInfos)
            {
                Diamond diamond = diamondComponent.GetChild<Diamond>(diamondInfo.Id);
                diamond.UpdateIndex(diamondInfo.LieIndex, diamondInfo.HangIndex);
            }
            await ETTask.CompletedTask;
        }
    }
}