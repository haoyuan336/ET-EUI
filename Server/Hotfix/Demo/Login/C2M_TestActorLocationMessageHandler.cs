namespace ET
{
    [ActorMessageHandler]
    public class C2M_TestActorLocationMessageHandler: AMActorLocationHandler<Unit, C2M_TestActorLocationMessage>
    {
        protected override async ETTask Run(Unit entity, C2M_TestActorLocationMessage message)
        {
            Log.Debug("C2M_TestActorLocationMessageHandler" + message.Content);
            MessageHelper.SendToClient(entity, new M2C_TestActorMessage() { Content = "CCCCCCCCCCCCCCCCC" });
            await ETTask.CompletedTask;
        }
    }
}