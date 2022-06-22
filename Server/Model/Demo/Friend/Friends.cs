using System;
using System.Data.SqlTypes;
using System.Text;

namespace ET
{
    public class Friends: Entity
    {
        public long OwnerId;
        public long FriendId;
        public long CreateTime = TimeHelper.ServerNow();
        public int State = (int)StateType.Active;
        public bool IsGift = false;  //是否赠送了礼物
    }
}