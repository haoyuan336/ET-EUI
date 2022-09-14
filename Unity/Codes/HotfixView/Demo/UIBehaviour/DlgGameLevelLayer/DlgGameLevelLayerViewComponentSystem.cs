
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgGameLevelLayerViewComponentAwakeSystem : AwakeSystem<DlgGameLevelLayerViewComponent> 
	{
		public override void Awake(DlgGameLevelLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgGameLevelLayerViewComponentDestroySystem : DestroySystem<DlgGameLevelLayerViewComponent> 
	{
		public override void Destroy(DlgGameLevelLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
