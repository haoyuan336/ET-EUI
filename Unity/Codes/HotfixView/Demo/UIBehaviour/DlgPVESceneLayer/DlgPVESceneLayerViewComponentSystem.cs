
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgPVESceneLayerViewComponentAwakeSystem : AwakeSystem<DlgPVESceneLayerViewComponent> 
	{
		public override void Awake(DlgPVESceneLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgPVESceneLayerViewComponentDestroySystem : DestroySystem<DlgPVESceneLayerViewComponent> 
	{
		public override void Destroy(DlgPVESceneLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
