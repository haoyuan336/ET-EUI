
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgGoldInfoUIViewComponentAwakeSystem : AwakeSystem<DlgGoldInfoUIViewComponent> 
	{
		public override void Awake(DlgGoldInfoUIViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgGoldInfoUIViewComponentDestroySystem : DestroySystem<DlgGoldInfoUIViewComponent> 
	{
		public override void Destroy(DlgGoldInfoUIViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
