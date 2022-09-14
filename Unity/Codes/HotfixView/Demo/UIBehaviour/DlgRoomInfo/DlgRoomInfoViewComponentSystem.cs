
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgRoomInfoViewComponentAwakeSystem : AwakeSystem<DlgRoomInfoViewComponent> 
	{
		public override void Awake(DlgRoomInfoViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgRoomInfoViewComponentDestroySystem : DestroySystem<DlgRoomInfoViewComponent> 
	{
		public override void Destroy(DlgRoomInfoViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
