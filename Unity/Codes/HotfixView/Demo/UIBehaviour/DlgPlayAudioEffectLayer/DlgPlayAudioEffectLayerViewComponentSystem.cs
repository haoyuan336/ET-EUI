
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgPlayAudioEffectLayerViewComponentAwakeSystem : AwakeSystem<DlgPlayAudioEffectLayerViewComponent> 
	{
		public override void Awake(DlgPlayAudioEffectLayerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgPlayAudioEffectLayerViewComponentDestroySystem : DestroySystem<DlgPlayAudioEffectLayerViewComponent> 
	{
		public override void Destroy(DlgPlayAudioEffectLayerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
