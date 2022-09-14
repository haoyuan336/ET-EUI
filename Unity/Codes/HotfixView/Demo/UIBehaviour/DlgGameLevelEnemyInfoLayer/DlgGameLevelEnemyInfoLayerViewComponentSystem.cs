
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgGameLevelEnemyInfoLayerViewComponentAwakeSystem : AwakeSystem<DlgGameLevelEnemyInfoLayerViewComponent> 
	{
		public override void Awake(DlgGameLevelEnemyInfoLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgGameLevelEnemyInfoLayerViewComponentDestroySystem : DestroySystem<DlgGameLevelEnemyInfoLayerViewComponent> 
	{
		public override void Destroy(DlgGameLevelEnemyInfoLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
