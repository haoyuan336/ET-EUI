
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgMainSceneBgViewComponentAwakeSystem : AwakeSystem<DlgMainSceneBgViewComponent> 
	{
		public override void Awake(DlgMainSceneBgViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgMainSceneBgViewComponentDestroySystem : DestroySystem<DlgMainSceneBgViewComponent> 
	{
		public override void Destroy(DlgMainSceneBgViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
