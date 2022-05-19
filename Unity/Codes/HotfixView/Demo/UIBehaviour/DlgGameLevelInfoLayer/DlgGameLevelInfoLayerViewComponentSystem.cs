
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgGameLevelInfoLayerViewComponentAwakeSystem : AwakeSystem<DlgGameLevelInfoLayerViewComponent> 
	{
		public override void Awake(DlgGameLevelInfoLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgGameLevelInfoLayerViewComponentDestroySystem : DestroySystem<DlgGameLevelInfoLayerViewComponent> 
	{
		public override void Destroy(DlgGameLevelInfoLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
