
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgUserInfoLayerViewComponentAwakeSystem : AwakeSystem<DlgUserInfoLayerViewComponent> 
	{
		public override void Awake(DlgUserInfoLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgUserInfoLayerViewComponentDestroySystem : DestroySystem<DlgUserInfoLayerViewComponent> 
	{
		public override void Destroy(DlgUserInfoLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
