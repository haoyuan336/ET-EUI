using System;
using System.Collections.Generic;

namespace ET
{
    public class C2M_PlayerChooseLevelNumRequestHandler: AMActorLocationRpcHandler<Unit, C2M_PlayerChooseLevelNumRequest,
        M2C_PlayerChooseLevelNumResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_PlayerChooseLevelNumRequest request, M2C_PlayerChooseLevelNumResponse response,
        Action reply)
        {
            var accountId = request.Account;
            var levelNum = request.LevelNum;

            List<Account> accounts = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<Account>(a => a.Id.Equals(accountId) && a.State == (int) StateType.Active);
            if (accounts.Count > 0)
            {
                accounts[0].PVELevelNumber = levelNum;
                await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(accounts[0]);
            }

            response.Error = ErrorCode.ERR_Success;
            accounts[0].Dispose();
            reply();
        }
    }
}