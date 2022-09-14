
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgPVPMatchFightLayerViewComponentAwakeSystem : AwakeSystem<DlgPVPMatchFightLayerViewComponent> 
	{
		public override void Awake(DlgPVPMatchFightLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgPVPMatchFightLayerViewComponentDestroySystem : DestroySystem<DlgPVPMatchFightLayerViewComponent> 
	{
		public override void Destroy(DlgPVPMatchFightLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
