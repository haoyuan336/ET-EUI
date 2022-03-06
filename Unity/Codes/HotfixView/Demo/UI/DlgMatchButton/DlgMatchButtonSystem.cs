using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public class DlgMatchButtonAwakeSystem: AwakeSystem<DlgMatchButton>
    {
        public override async void Awake(DlgMatchButton self)
        {
            // int errorCode = await LoginHelper.LoginGateServer(self.ZoneScene());
            await ETTask.CompletedTask;
        }
    }

    public static class DlgMatchButtonSystem
    {
        public static void RegisterUIEvent(this DlgMatchButton self)
        {
            self.View.E_MatchButtonButton.AddListener(() => { self.OnClickEventHandler(); });
        }

        public static async void OnClickEventHandler(this DlgMatchButton self)
        {
            // Log.Debug("Match Button click");
            int errcode =  await MatchHelper.MatchRoom(self.ZoneScene());
            Log.Debug("Match room message send success");
        }

        public static void ShowWindow(this DlgMatchButton self, Entity contextData = null)
        {
        }

        public static void UpdateCurrentMatchingCount(this DlgMatchButton self, int count)
        {
            Log.Debug("Update current matching count" + count);
            self.View.E_MatchingCountText.text = count.ToString();
        }
    }
}