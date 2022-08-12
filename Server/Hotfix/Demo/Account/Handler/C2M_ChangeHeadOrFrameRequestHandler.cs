using System;
using System.Collections.Generic;

namespace ET
{
    public class C2M_ChangeHeadOrFrameRequestHandler: AMActorLocationRpcHandler<Unit, C2M_ChangePlayerHeadOrFrameRequest,
        M2C_ChangePlayerHeadOrFrameResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_ChangePlayerHeadOrFrameRequest request, M2C_ChangePlayerHeadOrFrameResponse response,
        Action reply)
        {
            var accountId = request.AccountId;
            // var configId = request.ConfigId;
            var type = request.HeadType;
            long itemId = request.ItemId;

            //取出来用户信息
            List<Account> accounts = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<Account>(a => a.Id.Equals(accountId) && a.State == (int) StateType.Active);
            if (accounts.Count == 0)
            {
                response.Error = ErrorCode.ERR_NotFoundPlayer;
                reply();
                return;
            }

            Account account = accounts[0];
            switch (type)
            {
                case (int) HeadImageType.Head:
                    // account.HeadImageConfigId = configId;
                    account.HeadImageItemId = itemId;
                    break;
                // case (int) HeadImageType.HeadFrame:
                    // account.HeadFrameImageConfigId = configId;
                    // break;
            }

            await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(account);
            response.Error = ErrorCode.ERR_Success;
            response.AccountInfo = account.GetInfo();
            reply();
            account.Dispose();
        }
    }
}