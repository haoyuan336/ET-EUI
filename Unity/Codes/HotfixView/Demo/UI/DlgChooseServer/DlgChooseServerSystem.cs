using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{

    public static class DlgChooseServerSystem
    {
        public static async void RegisterUIEvent(this DlgChooseServer self)
        {
            self.View.E_OKButton.AddListenerAsync(() => { return self.OKOnClick(); });
            self.View.E_Label1Text.text = "name";

            await LoginHelper.GetServerInfo(self.ZoneScene());
            Text[] list = new Text[2] { self.View.E_Label1Text, self.View.E_Label2Text };
            Toggle[] toggles = new Toggle[2] { self.View.E_Choose1Toggle, self.View.E_Choose2Toggle };

            int index = 0;
            // Debug.Log($"count={self.ZoneScene().GetComponent<ServerInfosComponent>().ServerInfos.Count}");
            foreach (var serverInfo in self.ZoneScene().GetComponent<ServerInfosComponent>().ServerInfos)
            {
                Log.Debug($"index{index}");
                list[index].text = serverInfo.ServerName;
                self.AddToggleOnClickListener(toggles[index], serverInfo);
                index++;
            }
        }

        public static void AddToggleOnClickListener(this DlgChooseServer self, Toggle toggle, ServerInfo serverInfo)
        {
            toggle.onValueChanged.AddListener(delegate(bool arg0)
            {
                // Log.Debug("value change " + arg0);
                if (arg0)
                {
                    Log.Debug($"choose server info{serverInfo.Id}");
                    self.ZoneScene().GetComponent<ServerInfosComponent>().SetCurrentServerId(serverInfo.Id);
                    
                }
            });
        }

        public static async ETTask OKOnClick(this DlgChooseServer self)
        {
            Log.Debug("choose server ok button click" + self.ZoneScene().GetComponent<ServerInfosComponent>().CurrentServerId);

            LoginHelper.GetRealmKey(self.ZoneScene());
            await ETTask.CompletedTask;
        }

        public static void ShowWindow(this DlgChooseServer self, Entity contextData = null)
        {
        }
    }
}