using System.Collections.Generic;
using ET.EventType;

namespace ET
{
    public class DlgSettingSetMailEventHandler: AEvent<EventType.SetNewMails>
    {
        protected override async ETTask Run(SetNewMails a)
        {
            Log.Debug("显示新邮件提示");
            Scene scene = a.Scene;
            UIComponent uiComponent = scene.GetComponent<UIComponent>();
            UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_SettingUI);
            if (baseWindow != null)
            {
                baseWindow.GetComponent<DlgSettingUI>().ShowRedDot(true);
            }
            else
            {
                Log.Debug("无basewindow");
            }

            List<MailInfo> mailInfos = a.MailInfos;

            List<MailInfo> addFriendMailInfos = mailInfos.FindAll(b => b.MailType == (int) MailType.AddFriendRequest);

            UIBaseWindow searchLayer = uiComponent.GetUIBaseWindow(WindowID.WindowID_SearchUserLayer);
            searchLayer.GetComponent<DlgSearchUserLayer>().ReceiveAddFriendMessage(addFriendMailInfos);

            await ETTask.CompletedTask;
        }
    }
}