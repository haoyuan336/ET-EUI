using System;

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
    }
}