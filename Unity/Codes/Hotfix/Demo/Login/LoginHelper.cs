using System;
using System.Runtime.InteropServices;
using ET.Account;
using UnityEngine;

namespace ET
{
    public static class LoginHelper
    {
        public static async ETTask<int> Login(Scene zoneScene, string address, string account, string password)
        {
            A2C_LoginAccount a2CLoginAccount = null;
            Session session = null;
            try
            {
                session = zoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(address));
                // MD5Helper
                a2CLoginAccount =
                        (A2C_LoginAccount) await session.Call(new C2A_LoginAccount()
                        {
                            AccountName = account, Password = MD5Helper.StringToMD5(password)
                        });
            }
            catch (Exception e)
            {
                Log.Debug(e.ToString());
                session?.Dispose();
                return a2CLoginAccount.Error;
            }

            if (a2CLoginAccount.Error != ErrorCode.ERR_Success)
            {
                Log.Error(a2CLoginAccount.Error.ToString());

                return a2CLoginAccount.Error;
            }

            zoneScene.AddComponent<SessionComponent>().Session = session;
            zoneScene.GetComponent<AccountInfoComponent>().AccountId = a2CLoginAccount.AccountId;
            zoneScene.GetComponent<AccountInfoComponent>().Token = a2CLoginAccount.Token;
            zoneScene.GetComponent<SessionComponent>().Session.AddComponent<PingComponent>();
            await ETTask.CompletedTask;
            return ErrorCode.ERR_Success;
        }

        public static async ETTask LoginRealServer()
        {
            await ETTask.CompletedTask;
        }

        public static async ETTask<int> GetServerInfo(Scene zoneScene)
        {
            A2C_GetServerInfo a2CGetServerInfo = null;

            try
            {
                a2CGetServerInfo = (A2C_GetServerInfo) await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2A_GetServerInfo()
                {
                    AccountId = zoneScene.GetComponent<AccountInfoComponent>().AccountId,
                    Token = zoneScene.GetComponent<AccountInfoComponent>().Token,
                });
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }

            if (a2CGetServerInfo.Error != ErrorCode.ERR_Success)
            {
                Log.Error(a2CGetServerInfo.Error.ToString());
                return a2CGetServerInfo.Error;
            }

            zoneScene.GetComponent<ServerInfosComponent>().Clear();
            foreach (var serinfo in a2CGetServerInfo.ServerInfoList)
            {
                ServerInfo serverInfo = zoneScene.GetComponent<ServerInfosComponent>().AddChild<ServerInfo>();
                serverInfo.FromMessage(serinfo);
                zoneScene.GetComponent<ServerInfosComponent>().Add(serverInfo);
            }

            await ETTask.CompletedTask;
            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> GetRealmKey(Scene zoneScene)
        {
            A2C_GetRealmKey a2CGetRealmKey = null;
            try
            {
                a2CGetRealmKey = (A2C_GetRealmKey) await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2A_GetRealmKey()
                {
                    Token = zoneScene.GetComponent<AccountInfoComponent>().Token,
                    AccountId = zoneScene.GetComponent<AccountInfoComponent>().AccountId,
                    ServerId = zoneScene.GetComponent<ServerInfosComponent>().CurrentServerId
                });
            }
            catch (Exception e)
            {
                Log.Debug(e.ToString());
            }

            if (a2CGetRealmKey != null && a2CGetRealmKey.Error != ErrorCode.ERR_Success)
            {
                Log.Debug(a2CGetRealmKey.Error.ToString());
                return a2CGetRealmKey.Error;
            }
            Log.Debug("获得正确的key" + a2CGetRealmKey.RealmKey);

            
            await ETTask.CompletedTask;
            return ErrorCode.ERR_Success;
        }
    }
}