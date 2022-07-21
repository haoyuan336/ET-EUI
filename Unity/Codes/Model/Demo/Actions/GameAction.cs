using System;

namespace ET
{
    public class GameAction: Entity
    {
        public long OwnerId;
        public int ConfigId;
        public int State = (int) StateType.Active;
        public long CreateTime = TimeHelper.ServerNow();
        public bool Win;
        public long FriendId; //好友id
        public bool IsReceived = false; //是否已领取
        public int Value = 0; //包含的值
    }
}