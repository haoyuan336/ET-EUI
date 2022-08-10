using System;
using System.Collections.Generic;

namespace ET
{
    public class C2M_EnterChangeTempSceneRequestHandler: AMActorLocationRpcHandler<Unit, C2M_EnterChangeTempSceneRequest,
        M2C_EnterChangeTempSceneResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_EnterChangeTempSceneRequest request, M2C_EnterChangeTempSceneResponse response,
        Action reply)
        {
            var zone = unit.DomainZone();
            //收钱退出当前房间
            PVERoom pveRoom = unit.DomainScene().GetComponent<RoomComponent>().GetChild<PVERoom>(request.RoomId);

            if (pveRoom != null)
            {
                foreach (var target in pveRoom.GetComponent<FightComponent>().Units)
                {
                    target.RemoveAllChild<HeroCard>();
                }

                // room.PlayerExitGame(request.Account);
                pveRoom.Dispose();
            }

            StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(unit.DomainZone(), "ChangeTempScene");
            await TransferHelper.Transfer(unit, startSceneConfig.InstanceId, startSceneConfig.Name);

            
            //然后在发送进入游戏的消息
            
            
            //将当前选择的队伍id 保存
            //传送至pve游戏地图

            response.Error = ErrorCode.ERR_Success;
            reply();

            await ETTask.CompletedTask;
        }
    }
}