
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgFormationUIViewComponentAwakeSystem : AwakeSystem<DlgFormationUIViewComponent> 
	{
		public override void Awake(DlgFormationUIViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgFormationUIViewComponentDestroySystem : DestroySystem<DlgFormationUIViewComponent> 
	{
		public override void Destroy(DlgFormationUIViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
