
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgCallHeroLayerViewComponentAwakeSystem : AwakeSystem<DlgCallHeroLayerViewComponent> 
	{
		public override void Awake(DlgCallHeroLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgCallHeroLayerViewComponentDestroySystem : DestroySystem<DlgCallHeroLayerViewComponent> 
	{
		public override void Destroy(DlgCallHeroLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
