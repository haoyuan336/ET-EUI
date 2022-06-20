
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgSearchUserLayerViewComponentAwakeSystem : AwakeSystem<DlgSearchUserLayerViewComponent> 
	{
		public override void Awake(DlgSearchUserLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgSearchUserLayerViewComponentDestroySystem : DestroySystem<DlgSearchUserLayerViewComponent> 
	{
		public override void Destroy(DlgSearchUserLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
