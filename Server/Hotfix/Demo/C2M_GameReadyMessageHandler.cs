namespace ET
{
    public class C2M_GameReadMessageHandler: AMActorLocationHandler<Unit, C2M_GameReadyMessage>
    {
        protected override async ETTask Run(Unit unit, C2M_GameReadyMessage message)
        {
            if (unit.DomainScene().SceneType == SceneType.PVEGameScene)
            {
                Log.Debug($"unit game readyP{unit.DomainScene().SceneType}");
                PVERoom pveRoom = unit.DomainScene().GetComponent<RoomComponent>().AddChild<PVERoom>();
                unit.AccountId = message.AccountId;
                pveRoom.PlayerGameReady(unit, message.AccountId);
            }
            else if (unit.DomainScene().SceneType == SceneType.PVPGameScene)
            {
                var roomId = unit.CurrentRoomId;
                // Log.Warning($"创建房间{roomId}");
                //玩家准备好游戏了
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.CreatePVPGameRoom, roomId.GetHashCode()))
                {
                    RoomComponent roomComponent = unit.DomainScene().GetComponent<RoomComponent>();
                    PVPRoom pvpRoom = roomComponent.GetChild<PVPRoom>(roomId);
                    // Log.Warning("pvpRoom");
                    if (pvpRoom == null || pvpRoom.IsDisposed)
                    {
                        pvpRoom = roomComponent.AddChildWithId<PVPRoom>(roomId);
                        // Log.Warning("创建对战房间成功");
                    }

                    pvpRoom.PlayerGameReady(unit);
                }
            }

            await ETTask.CompletedTask;
        }
    }
}