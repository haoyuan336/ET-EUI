
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_ItemWeaponDestroySystem : DestroySystem<Scroll_ItemWeapon> 
	{
		public override void Destroy( Scroll_ItemWeapon self )
		{
			self.DestroyWidget();
		}
	}
}
