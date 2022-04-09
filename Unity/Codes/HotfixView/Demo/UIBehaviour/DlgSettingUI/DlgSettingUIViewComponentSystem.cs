
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgSettingUIViewComponentAwakeSystem : AwakeSystem<DlgSettingUIViewComponent> 
	{
		public override void Awake(DlgSettingUIViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgSettingUIViewComponentDestroySystem : DestroySystem<DlgSettingUIViewComponent> 
	{
		public override void Destroy(DlgSettingUIViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
