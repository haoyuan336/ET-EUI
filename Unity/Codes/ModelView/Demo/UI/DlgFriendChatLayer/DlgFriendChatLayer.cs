using System.Collections.Generic;

namespace ET
{
    public class DlgFriendChatLayer: Entity, IAwake
    {
        public DlgFriendChatLayerViewComponent View
        {
            get => this.Parent.GetComponent<DlgFriendChatLayerViewComponent>();
        }

        public Dictionary<int, Scroll_ItemChat> ItemChats = new Dictionary<int, Scroll_ItemChat>();

        // public 
        public List<ChatInfo> ChatInfos = new List<ChatInfo>();

        public AccountInfo ChatToAccountInfo;
    }
}