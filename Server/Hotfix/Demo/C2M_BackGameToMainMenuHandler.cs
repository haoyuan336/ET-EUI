using System;
using System.Collections.Generic;

namespace ET
{
    public class C2M_BackGameToMainMenuHandler: AMActorLocationRpcHandler<Unit, C2M_BackGameToMainMenuRequest, M2C_BackGameToMainMenuResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_BackGameToMainMenuRequest request, M2C_BackGameToMainMenuResponse response, Action reply)
        {
            //消除卡牌
            RoomComponent roomComponent = unit.DomainScene().GetComponent<RoomComponent>();
            response.Error = ErrorCode.ERR_Success;
            FightComponent fightComponent;

            switch (unit.DomainScene().SceneType)
            {
                case SceneType.PVEGameScene:
                    var pveRoom = roomComponent.GetChild<PVERoom>(request.RoomId);
                    fightComponent = pveRoom.GetComponent<FightComponent>();
                    foreach (var currentUnit in fightComponent.Units)
                    {
                        List<HeroCard> heroCards = currentUnit.GetChilds<HeroCard>();
                        foreach (var heroCard in heroCards)
                        {
                            heroCard.Dispose();
                        }
                    }
                    fightComponent.Dispose();
                    break;
                case SceneType.PVPGameScene:
                    var pvpRoom = roomComponent.GetChild<PVPRoom>(request.RoomId);
                    fightComponent = pvpRoom.GetComponent<FightComponent>();
                    foreach (var currentUnit in fightComponent.Units)
                    {
                        List<HeroCard> heroCards = currentUnit.GetChilds<HeroCard>();
                        foreach (var heroCard in heroCards)
                        {
                            heroCard.Dispose();
                        }
                    }
                    fightComponent.Dispose();
                    break;
            }

           

            response.Error = ErrorCode.ERR_Success;
            reply();
            StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(unit.DomainZone(), "MainScene");
            //将unit 手里面的卡牌删掉

            await TransferHelper.Transfer(unit, startSceneConfig.InstanceId, startSceneConfig.Name);
            await ETTask.CompletedTask;
        }
    }
}