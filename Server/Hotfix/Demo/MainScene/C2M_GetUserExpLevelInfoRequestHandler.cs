using System;
using System.Collections.Generic;

namespace ET.MainScene
{
    public class C2M_GetUserExpLevelInfoRequestHandler: AMActorLocationRpcHandler<Unit, C2M_GetUserExpInfoRequest, M2C_GetUserExpInfoResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_GetUserExpInfoRequest request, M2C_GetUserExpInfoResponse response, Action reply)
        {
            long AccountId = request.AccountId;
            Log.Debug($"account id {AccountId}");
            List<Account> accounts = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Query<Account>(a => a.Id.Equals(AccountId));
            // List<Account> accounts = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Query<Account>(a => a.Id.Equals(AccountId));
            Log.Debug($"accounts {accounts.Count}");
            if (accounts.Count > 0)
            {
                response.Error = ErrorCode.ERR_Success;
                response.UserLevel = accounts[0].Level == 0? 1 : accounts[0].Level;
                response.Exp = accounts[0].Exp;
                response.UserName = accounts[0].AccountName;
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