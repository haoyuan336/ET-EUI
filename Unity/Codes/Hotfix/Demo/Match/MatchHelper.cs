using System;
using System.Runtime.InteropServices;

namespace ET
{
    public static class MatchHelper
    {
        public static async ETTask<int> MatchRoom(Scene zoneScene)
        {
            // Log.Debug("Match helper");
            M2C_MatchRoomResponse m2CMatchRoomResponse = null;
            try
            {
                Session session = zoneScene.GetComponent<SessionComponent>().Session;
                m2CMatchRoomResponse = (M2C_MatchRoomResponse) await session.Call(new C2M_MatchRoomRequest() { });
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }

            if (m2CMatchRoomResponse.Error != ErrorCode.ERR_Success)
            {
                return m2CMatchRoomResponse.Error;
            }

            await ETTask.CompletedTask;
            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> CancelMatch(Scene zoneScene)
        {
            M2C_CancelMatchRoomResponse m2CCancelMatchRoomResponse = null;
            try
            {
                Session session = zoneScene.GetComponent<SessionComponent>().Session;
                m2CCancelMatchRoomResponse = (M2C_CancelMatchRoomResponse) await session.Call(new C2M_CancelMatchRoomRequest() { });
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }

            if (m2CCancelMatchRoomResponse.Error != ErrorCode.ERR_Success)
            {
                Log.Error(m2CCancelMatchRoomResponse.Error.ToString());
                return m2CCancelMatchRoomResponse.Error;
            }

            await ETTask.CompletedTask;
            return ErrorCode.ERR_Success;
        }
    }
}