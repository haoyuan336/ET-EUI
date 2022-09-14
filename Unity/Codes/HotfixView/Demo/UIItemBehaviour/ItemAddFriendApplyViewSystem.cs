
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_ItemAddFriendApplyDestroySystem : DestroySystem<Scroll_ItemAddFriendApply> 
	{
		public override void Destroy( Scroll_ItemAddFriendApply self )
		{
			self.DestroyWidget();
		}
	}
}
