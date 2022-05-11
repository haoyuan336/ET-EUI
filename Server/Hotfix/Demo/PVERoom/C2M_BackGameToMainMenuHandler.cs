using System;

namespace ET
{
    public class C2M_BackGameToMainMenuHandler: AMActorLocationRpcHandler<Unit, C2M_BackGameToMainMenuRequest, M2C_BackGameToMainMenuResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_BackGameToMainMenuRequest request, M2C_BackGameToMainMenuResponse response, Action reply)
        {
            switch (unit.DomainScene().SceneType)
            {
                case SceneType.PVEGameScene:
                    Log.Debug("receive player scrollscreen message");
                    PVERoomComponent roomComponent = unit.DomainScene().GetComponent<PVERoomComponent>();
                    PVERoom room = roomComponent.GetChild<PVERoom>(request.RoomId);

                    if (room != null)
                    {
                        foreach (var target in room.Units)
                        {
                            target.RemoveAllChild<HeroCard>();
                        }

                        // room.PlayerExitGame(request.Account);
                        room.Dispose();
                        response.Error = ErrorCode.ERR_Success;
                    }
                    else
                    {
                        response.Error = ErrorCode.ERR_NotFoundRoom;
                    }

                    break;
            }

            reply();
            StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(unit.DomainZone(), "MainScene");
            //将unit 手里面的卡牌删掉

            await TransferHelper.Transfer(unit, startSceneConfig.InstanceId, startSceneConfig.Name);
            await ETTask.CompletedTask;
        }
    }
}