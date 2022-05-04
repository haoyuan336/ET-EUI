
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgGameLoaseUIViewComponentAwakeSystem : AwakeSystem<DlgGameLoaseUIViewComponent> 
	{
		public override void Awake(DlgGameLoaseUIViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgGameLoaseUIViewComponentDestroySystem : DestroySystem<DlgGameLoaseUIViewComponent> 
	{
		public override void Destroy(DlgGameLoaseUIViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
