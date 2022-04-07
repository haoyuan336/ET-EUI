
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgGameWinUIViewComponentAwakeSystem : AwakeSystem<DlgGameWinUIViewComponent> 
	{
		public override void Awake(DlgGameWinUIViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgGameWinUIViewComponentDestroySystem : DestroySystem<DlgGameWinUIViewComponent> 
	{
		public override void Destroy(DlgGameWinUIViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
