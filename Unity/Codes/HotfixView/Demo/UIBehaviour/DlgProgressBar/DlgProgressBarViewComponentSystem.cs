
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgProgressBarViewComponentAwakeSystem : AwakeSystem<DlgProgressBarViewComponent> 
	{
		public override void Awake(DlgProgressBarViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgProgressBarViewComponentDestroySystem : DestroySystem<DlgProgressBarViewComponent> 
	{
		public override void Destroy(DlgProgressBarViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
