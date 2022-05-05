
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgStoreViewComponentAwakeSystem : AwakeSystem<DlgStoreViewComponent> 
	{
		public override void Awake(DlgStoreViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgStoreViewComponentDestroySystem : DestroySystem<DlgStoreViewComponent> 
	{
		public override void Destroy(DlgStoreViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
