using System.Linq;
using ET.EventType;

namespace ET
{
    public class ReceiveChatFromFriendHandler: AEvent<EventType.ReceiveChat>
    {
        protected override async ETTask Run(ReceiveChat a)
        {
            ChatInfo chatInfo = a.ChatInfo;

            Scene scene = a.Scene;

            UIComponent uiComponent = scene.GetComponent<UIComponent>();

            UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_FriendChatLayer);


            // var friendLayerIsShow = uiComponent.VisibleWindowsDic.ContainsKey((int) WindowID.WindowID_FriendLayer);
            // if (!friendLayerIsShow)
            // {
            //     UIBaseWindow settingBaseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_SettingUI);
            //     if (settingBaseWindow != null)
            //     {
            //         settingBaseWindow.GetComponent<DlgSettingUI>().ShowNewChatMark(true);
            //     }
            // }
            
            var layerIsHide = uiComponent.VisibleWindowsDic.ContainsKey((int) WindowID.WindowID_FriendChatLayer);
            Log.Debug($"收到了 好友发来的消息{layerIsHide} ");

            var isShowCurentChat = false;
            if (baseWindow != null && layerIsHide)
            {
                Log.Debug("渲染消息内容");

                isShowCurentChat = baseWindow.GetComponent<DlgFriendChatLayer>().ReceiveChat(chatInfo);
            }

            if (!isShowCurentChat)
            {
                UIBaseWindow friendLayer = uiComponent.GetUIBaseWindow(WindowID.WindowID_SettingUI);
                if (friendLayer != null)
                {
                    friendLayer.GetComponent<DlgSettingUI>().ReceiveChatInfo(chatInfo);
                }
            }

            await ETTask.CompletedTask;
        }
    }
}