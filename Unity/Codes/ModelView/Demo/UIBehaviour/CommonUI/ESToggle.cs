
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class ESToggle : Entity,ET.IAwake<UnityEngine.Transform>,IDestroy 
	{
		public void DestroyWidget()
		{
			this.uiTransform = null;
		}

		public Transform uiTransform = null;
	}
}
