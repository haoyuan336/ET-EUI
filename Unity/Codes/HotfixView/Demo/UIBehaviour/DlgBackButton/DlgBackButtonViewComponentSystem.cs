
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgBackButtonViewComponentAwakeSystem : AwakeSystem<DlgBackButtonViewComponent> 
	{
		public override void Awake(DlgBackButtonViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgBackButtonViewComponentDestroySystem : DestroySystem<DlgBackButtonViewComponent> 
	{
		public override void Destroy(DlgBackButtonViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
