
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_ItemChatDestroySystem : DestroySystem<Scroll_ItemChat> 
	{
		public override void Destroy( Scroll_ItemChat self )
		{
			self.DestroyWidget();
		}
	}
}
