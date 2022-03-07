

namespace ET
{
	
	public static class SessionPlayerComponentSystem
	{
		public  class SessionPlayerComponentAwakeSystem: AwakeSystem<SessionPlayerComponent>
		{
			public override async void Awake(SessionPlayerComponent self)
			{
				while (true)
				{
					await TimerComponent.Instance.WaitAsync(1000);
					if (self.IsDisposed)
					{
						// Log.Error("is Disponsed");
						return;
					}
				}
			}
		}
		
		public class SessionPlayerComponentDestroySystem: DestroySystem<SessionPlayerComponent>
		{
			public override void Destroy(SessionPlayerComponent self)
			{
				// 发送断线消息
				ActorLocationSenderComponent.Instance.Send(self.PlayerId, new G2M_SessionDisconnect());
				self.Domain.GetComponent<PlayerComponent>()?.Remove(self.PlayerId);
			}
		}

		public static Player GetMyPlayer(this SessionPlayerComponent self)
		{
			return self.Domain.GetComponent<PlayerComponent>().Get(self.PlayerId);
		}
	}
}
