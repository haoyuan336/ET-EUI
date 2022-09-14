
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public   class ESHeroCardInfoUIAwakeSystem : AwakeSystem<ESHeroCardInfoUI,Transform> 
	{
		public override void Awake(ESHeroCardInfoUI self,Transform transform)
		{
			self.uiTransform = transform;
		}
	}


	[ObjectSystem]
	public class ESHeroCardInfoUIDestroySystem : DestroySystem<ESHeroCardInfoUI> 
	{
		public override void Destroy(ESHeroCardInfoUI self)
		{
			self.DestroyWidget();
		}
	}
}
