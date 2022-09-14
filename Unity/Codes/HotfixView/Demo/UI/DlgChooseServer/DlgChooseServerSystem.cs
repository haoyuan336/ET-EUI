
using System;


namespace ET
{
    public static class DlgChooseServerSystem
    {
        public static async void RegisterUIEvent(this DlgChooseServer self)
        {
            self.View.E_OKButton.AddListenerAsync(() => { return self.OKOnClick(); });
            self.View.E_Label1Text.text = "name";

            await LoginHelper.GetServerInfo(self.DomainScene());
            UnityEngine.UI.Text[] list = new UnityEngine.UI.Text[2] { self.View.E_Label1Text, self.View.E_Label2Text };
            UnityEngine.UI.Toggle[] toggles = new UnityEngine.UI.Toggle[2] { self.View.E_Choose1Toggle, self.View.E_Choose2Toggle };
        
            
            int index = 0;
            // Debug.Log($"count={self.ZoneScene().GetComponent<ServerInfosComponent>().ServerInfos.Count}");
            foreach (var serverInfo in self.DomainScene().GetComponent<ServerInfosComponent>().ServerInfos)
            {
                Log.Debug($"index{index}");
                list[index].text = serverInfo.ServerName;
                self.AddToggleOnClickListener(toggles[index], serverInfo);
                index++;
            }
        }

        public static void AddToggleOnClickListener(this DlgChooseServer self, UnityEngine.UI.Toggle toggle, ServerInfo serverInfo)
        {
            toggle.AddListener(delegate(bool arg0)
            {
                // Log.Debug("value change " + arg0);
                if (arg0)
                {
                    Log.Debug($"choose server info{serverInfo.Id}");
                    self.ZoneScene().GetComponent<ServerInfosComponent>().SetCurrentServerId(serverInfo.Id);
                }
            });
           
        }

        public static void ToggleClickListener(this DlgChooseServer self, bool value)
        {
            Log.Debug("value = " + value);
        }

        public static async ETTask OKOnClick(this DlgChooseServer self)
        {
            Log.Debug("choose server ok button click" + self.ZoneScene().GetComponent<ServerInfosComponent>().CurrentServerId);
            if (self.DomainScene().GetComponent<ServerInfosComponent>().CurrentServerId == 0)
            {
                Log.Error("Please choose server");
                return;
            }

            try
            {
                int errorCode = await LoginHelper.GetRealmKey(self.ZoneScene());
                // await LoginHelper.GetServerInfo(self.DomainScene());
                if (errorCode != ErrorCode.ERR_Success)
                {
                    Log.Error(errorCode.ToString());
                    return;
                }

                errorCode = await LoginHelper.EnterGame(self.ZoneScene());
                if (errorCode == ErrorCode.ERR_Success)
                {
                    self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_ChooseServer);
                    errorCode = await LoginHelper.LoginGateServer(self.ZoneScene());
                    if (errorCode == ErrorCode.ERR_Success)
                    {
                        // self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_MatchButton);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }

            await ETTask.CompletedTask;
        }

        public static void ShowWindow(this DlgChooseServer self, Entity contextData = null)
        {
        }
    }
}