
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgPVPFightPrepareLayerViewComponentAwakeSystem : AwakeSystem<DlgPVPFightPrepareLayerViewComponent> 
	{
		public override void Awake(DlgPVPFightPrepareLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgPVPFightPrepareLayerViewComponentDestroySystem : DestroySystem<DlgPVPFightPrepareLayerViewComponent> 
	{
		public override void Destroy(DlgPVPFightPrepareLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
