using System.Collections.Generic;

namespace ET
{
    public class DlgMailLayer: Entity, IAwake
    {
        public DlgMailLayerViewComponent View
        {
            get => this.Parent.GetComponent<DlgMailLayerViewComponent>();
        }

        public List<MailInfo> MailInfos = new List<MailInfo>();
        public Dictionary<int, Scroll_ItemMail> ItemMails = new Dictionary<int, Scroll_ItemMail>();
    }
}