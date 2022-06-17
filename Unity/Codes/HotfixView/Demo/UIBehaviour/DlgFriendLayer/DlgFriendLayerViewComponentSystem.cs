
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgFriendLayerViewComponentAwakeSystem : AwakeSystem<DlgFriendLayerViewComponent> 
	{
		public override void Awake(DlgFriendLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgFriendLayerViewComponentDestroySystem : DestroySystem<DlgFriendLayerViewComponent> 
	{
		public override void Destroy(DlgFriendLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
