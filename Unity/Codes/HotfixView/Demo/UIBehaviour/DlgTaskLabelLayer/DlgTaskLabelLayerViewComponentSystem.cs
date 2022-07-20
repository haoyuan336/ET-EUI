
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgTaskLabelLayerViewComponentAwakeSystem : AwakeSystem<DlgTaskLabelLayerViewComponent> 
	{
		public override void Awake(DlgTaskLabelLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgTaskLabelLayerViewComponentDestroySystem : DestroySystem<DlgTaskLabelLayerViewComponent> 
	{
		public override void Destroy(DlgTaskLabelLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
