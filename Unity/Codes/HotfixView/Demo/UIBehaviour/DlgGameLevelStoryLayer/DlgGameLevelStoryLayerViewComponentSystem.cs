
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgGameLevelStoryLayerViewComponentAwakeSystem : AwakeSystem<DlgGameLevelStoryLayerViewComponent> 
	{
		public override void Awake(DlgGameLevelStoryLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgGameLevelStoryLayerViewComponentDestroySystem : DestroySystem<DlgGameLevelStoryLayerViewComponent> 
	{
		public override void Destroy(DlgGameLevelStoryLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
