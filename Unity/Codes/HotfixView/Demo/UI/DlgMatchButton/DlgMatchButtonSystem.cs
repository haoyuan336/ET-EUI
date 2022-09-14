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
            if (!self.IsMatching)
            {
                int errcode = await MatchHelper.MatchRoom(self.ZoneScene());
                self.IsMatching = true;
                Log.Debug("Match room message send success");
                if (errcode == ErrorCode.ERR_Success)
                {
                    self.View.E_MatchText.text = "Cancel Match";
                }
            }
            else
            {
                self.View.E_MatchingCountText.text = "当前匹配人数:0";
                int errocde = await MatchHelper.CancelMatch(self.ZoneScene());
                self.IsMatching = false;
                Log.Debug("Cancel Match room send Success");
                if (errocde == ErrorCode.ERR_Success)
                {
                    self.View.E_MatchText.text = "Match Room";
                }
            }
            // Log.Debug("Match Button click");
        }

        public static void ShowWindow(this DlgMatchButton self, Entity contextData = null)
        {
        }

        public static void UpdateCurrentMatchingCount(this DlgMatchButton self, int count)
        {
            Log.Debug("Update current matching count" + count);
            self.View.E_MatchingCountText.text = $"当前匹配人数:{count.ToString()}";
        }
    }
}