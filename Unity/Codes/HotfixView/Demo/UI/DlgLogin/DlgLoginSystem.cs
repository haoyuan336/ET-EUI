using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgLoginSystem
    {
        public static void RegisterUIEvent(this DlgLogin self)
        {
            Log.Debug("DlgLoginSystem RegisterUIEvent");
            // self.View.E_LoginButton.AddListener(() => { self.OnLoginClickHandler(); });
            self.View.E_LoginButton.AddListenerAsync(() => { return self.OnLoginClickHandler(); });
            // self.View.E_CancelButton.AddListenerAsync(() =>
            // {
            //     return self.
            // });
            // self.View.E_LoginButton.AddListener(() =>
            // {
            //     Log.Debug("buttoin click");
            // });
            self.PlayBgMusic();
        }
        public static async void PlayBgMusic(this DlgLogin self)
        {
            //首先家在音频
            var audioClip = await AddressableComponent.Instance.LoadAssetByPathAsync<AudioClip>("Assets/Res/Audios/Main_1.mp3");
            AudioSource audioSource = self.View.uiTransform.GetComponent<AudioSource>();
            audioSource.clip = audioClip;
            audioSource.loop = true;
            audioSource.Play();
        }
        public static void OnCancelClickhandler(this DlgLogin self)
        {
        }

        public static void ShowWindow(this DlgLogin self, Entity contextData = null)
        {
        }

        public static async ETTask OnLoginClickHandler(this DlgLogin self)
        {
            Log.Debug("OnLoginClickHandler");
            if (self.View.E_AccountInputField.GetComponent<InputField>().text.Length == 0 ||
                self.View.E_PasswordInputField.GetComponent<InputField>().text.Length == 0)
            {
                return;
            }
            try
            {
                int errorCode = await LoginHelper.Login(self.DomainScene(),
                    ConstValue.LoginAddress,
                    self.View.E_AccountInputField.GetComponent<InputField>().text,
                    self.View.E_PasswordInputField.GetComponent<InputField>().text);
                // Log.Debug("View  errorcode = " + errorCode);
                if (errorCode != ErrorCode.ERR_Success)
                {
                    return;
                }
                //todo 显示登录成功之后的UI
                //登录成功，进入选择服务器页面

                self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Login);
                await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_LoadingLayer);
                await LoginHelper.GetServerInfo(self.DomainScene());
                // self.DomainScene().GetComponent<ServerInfosComponent>().CurrentServerId == 0
                ServerInfo serverInfo = self.DomainScene().GetComponent<ServerInfosComponent>().ServerInfos[0];
                self.DomainScene().GetComponent<ServerInfosComponent>().SetCurrentServerId(serverInfo.Id);
                try
                {
                    int realCode = await LoginHelper.GetRealmKey(self.ZoneScene());
                    // await LoginHelper.GetServerInfo(self.DomainScene());
                    if (realCode != ErrorCode.ERR_Success)
                    {
                        Log.Error(realCode.ToString());
                        return;
                    }

                    realCode = await LoginHelper.EnterGame(self.ZoneScene());
                    if (realCode == ErrorCode.ERR_Success)
                    {
                        realCode = await LoginHelper.LoginGateServer(self.ZoneScene());
                        if (realCode == ErrorCode.ERR_Success)
                        {
                            // self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_MatchButton);
                            // Addressables.LoadSceneAsync()
                            await AddressableComponent.Instance.LoadAssetsByLabelAsync<GameObject>("UI", (result) => { });
                            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_LoadingLayer);
                        }
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e.ToString());
                }
                // await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_ChooseServer);
            }
            catch (Exception e)
            {
                Log.Debug(e.ToString());
            }

            await ETTask.CompletedTask;
        }

        public static void HideWindow(this DlgLogin self)
        {
        }
    }
}