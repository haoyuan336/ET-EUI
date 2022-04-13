
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgMainSceneMenuViewComponentAwakeSystem : AwakeSystem<DlgMainSceneMenuViewComponent> 
	{
		public override void Awake(DlgMainSceneMenuViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgMainSceneMenuViewComponentDestroySystem : DestroySystem<DlgMainSceneMenuViewComponent> 
	{
		public override void Destroy(DlgMainSceneMenuViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
