
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgGameUIViewComponentAwakeSystem : AwakeSystem<DlgGameUIViewComponent> 
	{
		public override void Awake(DlgGameUIViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgGameUIViewComponentDestroySystem : DestroySystem<DlgGameUIViewComponent> 
	{
		public override void Destroy(DlgGameUIViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
