
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class ESHeroCardAwakeSystem : AwakeSystem<ESHeroCard,Transform> 
	{
		public override void Awake(ESHeroCard self,Transform transform)
		{
			self.uiTransform = transform;
		}
	}


	[ObjectSystem]
	public class ESHeroCardDestroySystem : DestroySystem<ESHeroCard> 
	{
		public override void Destroy(ESHeroCard self)
		{
			self.DestroyWidget();
		}
	}
}
