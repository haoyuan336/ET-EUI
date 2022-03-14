using System.Runtime.InteropServices;
using UnityEngine;

namespace ET
{
    public class ReferCurrentMatchingCountText: AEvent<EventType.ReferCurrentMatchingCountText>
    {
        protected override async ETTask Run(EventType.ReferCurrentMatchingCountText a)
        {
            Scene zoneScene = a.zoneScene;
            int count = a.Count;
            UIComponent uiComponent = zoneScene.GetComponent<UIComponent>();
            UIBaseWindow uiBaseWindow = uiComponent.AllWindowsDic[(int) WindowID.WindowID_MatchButton];
            if (uiBaseWindow.GetComponent<DlgMatchButton>() != null)
            {
                Log.Debug("have");
                uiBaseWindow.GetComponent<DlgMatchButton>().UpdateCurrentMatchingCount(count);
            }

            await ETTask.CompletedTask;
        }
    }

    public class SyncCreateRoomMessage: AEvent<EventType.SyncCreateRoomMessage>
    {
        protected override async ETTask Run(EventType.SyncCreateRoomMessage a)
        {

            // Scene zoneScene = a.zoneScene;
            // int count = a.Count;
            // UIComponent uiComponent = zoneScene.GetComponent<UIComponent>();
            // UIBaseWindow uiBaseWindow = uiComponent.AllWindowsDic[(int) WindowID.WindowID_MatchButton];
            Scene scene = a.zoneScene;
            scene.GetComponent<UIComponent>().HideWindow(WindowID.WindowID_MatchButton);
            // scene.GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_RoomInfo);
            scene.GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_GameUI);
            await ETTask.CompletedTask;
        }
    }
}