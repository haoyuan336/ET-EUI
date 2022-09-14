
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgAllHeroBagLayerViewComponentAwakeSystem : AwakeSystem<DlgAllHeroBagLayerViewComponent> 
	{
		public override void Awake(DlgAllHeroBagLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgAllHeroBagLayerViewComponentDestroySystem : DestroySystem<DlgAllHeroBagLayerViewComponent> 
	{
		public override void Destroy(DlgAllHeroBagLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
