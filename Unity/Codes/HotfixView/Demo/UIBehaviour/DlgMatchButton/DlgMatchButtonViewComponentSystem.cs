
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgMatchButtonViewComponentAwakeSystem : AwakeSystem<DlgMatchButtonViewComponent> 
	{
		public override void Awake(DlgMatchButtonViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgMatchButtonViewComponentDestroySystem : DestroySystem<DlgMatchButtonViewComponent> 
	{
		public override void Destroy(DlgMatchButtonViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
