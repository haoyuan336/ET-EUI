
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgMessageTaskActiveInfoViewComponentAwakeSystem : AwakeSystem<DlgMessageTaskActiveInfoViewComponent> 
	{
		public override void Awake(DlgMessageTaskActiveInfoViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgMessageTaskActiveInfoViewComponentDestroySystem : DestroySystem<DlgMessageTaskActiveInfoViewComponent> 
	{
		public override void Destroy(DlgMessageTaskActiveInfoViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
