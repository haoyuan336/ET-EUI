using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ET
{
    public static class FriendsSystem
    {
        public static FriendInfo GetFriendInfo(this Friends self)
        {
            return new FriendInfo()
            {
                FriendsId = self.Id,
                OwnerId = self.OwnerId,
                FriendId = self.FriendId,
                CreateTime = self.CreateTime
                // IsGift = self.IsGift
            };
        }
    }
}