using System.Collections;
using System.Collections.Generic;
using System;
using ET.EventType;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public class MatchSuccess: AEvent<EventType.ShowMatchPVPRoomSuccessAnim>
    {
        protected override async ETTask Run(ShowMatchPVPRoomSuccessAnim a)
        {
            Scene scene = a.Scene;
            Log.Debug("播放匹配成功动画");
            UIComponent uiComponent = scene.CurrentScene().GetComponent<UIComponent>();
            UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_PVPMatchFightLayer);
            baseWindow.GetComponent<DlgPVPMatchFightLayer>().MatchSuccess();
            await ETTask.CompletedTask;
        }
    }

    public static class DlgPVPMatchFightLayerSystem
    {
        public static void RegisterUIEvent(this DlgPVPMatchFightLayer self)
        {
            self.View.E_BackButton.AddListener(() =>
            {
                self.CancelMatch();
                self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_PVPMatchFightLayer);
            },ConstValue.BackButtonAudioStr);
            self.View.E_CancelMatchButton.AddListener(self.OnCancelMatchButton);

            self.Loop();
        }

        public static void OnCancelMatchButton(this DlgPVPMatchFightLayer self)
        {
            //取消匹配按钮
            Log.Debug("取消匹配按钮");
            if (self.IsMatching)
            {
                self.CancelMatch();
            }
            else
            {
                self.ToMatch();
            }
        }

        public static async void CancelMatch(this DlgPVPMatchFightLayer self)
        {
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            C2M_CancelMatchRoomRequest request = new C2M_CancelMatchRoomRequest();
            M2C_CancelMatchRoomResponse response = await session.Call(request) as M2C_CancelMatchRoomResponse;
            if (response.Error == ErrorCode.ERR_Success)
            {
                self.View.E_CancelText.text = "MATCH";
                self.IsMatching = false;
                self.MatchTime = 0;
                self.UpdateCountDowmView();
            }
        }

        public static async void ToMatch(this DlgPVPMatchFightLayer self)
        {
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            C2M_MatchRoomRequest request = new C2M_MatchRoomRequest();
            M2C_MatchRoomResponse response = await session.Call(request) as M2C_MatchRoomResponse;
            if (response.Error == ErrorCode.ERR_Success)
            {
                Log.Debug("匹配消息发送成功");
                self.IsMatching = true;
                self.View.E_CancelText.text = "CANCEL";
                self.StartMatchCountDown();
            }

            await ETTask.CompletedTask;
        }

        public static void ShowWindow(this DlgPVPMatchFightLayer self, Entity contextData = null)
        {
            self.IsShow = true;
            self.ToMatch();
            // request.
        }

        public static void StartMatchCountDown(this DlgPVPMatchFightLayer self)
        {
            //开始匹配倒计时
            self.MatchTime = 0;
        }

        public static void HideWindow(this DlgPVPMatchFightLayer self)
        {
            self.IsShow = false;
        }

        public static async void Loop(this DlgPVPMatchFightLayer self)
        {
            while (!self.IsDisposed)
            {
                if (self.IsShow)
                {
                    if (self.IsMatching)
                    {
                        self.MatchTime++;
                        self.UpdateCountDowmView();
                    }
                }

                await TimerComponent.Instance.WaitAsync(1000);
            }
        }

        public static void UpdateCountDowmView(this DlgPVPMatchFightLayer self)
        {
            var min = (int)(self.MatchTime / 60);
            var second = self.MatchTime % 60;
            var minStr = string.Format("{00:00}", min);
            var secStr = string.Format("{00:00}", second);
            self.View.E_MatchTimeCountText.text = $"{minStr}: {secStr}";
        }

        public static async void MatchSuccess(this DlgPVPMatchFightLayer self)
        {
            Log.Debug("匹配成功");
            self.IsMatching = false;

            self.View.E_CancelMatchButton.interactable = false;
            self.View.E_BackButton.interactable = false;

            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            // await uiComponent.ShowWindow(WindowID.WindowID_AlertLayer);
            // UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_AlertLayer);
            // baseWindow.GetComponent<DlgAlertLayer>().SetText("匹配房间成功");
            await TimerComponent.Instance.WaitAsync(1000);
            // uiComponent.HideWindow(WindowID.WindowID_AlertLayer);
            long accountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            C2M_ReadyToPVPRoomRequest request = new C2M_ReadyToPVPRoomRequest();
            request.Account = accountId;
            M2C_ReadyToPVPRoomResponse response = await session.Call(request) as M2C_ReadyToPVPRoomResponse;
            if (response.Error == ErrorCode.ERR_Success)
            {
                // Log.Debug("发送准备好进入房间的消息");
                uiComponent.HideWindow(WindowID.WindowID_PVPMatchFightLayer);
                uiComponent.HideWindow(WindowID.WindowID_PVPSceneLayer);
                uiComponent.HideWindow(WindowID.WindowID_PVPFightPrepareLayer);
                uiComponent.HideWindow(WindowID.WindowID_GoldInfoUI);
            }
        }
    }
}