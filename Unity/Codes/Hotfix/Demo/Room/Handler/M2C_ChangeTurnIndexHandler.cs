namespace ET
{
    public class M2C_ChangeTurnIndexHandler: AMHandler<M2C_ChangeCurrentTurnSeatIndex>
    {
        protected override async ETTask Run(Session session, M2C_ChangeCurrentTurnSeatIndex message)
        {
            session.DomainScene().GetComponent<PlayerComponent>().CurrentTurnIndex = message.CurrentTurnIndex;
            Game.EventSystem.Publish(new EventType.UpdateCurrentTurnSeatIndex(){TurnIndex = message.CurrentTurnIndex, zoneScene = session.DomainScene()});
            await ETTask.CompletedTask;
        }
    }
}