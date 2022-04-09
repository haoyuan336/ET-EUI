
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgHeroInfoLayerUIViewComponentAwakeSystem : AwakeSystem<DlgHeroInfoLayerUIViewComponent> 
	{
		public override void Awake(DlgHeroInfoLayerUIViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgHeroInfoLayerUIViewComponentDestroySystem : DestroySystem<DlgHeroInfoLayerUIViewComponent> 
	{
		public override void Destroy(DlgHeroInfoLayerUIViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
