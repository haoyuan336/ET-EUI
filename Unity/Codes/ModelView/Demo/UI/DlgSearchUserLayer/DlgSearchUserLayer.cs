using System.Collections.Generic;

namespace ET
{
    public class DlgSearchUserLayer: Entity, IAwake
    {
        public DlgSearchUserLayerViewComponent View
        {
            get => this.Parent.GetComponent<DlgSearchUserLayerViewComponent>();
        }

        public Dictionary<int, Scroll_ItemRecommend> ItemRecommends = new Dictionary<int, Scroll_ItemRecommend>();
        public List<AccountInfo> RecommendAccountInfos = new List<AccountInfo>();
        public Dictionary<int, Scroll_ItemAddFriendApply> ItemAddFriendApplies = new Dictionary<int, Scroll_ItemAddFriendApply>();
        public List<AccountInfo> ApplyAccountInfos = new List<AccountInfo>();
        public List<MailInfo> ApplyMailInfos = new List<MailInfo>();
    }
}