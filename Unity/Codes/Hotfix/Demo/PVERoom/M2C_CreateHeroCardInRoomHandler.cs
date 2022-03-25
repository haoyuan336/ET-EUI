namespace ET
{
    public class M2C_CreateHeroCardInRoomHandler: AMHandler<M2C_CreateHeroCardInRoom >
    {
        protected override async  ETTask Run(Session session, M2C_CreateHeroCardInRoom message)
        {
            // Log.Debug("create hero card");
            
            await ETTask.CompletedTask;
        }
    }
}