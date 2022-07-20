
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgOwnAwardTipsLayerViewComponentAwakeSystem : AwakeSystem<DlgOwnAwardTipsLayerViewComponent> 
	{
		public override void Awake(DlgOwnAwardTipsLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgOwnAwardTipsLayerViewComponentDestroySystem : DestroySystem<DlgOwnAwardTipsLayerViewComponent> 
	{
		public override void Destroy(DlgOwnAwardTipsLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
