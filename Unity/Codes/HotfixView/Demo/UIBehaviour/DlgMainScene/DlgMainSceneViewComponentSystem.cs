
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgMainSceneViewComponentAwakeSystem : AwakeSystem<DlgMainSceneViewComponent> 
	{
		public override void Awake(DlgMainSceneViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgMainSceneViewComponentDestroySystem : DestroySystem<DlgMainSceneViewComponent> 
	{
		public override void Destroy(DlgMainSceneViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
