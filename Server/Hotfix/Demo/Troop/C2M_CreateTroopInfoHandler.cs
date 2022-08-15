// using System;
// using System.Collections.Generic;
// using System.Diagnostics;
//
// namespace ET
// {
//     public class C2M_CreateTroopInfoHandler: AMActorLocationRpcHandler<Unit, C2M_CreateTroopRequest, M2C_CreateTroopResponse>
//     {
//         protected override async ETTask Run(Unit unit, C2M_CreateTroopRequest request, M2C_CreateTroopResponse response, Action reply)
//         {
//             long Account = request.AccountId;
//
//             List<Troop> troops = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Query<Troop>((a) => a.OnwerId == Account);
//             if (troops.Count >= 6)
//             {
//                 response.Error = ErrorCode.ERR_TroopCountIsFull;
//                 reply();
//                 return;
//             }
//
//             Troop troop = new Troop();
//             troop.Id = IdGenerater.Instance.GenerateId();
//             troop.OnwerId = Account;
//             troop.CreateTime = TimeHelper.DateTimeNow().ToString();
//             response.TroopInfo = troop.GetTroopMessageInfo();
//             response.Error = ErrorCode.ERR_Success;
//             reply();
//             await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(troop);
//             await ETTask.CompletedTask;
//         }
//     }
// }