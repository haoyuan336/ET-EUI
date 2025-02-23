﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace ET
{
    public class C2M_StartPVEGameHandler: AMActorLocationRpcHandler<Unit, C2M_StartPVEGameRequest, M2C_StartPVEGameResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_StartPVEGameRequest request, M2C_StartPVEGameResponse response, Action reply)
        {
            Log.Debug($"start game {request.AccountId}");
            List<Account> accounts = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<Account>((a) => a.Id.Equals(request.AccountId));

            
            if (accounts.Count > 0)
            {
                // accounts[0].CurrentTroopId = request.TroopId;
                //将当前选择的队伍id 保存
                // await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(accounts[0]);
                response.Error = ErrorCode.ERR_Success;
                reply();
                //传送至pve游戏地图
                StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(unit.DomainZone(), "PVEGameScene");
                await TransferHelper.Transfer(unit, startSceneConfig.InstanceId, startSceneConfig.Name);
            }
            else
            {
                response.Error = ErrorCode.ERR_NotFoundPlayer;
                reply();

            }

            await ETTask.CompletedTask;
        }
    }
}