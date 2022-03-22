
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgEditorTroopLayerViewComponentAwakeSystem : AwakeSystem<DlgEditorTroopLayerViewComponent> 
	{
		public override void Awake(DlgEditorTroopLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgEditorTroopLayerViewComponentDestroySystem : DestroySystem<DlgEditorTroopLayerViewComponent> 
	{
		public override void Destroy(DlgEditorTroopLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
