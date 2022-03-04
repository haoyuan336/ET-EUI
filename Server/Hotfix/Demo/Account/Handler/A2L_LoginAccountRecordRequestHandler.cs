using System;

namespace ET
{
    [ActorMessageHandler]
    public class A2L_LoginAccountRecordHandler: AMActorRpcHandler<Scene, A2L_LoginAccountRequest, L2A_LoginAccountResponse>
    {
        protected override async ETTask Run(Scene unit, A2L_LoginAccountRequest request, L2A_LoginAccountResponse response, Action reply)
        {
            long accountId = request.AccountId;
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginCenterLock, accountId.GetHashCode()))
            {
                if (!unit.GetComponent<LoginInfoRecordComponent>().IsExist(accountId))
                {
                    reply();
                    return;
                }

                int zoneScene = unit.GetComponent<LoginInfoRecordComponent>().Get(accountId);
                StartSceneConfig startSceneConfig = RealmGateAddressHelper.GetGate(zoneScene, accountId);

                G2L_DisconnectGateUnit g2LDisconnectGateUnit =
                        (G2L_DisconnectGateUnit) await MessageHelper.CallActor(startSceneConfig.InstanceId,
                            new L2G_DisconnectGateUnit() { AccountId = accountId });
                response.Error = g2LDisconnectGateUnit.Error;
                reply();
            }

            await ETTask.CompletedTask;
        }
    }
}