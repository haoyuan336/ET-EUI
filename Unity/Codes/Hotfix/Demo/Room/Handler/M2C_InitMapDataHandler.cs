﻿namespace ET
{
    public class M2C_InitMapDataHandler : AMHandler<M2C_InitMapData>
    {
        protected override async ETTask Run(Session session, M2C_InitMapData message)
        {
            // foreach (var diamondInfo in message.DiamondInfo)
            // {
            //     session.DomainScene().GetComponent<DiamondComponent>().CreateDiamoneWithMessage(diamondInfo);
            // }
            DiamondComponent diamondComponent = session.ZoneScene().CurrentScene().AddComponent<DiamondComponent>();
            session.ZoneScene().CurrentScene().AddComponent<RoomComponent>();

            await Game.EventSystem.PublishAsync(new EventType.InitObjectPool()
            {
                Scene = session.ZoneScene()
            });
            
            diamondComponent.InitMapWithMessage(message);
            await ETTask.CompletedTask;
        }
    }
}