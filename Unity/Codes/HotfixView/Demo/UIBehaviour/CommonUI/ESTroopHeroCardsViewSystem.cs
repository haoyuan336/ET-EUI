
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class ESTroopHeroCardsAwakeSystem : AwakeSystem<ESTroopHeroCards,Transform> 
	{
		public override void Awake(ESTroopHeroCards self,Transform transform)
		{
			self.uiTransform = transform;
		}
	}


	[ObjectSystem]
	public class ESTroopHeroCardsDestroySystem : DestroySystem<ESTroopHeroCards> 
	{
		public override void Destroy(ESTroopHeroCards self)
		{
			self.DestroyWidget();
		}
	}
}
