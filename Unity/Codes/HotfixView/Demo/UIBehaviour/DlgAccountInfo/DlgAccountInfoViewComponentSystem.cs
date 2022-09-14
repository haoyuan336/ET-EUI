
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgAccountInfoViewComponentAwakeSystem : AwakeSystem<DlgAccountInfoViewComponent> 
	{
		public override void Awake(DlgAccountInfoViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgAccountInfoViewComponentDestroySystem : DestroySystem<DlgAccountInfoViewComponent> 
	{
		public override void Destroy(DlgAccountInfoViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
