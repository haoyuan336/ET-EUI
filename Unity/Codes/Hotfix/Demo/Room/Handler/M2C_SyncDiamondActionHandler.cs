using System;
using System.Collections.Generic;
using ET.EventType;

namespace ET
{
    public class M2C_SyncDiamondActionHandler: AMHandler<M2C_SyncDiamondAction>
    {
        protected override async ETTask Run(Session session, M2C_SyncDiamondAction message)
        {
            RoomComponent roomComponent = session.ZoneScene().CurrentScene().GetComponent<RoomComponent>();
            await roomComponent.ProcessActionMessage(message.ActionMessage);
            await Game.EventSystem.PublishAsync(new UnLockTouchLock() { ZoneScene = session.ZoneScene().CurrentScene(), IsLockTouch = false });
        }
    }
}