using System.Collections.Generic;
using System.Linq;

namespace ET.Handler
{
    public class C2M_PlayerRegisterMailMessageHandler: AMActorLocationHandler<Unit, C2M_RegisterNewMailBoxMessage>
    {
        protected override async ETTask Run(Unit unit, C2M_RegisterNewMailBoxMessage message)
        {
            long accountId = message.AccountId;
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginAccount, accountId.GetHashCode()))
            {
                await unit.DomainScene().GetComponent<MailComponent>().NewPlayerEnter(message, unit);
              
            }

            await ETTask.CompletedTask;
        }
    }
}