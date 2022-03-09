using System.Collections;
using System.Collections.Generic;
using System;
using ET.EventType;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgRoomInfoSystem
    {
        public static void RegisterUIEvent(this DlgRoomInfo self)
        {
        }

        public static void ShowWindow(this DlgRoomInfo self, Entity contextData = null)
        {
        }

        public static void UpdateRoomInfo(this DlgRoomInfo self, UpdateRoomInfo info)
        {
            self.View.E_RoomIDText.text = $"RoomId:{info.RoomId.ToString()}";
            self.View.E_TurnIndexText.text = $"TurnIndex:{info.TurnIndex.ToString()}";
            self.View.E_MySeatIndexText.text = $"MySeatIndex:{info.MySeatIndex.ToString()}";
        }
    }
}