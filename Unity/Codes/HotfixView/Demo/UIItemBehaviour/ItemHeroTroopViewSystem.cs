
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_ItemHeroTroopDestroySystem : DestroySystem<Scroll_ItemHeroTroop> 
	{
		public override void Destroy( Scroll_ItemHeroTroop self )
		{
			self.DestroyWidget();
		}
	}
}
