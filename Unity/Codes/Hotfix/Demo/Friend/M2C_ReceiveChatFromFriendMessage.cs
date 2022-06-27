using UnityEngine;

namespace ET.Friend
{
    public class M2C_ReceiveChatFromFriendMessage: AMHandler<M2C_ReceiveChatFromFriend>
    {
        protected override async ETTask Run(Session session, M2C_ReceiveChatFromFriend message)
        {


            Scene scene = session.DomainScene().CurrentScene();
            Game.EventSystem.Publish(new EventType.ReceiveChat()
            {
                ChatInfo = message.ChatInfo,
                Scene = scene
            });

            await ETTask.CompletedTask;
        }
    }
}