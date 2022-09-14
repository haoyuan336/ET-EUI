
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgPVPEditorTroopLayerViewComponentAwakeSystem : AwakeSystem<DlgPVPEditorTroopLayerViewComponent> 
	{
		public override void Awake(DlgPVPEditorTroopLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgPVPEditorTroopLayerViewComponentDestroySystem : DestroySystem<DlgPVPEditorTroopLayerViewComponent> 
	{
		public override void Destroy(DlgPVPEditorTroopLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
