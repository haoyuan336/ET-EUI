
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgAddSubPlaneViewComponentAwakeSystem : AwakeSystem<DlgAddSubPlaneViewComponent> 
	{
		public override void Awake(DlgAddSubPlaneViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgAddSubPlaneViewComponentDestroySystem : DestroySystem<DlgAddSubPlaneViewComponent> 
	{
		public override void Destroy(DlgAddSubPlaneViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
