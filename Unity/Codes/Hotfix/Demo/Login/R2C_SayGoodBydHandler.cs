using UnityEngine;

namespace ET
{
    [MessageHandler]
    public class R2C_SayGoodBydHandler:AMHandler<R2C_SayGoodBye>
    {
        protected override async  ETTask Run(Session session, R2C_SayGoodBye message)
        {
            Log.Debug($"good byg messgae={message.GoodBye}");
            await ETTask.CompletedTask;
        }
    }
}