
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_ItemHeroCardDestroySystem : DestroySystem<Scroll_ItemHeroCard> 
	{
		public override void Destroy( Scroll_ItemHeroCard self )
		{
			self.DestroyWidget();
		}
	}
}
