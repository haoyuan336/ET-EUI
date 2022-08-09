using System;

namespace ET
{
    public class C2M_ReadyToPVPRoomRequestHandler: AMActorLocationRpcHandler<Unit , C2M_ReadyToPVPRoomRequest, M2C_ReadyToPVPRoomResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_ReadyToPVPRoomRequest request, M2C_ReadyToPVPRoomResponse response, Action reply)
        {
            
            response.Error = ErrorCode.ERR_Success;
            reply();
            
            StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(unit.DomainZone(), "PVPGameScene");
            await TransferHelper.Transfer(unit, startSceneConfig.InstanceId, startSceneConfig.Name);
   
            await ETTask.CompletedTask;
        }
    }
}