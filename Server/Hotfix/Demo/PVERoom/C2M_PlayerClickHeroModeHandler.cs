namespace ET
{
    public class C2M_PlayerClickHeroModeHandler: AMActorLocationHandler<Unit,C2M_PlayerClickHeroMode>
    {
        protected override async ETTask Run(Unit unit, C2M_PlayerClickHeroMode message)
        {
            // Log.Warning($"receive player click hero mode message  {message.HeroId}");
            // Log
            PVERoom pveRoom = unit.DomainScene().GetComponent<RoomComponent>().GetChild<PVERoom>(message.RoomId);
            // unit.AccountId = message.AccountId;
            // pveRoom.PlayerGameReady(unit, message.AccountId);
            pveRoom.PlayChooseAttackHero(message.HeroId);
            await ETTask.CompletedTask;
        }
    }
}