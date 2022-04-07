namespace ET
{
    public class C2M_GameReadMessageHandler: AMActorLocationHandler<Unit, C2M_GameReadyMessage>
    {
        protected override async ETTask Run(Unit unit, C2M_GameReadyMessage message)
        {
            Log.Debug("unit game ready");
            PVERoom pveRoom = unit.DomainScene().GetComponent<PVERoomComponent>().AddChild<PVERoom>();
            unit.AccountId = message.AccountId;
            pveRoom.PlayerGameReady(unit, message.AccountId);

            await ETTask.CompletedTask;
        }
    }
}