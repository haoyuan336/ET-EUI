using System;
using System.Collections.Generic;

namespace ET
{
    public class C2M_AddPowerRequestHandler: AMActorLocationRpcHandler<Unit, C2M_AddPowerRequest, M2C_AddPowerResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_AddPowerRequest request, M2C_AddPowerResponse response, Action reply)
        {
            long AccountId = request.AccountId;
            int count = request.Count;
            List<Account> accounts = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Query<Account>(a => a.Id.Equals(AccountId));
            if (accounts.Count > 0)
            {
                Account account = accounts[0];
                account.PowerCount += count;
                await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(account);
                response.Error = ErrorCode.ERR_Success;
                response.Count = account.PowerCount;
            }
            else
            {
                response.Error = ErrorCode.ERR_NotFoundPlayer;
            }

            reply();
            await ETTask.CompletedTask;
        }
    }
}