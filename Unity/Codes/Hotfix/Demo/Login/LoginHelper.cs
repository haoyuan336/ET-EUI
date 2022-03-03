using System;
using System.Runtime.InteropServices;
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
                a2CLoginAccount = (A2C_LoginAccount) await session.Call(new C2A_LoginAccount() { AccountName = account, Password = password });
            }
            catch (Exception e)
            {
                Log.Debug(e.ToString());
                session?.Dispose();
                return a2CLoginAccount.Error;
            }

            if (a2CLoginAccount.Error == ErrorCode.ERR_Success)
            {
            }

            await ETTask.CompletedTask;
            return ErrorCode.ERR_Success;
        }
    }
}