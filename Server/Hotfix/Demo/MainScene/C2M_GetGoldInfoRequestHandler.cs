using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace ET.MainScene
{
    public class C2M_GetGoldInfoRequestHandler: AMActorLocationRpcHandler<Unit, C2M_GetGoldInfoRequest, M2C_GetGoldInfoResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_GetGoldInfoRequest request, M2C_GetGoldInfoResponse response, Action reply)
        {
            long AccountId = request.AccountId;
            Log.Debug($"get gold info {AccountId}");

            List<Account> list = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Query<Account>(a => a.Id.Equals(AccountId));

            if (list.Count > 0)
            {
                response.Error = ErrorCode.ERR_Success;
                response.GoldCount = list[0].GoldCount;
                response.PowerCount = list[0].PowerCount;
                response.DiamondCount = list[0].DiamondCount;
            }

            reply();
            await ETTask.CompletedTask;
        }
    }
}