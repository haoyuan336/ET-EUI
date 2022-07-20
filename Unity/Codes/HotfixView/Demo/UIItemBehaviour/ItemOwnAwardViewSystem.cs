
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_ItemOwnAwardDestroySystem : DestroySystem<Scroll_ItemOwnAward> 
	{
		public override void Destroy( Scroll_ItemOwnAward self )
		{
			self.DestroyWidget();
		}
	}
}
