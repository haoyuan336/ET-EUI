
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgPVPSceneLayerViewComponentAwakeSystem : AwakeSystem<DlgPVPSceneLayerViewComponent> 
	{
		public override void Awake(DlgPVPSceneLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgPVPSceneLayerViewComponentDestroySystem : DestroySystem<DlgPVPSceneLayerViewComponent> 
	{
		public override void Destroy(DlgPVPSceneLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
