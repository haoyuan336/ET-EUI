
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgPlayAudioEffectLayerViewComponent : Entity,IAwake,IDestroy 
	{
		public void DestroyWidget()
		{
			this.uiTransform = null;
		}

		public Transform uiTransform = null;
	}
}
