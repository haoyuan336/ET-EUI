
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

	public class Scroll_ItemHeroTroopAwakeSystem: AwakeSystem<Scroll_ItemHeroTroop>
	{
		public override void Awake(Scroll_ItemHeroTroop self)
		{
			// self.E_ToggleToggle.isOn = false;
		}
	}
}
