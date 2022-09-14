
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgMainMenuUIViewComponentAwakeSystem : AwakeSystem<DlgMainMenuUIViewComponent> 
	{
		public override void Awake(DlgMainMenuUIViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgMainMenuUIViewComponentDestroySystem : DestroySystem<DlgMainMenuUIViewComponent> 
	{
		public override void Destroy(DlgMainMenuUIViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
