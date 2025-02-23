﻿using System.Collections.Generic;

namespace ET
{
    public class DlgFriendLayer: Entity, IAwake
    {
        public DlgFriendLayerViewComponent View
        {
            get => this.Parent.GetComponent<DlgFriendLayerViewComponent>();
        }

        public Dictionary<int, Scroll_ItemFriend> ItemFriends = new Dictionary<int, Scroll_ItemFriend>();

        public List<AccountInfo> AccountInfos = new List<AccountInfo>();

        // public List<FriendInfo> FriendInfos = new List<FriendInfo>();
        public Dictionary<long, FriendInfo> FriendInfos = new Dictionary<long, FriendInfo>();
        public Dictionary<long, ChatInfo> ChatInfosMap = new Dictionary<long, ChatInfo>();
    }
}