using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace ET
{
    public class C2M_GetAccountInfoWithIdRequestHandler: AMActorLocationRpcHandler<Unit, C2M_GetAccountInfoWithAccountIdRequest,
        M2C_GetAccountInfoWidthAccointIdResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_GetAccountInfoWithAccountIdRequest request,
        M2C_GetAccountInfoWidthAccointIdResponse response,
        Action reply)
        {
            long accountId = request.AccountId;

            List<Account> accounts = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<Account>(a => a.Id.Equals(accountId) && a.State == (int) StateType.Active);
            if (accounts.Count == 0)
            {
                response.Error = ErrorCode.ERR_NotFoundPlayer;
                reply();
                return;
            }

            Account account = accounts[0];
            response.AccountInfo = account.GetInfo();
            response.Error = ErrorCode.ERR_Success;
            reply();
            account.Dispose();
        }
    }
}