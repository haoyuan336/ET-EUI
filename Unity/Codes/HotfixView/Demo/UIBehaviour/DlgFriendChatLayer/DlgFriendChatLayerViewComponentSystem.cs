
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgFriendChatLayerViewComponentAwakeSystem : AwakeSystem<DlgFriendChatLayerViewComponent> 
	{
		public override void Awake(DlgFriendChatLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgFriendChatLayerViewComponentDestroySystem : DestroySystem<DlgFriendChatLayerViewComponent> 
	{
		public override void Destroy(DlgFriendChatLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
