
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_ItemTroopHeroCardDestroySystem : DestroySystem<Scroll_ItemTroopHeroCard> 
	{
		public override void Destroy( Scroll_ItemTroopHeroCard self )
		{
			self.DestroyWidget();
		}
	}
}
