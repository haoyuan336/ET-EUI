
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_ItemFriendDestroySystem : DestroySystem<Scroll_ItemFriend> 
	{
		public override void Destroy( Scroll_ItemFriend self )
		{
			self.DestroyWidget();
		}
	}
}
